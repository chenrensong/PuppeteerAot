namespace PuppeteerAot.Cdp.Messaging
{
    public class FetchRequestPausedResponse : RequestWillBeSentPayload
    {
        public ResourceType? ResourceType { get; set; }
    }
}
