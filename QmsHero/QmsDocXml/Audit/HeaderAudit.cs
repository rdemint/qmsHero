using Wxml = DocumentFormat.OpenXml.Wordprocessing;
using Sxml = DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using FluentResults;

namespace QmsDocXml.Audit
{
    class HeaderAudit: DocAudit
    {
        public override void Audit(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach (var workSheetPart in workSheetParts)
            {
                foreach (var header in workSheetPart.Worksheet.Elements<Sxml.HeaderFooter>().ToList())
                {
                    if (header.DifferentOddEven != null && header.DifferentOddEven)
                    {
                        throw new MultipleHeadersExistException();
                    }
                }
            }
        }

        public override void Audit(WordprocessingDocument doc, WordDocConfig config)
        {
            Result<Wxml.TableCell> headerCellResult = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            if (headerCellResult.IsSuccess) { 
                this.CheckParsCount(headerCellResult.Value.Elements<Wxml.Paragraph>().ToList());
            }

            var revisionCellResult = WordPartHeaderTableCell.Get(doc, config.HeaderRevisionRow, config.HeaderRevisionCol);
            if(revisionCellResult.IsSuccess)
            {
                this.CheckParsCount(revisionCellResult.Value.Elements<Wxml.Paragraph>().ToList());

            }

            var effectiveDateCellResult = WordPartHeaderTableCell.Get(doc, config.HeaderEffectiveDateRow, config.HeaderEffectiveDateCol);
            if(effectiveDateCellResult.IsSuccess)
            {
                this.CheckParsCount(effectiveDateCellResult.Value.Elements<Wxml.Paragraph>().ToList());
            }

            var nameCellResult = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            if(nameCellResult.IsSuccess)
            {
                this.CheckParsCount(nameCellResult.Value.Elements<Wxml.Paragraph>().ToList());
            }
        }

        private void CheckParsCount(List<Wxml.Paragraph> pars)
        {
            if (pars.Count > 1)
            {
                throw new MultipleElementsExistException();
            }
        }
    }
}
