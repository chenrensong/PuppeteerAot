namespace PuppeteerAot.Cdp.Messaging
{
    public class PageGetFrameTree
    {
        public FramePayload Frame { get; set; }

        public PageGetFrameTree[] ChildFrames { get; set; }
    }
}
