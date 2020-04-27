using QDoc.Core;
using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Test
{
    class QDocManagerTestClass : QDocManager
    {
        public QDocManagerTestClass(): base()
        {
        }

        public override void Process(QDocProperty docProp)
        {
            throw new NotImplementedException();
        }

        public override void Process(QDocPropertyCollection docState)
        {
            throw new NotImplementedException();
        }
    }
}
