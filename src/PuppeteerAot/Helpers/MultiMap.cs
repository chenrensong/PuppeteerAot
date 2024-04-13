using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace PuppeteerAot.Helpers
{
    public class MultiMap<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, ICollection<TValue>> _map = new ConcurrentDictionary<TKey, ICollection<TValue>>();

        public void Add(TKey key, TValue value)
            => _map.GetOrAdd(key, _ => new ConcurrentSet<TValue>()).Add(value);

        public ICollection<TValue> Get(TKey key)
            => _map.TryGetValue(key, out var set) ? set : new ConcurrentSet<TValue>();

        public bool Has(TKey key, TValue value)
            => _map.TryGetValue(key, out var set) && set.Contains(value);

        public bool Delete(TKey key, TValue value)
            => _map.TryGetValue(key, out var set) && set.Remove(value);

        public bool TryRemove(TKey key, out ICollection<TValue> value)
            => _map.TryRemove(key, out value);

        public TValue FirstValue(TKey key)
            => _map.TryGetValue(key, out var set) ? set.FirstOrDefault() : default;

        public void Clear()
            => _map.Clear();
    }
}
