using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Exceptions
{
    public class DocPropertyGetException : Exception
    {
        public DocPropertyGetException()
        {
        }

        public DocPropertyGetException(string message) : base(message)
        {
        }

        public DocPropertyGetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DocPropertyGetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
