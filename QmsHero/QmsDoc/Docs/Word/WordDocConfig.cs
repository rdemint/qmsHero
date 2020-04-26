using QDoc.Interfaces;
using QmsDoc.Docs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Word
{
    public class WordDocConfig: DocConfig
    {
        int headerFooterSection;
        string effectiveDateText;
        int effectiveDateRow;
        int effectiveDateCol;
        Regex effectiveDateRegex;
        string revisionText;
        int revisionRow;
        int revisionCol;
        Regex revisionRegex;
        int logoRow;
        int logoCol;
        int logoHeight;
        int headerNameRow;
        int headerNameCol;
        string headerNameText;
        

        public WordDocConfig():base()
        {
            this.Initialize();
        }

        #region Header
        public int HeaderFooterSection { get => headerFooterSection; set => headerFooterSection = value; }
        public int HeaderEffectiveDateRow { get => effectiveDateRow; set => effectiveDateRow = value; }
        public int HeaderEffectiveDateCol { get => effectiveDateCol; set => effectiveDateCol = value; }
        public int HeaderRevisionCol { get => revisionCol; set => revisionCol = value; }
        public int HeaderRevisionRow { get => revisionRow; set => revisionRow = value; }
        public int HeaderLogoRow { get => logoRow; set => logoRow = value; }
        public int HeaderLogoCol { get => logoCol; set => logoCol = value; }
        public string HeaderEffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string HeaderRevisionText { get => revisionText; set => revisionText = value; }
        public Regex HeaderEffectiveDateRegex { 
            get => effectiveDateRegex; 
            set => effectiveDateRegex = value; }
        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public Regex HeaderRevisionRegex { get => revisionRegex; set => revisionRegex = value; }
        
        public int HeaderNameRow { get => headerNameRow; set => headerNameRow = value; }
        public int HeaderNameCol { get => headerNameCol; set => headerNameCol = value; }
        public string HeaderNameText { get => headerNameText; set => headerNameText = value; }

        #endregion
        public void Initialize()
        {
            this.HeaderFooterSection = 1;
            this.HeaderEffectiveDateText = "Effective Date: ";
            this.HeaderEffectiveDateRow = 1;
            this.HeaderEffectiveDateCol = 1;
            this.HeaderEffectiveDateRegex = new Regex(@"\d\d\d\d-\d\d-\d\d");
            this.HeaderRevisionText = "Rev. ";
            this.HeaderRevisionRow = 1;
            this.HeaderRevisionRegex = new Regex(@"\d{1,2}");
            this.HeaderRevisionCol = 2;
            //this.LogoHeight = 28;
            this.HeaderLogoCol = 0;
            this.HeaderLogoRow = 0;
            this.HeaderNameCol = 1;
            this.HeaderNameRow = 0;
            this.HeaderNameText = "DOCUMENT NAME: ";
        }

    }
}
