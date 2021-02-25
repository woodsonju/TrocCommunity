using System;
using System.Runtime.Serialization;

namespace TrocCommunity.WebUi.ExceptionTC
{
    [Serializable]
    public  class UserNameIsException : Exception
    {
        public UserNameIsException()
        {
        }

        public UserNameIsException(string message) : base(message)
        {
        }

        public UserNameIsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserNameIsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}