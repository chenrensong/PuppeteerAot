using System.Text.Json.Serialization;

namespace PuppeteerAot.Cdp.Messaging
{
    public class MessageArgument
    {
        [JsonPropertyName("unserializableValue")]
        public string UnserializableValue { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("objectId")]
        public string ObjectId { get; set; }
    }
}
