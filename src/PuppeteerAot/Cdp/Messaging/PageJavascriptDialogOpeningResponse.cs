namespace PuppeteerAot.Cdp.Messaging
{
    public class PageJavascriptDialogOpeningResponse
    {
        public DialogType Type { get; set; }

        public string DefaultPrompt { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
