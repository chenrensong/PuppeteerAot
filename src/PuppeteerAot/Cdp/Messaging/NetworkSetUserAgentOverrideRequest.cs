namespace PuppeteerAot.Cdp.Messaging
{
    public class NetworkSetUserAgentOverrideRequest
    {
        public string UserAgent { get; set; }

        public UserAgentMetadata UserAgentMetadata { get; set; }
    }
}
