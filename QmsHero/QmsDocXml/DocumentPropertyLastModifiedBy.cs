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
    class DocumentPropertyLastModifiedBy : DocProperty
    {
        public DocumentPropertyLastModifiedBy()
        {
        }

        public DocumentPropertyLastModifiedBy(object state) : base(state)
        {
        }

        public DocumentPropertyLastModifiedBy(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var prop = doc.PackageProperties.LastModifiedBy;
            if (prop != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else if (prop == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy("", 1));
            }
            return Results.Fail(new Error("Did not read the Modifed By Document Property"));

        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            var prop = doc.PackageProperties.LastModifiedBy;
            if (prop != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else if (prop == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy("", 1));
            }
            return Results.Fail(new Error("Did not read the Modifed By Property"));
        }
        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var prop = doc.PackageProperties.LastModifiedBy;
            if (prop != null)
            {
                prop = (string)this.state;
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            //else if (prop == null)
            //{
            //var newProp = new DocumentFormat.OpenXml.Wordprocessing.DocPartProperties
            //   prop = (string)state;
            //    return Results.Ok<QDocProperty>(new LastModifiedByDocumentProperty("", 1));
            //}

            else
            {
                return Results.Fail(new Error("Did not read the Modified By Property"));
            }
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            var prop = doc.PackageProperties.LastModifiedBy;
            if (prop != null)
            {
                prop = (string)this.state;
                return Results.Ok<QDocProperty>(new DocumentPropertyLastModifiedBy(prop, 1));
            }

            else
            {
                return Results.Fail(new Error("Did not read the Modified By Property"));
            }
        }
    }
}
