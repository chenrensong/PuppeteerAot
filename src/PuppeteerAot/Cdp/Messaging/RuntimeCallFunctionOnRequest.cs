using System.Collections.Generic;

namespace PuppeteerAot.Cdp.Messaging
{
    public class RuntimeCallFunctionOnRequest
    {
        public string FunctionDeclaration { get; set; }

        public int? ExecutionContextId { get; set; }

        public IEnumerable<object> Arguments { get; set; }

        public bool ReturnByValue { get; set; }

        public bool AwaitPromise { get; set; }

        public bool UserGesture { get; set; }

        public string ObjectId { get; set; }
    }
}
