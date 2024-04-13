namespace PuppeteerAot.Cdp.Messaging
{
    public class DomResolveNodeRequest
    {
        public object BackendNodeId { get; set; }

        public int ExecutionContextId { get; set; }
    }
}
