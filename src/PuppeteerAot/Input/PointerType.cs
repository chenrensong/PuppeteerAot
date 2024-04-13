using System.Runtime.Serialization;

namespace PuppeteerAot.Input
{
    public enum PointerType
    {
        /// <summary>
        /// Mouse.
        /// </summary>
        [EnumMember(Value = "mouse")]
        Mouse,

        /// <summary>
        /// Pen.
        /// </summary>
        [EnumMember(Value = "pen")]
        Pen,
    }
}
