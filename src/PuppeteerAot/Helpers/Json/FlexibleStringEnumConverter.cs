using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Helpers.Json
{
    public class FlexibleStringEnumConverter<TEnum> : JsonConverter<TEnum>
        where TEnum : struct, Enum
    {
        private readonly TEnum _fallbackValue;

        public FlexibleStringEnumConverter(TEnum fallbackValue)
        {
            _fallbackValue = fallbackValue;
        }

        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            if (Enum.TryParse<TEnum>(value, ignoreCase: true, out TEnum result))
            {
                return result;
            }
            else
            {
                return _fallbackValue;
            }
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
