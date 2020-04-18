using System;
using QDoc.Interfaces;
using System.IO;
using QDoc.Core;
using QFileUtil;

namespace QDoc.Docs
{
    public class Doc : IDoc
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

        public virtual IDoc Process(IDocState docState, DirectoryInfo targetDir)
        {
            var targetFile = FileUtil.FileCopy(this.FileInfo, targetDir);
            var targetDoc = new Doc(targetFile);
            targetDoc.Process(docState);
            return targetDoc;
        }

        public virtual void Process(IDocState docState)
        {
           var docProps = docState.ToCollection();
           foreach (QDocProperty docProp in docProps)
                {
                    Process(docProp);
                }
        }

        public IDoc Process(QDocProperty prop, DirectoryInfo targetDir)
        {
            var targetFile = FileUtil.FileCopy(this.FileInfo, targetDir);
            var targetDoc = new Doc(targetFile);
            targetDoc.Process(prop);
            return targetDoc;
        }
        public virtual void Process(QDocProperty prop)
        {
            throw new NotImplementedException();
        }


        public virtual IDocState Inspect(IDocState docState)
        {
            throw new NotImplementedException();
        }
            
        public virtual QDocProperty Inspect(QDocProperty prop)
        {
            throw new NotImplementedException();
        }
    }
}
