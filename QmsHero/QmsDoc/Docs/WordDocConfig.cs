
using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDoc.Docs
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
        int logoRow;
        int logoCol;
        string qWordDocAssembly = "QWordDoc";
        string qWordDocNamespace = "QWordDoc";

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
            this.RevisionCol = 2;
        }

        public string PropertyReferenceName(string docPropertyName)
        {
            return qWordDocAssembly + "." + docPropertyName + ", " + qWordDocNamespace;
        }
    }
}
