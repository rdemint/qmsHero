using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Test
{
    class QDocPropertyTestClass : QDocProperty
    {
        public QDocPropertyTestClass()
        {
        }

        public QDocPropertyTestClass(object value) : base(value)
        {
        }

        public QDocPropertyTestClass(string name, object state) : base(name, state)
        {
        }
    }
}
