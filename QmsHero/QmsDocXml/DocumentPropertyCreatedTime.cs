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
            var prop = doc.PackageProperties.Created;
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
            var prop = doc.PackageProperties.Created;
            if (prop != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else if (prop == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy("", 1));
            }
            return Results.Fail(new Error("Did not read the Created Time Property"));
        }
        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var prop = doc.PackageProperties.Created;
            if (prop != null)
            {
                prop = (DateTime)this.state;
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else
            {
                return Results.Fail(new Error("Did not read the Created Time Property"));
            }
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            var prop = doc.PackageProperties.Created;
            if (prop != null)
            {
                prop = (DateTime)this.state;
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else
            {
                return Results.Fail(new Error("Did not read the Created Time Property"));
            }
        }
    }
}
