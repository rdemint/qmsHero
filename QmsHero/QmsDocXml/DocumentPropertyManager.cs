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
    public class DocumentPropertyManager : DocProperty
    {
        public DocumentPropertyManager()
        {
        }

        public DocumentPropertyManager(object state) : base(state)
        {
        }

        public DocumentPropertyManager(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if (props.Manager != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyManager(props.Manager.Text, 1));
            }

            else if (props.Manager == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyManager("", 1));
            }
            return Results.Fail(new Error("Did not read the Company Manager Property"));

        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if (props.Manager != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyManager(props.Manager.Text, 1));
            }

            else if (props.Manager == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyManager("", 1));
            }
            return Results.Fail(new Error("Did not read the Company Manager Property"));
        }
        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if (props.Manager != null)
            {
                props.Manager.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyManager(props.Manager.Text, 1));
            }

            else if (props.Manager == null)
            {
                props.Manager = new DocumentFormat.OpenXml.ExtendedProperties.Manager();
                props.Manager.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyManager("", 1));
            }
            return Results.Fail(new Error("Did not read the Company Manager Property"));
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if (props.Manager != null)
            {
                props.Manager.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyManager(props.Manager.Text, 1));
            }

            else if (props.Manager == null)
            {
                props.Manager = new DocumentFormat.OpenXml.ExtendedProperties.Manager();
                props.Manager.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyManager("", 1));
            }
            return Results.Fail(new Error("Did not read the Company Manager Property"));
        }
    }
}
