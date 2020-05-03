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
    public class RenameDocument : DocAction
    {
        public RenameDocument(): base()
        {
        }

        public RenameDocument(object state) : base(state)
        {
        }

        public RenameDocument(object state, QDocPropertyResultCollection resultCollection) : base(state, resultCollection)
        {
        }

        public override Result<DocAction> Audit(ExcelDoc doc)
        {
            ResultCollection.Add(doc.Inspect(new HeaderName()));
            ResultCollection.Add(doc.Inspect(new FileDocName()));
            if (ResultCollection.EachItemSharesState())
            {
                return Results.Ok<DocAction>(new RenameDocument((string)this.State, ResultCollection));
            }

            else 
            {
                return Results.Fail(new Error($"The action {this.Name} did not succeed."));
            }
        }

        public override Result<DocAction> Audit(WordDoc doc)
        {
            throw new NotImplementedException();
        }

        public override Result<DocAction> Process(WordDoc doc)
        {
            ResultCollection.Add(doc.Process(new HeaderName((string)this.State)));
            ResultCollection.Add(doc.Process(new FileDocName((string)this.State)));
            if (ResultCollection.HasErrors())
            {
                return Results.Fail(new Error($"The action {this.Name} did not succeed."));
            }

            else
            {
                return Results.Ok<DocAction>(new RenameDocument((string)this.State));
            }


        }

        public override Result<DocAction> Process(ExcelDoc doc)
        {
            throw new NotImplementedException();
        }
    }
}
