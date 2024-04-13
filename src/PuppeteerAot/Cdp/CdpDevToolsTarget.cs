using System;
using System.Threading.Tasks;
using PuppeteerAot.Helpers;

namespace PuppeteerAot.Cdp
{
    /// <summary>
    /// DevTools target.
    /// </summary>
    public class CdpDevToolsTarget : CdpPageTarget
    {
        public CdpDevToolsTarget(
            TargetInfo targetInfo,
            CDPSession session,
            BrowserContext context,
            ITargetManager targetManager,
            Func<bool, Task<CDPSession>> sessionFactory,
            bool ignoreHTTPSErrors,
            ViewPortOptions defaultViewport,
            TaskQueue screenshotTaskQueue)
            : base(targetInfo, session, context, targetManager, sessionFactory, ignoreHTTPSErrors, defaultViewport, screenshotTaskQueue)
        {
        }

        /// <inheritdoc/>
        public override TargetType Type => TargetType.Other;
    }
}
