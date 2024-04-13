using System.Collections.Generic;

namespace PuppeteerAot.Helpers
{
    public static class DictionaryExtensions
    {
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> dic)
            => new(dic, dic.Comparer);
    }
}
