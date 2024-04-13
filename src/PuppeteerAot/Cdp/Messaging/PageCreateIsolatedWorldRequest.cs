namespace PuppeteerAot.Cdp.Messaging
{
    public class PageCreateIsolatedWorldRequest
    {
        public string FrameId { get; set; }

        public string WorldName { get; set; }

        public bool GrantUniveralAccess { get; set; }
    }
}
