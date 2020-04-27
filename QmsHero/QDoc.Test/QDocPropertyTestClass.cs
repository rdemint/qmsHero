using QDoc.Core;
using QDoc.Interfaces;
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

        public override QDocProperty Read(object doc, object docConfig)
        {
            throw new NotImplementedException();
        }

        public override void Write(object doc, IDocConfig docConfig)
        {
            throw new NotImplementedException();
        }
    }
}
