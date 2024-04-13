using System.Text.Json.Serialization;

namespace PuppeteerAot
{
    /// <summary>
    /// Target info.
    /// </summary>
    public class TargetInfo
    {
        public TargetInfo()
        {
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public TargetType Type { get;  set; }

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        [JsonPropertyName("url")]
        public string Url { get;  set; }

        /// <summary>
        /// Gets the target identifier.
        /// </summary>
        /// <value>The target identifier.</value>
        //[JsonPropertyName("targetId")]
        public string TargetId { get;  set; }

        /// <summary>
        /// Gets the target browser contextId.
        /// </summary>
        [JsonPropertyName("browserContextId")]
        public string BrowserContextId { get;  set; }

        /// <summary>
        /// Get the target that opened this target.
        /// </summary>
        [JsonPropertyName("openerId")]
        public string OpenerId { get;  set; }

        /// <summary>
        /// Gets whether the target is attached.
        /// </summary>
        [JsonPropertyName("attached")]
        public bool Attached { get;  set; }

        /// <summary>
        /// Provides additional details for specific target types. For example, for
        /// the type of "page", this may be set to "portal" or "prerender".
        /// </summary>
        [JsonPropertyName("subtype")]
        public string Subtype { get; set; }
    }
}
