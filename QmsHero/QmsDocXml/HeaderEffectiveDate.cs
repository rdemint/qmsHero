﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using WD = DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using QmsDoc.Docs.Word;
using QmsDoc.Core;
using QDoc.Interfaces;
using System.IO;
using QmsDoc.Docs.Excel;
using XL = DocumentFormat.OpenXml.Spreadsheet;
using QmsDoc.Exceptions;
using FluentResults;
using QDoc.Core;

namespace QmsDocXml
{
    public class HeaderEffectiveDate: DocProperty
    {

        public HeaderEffectiveDate() : base() { }

        public HeaderEffectiveDate(object value) : base(value) { }

        private HeaderEffectiveDate(object state, int stateCount) : base(state, stateCount)
        {
        }


        public Result<WD.Paragraph> FetchEffectiveDatePart(WordprocessingDocument doc, int row, int col)
        {

            Result<WD.TableCell> cellResult = WordPartHeaderTableCell.Get(doc, row, col);
            if(cellResult.IsSuccess)
            {
                WD.Paragraph p = cellResult.Value.Elements<WD.Paragraph>().First();
                return Results.Ok<WD.Paragraph>(p);
            }
            else
            {
                return Results.Fail(new Error("Did not find the table cell."));
            }
        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            Result<WD.Paragraph> parResult = FetchEffectiveDatePart(doc, docConfig.HeaderEffectiveDateRow, docConfig.HeaderEffectiveDateCol);
            if(parResult.IsFailed)
            {
                return Results.Fail(new Error("Did not find the table cell."));
            }
            Match match = Regex.Match(parResult.Value.InnerText, @"\d\d\d\d-\d\d-\d\d");
            if(match.Success)
            {
                return Results.Ok<QDocProperty>(new HeaderEffectiveDate(match.ToString(), 1));
            }
            else
            {
                return Results.Fail(new Error($"Did not find any text of the pattern {this.state} in the document text '{parResult.Value.InnerText}'."));
            }
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheet = doc.WorkbookPart.WorksheetParts.First().Worksheet;
            var header = workSheet.Elements<XL.HeaderFooter>().FirstOrDefault();
            if (header.DifferentOddEven != null && header.DifferentOddEven)
            {
                return Results.Fail(new Error("Multiple headers exist")
                    .CausedBy(new MultipleHeadersExistException()
                    ));
            }
            Match match = config.HeaderEffectiveDateRegex.Match(header.OddHeader.Text);
            if (match.Success)
            {
                var m = match.ToString();
                string matchText = m.Replace(config.HeaderEffectiveDateText, "");
                return Results.Ok<QDocProperty>(new HeaderEffectiveDate(matchText, 1));
            }

            else
            {
                return Results.Fail(new Error("Could not identify the current header effective date.")
                    .CausedBy(new DocReadException()
                    ));
            }
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            Result<WD.Paragraph> parResult = FetchEffectiveDatePart(doc, docConfig.HeaderEffectiveDateRow, docConfig.HeaderEffectiveDateCol);
            if(parResult.IsFailed)
            {
                return Results.Fail(new Error("Did not find the table cell."));
            }
            WD.Run myRun = (WD.Run)parResult.Value.Elements<WD.Run>().First().Clone();
            parResult.Value.RemoveAllChildren<WD.Run>();
            WD.Text text = myRun.Elements<WD.Text>().First();
            text.Text = docConfig.HeaderEffectiveDateText + (string)this.State;
            parResult.Value.Append(myRun);
            return Results.Ok<QDocProperty>(new HeaderEffectiveDate((string)this.State, 1));
        }

        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach (var workSheetPart in workSheetParts)
            {
                foreach (var header in workSheetPart.Worksheet.Elements<XL.HeaderFooter>().ToList())
                {
                    Match match = config.HeaderEffectiveDateRegex.Match(header.OddHeader.Text);
                    if (match.Success)
                    {
                        string currentRevVerbose = match.ToString();
                        string currentRev = currentRevVerbose.Replace(config.HeaderEffectiveDateText, "");
                        string replaceRevVerbose = currentRevVerbose.Replace(currentRev, (string)this.State);

                        string newInnerText = header.OddHeader.Text.Replace(currentRevVerbose, replaceRevVerbose);
                        header.OddHeader.Text = newInnerText;
                    }

                    else
                    {
                        return Results.Fail(new Error("Could not identify the current header effective date.")
                            .CausedBy(new DocWriteException()
                    ));
                    }
                }
            }
            return Results.Ok<QDocProperty>(new HeaderEffectiveDate((string)this.State, 1));

        }

        public override bool IsValid(object config)
        {
            var rx = ((WordDocConfig)config).HeaderEffectiveDateRegex;
            var match = rx.Match(this.State.ToString());
                if (
                    match.Success &&
                    base.IsValid(config)
                    )
                {
                    return true;
                }
                else { return false; }
        }

    }
}
