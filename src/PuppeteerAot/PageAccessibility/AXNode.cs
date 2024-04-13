using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using PuppeteerAot.Cdp.Messaging;
using PuppeteerAot.Helpers;

namespace PuppeteerAot.PageAccessibility
{
    public class AXNode
    {
        private readonly string _name;
        private readonly bool _richlyEditable;
        private readonly bool _editable;
        private readonly bool _hidden;
        private readonly string _role;
        private readonly bool _ignored;
        private bool? _cachedHasFocusableChild;

        private AXNode(AccessibilityGetFullAXTreeResponse.AXTreeNode payload)
        {
            Payload = payload;

            _name = payload.Name != null ? payload.Name.Value.GetString() : string.Empty;
            _role = payload.Role != null ? payload.Role.Value.GetString() : "Unknown";
            _ignored = payload.Ignored;

            _richlyEditable = payload.Properties?.FirstOrDefault(p => p.Name == "editable")?.Value.Value.GetString() == "richtext";
            _editable |= _richlyEditable;
            _hidden = payload.Properties?.FirstOrDefault(p => p.Name == "hidden")?.Value.Value.GetBoolean() == true;
            Focusable = payload.Properties?.FirstOrDefault(p => p.Name == "focusable")?.Value.Value.GetBoolean() == true;
        }

        public List<AXNode> Children { get; } = new();

        public bool Focusable { get; set; }

        public AccessibilityGetFullAXTreeResponse.AXTreeNode Payload { get; }

        public static AXNode CreateTree(IEnumerable<AccessibilityGetFullAXTreeResponse.AXTreeNode> payloads)
        {
            var nodeById = new Dictionary<string, AXNode>();
            foreach (var payload in payloads)
            {
                nodeById[payload.NodeId] = new AXNode(payload);
            }

            foreach (var node in nodeById.Values)
            {
                foreach (var childId in node.Payload.ChildIds)
                {
                    node.Children.Add(nodeById[childId]);
                }
            }

            return nodeById.Values.FirstOrDefault();
        }

