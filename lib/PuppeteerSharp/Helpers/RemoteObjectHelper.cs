using System;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;

using PuppeteerSharp.Cdp.Messaging;
using PuppeteerSharp.Helpers.Json;

namespace PuppeteerSharp.Helpers
{
    internal class RemoteObjectHelper
    {
        internal static object ValueFromRemoteObject<T>(RemoteObject remoteObject, bool stringify = false)
        {
            var unserializableValue = remoteObject.UnserializableValue;

            if (unserializableValue != null)
            {
                return ValueFromUnserializableValue(remoteObject, unserializableValue);
            }

            if (stringify)
            {
                if (remoteObject.Type == RemoteObjectType.Undefined)
                {
                    return "undefined";
                }

                if (remoteObject.Value.ValueKind == JsonValueKind.Undefined
                    || remoteObject.Value.ValueKind == JsonValueKind.Null)
                {
                    return "null";
                }
            }

            var value = remoteObject.Value;

            if (value.ValueKind == JsonValueKind.Null || value.ValueKind == JsonValueKind.Undefined)
            {
                return default(T);
            }

            return typeof(T) == typeof(JsonElement) ? value : ValueFromType<T>(value, remoteObject.Type, stringify);
        }

        internal static async Task ReleaseObjectAsync(CDPSession client, RemoteObject remoteObject, ILogger logger)
        {
            if (remoteObject.ObjectId == null)
            {
                return;
            }

            try
            {
                await client.SendAsync(
                    "Runtime.releaseObject",
                    new RuntimeReleaseObjectRequest
                    {
                        ObjectId = remoteObject.ObjectId,
                    },
                    false /* We don't need to wait for the object to be released*/).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Exceptions might happen in case of a page been navigated or closed.
                // Swallow these since they are harmless and we don't leak anything in this case.
                logger.LogWarning(ex.ToString());
            }
        }

        private static object ValueFromType<T>(JsonElement value, RemoteObjectType objectType, bool stringify = false)
        {
            switch (objectType)
            {
                case RemoteObjectType.Undefined:
                    return "undefined";
                case RemoteObjectType.Number:
                    return value.GetDouble();
                case RemoteObjectType.Boolean:
                    return value.GetBoolean();
                case RemoteObjectType.Bigint:
                    return value.GetInt64();
                case RemoteObjectType.Object:
                default: // string, symbol, function
                    if (stringify)
                    {
                        // 如果需要字符串化，直接转换整个 JsonElement 为字符串
                        return JsonSerializer.Serialize(value, JsonHelper.DefaultJsonSerializerSettings);
                    }
                    else
                    {
                        // 尝试直接反序列化为类型 T
                        return value.Deserialize<T>(JsonHelper.DefaultJsonSerializerSettings);
                    }
            }
        }

        private static object ValueFromUnserializableValue(RemoteObject remoteObject, string unserializableValue)
        {
            if (remoteObject.Type == RemoteObjectType.Bigint &&
                                decimal.TryParse(remoteObject.UnserializableValue.Replace("n", string.Empty), out var decimalValue))
            {
                return new BigInteger(decimalValue);
            }

            switch (unserializableValue)
            {
                case "-0":
                    return -0;
                case "NaN":
                    return double.NaN;
                case "Infinity":
                    return double.PositiveInfinity;
                case "-Infinity":
                    return double.NegativeInfinity;
                default:
                    throw new Exception("Unsupported unserializable value: " + unserializableValue);
            }
        }
    }
}
