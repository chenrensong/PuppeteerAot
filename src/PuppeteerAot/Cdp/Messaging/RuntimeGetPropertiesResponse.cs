using System.Collections.Generic;

namespace PuppeteerAot.Cdp.Messaging
{
    public class RuntimeGetPropertiesResponse
    {
        public IEnumerable<RuntimeGetPropertiesResponseItem> Result { get; set; }

        public class RuntimeGetPropertiesResponseItem
        {
            public object Enumerable { get; set; }

            public string Name { get; set; }

            public RemoteObject Value { get; set; }
        }
    }
}
