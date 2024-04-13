namespace PuppeteerAot.Cdp.Messaging
{
    public class PageHandleJavaScriptDialogRequest
    {
        public bool Accept { get; set; }

        public string PromptText { get; set; }
    }
}
