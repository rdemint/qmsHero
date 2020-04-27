using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Exceptions
{
    public class InvalidDocPropertyStateException : DocProcessingException
    {
        public InvalidDocPropertyStateException()
        {
        }

        public InvalidDocPropertyStateException(string message) : base(message)
        {
        }

        public InvalidDocPropertyStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDocPropertyStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
