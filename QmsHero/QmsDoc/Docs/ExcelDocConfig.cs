using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs
{
    public class ExcelDocConfig: QmsDocBaseConfig
    {
        string effectiveDateText;
        string revisionText;
        string revisionEffectiveDateSeparator;

        public ExcelDocConfig():base()
        {
            this.Initialize();
        }

        public string EffectiveDateText { get => effectiveDateText; set => effectiveDateText = value; }
        public string RevisionText { get => revisionText; set => revisionText = value; }
        public string RevisionEffectiveDateSeparator { get => revisionEffectiveDateSeparator; set => revisionEffectiveDateSeparator = value; }

        public override void Initialize()
        {
            base.Initialize();
            this.EffectiveDateText = "Effective Date: ";
            this.RevisionText = "Revision: ";
            this.revisionEffectiveDateSeparator = "\r\n";

        }
        #region HeaderFooter


        #endregion
    }
}
