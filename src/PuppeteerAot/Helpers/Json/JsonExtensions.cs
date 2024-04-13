using System;
using System.Text.Json;

namespace PuppeteerAot.Helpers.Json
{
    /// <summary>
    /// A set of extension methods for JsonElement.
    /// </summary>
    public static class JsonExtensions
    {
        public static T ToObject<T>(this JsonDocument document, bool camelCase)
        {
            if (document == null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return document.RootElement.ToObject<T>(camelCase);
        }

        /// <summary>
        /// Creates an instance of the specified .NET type from the <see cref="T:Newtonsoft.Json.Linq.JsonElement" />.
        /// </summary>
        /// <typeparam name="T">The object type that the token will be deserialized to.</typeparam>
        /// <param name="token">Json token.</param>
        /// <param name="camelCase">If set to <c>true</c> the CamelCasePropertyNamesContractResolver will be used.</param>
        /// <returns>The new object created from the JSON value.</returns>
        public static T ToObject<T>(this JsonElement token, bool camelCase)
        {

            if (camelCase)
            {
                return token.ToObject<T>(JsonHelper.DefaultJsonSerializerSettings);
            }
            return token.ToObject<T>(JsonHelper.NotCamelCaseJsonSerializerSettings);

        }

        public static object ToObject(this JsonElement element, Type type, bool camelCase = true)
        {
            try
            {
                return element.Deserialize(type, camelCase ? JsonHelper.DefaultJsonSerializerSettings : JsonHelper.NotCamelCaseJsonSerializerSettings);
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        /// <summary>
        /// Creates an instance of the specified .NET type from the <see cref="T:Newtonsoft.Json.Linq.JsonElement" />.
        /// </summary>
        /// <typeparam name="T">The object type that the token will be deserialized to.</typeparam>
        /// <param name="token">Json token.</param>
        /// <param name="jsonSerializerSettings">Serializer settings.</param>
        /// <returns>The new object created from the JSON value.</returns>
        public static T ToObject<T>(this JsonElement token, JsonSerializerOptions jsonSerializerSettings)
        {
            try
            {
                return token.Deserialize<T>(jsonSerializerSettings);
            }
            catch (Exception ex)
            {
                return default;
            }
        }

    }
}
