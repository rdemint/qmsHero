using System;
using QDoc.Interfaces;
using System.IO;
using QDoc.Core;
using QFileUtil;
using System.Collections.Generic;

namespace QDoc.Docs
{
    public abstract class Doc: IDoc
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

        public virtual void Process(QDocPropertyCollection docState)
        {
            foreach (QDocProperty prop in docState)
            {
                Process(prop);
            }
        }

        public abstract void Process(QDocProperty prop);

        public virtual QDocPropertyCollection Inspect(QDocPropertyCollection docState) {
            QDocPropertyCollection returnState = new QDocPropertyCollection();

            foreach (QDocProperty prop in docState)
            {
                returnState.Add(Inspect(prop));
            }
            return returnState;

        }

        public abstract QDocProperty Inspect(QDocProperty prop);


    }
}
