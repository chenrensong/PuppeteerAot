using System.Collections.Generic;

namespace PuppeteerAot.Cdp.Messaging
{
    public class PageGetNavigationHistoryResponse
    {
        public int CurrentIndex { get; set; }

        public List<HistoryEntry> Entries { get; set; }

        public class HistoryEntry
        {
            public int Id { get; set; }
        }
    }
}
