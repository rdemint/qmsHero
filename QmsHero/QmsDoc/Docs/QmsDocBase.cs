using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs

{
    public class QmsDocBase
    {
        QmsDocBaseConfig docConfig;
        string revision;
        string effectiveDate;
        string logoPath;
        string logoText;

        public virtual QmsDocBaseConfig Config { get => docConfig; set => docConfig = value; }
        
        public virtual string Revision { get => revision; set => revision = value; }
        public virtual string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public virtual string LogoPath { get => logoPath; set => logoPath = value; }
        public virtual string LogoText { get => logoText; set => logoText = value; }

        public QmsDocBase()
        {
                        
        }

        public virtual void Process(DocState docEdit)
        {
            throw new NotImplementedException();
        }

        public DocState Inspect() {
            throw new NotImplementedException();
        }


        public virtual void SaveAsPdf()
        { throw new NotImplementedException(); }


    }
}
