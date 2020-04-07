using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs
{
    public class WordDocConfig: QmsDocBaseConfig
    {
        int headerFooterSection;
        string effectiveDateText;
        int effectiveDateRow;
        int effectiveDateCol;
        string revisionText;
        int revisionRow;
        int revisionCol;
        int logoRow;
        int logoCol;

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

        #endregion
        public override void Initialize()
        {
            //Header
            this.HeaderFooterSection = 1;
            this.EffectiveDateText = "Effective Date: ";
            this.EffectiveDateRow = 2;
            this.EffectiveDateCol = 2;
            this.RevisionText = "Rev. ";
            this.RevisionRow = 2;
            this.RevisionCol = 3;
        }
    }
}
