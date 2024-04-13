using System;

namespace PuppeteerAot
{
    public class PageBinding
    {
        public string Name { get; set; }

        public Delegate Function { get; set; }
    }
}
