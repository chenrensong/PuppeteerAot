using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace PuppeteerAot.Helpers
{
    public class AsyncDictionaryHelper<TKey, TValue>
    {
        private readonly string _timeoutMessage;
        private readonly MultiMap<TKey, TaskCompletionSource<TValue>> _pendingRequests = new();
        private readonly ConcurrentDictionary<TKey, TValue> _dictionary = new();

        public AsyncDictionaryHelper(string timeoutMessage)
        {
            _timeoutMessage = timeoutMessage;
        }

        public ICollection<TValue> Values => _dictionary.Values;

        public async Task<TValue> GetItemAsync(TKey key)
        {
            var tcs = new TaskCompletionSource<TValue>(TaskCreationOptions.RunContinuationsAsynchronously);
            _pendingRequests.Add(key, tcs);

            if (_dictionary.TryGetValue(key, out var item))
            {
                _pendingRequests.Delete(key, tcs);
                return item;
            }

            return await tcs.Task.WithTimeout(
                new Action(() =>
                    throw new PuppeteerException(string.Format(CultureInfo.CurrentCulture, _timeoutMessage, key))),
                1000).ConfigureAwait(false);
        }

        public async Task<TValue> TryGetItemAsync(TKey key)
        {
            var tcs = new TaskCompletionSource<TValue>(TaskCreationOptions.RunContinuationsAsynchronously);
            _pendingRequests.Add(key, tcs);

            if (_dictionary.TryGetValue(key, out var item))
            {
                _pendingRequests.Delete(key, tcs);
                return item;
            }

            return await tcs.Task.WithTimeout(() => { }, 1000).ConfigureAwait(false);
        }

        public void AddItem(TKey key, TValue value)
        {
            _dictionary[key] = value;
            foreach (var tcs in _pendingRequests.Get(key))
            {
                tcs.TrySetResult(value);
            }
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            var result = _dictionary.TryRemove(key, out value);
            _ = _pendingRequests.TryRemove(key, out _);
            return result;
        }

        public void Clear()
        {
            _dictionary.Clear();
            _pendingRequests.Clear();
        }

        public TValue GetValueOrDefault(TKey key)
            => _dictionary.GetValueOrDefault(key);

        public bool TryGetValue(TKey key, out TValue value)
            => _dictionary.TryGetValue(key, out value);

        public bool ContainsKey(TKey key)
            => _dictionary.ContainsKey(key);
    }
}
