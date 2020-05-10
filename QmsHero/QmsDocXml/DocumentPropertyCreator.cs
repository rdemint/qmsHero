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
    public class DocumentPropertyCreator : DocProperty
    {
        public DocumentPropertyCreator()
        {
        }

        public DocumentPropertyCreator(object state) : base(state)
        {
        }

        public DocumentPropertyCreator(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var prop = doc.PackageProperties.Creator;
            if (prop != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else if (prop == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy("", 1));
            }
            return Results.Fail(new Error("Did not read the Company Document Property"));

        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            var prop = doc.PackageProperties.Creator;
            if (prop != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else if (prop == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy("", 1));
            }
            return Results.Fail(new Error("Did not read the Creator Property"));
        }
        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var prop = doc.PackageProperties.Creator;
            if (prop != null)
            {
                prop = (string)this.state;
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else
            {
                return Results.Fail(new Error("Did not read the Creator Property"));
            }
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            var prop = doc.PackageProperties.Creator;
            if (prop != null)
            {
                prop = (string)this.state;
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else
            {
                return Results.Fail(new Error("Did not read the Creator Property"));
            }
        }
    }
}
