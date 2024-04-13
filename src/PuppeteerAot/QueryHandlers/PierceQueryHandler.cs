using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PuppeteerAot.Cdp.Messaging.AccessibilityGetFullAXTreeResponse;

namespace PuppeteerAot.QueryHandlers
{
    public class PierceQueryHandler : QueryHandler
    {
        public PierceQueryHandler()
        {
            QuerySelector = @"(element, selector, {pierceQuerySelector}) => {
                return pierceQuerySelector(element, selector);
            }";

            QuerySelectorAll = @"(element, selector, {pierceQuerySelectorAll}) => {
                return pierceQuerySelectorAll(element, selector);
            }";
        }
    }
}
