using System;
using System.Runtime.Serialization;

namespace QmsDoc.Exceptions
{
    [Serializable]
    public class MultipleElementsExistException : DocProcessingException
    {
        public MultipleElementsExistException()
        {
        }

        public MultipleElementsExistException(string message) : base(message)
        {
        }

        public MultipleElementsExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MultipleElementsExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}