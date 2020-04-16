using System;
using System.Runtime.Serialization;

namespace QDoc.Core
{
    [Serializable]
    internal class ProjectDirNotSetException : Exception
    {
        public ProjectDirNotSetException()
        {
        }

        public ProjectDirNotSetException(string message) : base(message)
        {
        }

        public ProjectDirNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProjectDirNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}