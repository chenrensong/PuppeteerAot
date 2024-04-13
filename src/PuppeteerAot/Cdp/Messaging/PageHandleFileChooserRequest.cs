using System.Collections.Generic;

namespace PuppeteerAot.Cdp.Messaging
{
    public class PageHandleFileChooserRequest
    {
        public FileChooserAction Action { get; set; }

        public IEnumerable<string> Files { get; set; }
    }
}
