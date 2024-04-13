using PuppeteerAot.Media;

namespace PuppeteerAot.Cdp.Messaging
{
    public class PageCaptureScreenshotRequest
    {
        public string Format { get; set; }

        public int Quality { get; set; }

        public Clip Clip { get; set; }

        public bool CaptureBeyondViewport { get; set; }

        public bool? FromSurface { get; set; }

        public bool? OptimizeForSpeed { get; set; }
    }
}
