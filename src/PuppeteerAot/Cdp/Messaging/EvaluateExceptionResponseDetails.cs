namespace PuppeteerAot.Cdp.Messaging
{
    public class EvaluateExceptionResponseDetails
    {
        public EvaluateExceptionResponseInfo Exception { get; set; }

        public string Text { get; set; }

        public EvaluateExceptionResponseStackTrace StackTrace { get; set; }
    }
}