        public AXNode Find(Func<AXNode, bool> predicate)
        {
            if (predicate(this))
            {
                return this;
            }

            foreach (var child in Children)
            {
                var result = child.Find(predicate);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public bool IsLeafNode()
        {
            if (Children.Count == 0)
            {
                return true;
            }

            // These types of objects may have children that we use as internal
            // implementation details, but we want to expose them as leaves to platform
            // accessibility APIs because screen readers might be confused if they find
            // any children.
            if (IsPlainTextField() || IsTextOnlyObject())
            {
                return true;
            }

            // Roles whose children are only presentational according to the ARIA and
            // HTML5 Specs should be hidden from screen readers.
            // (Note that whilst ARIA buttons can have only presentational children, HTML5
            // buttons are allowed to have content.)
            switch (_role)
            {
                case "doc-cover":
                case "graphics-symbol":
                case "img":
                case "image":
                case "Meter":
                case "scrollbar":
                case "slider":
                case "separator":
                case "progressbar":
                    return true;
            }

            // Here and below: Android heuristics
            if (HasFocusableChild())
            {
                return false;
            }

            if (Focusable && !string.IsNullOrEmpty(_name))
            {
                return true;
            }

            if (_role == "heading" && !string.IsNullOrEmpty(_name))
            {
                return true;
            }

            return false;
        }

        public bool IsControl()
        {
            switch (_role)
            {
                case "button":
                case "checkbox":
                case "ColorWell":
                case "combobox":
                case "DisclosureTriangle":
                case "listbox":
                case "menu":
                case "menubar":
                case "menuitem":
                case "menuitemcheckbox":
                case "menuitemradio":
                case "radio":
                case "scrollbar":
                case "searchbox":
                case "slider":
                case "spinbutton":
                case "switch":
                case "tab":
                case "textbox":
                case "tree":
                case "treeitem":
                    return true;
                default:
                    return false;
            }
        }

        public bool IsInteresting(bool insideControl)
        {
            if (_role == "Ignored" || _hidden || _ignored)
            {
                return false;
            }

            if (Focusable || _richlyEditable)
            {
                return true;
            }

            // If it's not focusable but has a control role, then it's interesting.
            if (IsControl())
            {
                return true;
            }

            // A non focusable child of a control is not interesting
            if (insideControl)
            {
                return false;
            }

            return IsLeafNode() && !string.IsNullOrEmpty(_name);
        }

        public SerializedAXNode Serialize()
        {
            var properties = new Dictionary<string, JsonElement>();

            if (Payload.Properties != null)
            {
                foreach (var property in Payload.Properties)
                {
                    properties[property.Name.ToLower(CultureInfo.CurrentCulture)] = property.Value.Value;
                }
            }

            if (Payload.Name != null)
            {
                properties["name"] = Payload.Name.Value;
            }

            if (Payload.Value != null)
            {
                properties["value"] = Payload.Value.Value;
            }

            if (Payload.Description != null)
            {
                properties["description"] = Payload.Description.Value;
            }

            var node = new SerializedAXNode
            {
                Role = _role,
                Name = properties.GetValueOrDefault("name").GetString(),
                Value = properties.GetValueOrDefault("value").GetString(),
                Description = properties.GetValueOrDefault("description").GetString(),
                KeyShortcuts = properties.GetValueOrDefault("keyshortcuts").GetString(),
                RoleDescription = properties.GetValueOrDefault("roledescription").GetString(),
                ValueText = properties.GetValueOrDefault("valuetext").GetString(),
                Disabled = properties.GetValueOrDefault("disabled").GetBoolean(),
                Expanded = properties.GetValueOrDefault("expanded").GetBoolean(),

                // RootWebArea's treat focus differently than other nodes. They report whether their frame  has focus,
                // not whether focus is specifically on the root node.
                Focused = properties.GetValueOrDefault("focused").GetBoolean() == true && _role != "RootWebArea",
                Modal = properties.GetValueOrDefault("modal").GetBoolean() ,
                Multiline = properties.GetValueOrDefault("multiline").GetBoolean(),
                Multiselectable = properties.GetValueOrDefault("multiselectable").GetBoolean(),
                Readonly = properties.GetValueOrDefault("readonly").GetBoolean(),
                Required = properties.GetValueOrDefault("required").GetBoolean(),
                Selected = properties.GetValueOrDefault("selected").GetBoolean(),
                Checked = GetCheckedState(properties.GetValueOrDefault("checked").GetString()),
                Pressed = GetCheckedState(properties.GetValueOrDefault("pressed").GetString()),
                Level = properties.GetValueOrDefault("level").GetInt32(),
                ValueMax = properties.GetValueOrDefault("valuemax").GetInt32(),
                ValueMin = properties.GetValueOrDefault("valuemin").GetInt32(),
                AutoComplete = GetIfNotFalse(properties.GetValueOrDefault("autocomplete").GetString()),
                HasPopup = GetIfNotFalse(properties.GetValueOrDefault("haspopup").GetString()),
                Invalid = GetIfNotFalse(properties.GetValueOrDefault("invalid").GetString()),
                Orientation = GetIfNotFalse(properties.GetValueOrDefault("orientation").GetString()),
            };

            return node;
        }

        private bool IsPlainTextField()
            => !_richlyEditable && (_editable || _role == "textbox" || _role == "ComboBox" || _role == "searchbox");

        private bool IsTextOnlyObject()
            => _role == "LineBreak" ||
                _role == "text" ||
                _role == "InlineTextBox" ||
                _role == "StaticText";

        private bool HasFocusableChild()
        {
            return _cachedHasFocusableChild ??= Children.Any(c => c.Focusable || c.HasFocusableChild());
        }

        private string GetIfNotFalse(string value) => value != null && value != "false" ? value : null;

        private CheckedState GetCheckedState(string value)
        {
            switch (value)
            {
                case "mixed":
                    return CheckedState.Mixed;
                case "true":
                    return CheckedState.True;
                default:
                    return CheckedState.False;
            }
        }
    }
}
