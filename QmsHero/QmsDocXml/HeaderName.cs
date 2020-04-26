using DocumentFormat.OpenXml.Packaging;
using QmsDoc.Core;
using Wxml = DocumentFormat.OpenXml.Wordprocessing;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDocXml.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public override DocProperty Read(WordprocessingDocument doc, WordDocConfig config)
        {
            Wxml.TableCell cell = WordPartHeaderTableCell.Get(doc, config.HeaderNameRow, config.HeaderNameCol);
            var par = cell.Elements<Wxml.Paragraph>().First();
            string parText = par.InnerText;
            string result = parText.Replace(config.HeaderNameText, "");
            return new HeaderName(result);
        }

        public bool Audit(WordprocessingDocument doc, WordDocConfig config) 
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


    }
}
