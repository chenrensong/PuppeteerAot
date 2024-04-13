using System.Text.Json.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    public class TargetSetDiscoverTargetsRequest
    {
        public bool Discover { get; set; }

        public DiscoverFilter[] Filter { get; set; }

        public class DiscoverFilter
        {
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string Type { get; set; }

            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public bool? Exclude { get; set; }
        }
    }
}
