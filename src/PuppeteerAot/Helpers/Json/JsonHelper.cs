using PuppeteerAot.Cdp.Messaging;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace PuppeteerAot.Helpers.Json
{
    public static class JsonHelper
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


            JsonStringEnumMemberConverter<FrameDetachedReason>.CreateFallbackValue(FrameDetachedReason.Unknown);
            JsonStringEnumMemberConverter<RemoteObjectSubtype>.CreateFallbackValue(RemoteObjectSubtype.Other);
            JsonStringEnumMemberConverter<RemoteObjectType>.CreateFallbackValue(RemoteObjectType.Other);
            JsonStringEnumMemberConverter<ResourceType>.CreateFallbackValue(ResourceType.Unknown);
            JsonStringEnumMemberConverter<DOMWorldType>.CreateFallbackValue(DOMWorldType.Other);
            JsonStringEnumMemberConverter<SameSite>.CreateFallbackValue(SameSite.None);
            JsonStringEnumMemberConverter<TargetType>.CreateFallbackValue(TargetType.Other);



            //jsonStringEnumMemberConverter.Create<HttpStatusCode>();

            //jsonStringEnumMemberConverter.Create<CookieSourceScheme>();
            //jsonStringEnumMemberConverter.Create<ConsoleType>();

            //jsonStringEnumMemberConverter.Create<MediaFeature>();
            //jsonStringEnumMemberConverter.Create<InitiatorType>();
            //jsonStringEnumMemberConverter.Create<NavigationType>();
            //jsonStringEnumMemberConverter.Create<PointerType>();
            //jsonStringEnumMemberConverter.Create<MouseEventType>();
            //jsonStringEnumMemberConverter.Create<MediaType>();
            //jsonStringEnumMemberConverter.Create<DispatchKeyEventType>();
            //jsonStringEnumMemberConverter.Create<OverridePermission>();

            //jsonStringEnumMemberConverter.Create<WaitForFunctionPollingOption>();
            //jsonStringEnumMemberConverter.Create<VisionDeficiency>();
            //jsonStringEnumMemberConverter.Create<MouseButton>();
            //jsonStringEnumMemberConverter.Create<CookiePriority>();
            //jsonStringEnumMemberConverter.Create<DragEventType>();
            //jsonStringEnumMemberConverter.Create<FileChooserAction>();

            //options.Converters.Add(jsonStringEnumMemberConverter);
            return options;
        }
    }
}
