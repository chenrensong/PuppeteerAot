using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PuppeteerAot
{
    /// <summary>
    /// Target.
    /// </summary>
    [DebuggerDisplay("Target {Type} - {Url}")]
    public abstract class Target : ITarget
    {
        public Target(
            TargetInfo targetInfo,
            CDPSession session,
            BrowserContext context,
            ITargetManager targetManager,
            Func<bool, Task<CDPSession>> sessionFactory)
        {
            Session = session;
            TargetInfo = targetInfo;
            SessionFactory = sessionFactory;
            BrowserContext = context;
            TargetManager = targetManager;

            if (session != null)
            {
                session.Target = this;
            }
        }

        /// <inheritdoc/>
        public string Url => TargetInfo.Url;

        /// <inheritdoc/>
        public virtual TargetType Type => TargetInfo.Type;

        /// <inheritdoc/>
        public string TargetId => TargetInfo.TargetId;

        /// <inheritdoc/>
        public abstract ITarget Opener { get; }

        /// <inheritdoc/>
        IBrowser ITarget.Browser => Browser;

        /// <inheritdoc/>
        IBrowserContext ITarget.BrowserContext => BrowserContext;

        public BrowserContext BrowserContext { get; }

        public Browser Browser => BrowserContext.Browser;

        public Task<InitializationStatus> InitializedTask => InitializedTaskWrapper.Task;

        public TaskCompletionSource<InitializationStatus> InitializedTaskWrapper { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public Task CloseTask => CloseTaskWrapper.Task;

        public TaskCompletionSource<bool> CloseTaskWrapper { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public Func<bool, Task<CDPSession>> SessionFactory { get; private set; }

        public ITargetManager TargetManager { get; }

        public bool IsInitialized { get; set; }

        public CDPSession Session { get; }

        public TargetInfo TargetInfo { get; set; }

        /// <inheritdoc/>
        public virtual Task<IPage> PageAsync() => Task.FromResult<IPage>(null);

        /// <inheritdoc/>
        public virtual Task<WebWorker> WorkerAsync() => Task.FromResult<WebWorker>(null);

        /// <inheritdoc/>
        public abstract Task<IPage> AsPageAsync();

        /// <inheritdoc/>
        public abstract Task<ICDPSession> CreateCDPSessionAsync();
    }
}
