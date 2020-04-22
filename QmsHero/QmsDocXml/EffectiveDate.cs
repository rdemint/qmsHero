using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using QmsDoc.Docs.Word;
using QmsDoc.Core;
using QDoc.Interfaces;
using System.IO;

namespace QmsDocXml
{
    public class EffectiveDate: DocProperty
    {

        public EffectiveDate() : base() { }

        public EffectiveDate(object value) : base(value) { }


        #region word

        public Paragraph FetchEffectiveDatePart(WordprocessingDocument doc, int row, int col)
        {

            TableCell cell = WordPartHeaderTableCell.Get(doc, row, col);
            Paragraph p = cell.Elements<Paragraph>().First();
            return p;
        }

        public override DocProperty Read(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            Paragraph par = FetchEffectiveDatePart(doc, docConfig.EffectiveDateRow, docConfig.EffectiveDateCol);
            Match match = Regex.Match(par.InnerText, @"\d\d\d\d-\d\d-\d\d");
            return new EffectiveDate(match.ToString());
        }

        public override void Write(WordprocessingDocument doc, WordDocConfig docConfig, object value)
        {
                Paragraph par = FetchEffectiveDatePart(doc, docConfig.EffectiveDateRow, docConfig.EffectiveDateCol);
                Run myRun = (Run)par.Elements<Run>().First().Clone();
                par.RemoveAllChildren<Run>();
                Text text = myRun.Elements<Text>().First();
                text.Text = docConfig.EffectiveDateText + value;
                par.Append(myRun);
                this.OnPropertyChanged();
        }

        public override bool IsValid(IDocConfig config)
        {
            var rx = ((WordDocConfig)config).RevisionRegex;
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
