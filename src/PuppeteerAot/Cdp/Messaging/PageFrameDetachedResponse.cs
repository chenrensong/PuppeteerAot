namespace PuppeteerAot.Cdp.Messaging
{
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
