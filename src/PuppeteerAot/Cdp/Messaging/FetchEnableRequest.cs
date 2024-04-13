namespace PuppeteerAot.Cdp.Messaging
{
    public class FetchEnableRequest
    {
        public bool HandleAuthRequests { get; set; }

        public Pattern[] Patterns { get; set; }

        public class Pattern(string urlPattern)
        {
            public string UrlPattern { get; set; } = urlPattern;
        }
    }
}
