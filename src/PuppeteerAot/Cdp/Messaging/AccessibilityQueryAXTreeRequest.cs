namespace PuppeteerAot.Cdp.Messaging
{
    public class AccessibilityQueryAXTreeRequest
    {
        public string ObjectId { get; set; }

        public string AccessibleName { get; set; }

        public string Role { get; set; }
    }
}
