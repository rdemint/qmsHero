using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QmsDoc.Core;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Actions
{
    class RenameDocument : DocAction
    {
        public RenameDocument()
        {
        }

        public RenameDocument(object state) : base(state)
        {
        }

        public override Result<DocAction> Inspect(ExcelDoc doc)
        {
            throw new NotImplementedException();
        }

        public override Result<DocAction> Process(WordDoc doc)
        {
            var headerRevisionResult = doc.Process(new HeaderRevision((string)this.State));
            var 


        }
    }
}
