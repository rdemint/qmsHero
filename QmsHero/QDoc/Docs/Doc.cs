
using System;
using QDoc.Interfaces;
using System.IO;
using QDoc.Core;
using QFileUtil;
using System.Collections.Generic;
using FluentResults;

namespace QDoc.Docs
{
    public abstract class Doc
    {
        FileInfo fileInfo;
        IDocConfig docConfig;
        QDocPropertyResultCollection results;

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
        public QDocPropertyResultCollection PropertiesCollection { get => results; set => results = value; }
        #endregion

        public virtual QDocPropertyResultCollection Process(QDocPropertyCollection docState)
        {
            QDocPropertyResultCollection collection = new QDocPropertyResultCollection();
            foreach (QDocProperty prop in docState)
            {
                collection.Add(Process(prop));
            }
            return collection;
        }

        public abstract Result<QDocProperty> Process(QDocProperty prop);

        public virtual QDocPropertyResultCollection Inspect(QDocPropertyCollection docState)
        {
            QDocPropertyResultCollection collection = new QDocPropertyResultCollection();
            foreach (QDocProperty prop in docState)
            {
                collection.Add(Inspect(prop));
            }
            return collection;
        }

        public abstract Result<QDocProperty> Inspect(QDocProperty prop);


    }
}
