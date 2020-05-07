

using QDoc.Interfaces;
using QmsDoc.Docs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Excel
{
    public class ExcelDocConfig: DocConfig
    {
        string effectiveDateText;
        Regex effectiveDateRegex;
        string headerRevisionText;
        Regex revisionRegex;
        int logoHeight;
        string headerNameText;
        string headerNumber;
        Regex headerNameRegex;
        Regex headerNumberRegexWithContext;
        Regex headerNumberRegex;


        public ExcelDocConfig(): base()
        {
            this.Initialize();
        }

        public string HeaderEffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string HeaderRevisionText { get => headerRevisionText; set => headerRevisionText = value; }
        public Regex HeaderRevisionRegex { get => revisionRegex; set => revisionRegex = value; }
        public Regex HeaderEffectiveDateRegex { get => effectiveDateRegex; set => effectiveDateRegex = value; }
        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public Regex HeaderNameRegex { get => headerNameRegex; set => headerNameRegex = value; }
        public string HeaderNameText { 
            get => headerNameText;
            set
            {
                headerNameText = value;
            }
            }

        public string HeaderNumber { get => headerNumber; set => headerNumber = value; }
        public Regex HeaderNameRegexWithContext { get => headerNumberRegexWithContext; set => headerNumberRegexWithContext = value; }

        public void Initialize()
        {
            this.HeaderEffectiveDateText = "Effective Date: ";
            this.HeaderEffectiveDateRegex = new Regex(@"\d\d\d\d-\d\d-\d\d");
            this.HeaderRevisionText = "Revision: ";
            this.HeaderRevisionRegex = new Regex(@"(?<=\:)\s*\d{1,2}(?=\s*\Z)");
            this.HeaderNameText = "DOCUMENT NAME: ";
            this.headerNameRegex = new Regex(@"(?<=\: )(.*?)(?= \()");
            this.headerNumberRegexWithContext = new Regex(@"(?<=\: )(.*?)(?= \()");
        }
    }
}
