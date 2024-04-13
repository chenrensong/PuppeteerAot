using System.Runtime.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    public enum MouseEventType
    {
        /// <summary>
        /// Mouse moved.
        /// </summary>
        [EnumMember(Value = "mouseMoved")]
        MouseMoved,

        /// <summary>
        /// Mouse clicked.
        /// </summary>
        [EnumMember(Value = "mousePressed")]
        MousePressed,

        /// <summary>
        /// Mouse click released.
        /// </summary>
        [EnumMember(Value = "mouseReleased")]
        MouseReleased,

        /// <summary>
        /// Mouse wheel.
        /// </summary>
        [EnumMember(Value = "mouseWheel")]
        MouseWheel,
    }
}