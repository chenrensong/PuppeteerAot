using System;
using System.Runtime.Serialization;

namespace PuppeteerAot
{
    [Serializable]
    public class BufferException : PuppeteerException
    {
        public BufferException()
        {
        }

        public BufferException(string message) : base(message)
        {
        }

        public BufferException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BufferException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
