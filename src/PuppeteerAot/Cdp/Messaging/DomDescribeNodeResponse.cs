namespace PuppeteerAot.Cdp.Messaging
{
    public class DomDescribeNodeResponse
    {
        public DomNode Node { get; set; }

        public class DomNode
        {
            public string FrameId { get; set; }

            public object BackendNodeId { get; set; }
        }
    }
}
