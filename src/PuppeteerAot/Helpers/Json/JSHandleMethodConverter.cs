using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Helpers.Json
{
    /// <summary>
    /// JSHandleMethodConverter.
    /// </summary>
    public class JSHandleMethodConverter : JsonConverter<object>
    {
        /// <summary>
        /// CanConvert.
        /// </summary>
        /// <param name="typeToConvert">typeToConvert.</param>
        /// <returns>bool.</returns>
        public override bool CanConvert(Type typeToConvert)
        {
            // In System.Text.Json, you typically check the type here,
            // but since we're mimicking the behavior of always returning false,
            // it's not strictly necessary.
            return false;
        }

        /// <summary>
        /// Read.
        /// </summary>
        /// <param name="reader">reader.</param>
        /// <param name="typeToConvert">typeToConvert.</param>
        /// <param name="options">options.</param>
        /// <returns>object.</returns>
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // As with Newtonsoft.Json, we're not supporting deserialization,
            // so returning null or throwing an exception could be an option.
            return null; // Or throw new NotSupportedException("Deserialization is not supported.");
        }

        /// <summary>
        /// Write.
        /// </summary>
        /// <param name="writer">writer.</param>
        /// <param name="value">value.</param>
        /// <param name="options">options.</param>
        /// <exception cref="InvalidOperationException">InvalidOperationException.</exception>
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            // Mimicking the behavior of throwing an exception on serialization attempt.
            throw new InvalidOperationException("Unable to make function call. Are you passing a nested JSHandle?");
        }
    }
}
