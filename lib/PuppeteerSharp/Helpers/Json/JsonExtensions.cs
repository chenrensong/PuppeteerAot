/*
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using PuppeteerSharp.Helpers.Json;

namespace PuppeteerSharp.Helpers
{
    /// <summary>
    /// JSON extensions.
    /// </summary>
    internal static class JsonExtensions
    {
        static JsonExtensions() => DefaultJsonSerializerOptions = GetNewDefaultSerializerOptions(false);

        /// <summary>
        /// Base serialization options used by Microsoft.Playwright.
        /// </summary>
        public static JsonSerializerOptions DefaultJsonSerializerOptions { get; }

        /// <summary>
        /// Convert a <see cref="JsonElement"/> to an object.
        /// </summary>
        /// <typeparam name="T">Type to convert the <see cref="JsonElement"/> to.</typeparam>
        /// <param name="element">Element to convert.</param>
        /// <param name="options">Serialization options.</param>
        /// <returns>Converted value.</returns>
        public static T ToObject<T>(this JsonElement element, JsonSerializerOptions options = null)
            => element.Deserialize<T>(options ?? DefaultJsonSerializerOptions);

        /// <summary>
        /// Convert a <see cref="JsonElement"/> to an object.
        /// </summary>
        /// <param name="element">Element to convert.</param>
        /// <param name="type">Type to convert the <see cref="JsonElement"/> to.</param>
        /// <param name="options">Serialization options.</param>
        /// <returns>Converted value.</returns>
        public static object ToObject(this JsonElement element, Type type, JsonSerializerOptions options = null)
        => element.Deserialize(type, options ?? DefaultJsonSerializerOptions);

        /// <summary>
        /// Serialize an object.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="value">Object to serialize.</param>
        /// <param name="options">Serialization options.</param>
        /// <returns>Serialized object.</returns>
        public static string ToJson<T>(this T value, JsonSerializerOptions options = null)
            => JsonSerializer.Serialize(value, options ?? DefaultJsonSerializerOptions);

        /// <summary>
        /// Convert a <see cref="JsonDocument"/> to an object.
        /// </summary>
        /// <typeparam name="T">Type to convert the <see cref="JsonElement"/> to.</typeparam>
        /// <param name="document">Document to convert.</param>
        /// <param name="options">Serialization options.</param>
        /// <returns>Converted value.</returns>
        public static T ToObject<T>(this JsonDocument document, JsonSerializerOptions options = null)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return document.RootElement.ToObject<T>(options ?? DefaultJsonSerializerOptions);
        }

        internal static JsonSerializerOptions GetNewDefaultSerializerOptions(bool keepNulls)
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters =
            {
                    new JsonStringEnumMemberConverter(),
            },
            };
            if (!keepNulls)
            {
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }

            return options;
        }
    }
}
*/
