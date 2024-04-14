using PuppeteerAot.Helpers.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter<FileChooserAction>))]
    public enum FileChooserAction
    {
        /// <summary>
        /// Accept.
        /// </summary>
        [EnumMember(Value = "accept")]
        Accept,

        /// <summary>
        /// Fallback.
        /// </summary>
        [EnumMember(Value = "fallback")]
        Fallback,

        /// <summary>
        /// Cancel.
        /// </summary>
        [EnumMember(Value = "cancel")]
        Cancel,
    }
}
