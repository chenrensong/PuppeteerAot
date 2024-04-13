using System.Collections.Generic;

namespace PuppeteerAot.BrowserData
{
    public class ChromeGoodVersionsResult
    {
        public Dictionary<string, ChromeGoodVersionsResultVersion> Channels { get; set; }

        public class ChromeGoodVersionsResultVersion
        {
            public string Version { get; set; }
        }
    }
}
