using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PuppeteerAot.Helpers.Json
{
    public class JsonStringEnumMemberConverter<TEnum> : JsonConverterFactory
        where TEnum : struct, Enum
    {

        private static readonly ConcurrentDictionary<Type, JsonConverter> JsonConverterCache = new();

        private static readonly ConcurrentDictionary<Type, object> FallbackValueCache = new();

        public JsonStringEnumMemberConverter()
        {
        }

        public static JsonConverter CreateJsonConverter()
        {
            var nullableType = Nullable.GetUnderlyingType(typeof(TEnum));
            JsonConverter jsonConverter = (nullableType == null ? new EnumMemberConverter<TEnum>() : new NullableEnumMemberConverter<TEnum>());
            JsonConverterCache.TryAdd(typeof(TEnum), jsonConverter);
            return jsonConverter;
        }

        public static void CreateFallbackValue(TEnum fallbackValue)
        {
            FallbackValueCache.TryAdd(typeof(TEnum), fallbackValue);
        }

        public override bool CanConvert(Type typeToConvert)
            => typeToConvert.IsEnum || Nullable.GetUnderlyingType(typeToConvert)?.IsEnum == true;

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            if (JsonConverterCache.TryGetValue(typeToConvert, out var jsonConverter))
            {
                return jsonConverter;
            }
            return CreateJsonConverter();
        }

        private static TEnum? Read<TEnum>(ref Utf8JsonReader reader)
            where TEnum : struct, Enum
        {

            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            else if (reader.TokenType == JsonTokenType.String)
            {
                return EnumHelper.FromValueString<TEnum>(reader.GetString());
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                var enumTypeCode = Type.GetTypeCode(typeof(TEnum));
                return enumTypeCode switch
                {
                    TypeCode.Int32 => (TEnum)(object)reader.GetInt32(),
                    TypeCode.Int64 => (TEnum)(object)reader.GetInt64(),
                    TypeCode.Int16 => (TEnum)(object)reader.GetInt16(),
                    TypeCode.Byte => (TEnum)(object)reader.GetByte(),
                    TypeCode.UInt32 => (TEnum)(object)reader.GetUInt32(),
                    TypeCode.UInt64 => (TEnum)(object)reader.GetUInt64(),
                    TypeCode.UInt16 => (TEnum)(object)reader.GetUInt16(),
                    TypeCode.SByte => (TEnum)(object)reader.GetSByte(),
                    _ => throw new JsonException($"Enum '{typeof(TEnum).Name}' of {enumTypeCode} type is not supported."),
                };
            }

            throw new JsonException();

        }

        private static void Write<TEnum>(Utf8JsonWriter writer, TEnum? value)
            where TEnum : struct, Enum
        {
            if (value.HasValue)
            {
                writer.WriteStringValue(value.Value.ToValueString());
            }
            else
            {
                writer.WriteNullValue();
            }
        }


        private class EnumMemberConverter<TEnum> : JsonConverter<TEnum>
            where TEnum : struct, Enum
        {
            public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                => Read<TEnum>(ref reader) ?? (FallbackValueCache.ContainsKey(typeof(TEnum)) ? (TEnum)FallbackValueCache[typeof(TEnum)] : default);

            public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
                => Write<TEnum>(writer, value);
        }

        private class NullableEnumMemberConverter<TEnum> : JsonConverter<TEnum?>
            where TEnum : struct, Enum
        {
            public override TEnum? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
                => Read<TEnum>(ref reader);

            public override void Write(Utf8JsonWriter writer, TEnum? value, JsonSerializerOptions options)
                => Write<TEnum>(writer, value);
        }
    }
}
