using System;
using System.Threading.Tasks;
using PuppeteerAot.Helpers;

namespace PuppeteerAot.Cdp
{
    /// <summary>
    /// Page target.
    /// </summary>
    public class CdpPageTarget : CdpTarget
    {
        private readonly bool _ignoreHTTPSErrors;
        private readonly ViewPortOptions _defaultViewport;
        private readonly TaskQueue _screenshotTaskQueue;

        public CdpPageTarget(
            TargetInfo targetInfo,
            CDPSession session,
            BrowserContext context,
            ITargetManager targetManager,
            Func<bool, Task<CDPSession>> sessionFactory,
            bool ignoreHTTPSErrors,
            ViewPortOptions defaultViewport,
            TaskQueue screenshotTaskQueue)
            : base(targetInfo, (CdpCDPSession)session, (CdpBrowserContext)context, targetManager, sessionFactory, screenshotTaskQueue)
        {
            _ignoreHTTPSErrors = ignoreHTTPSErrors;
            _defaultViewport = defaultViewport;
            _screenshotTaskQueue = screenshotTaskQueue;
            PageTask = null;
        }

        public Task<Page> PageTask { get; set; }

        /// <inheritdoc/>
        public override async Task<IPage> PageAsync()
        {
            if (PageTask == null)
            {
                var session = (CdpCDPSession)(Session ?? await SessionFactory(false).ConfigureAwait(false));

                PageTask = CdpPage.CreateAsync(
                    session,
                    this,
                    _ignoreHTTPSErrors,
                    _defaultViewport,
                    _screenshotTaskQueue);
            }

            return await PageTask.ConfigureAwait(false);
        }

        public override void Initialize()
        {
            _ = InitializedTaskWrapper.Task.ContinueWith(
                async initializedTask =>
                {
                    var success = initializedTask.Result;
                    if (success != InitializationStatus.Success)
                    {
                        return;
                    }

                    var opener = Opener as CdpPageTarget;

                    var openerPageTask = opener?.PageTask;
                    if (openerPageTask == null || Type != TargetType.Page)
                    {
                        return;
                    }

                    var openerPage = await openerPageTask.ConfigureAwait(false);
                    if (!openerPage.HasPopupEventListeners)
                    {
                        return;
                    }

                    var popupPage = await PageAsync().ConfigureAwait(false);
                    openerPage.OnPopup(popupPage);
                },
                TaskScheduler.Default);
            CheckIfInitialized();
        }

        /// <inheritdoc/>
        protected override void CheckIfInitialized()
        {
            if (IsInitialized)
            {
                return;
            }

            IsInitialized = !string.IsNullOrEmpty(TargetInfo.Url);
            if (IsInitialized)
            {
                InitializedTaskWrapper.TrySetResult(InitializationStatus.Success);
            }
        }
    }
}
