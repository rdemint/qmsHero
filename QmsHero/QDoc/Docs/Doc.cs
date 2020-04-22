using System;
using QDoc.Interfaces;
using System.IO;
using QDoc.Core;
using QFileUtil;

namespace QDoc.Docs
{
    public abstract class Doc : IDoc
    {
        FileInfo fileInfo;
        IDocConfig docConfig;


        public Doc()
        {

        }

        public Doc(System.IO.FileInfo fileInfo)
        {
            this.FileInfo = fileInfo;
        }

        public Doc(FileInfo fileInfo, IDocConfig docConfig)
        {
            this.FileInfo = fileInfo;
            this.DocConfig = docConfig;
        }

        #region Config
        public FileInfo FileInfo { 
            get => fileInfo;
            set { fileInfo = value; } }

        public IDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        #endregion

        public virtual void Process(IDocState docState)
        {
           var docProps = docState.ToCollection();
           foreach (QDocProperty docProp in docProps)
                {
                    Process(docProp);
                }
        }

        public abstract void Process(QDocProperty prop);



        public virtual IDocState Inspect(IDocState docState)
        {
            throw new NotImplementedException();
        }

        public virtual QDocProperty Inspect(QDocProperty prop, FileInfo file)
        {
            throw new NotImplementedException();
        }


        public abstract QDocProperty Inspect(QDocProperty prop);
        
    }
}
