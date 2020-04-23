using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Exceptions
{
    public class MultipleHeadersExistException : MultipleElementsExistException
    {
        public MultipleHeadersExistException()
        {
        }

        public MultipleHeadersExistException(string message) : base(message)
        {
        }

        public MultipleHeadersExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MultipleHeadersExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
