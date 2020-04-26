using System;
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

namespace QmsDocXml
{
    public class HeaderEffectiveDate: DocProperty
    {

        public HeaderEffectiveDate() : base() { }

        public HeaderEffectiveDate(object value) : base(value) { }


        #region word

        public WD.Paragraph FetchEffectiveDatePart(WordprocessingDocument doc, int row, int col)
        {

            WD.TableCell cell = WordPartHeaderTableCell.Get(doc, row, col);
            WD.Paragraph p = cell.Elements<WD.Paragraph>().First();
            return p;
        }

        public override DocProperty Read(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            WD.Paragraph par = FetchEffectiveDatePart(doc, docConfig.HeaderEffectiveDateRow, docConfig.HeaderEffectiveDateCol);
            Match match = Regex.Match(par.InnerText, @"\d\d\d\d-\d\d-\d\d");
            return new HeaderEffectiveDate(match.ToString());
        }

        public override DocProperty Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheet = doc.WorkbookPart.WorksheetParts.First().Worksheet;
            var header = workSheet.Elements<XL.HeaderFooter>().FirstOrDefault();
            if (header.DifferentOddEven != null && header.DifferentOddEven)
            {
                throw new MultipleHeadersExistException();
            }
            Match match = config.HeaderEffectiveDateRegex.Match(header.OddHeader.Text);
            if (match.Success)
            {
                var m = match.ToString();
                return new HeaderEffectiveDate(m.Replace(config.HeaderEffectiveDateText, ""));
            }

            else
            {
                throw new DocReadException();
            }
        }

        public override void Write(WordprocessingDocument doc, WordDocConfig docConfig)
        {
                WD.Paragraph par = FetchEffectiveDatePart(doc, docConfig.HeaderEffectiveDateRow, docConfig.HeaderEffectiveDateCol);
                WD.Run myRun = (WD.Run)par.Elements<WD.Run>().First().Clone();
                par.RemoveAllChildren<WD.Run>();
                WD.Text text = myRun.Elements<WD.Text>().First();
                text.Text = docConfig.HeaderEffectiveDateText + (string)this.State;
                par.Append(myRun);
                this.OnPropertyChanged();
        }

        public override void Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach (var workSheetPart in workSheetParts)
            {
                foreach (var header in workSheetPart.Worksheet.Elements<XL.HeaderFooter>().ToList())
                {
                    if (header.DifferentOddEven != null && header.DifferentOddEven)
                    {
                        throw new MultipleHeadersExistException();
                    }

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
                        throw new DocReadException();
                    }


                }
            }
        }

        public override bool IsValid(IDocConfig config)
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
        #endregion

        #region excel
        //not implemented
        #endregion

    }
}
