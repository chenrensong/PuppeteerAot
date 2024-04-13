namespace PuppeteerAot.Cdp.Messaging
{
    public class RuntimeAddBindingRequest
    {
        public string Name { get; set; }

        public string ExecutionContextName { get; set; }

        public int? ExecutionContextId { get; set; }
    }
}
