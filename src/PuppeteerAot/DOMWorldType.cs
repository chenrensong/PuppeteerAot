using PuppeteerAot.Helpers.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PuppeteerAot
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter<DOMWorldType>))]
    public enum DOMWorldType
    {
        /// <summary>
        /// Other type.
        /// </summary>
        Other,

        /// <summary>
        /// Isolated type.
        /// </summary>
        [EnumMember(Value = "isolated")]
        Isolated,

        /// <summary>
        /// Default type.
        /// </summary>
        [EnumMember(Value = "default")]
        Default,
    }
}
