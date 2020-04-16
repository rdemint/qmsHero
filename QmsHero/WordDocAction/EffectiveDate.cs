using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QmsDoc.Docs;
using QmsDoc.Exceptions;
using System.Text.RegularExpressions;
using QmsDoc.Core;

namespace QWordDoc
{
    public class EffectiveDate: DocProperty
    {
 
        public EffectiveDate(): base()
        {
            this.Name = "EffectiveDate";
        }

        public EffectiveDate(string value) : base(value)
        {
            this.Name = "EffectiveDate";
        }

        public Paragraph FetchEffectiveDatePart(WordprocessingDocument doc, int row, int col)
        {

            TableCell cell = HeaderTableCell.Get(doc, row, col);
            Paragraph p = cell.Elements<Paragraph>().First();
            return p;
        }

        public override DocProperty Get(object doc, object docConfig)
        {
            WordprocessingDocument wdoc = (WordprocessingDocument)doc;
            WordDocConfig wdocConfig = (WordDocConfig)docConfig;
            Paragraph par = FetchEffectiveDatePart(wdoc, wdocConfig.EffectiveDateRow, wdocConfig.EffectiveDateCol);
            Match match = Regex.Match(par.InnerText, @"\d\d\d\d-\d\d-\d\d");
            if(!match.Success) {
                throw new DocPropertyGetException(); 
            }
            return new EffectiveDate(match.ToString());
        }

        public override void Set(object wdoc, IDocConfig wdocConfig, string value)
        {
            if(Accepts(value, wdocConfig))
            {
                WordprocessingDocument doc = (WordprocessingDocument)wdoc;
                WordDocConfig docConfig = (WordDocConfig)wdocConfig;
                Paragraph par = FetchEffectiveDatePart(doc, docConfig.EffectiveDateRow, docConfig.EffectiveDateCol);
                Run myRun = (Run)par.Elements<Run>().First().Clone();
                par.RemoveAllChildren<Run>();
                Text text = myRun.Elements<Text>().First();
                text.Text = docConfig.EffectiveDateText + value;
                par.Append(myRun);
                this.Value = value;
                this.OnPropertyChanged();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override bool Accepts(string value, IDocConfig config)
        {
            Match match = config.EffectiveDateRegex.Match(value);

            if(
                match.Success
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
