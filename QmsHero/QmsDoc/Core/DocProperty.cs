using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
namespace QmsDoc.Core
{
    public class QmsDocProperty : QDocProperty
    {
        public QmsDocProperty()
        {
        }

        public QmsDocProperty(string value) : base(value)
        {
        }

        public QmsDocProperty(string name, string value) : base(name, value)
        {
        }
    }
}
