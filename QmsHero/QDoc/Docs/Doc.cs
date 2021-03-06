﻿
using System;
using QDoc.Interfaces;
using System.IO;
using QDoc.Core;
using QFileUtil;
using System.Collections.Generic;
using FluentResults;
using System.Linq;

namespace QDoc.Docs
{
    public abstract class Doc: IDoc
    {
        FileInfo fileInfo;
        IDocConfig docConfig;
        QDocPropertyResultCollection propertyResultCollection;

        public Doc()
        {
            propertyResultCollection = new QDocPropertyResultCollection();
        }

        public Doc(System.IO.FileInfo fileInfo): this()
        {
            this.FileInfo = fileInfo;
            this.DocConfig = new DocConfig();
        }

        public Doc(FileInfo fileInfo, IDocConfig docConfig): this()
        {
            this.FileInfo = fileInfo;
            this.DocConfig = docConfig;
        }

        #region Config
        public FileInfo FileInfo { 
            get => fileInfo;
            set { fileInfo = value; } }

        public IDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public QDocPropertyResultCollection PropertyResultCollection { 
            get => propertyResultCollection;
            set
            {
                propertyResultCollection = value;
            }
        }
        
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
        public virtual QDocPropertyResultCollection Inspect(QDocPropertyCollection docState)
        {
            QDocPropertyResultCollection collection = new QDocPropertyResultCollection();
            foreach (QDocProperty prop in docState)
            {
                collection.Add(Inspect(prop));
            }
            return collection;
        }

        public abstract Result<QDocProperty> Process(QDocProperty prop);
        public abstract Result<QDocProperty> Inspect(QDocProperty prop);

        public virtual QDocPropertyResultCollection Process(QDocActionManager actionManager)
        {
            return actionManager.Process(this);
        }


        public virtual QDocPropertyResultCollection Inspect(QDocActionManager actionManager)
        {
            return actionManager.Inspect(this);
        }

        public bool HasPropertyProcessingErrors()
        {
            return propertyResultCollection.Any(prop => prop.IsFailed);
        }
        

        

        


    }
}
