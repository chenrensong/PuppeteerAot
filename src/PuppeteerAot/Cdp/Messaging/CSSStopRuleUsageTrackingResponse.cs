namespace PuppeteerAot.Cdp.Messaging
{
    public class CSSStopRuleUsageTrackingResponse
    {
        public CSSStopRuleUsageTrackingRuleUsage[] RuleUsage { get; set; }

        public class CSSStopRuleUsageTrackingRuleUsage
        {
            public string StyleSheetId { get; set; }

            public int StartOffset { get; set; }

            public int EndOffset { get; set; }

            public bool Used { get; set; }
        }
    }
}
