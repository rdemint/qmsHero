using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
namespace QmsDoc.Core
{
    public class DocProperty : QDocProperty
    {
        public DocProperty()
        {
        }

        public DocProperty(string value) : base(value)
        {
        }

        public DocProperty(string name, string value) : base(name, value)
        {
        }
    }
}
