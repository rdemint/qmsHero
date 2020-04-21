using System;
using System.Runtime.Serialization;

namespace QmsDocXml
{
    [Serializable]
    internal class MultipleElementsExistException : Exception
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