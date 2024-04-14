using PuppeteerAot.Helpers.Json;
using System.Text.Json.Serialization;

namespace PuppeteerAot
{
    /// <summary>
    /// SameSite values in cookies.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter<SameSite>))]
    public enum SameSite
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Strict.
        /// </summary>
        Strict,

        /// <summary>
        /// Lax.
        /// </summary>
        Lax,

        /// <summary>
        /// Extended.
        /// </summary>
        Extended,
    }
}
