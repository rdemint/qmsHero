﻿using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs
{
    public class ExcelDocConfig: IDocConfig
    {
        string effectiveDateText;
        Regex effectiveDateRegex;
        string revisionText;
        string revisionEffectiveDateSeparator;

        public ExcelDocConfig()
        {
            this.Initialize();
        }

        public string EffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string RevisionText { get => revisionText; set => revisionText = value; }
        public string RevisionEffectiveDateSeparator { get => revisionEffectiveDateSeparator; set => revisionEffectiveDateSeparator = value; }
        public Regex EffectiveDateRegex { get => effectiveDateRegex; set => effectiveDateRegex = value; }

        public void Initialize()
        {
            this.EffectiveDateText = "Effective Date: ";
            this.RevisionText = "Revision: ";
            this.RevisionEffectiveDateSeparator = "\r\n";

        }
        #region HeaderFooter


        #endregion
    }
}
