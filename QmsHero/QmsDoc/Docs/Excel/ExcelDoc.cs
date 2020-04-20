using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
using QDoc.Docs;
using QDoc.Interfaces;

namespace QmsDoc.Docs.Excel
{
    public class ExcelDoc : Doc
    {
        public ExcelDoc() { }

        public ExcelDoc(FileInfo fileInfo) : base(fileInfo) { }

        public ExcelDoc(FileInfo fileInfo, IDocConfig docConfig) : base(fileInfo, docConfig) { }

        public override void Process(QDocProperty prop)
        {
            throw new NotImplementedException();
        }
        public override QDocProperty Inspect(QDocProperty prop)
        {
            throw new NotImplementedException();
        }

        public override IDocState Inspect(IDocState docState)
        {
            return base.Inspect(docState);
        }


    }
}
