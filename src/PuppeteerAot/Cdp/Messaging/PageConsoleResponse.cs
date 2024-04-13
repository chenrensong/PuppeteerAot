namespace PuppeteerAot.Cdp.Messaging
{
    public class PageConsoleResponse
    {
        public ConsoleType Type { get; set; }

        public RemoteObject[] Args { get; set; }

        public int ExecutionContextId { get; set; }

        public StackTrace StackTrace { get; set; }
    }
}
