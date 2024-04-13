using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PuppeteerSharp.Helpers.Json
{
    internal class HttpMethodConverter : JsonConverter<HttpMethod>
    {
        public override HttpMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Read the string value from the JSON and convert it to an HttpMethod instance
            var method = reader.GetString();
            return new HttpMethod(method);
        }

        public override void Write(Utf8JsonWriter writer, HttpMethod value, JsonSerializerOptions options)
        {
            // Write the HttpMethod as a string to the JSON
            writer.WriteStringValue(value.Method);
        }
    }
}
