using PuppeteerAot.Helpers.Json;
using PuppeteerAot.Media;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter<DispatchKeyEventType>))]
    public enum DispatchKeyEventType
    {
        /// <summary>
        /// Key down.
        /// </summary>
        [EnumMember(Value = "keyDown")]
        KeyDown,

        /// <summary>
        /// Raw key down.
        /// </summary>
        [EnumMember(Value = "rawKeyDown")]
        RawKeyDown,

        /// <summary>
        /// Key up.
        /// </summary>
        [EnumMember(Value = "keyUp")]
        KeyUp,
    }
}
