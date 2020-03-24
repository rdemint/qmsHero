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
        public QmsDocBaseConfig Config { get => config; set => config = value; }
        public object Doc { get => doc; set => doc = value; }

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
