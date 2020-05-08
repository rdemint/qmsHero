using DocumentFormat.OpenXml.Packaging;
using QmsDoc.Core;
using Wxml = DocumentFormat.OpenXml.Wordprocessing;
using Sxml = DocumentFormat.OpenXml.Spreadsheet;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Exceptions;
using System.Text.RegularExpressions;
using FluentResults;
using QDoc.Core;

namespace QmsDocXml
{
    public class HeaderName : DocProperty
    {
        public HeaderName()
        {
        }

        public HeaderName(object state) : base(state)
        {
        }

        private HeaderName(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            string result = null;
            var header = doc.WorkbookPart.WorksheetParts.First().Worksheet.Elements<Sxml.HeaderFooter>().FirstOrDefault();
            
            Match match = config.HeaderNameRegex.Match(header.OddHeader.Text);
            Match match2 = Regex.Match(match.Value, "&\\\"(.*?)\\\"");
            if (match2.Success)
            {
                result = match.ToString().Replace(match2.ToString(), "");
            }
            else if (match.Success)
            {
                result = match.ToString().Replace(config.HeaderNameText, "");
            }

            else
            {
                result = header.OddHeader.Text;
            }

            result = result.Replace(config.HeaderNameText, "");
            return Results.Ok<QDocProperty>(new HeaderName(result, 1));
        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            Wxml.TableCell cell = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            var par = cell.Elements<Wxml.Paragraph>().First();
            string parText = par.InnerText;
            //string result = parText.Replace(config.HeaderNameText, "");
            //Match match = config.HeaderNameRegex.Match(result);
            Match match = config.HeaderNameRegex.Match(parText);

            if (match.Success)
            {
            return Results.Ok<QDocProperty>(new HeaderName(match.ToString(), 1));
            }
            else
            {
                return Results.Fail(new Error($"Could not match for the document name text within, '{parText}'"));
            }
        }

        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach (var workSheetPart in workSheetParts)
            {
                foreach (var header in workSheetPart.Worksheet.Elements<Sxml.HeaderFooter>().ToList())
                {
                    Match currentNameMatch = config.HeaderNameRegex.Match(header.OddHeader.Text);
                    if (currentNameMatch.Success)
                    {
                        string newHeaderText = header.OddHeader.Text.Replace(currentNameMatch.ToString(), (string)this.State);
                        header.OddHeader.Text = newHeaderText;
                    }

                    else
                    {
                        return Results.Fail(new Error("Could not determine the current name to replace"));
                    }

                }
            }
            return Results.Ok<QDocProperty>(new HeaderName((string)this.State, 1));
        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            Wxml.TableCell cell = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            var par = cell.Elements<Wxml.Paragraph>().First();
            //Wxml.Run firstRunClone = (Wxml.Run)par.Elements<Wxml.Run>().First().Clone();
            //par.RemoveAllChildren<Wxml.Run>();
            //firstRunClone.Elements<Wxml.Text>().First().Text = config.HeaderNameText + (string)this.State;
            //par.Append(firstRunClone);
            Match currentNameMatch = config.HeaderNameRegex.Match(par.InnerText);
            if(currentNameMatch.Success)
            {
                foreach(var run in par.Elements<Wxml.Run>())
                {
                    var textEl = run.Elements<Wxml.Text>().First();
                    Match runTextMatch = Regex.Match(textEl.Text, currentNameMatch.ToString());
                    if (runTextMatch.Success)
                    {
                        string newRunText = textEl.Text.Replace(runTextMatch.ToString(), (string)this.State);
                        textEl.Text = newRunText;
                        return Results.Ok<QDocProperty>(new HeaderName((string)this.State, 1));
                    }
                }
            }
            return Results.Fail(new Error($"Could not identify the current Name within a run in the text {par.InnerText} to replace with the new name {this.State.ToString()}, using regular expression {config.HeaderNameRegex.ToString()}"));
        }

    }
}
