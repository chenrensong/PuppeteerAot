using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace PuppeteerAot
{
    public class MessageTask
    {
        public MessageTask()
        {
        }

        public string Message { get; set; }

        public TaskCompletionSource<JsonElement> TaskWrapper { get; set; }

        public string Method { get; set; }
    }
}
