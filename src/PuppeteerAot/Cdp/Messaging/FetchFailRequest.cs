namespace PuppeteerAot.Cdp.Messaging
{
    public class FetchFailRequest
    {
        public string RequestId { get; set; }

        public string ErrorReason { get; set; }
    }
}
