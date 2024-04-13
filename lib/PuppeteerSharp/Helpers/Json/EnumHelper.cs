using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace PuppeteerSharp.Helpers.Json
{
    internal static class EnumHelper
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<Enum, string>> EnumToStringCache
            = new();

        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, Enum>> StringToEnumCache
            = new();

        public static string ToValueString<TEnum>(this TEnum value)
            where TEnum : struct, Enum
        {
            var enumValues = EnumToStringCache.GetOrAdd(typeof(TEnum), type =>
            {
                var names = Enum.GetNames(type);
                var dictionary = new Dictionary<Enum, string>();
                foreach (var t in names)
                {
                    var field = type.GetField(t);
                    var valueName = field.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? t;
                    var value = (TEnum)field.GetValue(null);
                    dictionary[value] = valueName;
                }

                return dictionary;
            });

            return enumValues[value];
        }

        public static TEnum FromValueString<TEnum>(string value)
            where TEnum : struct, Enum
        {
            var enumValues = StringToEnumCache.GetOrAdd(typeof(TEnum), type =>
            {
                var names = Enum.GetNames(type);
                var dictionary = new Dictionary<string, Enum>();
                foreach (var valueName in names)
                {
                    var field = type.GetField(valueName);
                    var value = (TEnum)field.GetValue(null);
                    dictionary[valueName] = value;
                    if (field.GetCustomAttribute<EnumMemberAttribute>()?.Value is string enumMember)
                    {
                        dictionary[enumMember] = value;
                    }
                }

                return dictionary;
            });

            return (TEnum)enumValues[value];
        }
    }
}
