using FluentResults;
using QDoc.Core;
using QDoc.Interfaces;
using QmsDoc.Docs.Common;
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

        public override Result<int> Read(object doc, object docConfig)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Write(object doc, object docConfig)
        {
            throw new NotImplementedException();
        }
    }
}
