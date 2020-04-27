using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.Properties
{
    public class AsDoc : DocProperty
    {
        public AsDoc()
        {
        }

        public AsDoc(object state) : base(state)
        {
        }

        public override DocProperty Read(FileInfo file, DocConfig config)
        {
            return base.Read(file, config);
        }
    }
}
