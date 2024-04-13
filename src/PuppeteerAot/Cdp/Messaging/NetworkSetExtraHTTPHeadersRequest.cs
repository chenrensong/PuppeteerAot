using System.Collections.Generic;

namespace PuppeteerAot.Cdp.Messaging
{
    public class NetworkSetExtraHTTPHeadersRequest(Dictionary<string, string> headers)
    {
        public Dictionary<string, string> Headers { get; set; } = headers;
    }
}
