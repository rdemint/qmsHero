using DocumentFormat.OpenXml.ExtendedProperties;
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
    public class DocumentPropertyCompany : DocProperty
    {
        public DocumentPropertyCompany()
        {
        }

        public DocumentPropertyCompany(object state) : base(state)
        {
        }

        public DocumentPropertyCompany(object state, int stateCount) : base(state, stateCount)
        {
        }
        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if(props.Company != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany(props.Company.Text, 1));
            }

            else if(props.Company == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany("", 1));
            }
            return Results.Fail(new Error("Did not read the Company Document Property"));

        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if (props.Company != null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany(props.Company.Text, 1));
            }

            else if (props.Company == null)
            {
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany("", 1));
            }
            return Results.Fail(new Error("Did not read the Company Document Property"));
        }
        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if (props.Company != null)
            {
                props.Company.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany(props.Company.Text, 1));
            }

            else if (props.Company == null)
            {
                props.Company = new DocumentFormat.OpenXml.ExtendedProperties.Company();
                props.Company.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany(props.Company.Text, 1));
            }
            return Results.Fail(new Error("Did not read the Company Document Property"));
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            var props = doc.ExtendedFilePropertiesPart.Properties;
            if (props.Company != null)
            {
                props.Company.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany(props.Company.Text, 1));
            }

            else if (props.Company == null)
            {
                props.Company = new DocumentFormat.OpenXml.ExtendedProperties.Company();
                props.Company.Text = (string)state;
                return Results.Ok<QDocProperty>(new DocumentPropertyCompany(props.Company.Text, 1));
            }
            return Results.Fail(new Error("Did not read the Company Document Property"));
        }

        
    }
}
