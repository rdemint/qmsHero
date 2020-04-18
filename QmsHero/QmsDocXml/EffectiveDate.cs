﻿using System;
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
            WordprocessingDocument wdoc = (WordprocessingDocument)doc;
            WordDocConfig wdocConfig = (WordDocConfig)docConfig;
            Paragraph par = FetchEffectiveDatePart(wdoc, wdocConfig.EffectiveDateRow, wdocConfig.EffectiveDateCol);
            Match match = Regex.Match(par.InnerText, @"\d\d\d\d-\d\d-\d\d");
            return new EffectiveDate(match.ToString());
        }

        public override void Write(object wdoc, IDocConfig wdocConfig, object value)
        {
                WordprocessingDocument doc = (WordprocessingDocument)wdoc;
                WordDocConfig docConfig = (WordDocConfig)wdocConfig;
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
            //Match match = Regex.Match(this.Value, @"\d\d\d\d-\d\d-\d\d");
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