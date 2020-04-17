using System;
using System.Runtime.Serialization;

namespace QmsDocXml.Docs.Word.Properties
{
    [Serializable]
    internal class DocReadException : Exception
    {
        public DocReadException()
        {
        }

        public DocReadException(string message) : base(message)
        {
        }

        public DocReadException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DocReadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}