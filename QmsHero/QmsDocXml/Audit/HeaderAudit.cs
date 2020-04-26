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
            Wxml.TableCell headerCell = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            this.CheckParsCount(headerCell.Elements<Wxml.Paragraph>().ToList());
            

            Wxml.TableCell revisionCell = WordPartHeaderTableCell.Get(doc, config.RevisionRow, config.RevisionCol);
            this.CheckParsCount(revisionCell.Elements<Wxml.Paragraph>().ToList());

            Wxml.TableCell effectiveDateCell = WordPartHeaderTableCell.Get(doc, config.EffectiveDateRow, config.EffectiveDateCol);
            this.CheckParsCount(effectiveDateCell.Elements<Wxml.Paragraph>().ToList());

            Wxml.TableCell nameCell = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            this.CheckParsCount(nameCell.Elements<Wxml.Paragraph>().ToList());
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
