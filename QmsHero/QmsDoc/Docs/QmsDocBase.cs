using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs

{
    public class QmsDocBase
    {
        QmsDocBaseConfig config;
        object doc;
        string revision;
        string effectiveDate;
        string logoPath;
        string logoText;

        public virtual QmsDocBaseConfig Config { get => config; set => config = value; }
        public virtual object Doc { get => doc; set => doc = value; }
        public virtual string Revision { get => revision; set => revision = value; }
        public virtual string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public virtual string LogoPath { get => logoPath; set => logoPath = value; }
        public virtual string LogoText { get => logoText; set => logoText = value; }

        public QmsDocBase()
        {
                        
        }
        public virtual void CloseDocument()
        {
            throw new System.NotImplementedException();
        }
        public virtual void OpenDocument()
        {
            throw new System.NotImplementedException();
        }

        public virtual void SaveAsPdf()
        { throw new NotImplementedException(); }

        //private object GetConfig([CallerMemberName] string propName="")
        //{
        //    return this.Config.GetProperty(this.Doc, propName);
        //}
    }
}
