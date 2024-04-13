using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PuppeteerAot.Cdp.Messaging.AccessibilityGetFullAXTreeResponse;

namespace PuppeteerAot.QueryHandlers
{
    public class CssQueryHandler : QueryHandler
    {
        public CssQueryHandler()
        {
            QuerySelector = @"(element, selector) => {
                return element.querySelector(selector);
            }";

            QuerySelectorAll = @"(element, selector) => {
                return element.querySelectorAll(selector);
            }";
        }
    }
}
