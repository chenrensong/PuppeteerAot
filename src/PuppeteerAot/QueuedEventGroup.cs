using PuppeteerAot.Cdp.Messaging;

namespace PuppeteerAot
{
    public class QueuedEventGroup
    {
        public ResponseReceivedResponse ResponseReceivedEvent { get; set; }

        public LoadingFinishedEventResponse LoadingFinishedEvent { get; set; }

        public LoadingFailedEventResponse LoadingFailedEvent { get; set; }
    }
}
