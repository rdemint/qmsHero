
using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QDoc.Docs
{
    public class DocConfig: IDocConfig
    {
        public DocConfig():base()
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
        public virtual void Initialize()
        {
            throw new NotImplementedException();
        }

    }
}
