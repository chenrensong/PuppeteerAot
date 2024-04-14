using PuppeteerAot.Helpers.Json;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    [JsonConverter(typeof(JsonStringEnumMemberConverter<FrameDetachedReason>))]
    public enum FrameDetachedReason
    {
        /// <summary>
        /// Unknown.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// Remove.
        /// </summary>
        Remove,

        /// <summary>
        /// Swap.
        /// </summary>
        Swap,
    }

    public class PageFrameDetachedResponse : BasicFrameResponse
    {
        public FrameDetachedReason Reason { get; set; }
    }
}
