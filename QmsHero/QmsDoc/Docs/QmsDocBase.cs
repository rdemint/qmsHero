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

        public QmsDocBaseConfig Config { get => config; set => config = value; }

        public QmsDocBase()
        {
            
        }
        public virtual int CloseDocument()
        {
            throw new System.NotImplementedException();
        }
        public virtual int OpenDocument()
        {
            throw new System.NotImplementedException();
        }

        public virtual void SaveAsPdf()
        { throw new NotImplementedException(); }

        public object GetConfig([CallerMemberName] string propName="")
        {

        }
    }
}
