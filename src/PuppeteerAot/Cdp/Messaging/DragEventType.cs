using PuppeteerAot.Helpers.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter<DragEventType>))]
    public enum DragEventType
    {
        /// <summary>
        /// Drag event.
        /// </summary>
        [EnumMember(Value = "dragEnter")]
        DragEnter,

        /// <summary>
        /// Drag over.
        /// </summary>
        [EnumMember(Value = "dragOver")]
        DragOver,

        /// <summary>
        /// Drop.
        /// </summary>
        [EnumMember(Value = "drop")]
        Drop,
    }
}
