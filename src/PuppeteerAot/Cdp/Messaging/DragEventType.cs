using System.Runtime.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
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
