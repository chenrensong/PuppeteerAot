using System.Collections.Generic;

namespace PuppeteerAot.Cdp.Messaging
{
    public class EmulationSetEmulatedMediaFeatureRequest
    {
        public IEnumerable<MediaFeatureValue> Features { get; set; }
    }
}
