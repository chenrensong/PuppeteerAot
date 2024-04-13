using System.Collections.Generic;

namespace PuppeteerAot.Cdp.Messaging
{
    public class ContinueWithAuthRequest
    {
        public string RequestId { get; set; }

        public ContinueWithAuthRequestChallengeResponse AuthChallengeResponse { get; set; }

        public string RawResponse { get; set; }

        public string ErrorReason { get; set; }

        public string Url { get; set; }

        public string Method { get; set; }

        public string PostData { get; set; }

        public Dictionary<string, string> Headers { get; set; }
    }
}
