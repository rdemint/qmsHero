using System;
using System.Runtime.Serialization;

namespace QmsDoc.Exceptions
{
    [Serializable]
    internal class ReadDocumentNotValidException : Exception
    {
        public ReadDocumentNotValidException()
        {
        }

        public ReadDocumentNotValidException(string message) : base(message)
        {
        }

        public ReadDocumentNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReadDocumentNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}