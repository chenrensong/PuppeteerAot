using PuppeteerAot.Cdp.Messaging;
using PuppeteerAot.Helpers.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Input
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter<PointerType>))]
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
