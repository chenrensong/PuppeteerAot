using System.Text.Json;
using PuppeteerSharp.Helpers.Json;

namespace PuppeteerSharp.Cdp.Messaging
{
    internal class BindingCalledResponse
    {
        private string _payloadJson;

        public int ExecutionContextId { get; set; }

        public BindingCalledResponsePayload BindingPayload { get; set; }

        public string Payload
        {
            get => _payloadJson;
            set
            {
                _payloadJson = value;
                var json = JsonSerializer.Deserialize<JsonDocument>(_payloadJson, JsonHelper.DefaultJsonSerializerSettings);
                BindingPayload = json.RootElement.ToObject<BindingCalledResponsePayload>(true);
                BindingPayload.JsonObject = json.RootElement;
            }
        }

        internal class BindingCalledResponsePayload
        {
            public string Type { get; set; }

            public string Name { get; set; }

            public JsonElement[] Args { get; set; }

            public int Seq { get; set; }

            public JsonElement JsonObject { get; set; }

            public bool IsTrivial { get; set; }
        }
    }
}
