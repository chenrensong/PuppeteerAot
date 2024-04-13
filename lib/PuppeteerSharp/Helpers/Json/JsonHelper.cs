using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using PuppeteerSharp.Cdp.Messaging;

namespace PuppeteerSharp.Helpers.Json
{
    internal static class JsonHelper
    {
        public static readonly JsonSerializerOptions DefaultJsonSerializerSettings = Create(true);

        public static readonly JsonSerializerOptions NotCamelCaseJsonSerializerSettings = Create(false);

        private static JsonSerializerOptions Create(bool camelCase)
        {
            var options = new JsonSerializerOptions();
            if (camelCase)
            {
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }

            options.TypeInfoResolver = JsonSerializer.IsReflectionEnabledByDefault ?
                new DefaultJsonTypeInfoResolver() : JsonModelContext.Default;
            options.Converters.Add(new JsonStringEnumMemberConverter());
            options.Converters.Add(new FlexibleStringEnumConverter<FrameDetachedReason>(FrameDetachedReason.Unknown));
            options.Converters.Add(new FlexibleStringEnumConverter<RemoteObjectSubtype>(RemoteObjectSubtype.Other));
            options.Converters.Add(new FlexibleStringEnumConverter<RemoteObjectType>(RemoteObjectType.Other));
            options.Converters.Add(new FlexibleStringEnumConverter<ResourceType>(ResourceType.Unknown));
            options.Converters.Add(new FlexibleStringEnumConverter<DOMWorldType>(DOMWorldType.Other));
            options.Converters.Add(new FlexibleStringEnumConverter<SameSite>(SameSite.None));
            options.Converters.Add(new FlexibleStringEnumConverter<TargetType>(TargetType.Other));

            return options;
        }
    }
}
