using System;
using System.Drawing;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using PuppeteerAot.Cdp.Messaging;
using PuppeteerAot.Input;
using PuppeteerAot.Media;

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

            //options.Converters.Add(new FlexibleStringEnumConverter<FrameDetachedReason>(FrameDetachedReason.Unknown));
            //options.Converters.Add(new FlexibleStringEnumConverter<RemoteObjectSubtype>(RemoteObjectSubtype.Other));
            //options.Converters.Add(new FlexibleStringEnumConverter<RemoteObjectType>(RemoteObjectType.Other));
            //options.Converters.Add(new FlexibleStringEnumConverter<ResourceType>(ResourceType.Unknown));
            //options.Converters.Add(new FlexibleStringEnumConverter<DOMWorldType>(DOMWorldType.Other));
            //options.Converters.Add(new FlexibleStringEnumConverter<SameSite>(SameSite.None));
            //options.Converters.Add(new FlexibleStringEnumConverter<TargetType>(TargetType.Other));


            var jsonStringEnumMemberConverter = new JsonStringEnumMemberConverter();
            jsonStringEnumMemberConverter.Create<FrameDetachedReason>(FrameDetachedReason.Unknown);
            jsonStringEnumMemberConverter.Create<RemoteObjectSubtype>(RemoteObjectSubtype.Other);
            jsonStringEnumMemberConverter.Create<ResourceType>(ResourceType.Unknown);
            jsonStringEnumMemberConverter.Create<DOMWorldType>(DOMWorldType.Other);
            jsonStringEnumMemberConverter.Create<SameSite>(SameSite.None);
            jsonStringEnumMemberConverter.Create<TargetType>(TargetType.Other);


            jsonStringEnumMemberConverter.Create<CookieSourceScheme>();
            jsonStringEnumMemberConverter.Create<RemoteObjectType>();
            jsonStringEnumMemberConverter.Create<ConsoleType>();
            jsonStringEnumMemberConverter.Create<HttpStatusCode>();
            jsonStringEnumMemberConverter.Create<MediaFeature>();
            jsonStringEnumMemberConverter.Create<InitiatorType>();
            jsonStringEnumMemberConverter.Create<NavigationType>();
            jsonStringEnumMemberConverter.Create<PointerType>();
            jsonStringEnumMemberConverter.Create<MouseEventType>();
            jsonStringEnumMemberConverter.Create<MediaType>();
            jsonStringEnumMemberConverter.Create<DispatchKeyEventType>();
            jsonStringEnumMemberConverter.Create<OverridePermission>();
            jsonStringEnumMemberConverter.Create<WaitForFunctionPollingOption>();
            jsonStringEnumMemberConverter.Create<VisionDeficiency>();
            jsonStringEnumMemberConverter.Create<MouseButton>();
            jsonStringEnumMemberConverter.Create<CookiePriority>();
            jsonStringEnumMemberConverter.Create<DragEventType>();
            jsonStringEnumMemberConverter.Create<FileChooserAction>();
            options.Converters.Add(jsonStringEnumMemberConverter);
            return options;
        }
    }
}
