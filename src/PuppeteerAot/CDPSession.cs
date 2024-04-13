using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PuppeteerAot.Cdp;
using PuppeteerAot.Helpers;
using PuppeteerAot.Helpers.Json;

namespace PuppeteerAot
{
    /// <inheritdoc/>
    public abstract class CDPSession : ICDPSession
    {
        /// <inheritdoc/>
        public event EventHandler<MessageEventArgs> MessageReceived;

        /// <inheritdoc/>
        public event EventHandler Disconnected;

        /// <inheritdoc/>
        public event EventHandler<SessionEventArgs> SessionAttached;

        /// <inheritdoc/>
        public event EventHandler<SessionEventArgs> SessionDetached;

        public event EventHandler<SessionEventArgs> Ready;

        public event EventHandler<SessionEventArgs> Swapped;

        /// <inheritdoc/>
        public string Id { get; init; }

        /// <inheritdoc/>
        public ILoggerFactory LoggerFactory => Connection.LoggerFactory;

        public Connection Connection { get; set; }

        public Target Target { get; set; }

        public abstract CDPSession ParentSession { get; }

        /// <inheritdoc/>
        public async Task<T> SendAsync<T>(string method, object args = null, CommandOptions options = null)
        {
            var content = await SendAsync(method, args, true, options).ConfigureAwait(false);
            return content.ToObject<T>(true);
        }

        /// <inheritdoc/>
        public abstract Task<JsonElement> SendAsync(string method, object args = null, bool waitForCallback = true, CommandOptions options = null);

        /// <inheritdoc/>
        public abstract Task DetachAsync();

        public void OnSessionReady(CDPSession session) => Ready?.Invoke(this, new SessionEventArgs(session));

        public abstract void Close(string closeReason);

        public void OnSessionAttached(CDPSession session)
            => SessionAttached?.Invoke(this, new SessionEventArgs(session));

        public void OnSessionDetached(CDPSession session)
            => SessionDetached?.Invoke(this, new SessionEventArgs(session));

        public void OnSwapped(CDPSession session) => Swapped?.Invoke(this, new SessionEventArgs(session));

        /// <summary>
        /// Emits <see cref="MessageReceived"/> event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected void OnMessageReceived(MessageEventArgs e) => MessageReceived?.Invoke(this, e);

        /// <summary>
        /// Emits <see cref="Disconnected"/> event.
        /// </summary>
        protected void OnDisconnected() => Disconnected?.Invoke(this, EventArgs.Empty);
    }
}
