using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using PuppeteerAot.Cdp.Messaging;

namespace PuppeteerAot.Cdp
{
    public class NetworkEventManager
    {
        private readonly ConcurrentDictionary<string, RequestWillBeSentPayload> _requestWillBeSentMap = new();
        private readonly ConcurrentDictionary<string, FetchRequestPausedResponse> _requestPausedMap = new();
        private readonly ConcurrentDictionary<string, CdpHttpRequest> _httpRequestsMap = new();
        private readonly ConcurrentDictionary<string, QueuedEventGroup> _queuedEventGroupMap = new();
        private readonly ConcurrentDictionary<string, List<RedirectInfo>> _queuedRedirectInfoMap = new();
        private readonly ConcurrentDictionary<string, List<ResponseReceivedExtraInfoResponse>> _responseReceivedExtraInfoMap = new();

        public int NumRequestsInProgress
            => _httpRequestsMap.Values.Count(r => r.Response == null);

        public void Forget(string requestId)
        {
            _requestWillBeSentMap.TryRemove(requestId, out _);
            _requestPausedMap.TryRemove(requestId, out _);
            _queuedEventGroupMap.TryRemove(requestId, out _);
            _queuedRedirectInfoMap.TryRemove(requestId, out _);
            _responseReceivedExtraInfoMap.TryRemove(requestId, out _);
            _httpRequestsMap.TryRemove(requestId, out _);
        }

        public List<ResponseReceivedExtraInfoResponse> ResponseExtraInfo(string networkRequestId)
            => _responseReceivedExtraInfoMap.GetOrAdd(networkRequestId, static _ => new());

        public void QueueRedirectInfo(string fetchRequestId, RedirectInfo redirectInfo)
            => QueuedRedirectInfo(fetchRequestId).Add(redirectInfo);

        public RedirectInfo TakeQueuedRedirectInfo(string fetchRequestId)
        {
            var list = QueuedRedirectInfo(fetchRequestId);
            var result = list.FirstOrDefault();

            if (result != null)
            {
                list.Remove(result);
            }

            return result;
        }

        public ResponseReceivedExtraInfoResponse ShiftResponseExtraInfo(string networkRequestId)
        {
            var list = _responseReceivedExtraInfoMap.GetOrAdd(networkRequestId, static _ => new());
            var result = list.FirstOrDefault();

            if (result != null)
            {
                list.Remove(result);
            }

            return result;
        }

        public void StoreRequestWillBeSent(string networkRequestId, RequestWillBeSentPayload e)
            => _requestWillBeSentMap.AddOrUpdate(networkRequestId, e, (_, _) => e);

        public RequestWillBeSentPayload GetRequestWillBeSent(string networkRequestId)
        {
            _requestWillBeSentMap.TryGetValue(networkRequestId, out var result);
            return result;
        }

        public void ForgetRequestWillBeSent(string networkRequestId)
            => _requestWillBeSentMap.TryRemove(networkRequestId, out _);

        public FetchRequestPausedResponse GetRequestPaused(string networkRequestId)
        {
            _requestPausedMap.TryGetValue(networkRequestId, out var result);
            return result;
        }

        public void ForgetRequestPaused(string networkRequestId)
            => _requestPausedMap.TryRemove(networkRequestId, out _);

        public void StoreRequestPaused(string networkRequestId, FetchRequestPausedResponse e)
            => _requestPausedMap.AddOrUpdate(networkRequestId, e, (_, _) => e);

        public CdpHttpRequest GetRequest(string networkRequestId)
        {
            _httpRequestsMap.TryGetValue(networkRequestId, out var result);
            return result;
        }

        public void StoreRequest(string networkRequestId, CdpHttpRequest request)
            => _httpRequestsMap.AddOrUpdate(networkRequestId, request, (_, _) => request);

        public void ForgetRequest(string requestId)
            => _requestWillBeSentMap.TryRemove(requestId, out _);

        public void QueuedEventGroup(string networkRequestId, QueuedEventGroup group)
            => _queuedEventGroupMap.AddOrUpdate(networkRequestId, group, (_, _) => group);

        public QueuedEventGroup GetQueuedEventGroup(string networkRequestId)
        {
            _queuedEventGroupMap.TryGetValue(networkRequestId, out var result);
            return result;
        }

        // Puppeteer doesn't have this. but it seems that .NET needs this to avoid race conditions
        public void ForgetQueuedEventGroup(string networkRequestId)
            => _queuedEventGroupMap.TryRemove(networkRequestId, out _);

        private List<RedirectInfo> QueuedRedirectInfo(string fetchRequestId)
            => _queuedRedirectInfoMap.GetOrAdd(fetchRequestId, static _ => new());
    }
}
