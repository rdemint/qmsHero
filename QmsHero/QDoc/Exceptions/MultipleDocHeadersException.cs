using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Exceptions
{
    public class MultipleDocHeadersException: Exception
    {
        public MultipleDocHeadersException():base()
        {

        }

        public MultipleDocHeadersException(string message) : base(message)
        {
        }

        public MultipleDocHeadersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MultipleDocHeadersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
