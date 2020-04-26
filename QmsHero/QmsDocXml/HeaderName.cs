﻿using DocumentFormat.OpenXml.Packaging;
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

        public override DocProperty Read(SpreadsheetDocument doc, ExcelDocConfig config)
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
            return new HeaderName(result);
        }

        public override DocProperty Read(WordprocessingDocument doc, WordDocConfig config)
        {
            Wxml.TableCell cell = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            var par = cell.Elements<Wxml.Paragraph>().First();
            string parText = par.InnerText;
            string result = parText.Replace(config.HeaderNameText, "");
            return new HeaderName(result);
        }

        public override void Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach (var workSheetPart in workSheetParts)
            {
                foreach (var header in workSheetPart.Worksheet.Elements<Sxml.HeaderFooter>().ToList())
                {
                    Match match = config.HeaderNameRegex.Match(header.OddHeader.Text);
                    if (match.Success)
                    {
                        string currentHeaderVerbose = match.ToString();
                        string currentHeader = currentHeaderVerbose.Replace(config.HeaderNameText, "");
                        string replaceHeaderVerbose = currentHeaderVerbose.Replace(currentHeader, (string)this.State);

                        string newInnerText = header.OddHeader.Text.Replace(currentHeaderVerbose, replaceHeaderVerbose);
                        header.OddHeader.Text = newInnerText;
                    }

                    else
                    {
                        throw new DocReadException();
                    }

                }
            }
        }

        public override void Write(WordprocessingDocument doc, WordDocConfig config)
        {
            Wxml.TableCell cell = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            var par = cell.Elements<Wxml.Paragraph>().First();
            Wxml.Run firstRunClone = (Wxml.Run)par.Elements<Wxml.Run>().First().Clone();
            par.RemoveAllChildren<Wxml.Run>();
            firstRunClone.Elements<Wxml.Text>().First().Text = config.HeaderNameText + (string)this.State;
            par.Append(firstRunClone);
        }

    }
}