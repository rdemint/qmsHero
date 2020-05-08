using QDoc.Docs;
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
        string headerEffectiveDateText;
        int headerEffectiveDateRow;
        int headerEffectiveDateCol;
        Regex headerEffectiveDateRegex;
        string headerRevisionText;
        int headerRevisionRow;
        int headerRevisionCol;
        Regex headerRevisionRegex;
        int logoRow;
        int logoCol;
        int logoHeight;
        int headerNameRow;
        int headerNameCol;
        string headerNameText;
        Regex headerNameRegex;
        string headerNumber;
        Regex headerNumberRegex;
        

        public WordDocConfig():base()
        {
            this.Initialize();
        }

        #region Header
        public int HeaderFooterSection { get => headerFooterSection; set => headerFooterSection = value; }
        public int HeaderEffectiveDateRow { get => headerEffectiveDateRow; set => headerEffectiveDateRow = value; }
        public int HeaderEffectiveDateCol { get => headerEffectiveDateCol; set => headerEffectiveDateCol = value; }
        public int HeaderRevisionCol { get => headerRevisionCol; set => headerRevisionCol = value; }
        public int HeaderRevisionRow { get => headerRevisionRow; set => headerRevisionRow = value; }
        public int HeaderLogoRow { get => logoRow; set => logoRow = value; }
        public int HeaderLogoCol { get => logoCol; set => logoCol = value; }
        public string HeaderEffectiveDateText { get => headerEffectiveDateText; set => headerEffectiveDateText = value; }
        public string HeaderRevisionText { get => headerRevisionText; set => headerRevisionText = value; }
        public Regex HeaderEffectiveDateRegex { 
            get => headerEffectiveDateRegex; 
            set => headerEffectiveDateRegex = value; }
        public int LogoHeight { get => logoHeight; set => logoHeight = value; }
        public Regex HeaderRevisionRegex { get => headerRevisionRegex; set => headerRevisionRegex = value; }
        
        public int HeaderNameRow { get => headerNameRow; set => headerNameRow = value; }
        public int HeaderNameCol { get => headerNameCol; set => headerNameCol = value; }
        public string HeaderNameText { 
            get => headerNameText;
            set
            {
                headerNameText = value;
                //this.HeaderNameRegex = GenerateHeaderNameRegex(value);
            }
            }
        public string HeaderNumber { get => headerNumber; set => headerNumber = value; }
        public Regex HeaderNumberRegex { get => headerNumberRegex; set => headerNumberRegex = value; }
        public Regex HeaderNameRegex { get => headerNameRegex; set => headerNameRegex = value; }

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
            //this.HeaderNameRegex = GenerateHeaderNameRegex(this.HeaderNameText);
            this.headerNameRegex = new Regex(@"(?<=\: )(.*?)(?= \()");



        }


    }
}
