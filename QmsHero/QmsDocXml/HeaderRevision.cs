using DocumentFormat.OpenXml.Packaging;
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

        public Paragraph FetchRevisionPart(WordprocessingDocument doc, WordDocConfig config)
        {
            TableCell cell = WordPartHeaderTableCell.Get(doc, config.HeaderRevisionRow, config.HeaderRevisionCol);
            return cell.Elements<Paragraph>().First();
        }

        public override DocProperty Read(WordprocessingDocument doc, WordDocConfig config)
        {
            Paragraph par = FetchRevisionPart(doc, config);
            Match match = config.HeaderRevisionRegex.Match(par.InnerText);
            return new HeaderRevision(match.ToString());
        }

        public override DocProperty Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheet = doc.WorkbookPart.WorksheetParts.First().Worksheet;
            var header = workSheet.Elements<XL.HeaderFooter>().FirstOrDefault();
            if (header.DifferentOddEven != null && header.DifferentOddEven)
            {
                throw new MultipleHeadersExistException();
            }
            Match match = config.HeaderRevisionRegex.Match(header.OddHeader.Text);
            if(match.Success)
            {
                var m = match.ToString();
                return new HeaderRevision(m.Replace(config.HeaderRevisionText, ""));
            }

            else
            {
                throw new DocReadException();
            }
        }


        public override void Write(WordprocessingDocument doc, WordDocConfig config)
        {
            Paragraph par = FetchRevisionPart(doc, config);
            par.RemoveAllChildren();
            Run run = new Run();
            Text text = new Text();
            text.Text = config.HeaderRevisionText + (string)this.State;
            run.Append(text);
            par.Append(run);
            OnPropertyChanged(); ;
        }

        public override void Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach(var workSheetPart in workSheetParts)
            {
                foreach(var header in workSheetPart.Worksheet.Elements<XL.HeaderFooter>().ToList())
                {
                    if (header.DifferentOddEven != null && header.DifferentOddEven)
                    {
                        throw new MultipleHeadersExistException();
                    }

                    Match match = config.HeaderRevisionRegex.Match(header.OddHeader.Text);
                    if (match.Success)
                    {
                        string currentRevVerbose = match.ToString();
                        string currentRev = currentRevVerbose.Replace(config.HeaderRevisionText, "");
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
