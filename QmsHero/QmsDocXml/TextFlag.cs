using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml
{
    public class TextFlag: DocProperty
    {
        public TextFlag()
        {
        }

        public TextFlag(object state) : base(state)
        {
        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            string docText;
            foreach(var part in doc.Package.GetParts())
            {
                using(StreamReader streamReader = new StreamReader(part.GetStream()))
                {
                    docText = streamReader.ReadToEnd();
                }
                if (docText.Contains((string)this.State))
                    return Results.Fail(new Error($"The document contains the disallowed text, '{this.State.ToString()}', at least in the following part: {part.ContentType.ToString()}."));
            }
                return Results.Ok<QDocProperty>(new TextFlag((string)this.State));
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            bool flagFound = false;
            string docText;
            foreach(WorksheetPart worksheetPart in doc.WorkbookPart.WorksheetParts)
            {
                using (StreamReader streamReader = new StreamReader(worksheetPart.GetStream()))
                {
                    docText = streamReader.ReadToEnd();
                }
                if (docText.Contains((string)this.State))
                    flagFound = true;
            }
             if(flagFound==true)
                return Results.Fail(new Error($"The document contains the disallowed text, {this.State.ToString()}."));
            else
            {
                return Results.Ok<QDocProperty>(new TextFlag((string)this.State));
            }    
        }
    }
}
