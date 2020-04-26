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
            if (header.DifferentOddEven != null && header.DifferentOddEven)
            {
                throw new MultipleHeadersExistException();
            }
            Match match = config.HeaderNameRegex.Match(header.OddHeader.Text);
            Match match2 = Regex.Match(match.Value, "&\\\"(.*?)\\\"");
            if (match2.Success)
            {
                result = match.ToString().Replace(match2.ToString(), "");
            }
            else
            {
                result = match.ToString().Replace(config.HeaderNameText, "");
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

        public bool ReadAudit(WordprocessingDocument doc, WordDocConfig config) 
        {
            var result = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            var pars = result.Elements<Wxml.Paragraph>().ToList();
            if(pars.Count>1)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        public bool ReadAudit(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            throw new NotImplementedException();
        }




    }
}
