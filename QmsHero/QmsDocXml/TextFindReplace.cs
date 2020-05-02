using DocumentFormat.OpenXml.Packaging;
using Wxml = DocumentFormat.OpenXml.Wordprocessing;
using Sxml = DocumentFormat.OpenXml.Spreadsheet;
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
using System.Text.RegularExpressions;
using QmsDocXml.Common;

namespace QmsDocXml
{
    public class TextFindReplace : DocProperty
    {
        public TextFindReplace()
        {
        }

        public TextFindReplace(object state) : base(state)
        {
        }

        //public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        //{
        //    bool flagFound = false;
        //    string docText;
        //    foreach (WorksheetPart worksheetPart in doc.WorkbookPart.WorksheetParts)
        //    {
        //        //var textMatch = worksheetPart.Worksheet.Elements<Text>
        //    }
        //    if (flagFound == true)
        //        return Results.Fail(new Error($"The document contains the disallowed text, {this.State.ToString()}."));
        //    else
        //    {
        //        return Results.Ok<QDocProperty>(new TextFlag((string)this.State));
        //    }
        //}

        //public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        //{
        //    return base.Read(doc, config);
        //}

        public override Result<QDocProperty> Read(WordprocessingDocument doc, Regex rx)
        {
            MatchCollection matches = TextXml.Search(doc, rx);
            if (matches.Count > 0)
                return Results.Fail(new Error($"The document contains {matches.Count} matches for '{matches.ToString()}'"));
            else
            {
                return Results.Ok<QDocProperty>(new TextFindReplace((string)this.State));
            }
        }
        
        public override Result<QDocProperty> Write(WordprocessingDocument doc, Regex rx)
        {
            TextXml.SearchAndReplace(doc, rx, this.State.ToString());
            //string docText;
            ////headers
            //foreach(var header in doc.MainDocumentPart.HeaderParts.ToList())
            //{
            //    var headerMatches = header.RootElement.Elements<Wxml.Text>().Where(text=> text.Text.ToLower().Contains((string)this.State.ToString().ToLower()));
            //    if (headerMatches.Any())
            //        foreach(var headerMatch in headerMatches)
            //        {
            //            headerMatch.Select(el=> el.Elements<Wxml.Text>()
            //            .Select(textEl=> textEl.Text.r)
            //        }
            //}
            ////footer
            //foreach(var footer in doc.MainDocumentPart.FooterParts.ToList())
            //{
            //    var footerMatches = footer.RootElement.Elements<Wxml.Text>().Where(text => text.Text.ToLower().Contains((string)this.State.ToString().ToLower()));
            //    if (footerMatches.Any())
            //        return Results.Fail(new Error($"The document contains the disallowed text, '{this.State.ToString()}', at least in the footer part."));
            //}

            ////body

            //var matches = doc.MainDocumentPart.Document
            //    .Elements<Wxml.Text>()
            //    .Where(text => text.Text.ToLower()
            //    .Contains((string)this.State.ToString().ToLower()));
            //if (matches.Any())
            //    return Results.Fail(new Error($"The document contains the disallowed text, '{this.State.ToString()}', at least in the body part."));

            TextXml.SearchAndReplace(doc, rx, this.State.ToString());
            return Results.Ok<QDocProperty>(new TextFlag((string)this.State));
        }
    }
}
