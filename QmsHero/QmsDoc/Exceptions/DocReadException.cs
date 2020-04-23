using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Exceptions
{
    public class DocReadException : DocProcessingException
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
