using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs

{
    public class QmsDocBase
    {
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

    }
}
