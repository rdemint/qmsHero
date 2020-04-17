using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Exceptions
{
    class DocConfigNotValidException : Exception
    {
        public DocConfigNotValidException()
        {
        }

        public DocConfigNotValidException(string message) : base(message)
        {
        }

        public DocConfigNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DocConfigNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
