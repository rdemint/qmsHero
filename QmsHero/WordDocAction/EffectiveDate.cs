﻿using QmsDoc.Interfaces;
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
            //this.Value = match.ToString();
            //return this.Value;
            return new EffectiveDate(match.ToString());
        }

        public override void Set(object wdoc, object wdocConfig)
        {
            throw new NotImplementedException();
        }
    }
}