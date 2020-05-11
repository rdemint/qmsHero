using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml
{
    public class DocumentPropertyCreatedTime : DocProperty
    {
        public DocumentPropertyCreatedTime()
        {
        }

        public DocumentPropertyCreatedTime(object state) : base(state)
        {
        }

        public DocumentPropertyCreatedTime(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(doc.PackageProperties.Created, 1));
        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(doc.PackageProperties.Created, 1));
        }
        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            doc.PackageProperties.Created = (DateTime)this.state;
            return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(doc.PackageProperties.Created, 1));
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            doc.PackageProperties.Created = (DateTime)this.state;
            return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(doc.PackageProperties.Created, 1));
        }
    }
}
