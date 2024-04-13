using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static PuppeteerAot.Cdp.Messaging.AccessibilityGetFullAXTreeResponse;

namespace PuppeteerAot.QueryHandlers
{
    public class TextQueryHandler : QueryHandler
    {
        public TextQueryHandler()
        {
            QuerySelectorAll = @"(element, selector, {textQuerySelectorAll}) => {
                return textQuerySelectorAll(element, selector);
            }";
        }
    }
}
