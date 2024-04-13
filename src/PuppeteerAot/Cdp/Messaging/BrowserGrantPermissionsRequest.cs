namespace PuppeteerAot.Cdp.Messaging
{
    public class BrowserGrantPermissionsRequest
    {
        public string Origin { get; set; }

        public string BrowserContextId { get; set; }

        public OverridePermission[] Permissions { get; set; }
    }
}
