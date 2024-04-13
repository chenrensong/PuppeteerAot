using PuppeteerAot.Cdp.Messaging;

namespace PuppeteerAot
{
    public class RedirectInfo
    {
        public RequestWillBeSentPayload Event { get; set; }

        public string FetchRequestId { get; set; }
    }
}
