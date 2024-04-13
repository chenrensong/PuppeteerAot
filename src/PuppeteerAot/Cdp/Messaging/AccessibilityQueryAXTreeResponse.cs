using System.Collections.Generic;
using static PuppeteerAot.Cdp.Messaging.AccessibilityGetFullAXTreeResponse;

namespace PuppeteerAot.Cdp.Messaging
{
    public class AccessibilityQueryAXTreeResponse
    {
        public IEnumerable<AXTreeNode> Nodes { get; set; }
    }
}
