using System;
using System.Runtime.Serialization;

namespace PuppeteerAot
{
    [Serializable]
    public class InvalidTargetException : PuppeteerException
    {
        public InvalidTargetException()
        {
        }

        public InvalidTargetException(string message) : base(message)
        {
        }

        public InvalidTargetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidTargetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
