using System;
using System.Runtime.Serialization;

namespace QFileUtil.Exceptions
{
    [Serializable]
    internal class FileCopyNotReadyException : Exception
    {
        public FileCopyNotReadyException()
        {
        }

        public FileCopyNotReadyException(string message) : base(message)
        {
        }

        public FileCopyNotReadyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileCopyNotReadyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}