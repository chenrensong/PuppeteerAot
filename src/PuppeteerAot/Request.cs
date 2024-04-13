using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PuppeteerAot
{
    /// <inheritdoc/>
    public abstract class Request<TResponse>
        : IRequest
        where TResponse : IResponse
    {
        /// <inheritdoc/>
        public string Id { get; init; }

        /// <inheritdoc/>
        public string InterceptionId { get; init; }

        /// <inheritdoc/>
        public string FailureText { get; set; }

        /// <inheritdoc cref="Response"/>
        public virtual TResponse Response { get; set; }

        /// <inheritdoc/>
        IResponse IRequest.Response => Response;

        /// <inheritdoc/>
        public ResourceType ResourceType { get; init; }

        /// <inheritdoc/>
        public IFrame Frame { get; init; }

        /// <inheritdoc/>
        public bool IsNavigationRequest { get; init; }

        /// <inheritdoc/>
        public HttpMethod Method { get; init; }

        /// <inheritdoc/>
        public object PostData { get; init; }

        /// <inheritdoc/>
        public Dictionary<string, string> Headers { get; init; }

        /// <inheritdoc/>
        public string Url { get; init; }

        /// <inheritdoc/>
        public IRequest[] RedirectChain => RedirectChainList.ToArray();

        /// <inheritdoc />
        public Initiator Initiator { get; init; }

        /// <inheritdoc />
        public bool HasPostData { get; init; }

        public List<IRequest> RedirectChainList { get; init; }

        public abstract Payload ContinueRequestOverrides
        {
            get;
        }

        public abstract ResponseData ResponseForRequest
        {
            get;
        }

        public abstract RequestAbortErrorCode AbortErrorReason
        {
            get;
        }

        public bool FromMemoryCache { get; set; }

        /// <inheritdoc/>
        public abstract Task ContinueAsync(Payload overrides = null, int? priority = null);

        /// <inheritdoc/>
        public abstract Task RespondAsync(ResponseData response, int? priority = null);

        /// <inheritdoc/>
        public abstract Task AbortAsync(
            RequestAbortErrorCode errorCode = RequestAbortErrorCode.Failed,
            int? priority = null);

        /// <inheritdoc />
        public abstract Task<string> FetchPostDataAsync();

        public abstract Task FinalizeInterceptionsAsync();

        public abstract void EnqueueInterceptionAction(Func<IRequest, Task> pendingHandler);
    }
}
