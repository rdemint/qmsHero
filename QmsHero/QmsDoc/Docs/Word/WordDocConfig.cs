using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Word
{
    public class WordDocConfig: IDocConfig
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
        Regex isSopRegex;
        Regex isFormRegex;

        public WordDocConfig():base()
        {
            this.Initialize();
        }

        #region Header
        public int HeaderFooterSection { get => headerFooterSection; set => headerFooterSection = value; }
        public int EffectiveDateRow { get => effectiveDateRow; set => effectiveDateRow = value; }
        public int EffectiveDateCol { get => effectiveDateCol; set => effectiveDateCol = value; }
        public int RevisionCol { get => revisionCol; set => revisionCol = value; }
        public int RevisionRow { get => revisionRow; set => revisionRow = value; }
        public int LogoRow { get => logoRow; set => logoRow = value; }
        public int LogoCol { get => logoCol; set => logoCol = value; }
        public string EffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string RevisionText { get => revisionText; set => revisionText = value; }
        public Regex EffectiveDateRegex { 
            get => effectiveDateRegex; 
            set => effectiveDateRegex = value; }
        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public Regex RevisionRegex { get => revisionRegex; set => revisionRegex = value; }
        public Regex IsSopRegex { get => isSopRegex; set => isSopRegex = value; }
        public Regex IsFormRegex { get => isFormRegex; set => isFormRegex = value; }
        public int HeaderNameRow { get => headerNameRow; set => headerNameRow = value; }
        public int HeaderNameCol { get => headerNameCol; set => headerNameCol = value; }
        public string HeaderNameText { get => headerNameText; set => headerNameText = value; }

        #endregion
        public void Initialize()
        {
            //Header
            this.HeaderFooterSection = 1;
            this.EffectiveDateText = "Effective Date: ";
            this.EffectiveDateRow = 1;
            this.EffectiveDateCol = 1;
            this.EffectiveDateRegex = new Regex(@"\d\d\d\d-\d\d-\d\d");
            this.RevisionText = "Rev. ";
            this.RevisionRow = 1;
            this.RevisionRegex = new Regex(@"\d{1,2}");
            this.RevisionCol = 2;
            //this.LogoHeight = 28;
            this.LogoCol = 0;
            this.LogoRow = 0;
            this.IsSopRegex = new Regex(@"SOP-");
            this.IsFormRegex = new Regex(@"F-");
            this.HeaderNameCol = 1;
            this.HeaderNameRow = 0;
            this.HeaderNameText = "DOCUMENT NAME: ";
        }

    }
}
