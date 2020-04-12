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
        
        public virtual string GetRevision() { throw new NotImplementedException(); }
        public virtual string Revision { get => revision; set => revision = value; }
        public virtual string GetEffectiveDate() { throw new NotImplementedException(); }
        public virtual string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public virtual string LogoPath { get => logoPath; set => logoPath = value; }
        public virtual string LogoText { get => logoText; set => logoText = value; }

        public QmsDocBase()
        {
                        
        }

        public void Process(DocState docEdit)
        {
            var docProps = docEdit.ToCollection();
            foreach (DocProperty docProp in docProps)
            {
                var propertyInfo = this.GetType().GetProperty(docProp.Name);
                propertyInfo?.SetValue(this, docProp.Value);
            }
        }

        public DocState Inspect() {
            var state = new DocState();
            state.DocHeader.Revision.Value = this.GetRevision();
            state.DocHeader.EffectiveDate.Value = this.GetEffectiveDate();
            return state;


        }


        public virtual void SaveAsPdf()
        { throw new NotImplementedException(); }


    }
}
