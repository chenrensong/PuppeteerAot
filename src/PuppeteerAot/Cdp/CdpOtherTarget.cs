using System;
using System.Threading.Tasks;
using PuppeteerAot.Helpers;

namespace PuppeteerAot.Cdp
{
    /// <summary>
    /// Other target.
    /// </summary>
    public class CdpOtherTarget : CdpTarget
    {
        public CdpOtherTarget(
            TargetInfo targetInfo,
            CDPSession session,
            BrowserContext context,
            ITargetManager targetManager,
            Func<bool, Task<CDPSession>> sessionFactory,
            TaskQueue screenshotTaskQueue)
            : base(targetInfo, (CdpCDPSession)session, (CdpBrowserContext)context, targetManager, sessionFactory, screenshotTaskQueue)
        {
        }
    }
}
