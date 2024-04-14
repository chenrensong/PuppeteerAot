using PuppeteerAot.Helpers.Json;
using PuppeteerAot.Input;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter<MouseEventType>))]
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
