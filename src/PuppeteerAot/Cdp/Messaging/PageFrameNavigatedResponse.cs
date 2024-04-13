namespace PuppeteerAot.Cdp.Messaging;

public class PageFrameNavigatedResponse
{
    public FramePayload Frame { get; set; }

    public NavigationType Type { get; set; }
}
