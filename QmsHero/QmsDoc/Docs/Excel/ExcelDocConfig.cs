
using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Excel
{
    public class ExcelDocConfig: IDocConfig
    {
        string effectiveDateText;
        Regex effectiveDateRegex;
        string revisionText;
        string revisionEffectiveDateSeparator;
        Regex revisionRegex;
        int logoHeight;
        Regex isSopRegex;
        Regex isFormRegex;

        public ExcelDocConfig()
        {
            this.Initialize();
        }

        public string EffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string RevisionText { get => revisionText; set => revisionText = value; }
        public string RevisionEffectiveDateSeparator { get => revisionEffectiveDateSeparator; set => revisionEffectiveDateSeparator = value; }
        public Regex RevisionRegex { get => revisionRegex; set => revisionRegex = value; }
        public Regex EffectiveDateRegex { get => effectiveDateRegex; set => effectiveDateRegex = value; }
        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public Regex IsFormRegex { get => isFormRegex; set => isFormRegex = value; }
        public Regex IsSopRegex { get => isSopRegex; set => isSopRegex = value; }

        public void Initialize()
        {
            this.EffectiveDateText = "Effective Date: ";
            this.RevisionText = "Revision: ";
            this.RevisionRegex = new Regex(Regex.Escape(this.RevisionText) + @"\d{1,2}");
            this.RevisionEffectiveDateSeparator = "\r\n";
            this.LogoHeight = 28;
            this.IsFormRegex = new Regex(@"F-");
            this.IsSopRegex = new Regex(@"SOP-");


        }
        #region HeaderFooter


        #endregion
    }
}
