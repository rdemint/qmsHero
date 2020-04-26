

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
        Regex revisionRegex;
        int logoHeight;
        string headerNameText;
        Regex headerNameRegex;

        public ExcelDocConfig()
        {
            this.Initialize();
        }

        public string HeaderEffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string HeaderRevisionText { get => revisionText; set => revisionText = value; }
        public Regex HeaderRevisionRegex { get => revisionRegex; set => revisionRegex = value; }
        public Regex HeaderEffectiveDateRegex { get => effectiveDateRegex; set => effectiveDateRegex = value; }
        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public Regex HeaderNameRegex { get => headerNameRegex; set => headerNameRegex = value; }
        public string HeaderNameText { get => headerNameText; set => headerNameText = value; }

        public void Initialize()
        {
            this.HeaderEffectiveDateText = "Effective Date: ";
            this.HeaderEffectiveDateRegex = new Regex(@"\d\d\d\d-\d\d-\d\d");
            this.HeaderRevisionText = "Revision: ";
            this.HeaderRevisionRegex = new Regex(Regex.Escape(this.HeaderRevisionText) + @"\d{1,2}");
            this.HeaderNameText = "DOCUMENT NAME: ";
            this.HeaderNameRegex = new Regex(Regex.Escape(this.HeaderNameText) + @".*\)");



        }
        #region HeaderFooter


        #endregion
    }
}
