using QDoc.Docs;
using QDoc.Interfaces;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs
{
    public abstract class QmsDocBase: Doc
    {
        public QmsDocBase()
        {
        }

        public QmsDocBase(FileInfo fileInfo) : base(fileInfo)
        {
        }

        public QmsDocBase(FileInfo fileInfo, IDocConfig docConfig) : base(fileInfo, docConfig)
        {
        }

        public new void Process(IDocState docState, DirectoryInfo targetDir)
        {
            var docProps = docState.ToCollection();
            foreach(DocProperty prop in docProps)
            {
                throw new NotImplementedException();
            }
        }
        
        public new void Process(IDocState docState)
        {
            var docProps = docState.ToCollection();
            foreach (DocProperty docProp in docProps)
            {
                Process(docProp);
            }
        }

        public virtual void Process(DocProperty docProp)
        {
            throw new NotImplementedException();
        }

        public new IDocState Inspect(IDocState docState)
        {
            throw new NotImplementedException();
        }


        public virtual DocProperty Inspect(DocProperty docProp)
        {
            throw new NotImplementedException();
        }

    }
}
