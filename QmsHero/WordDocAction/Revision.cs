using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QmsDoc.Word.Interfaces;
using QmsDoc.Docs;
using QmsDoc.Exceptions;
using System.Text.RegularExpressions;
using QmsDoc.Core;

namespace QmsDoc.Word
{
    public class Revision: DocProperty, IWordProperty
    {
 
        public Revision(): base()
        {
            this.Name = "Revision";
        }

        public Revision(string value) : base(value)
        {
            this.Name = "Revision";
        }

        public Paragraph FetchEffectiveDatePart(WordprocessingDocument doc, int row, int col)
        {

            TableCell cell = HeaderTableCell.Get(doc, row, col);
            Paragraph p = cell.Elements<Paragraph>().First();
            return p;
        }

        public DocProperty Get(WordprocessingDocument doc, WordDocConfig config)
        {
            Paragraph par = FetchEffectiveDatePart(doc, config.RevisionRow, config.RevisionCol);
            Match match = Regex.Match(par.InnerText, @"\d\d\d\d-\d\d-\d\d");
            if(!match.Success) {
                throw new DocPropertyGetException(); 
            }
            //this.Value = match.ToString();
            //return this.Value;
            return new Revision(match.ToString());
        }

        public void Set(WordprocessingDocument doc, WordDocConfig config)
        {
            throw new NotImplementedException();
        }
    }
}
