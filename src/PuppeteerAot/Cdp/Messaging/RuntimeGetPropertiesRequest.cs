namespace PuppeteerAot.Cdp.Messaging
{
    public class RuntimeGetPropertiesRequest
    {
        public bool OwnProperties { get; set; }

        public string ObjectId { get; set; }
    }
}
