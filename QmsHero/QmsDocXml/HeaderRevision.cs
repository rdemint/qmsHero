﻿using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using XL = DocumentFormat.OpenXml.Spreadsheet;
using QDoc.Interfaces;
using QmsDoc.Docs.Word;
using QmsDoc.Core;
using QmsDoc.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using QmsDoc.Docs.Excel;
using System.Runtime.Serialization;
using FluentResults;
using QDoc.Core;
using QmsDocXml.Common;

namespace QmsDocXml
{
    public class HeaderRevision: DocProperty
    {
        public HeaderRevision()
        {
        }

        public HeaderRevision(object state) : base(state)
        {
        }

        private HeaderRevision(object state, int stateCount) : base(state, stateCount)
        {
        }

        public Result<Paragraph> FetchRevisionPart(WordprocessingDocument doc, WordDocConfig config)
        {
            var tableCellResult = WordPartHeaderTableCell.Get(doc, config.HeaderRevisionRow, config.HeaderRevisionCol);
            if(tableCellResult.IsFailed)
            {
                return Results.Fail(new Error($"Did not identify the table cell at row {config.HeaderRevisionRow} and column {config.HeaderRevisionCol}."));
            }
            else
            {
                return Results.Ok<Paragraph>(tableCellResult.Value.Elements<Paragraph>().First());

            }
        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            var parResult = FetchRevisionPart(doc, config);
            if(parResult.IsFailed)
            {
                return Results.Fail(new Error("Did not identify the revision table cell in the document."));
            }
            Match match = config.HeaderRevisionRegex.Match(parResult.Value.InnerText);
            if(match.Success)
            {
                return Results.Ok<QDocProperty>(new HeaderRevision(match.ToString(), 1));
            }
            else
            {
                return Results.Fail(new Error($"Pattern {config.HeaderRevisionRegex.ToString()} did not match any text in table cell paragraph text {parResult.Value.InnerText}."));
            }
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheet = doc.WorkbookPart.WorksheetParts.First().Worksheet;
            var header = workSheet.Elements<XL.HeaderFooter>().FirstOrDefault();
            if (header.DifferentOddEven != null && header.DifferentOddEven)
            {
                return Results.Fail(new Error("Multiple headers exist in the document"));
            }
            Match match = config.HeaderRevisionRegex.Match(header.OddHeader.Text);
            if(match.Success)
            {
                var m = match.ToString();
                return Results.Ok<QDocProperty>(
                    new HeaderRevision(m.Replace(config.HeaderRevisionText, ""), 1)
                    );
            }

            else
            {
                return Results.Fail(
                    new Error("Could not read the the header revision from the file")
                        .CausedBy(new DocReadException())
                        );
            }
        }


        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            var parResult = FetchRevisionPart(doc, config);
            if (parResult.IsFailed)
            {
                return Results.Fail(new Error("Did not identify the revision table cell in the document."));
            }
            Match parMatch = config.HeaderRevisionRegex.Match(parResult.Value.InnerText);
            if(parMatch.Success)
            {
                var tempParList = new List<Paragraph>();
                tempParList.Add(parResult.Value);
                TextXml.ReplaceParagraphElementText(tempParList, config.HeaderRevisionRegex, (string)this.state);
                //Text text = par.Elements<Run>().First().Elements<Text>().First();
                //text.Text = config.HeaderRevisionText + (string)this.State;
                return Results.Ok<QDocProperty>(new HeaderRevision((string)this.State, 1));
            }
            else
            {
                return Results.Fail((
                    new Error($"Did not identify header text, '{(string)this.state} in the target document text, '{parResult.Value.InnerText}'.")));
            }
        }

        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach(var workSheetPart in workSheetParts)
            {
                foreach(var header in workSheetPart.Worksheet.Elements<XL.HeaderFooter>().ToList())
                {
                    if (header.DifferentOddEven != null && header.DifferentOddEven)
                    {
                        return Results.Fail(new Error("Multiple headers exist in the document"));
                    }

                    Match match = config.HeaderRevisionRegex.Match(header.OddHeader.Text);
                    if (match.Success)
                    {
                        string currentRev = match.ToString();
                        header.OddHeader.Text = Regex.Replace(
                            header.OddHeader.Text, 
                            config.HeaderRevisionRegex.ToString(), 
                            (string)this.State);
                        //string newInnerText = header.OddHeader.Text.Replace(currentRev, replaceRev);
                        //header.OddHeader.Text = newInnerText;
                    }

                    else
                    {
                        return Results.Fail(
                    new Error("Could not read the the header revision from the file")
                        .CausedBy(new DocReadException())
                        );
                    }


                }
            }
            return Results.Ok<QDocProperty>(new HeaderRevision((string)this.State, 1));
        }

        public override bool IsValid(object config)
        {
            var wConfig = config as WordDocConfig;
            var xlConfig = config as ExcelDocConfig;

            Regex rx = null;

            if(wConfig!=null)
            {
                rx = wConfig.HeaderEffectiveDateRegex;

            }
            else if(xlConfig!=null)
            {
                rx = xlConfig.HeaderEffectiveDateRegex;
                throw new NotImplementedException();
                //effectivedateregex works differently in Excel, this wont work

            }
            var match = rx.Match(this.State.ToString());
            if (
                match.Success &&
                base.IsValid(config)
                ) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            }
        }
    }

}
