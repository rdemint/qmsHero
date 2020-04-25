
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Exceptions
{
    public class DocWriteException : DocProcessingException
    {
        public DocWriteException()
        {
        }

        public DocWriteException(string message) : base(message)
        {
        }

        public DocWriteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DocWriteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
