using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Exceptions
{
    public class DocProcessingException : Exception
    {
        public DocProcessingException()
        {
        }

        public DocProcessingException(string message) : base(message)
        {
        }

        public DocProcessingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DocProcessingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
