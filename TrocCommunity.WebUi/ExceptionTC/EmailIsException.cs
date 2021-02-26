using System;
using System.Runtime.Serialization;

namespace TrocCommunity.WebUi.ExceptionTC
{
    [Serializable]
    public  class EmailIsException : Exception
    {
        public EmailIsException()
        {
        }

        public EmailIsException(string message) : base(message)
        {
        }

        public EmailIsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmailIsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}