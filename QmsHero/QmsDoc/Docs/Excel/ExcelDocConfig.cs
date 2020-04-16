using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Excel
{
    public class QExcelDocConfig: IQDocConfig
    {
        string effectiveDateText;
        Regex effectiveDateRegex;
        string revisionText;
        string revisionEffectiveDateSeparator;
        int logoHeight;

        public QExcelDocConfig()
        {
            this.Initialize();
        }

        public string EffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string RevisionText { get => revisionText; set => revisionText = value; }
        public string RevisionEffectiveDateSeparator { get => revisionEffectiveDateSeparator; set => revisionEffectiveDateSeparator = value; }
        public Regex EffectiveDateRegex { get => effectiveDateRegex; set => effectiveDateRegex = value; }
        public int LogoHeight { get => logoHeight; set => logoHeight = value; }

        public void Initialize()
        {
            this.EffectiveDateText = "Effective Date: ";
            this.RevisionText = "Revision: ";
            this.RevisionEffectiveDateSeparator = "\r\n";
            this.LogoHeight = 28;

        }
        #region HeaderFooter


        #endregion
    }
}
