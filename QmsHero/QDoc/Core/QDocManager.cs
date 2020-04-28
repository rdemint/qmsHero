using System;
using Directory = System.IO.Directory;
using FileInfo = System.IO.FileInfo;
using System.Collections.Generic;
using Contract = System.Diagnostics.Contracts.Contract;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Docs;
using QDoc.Interfaces;
using System.IO;
using QFileUtil;
using FluentResults;

namespace QDoc.Core
{
    public abstract class QDocManager
    {
        IQDocManagerConfig docManagerConfig;
        IFileCopyManager fileManager;
        QDocFactory docFactory;
        public QDocManager()
        {
            docManagerConfig = new QDocManagerConfig();
            fileManager = new FileCopyManager();
        }

        #region Properties
        public IQDocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public QDocFactory DocFactory { get => docFactory; set => docFactory = value; }
        public IFileCopyManager FileManager { get => fileManager; set => fileManager = value; }

        #endregion


        #region Methods

        public bool DirIsValid(string path)
        {
            if (path != null && path.Length > this.DocManagerConfig.SafeProcessingLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanProcessFiles()
        {
            return this.fileManager.IsReadyToCopy();
        }

        public virtual Result<QDocProperty> Process(FileInfo file, QDocProperty docProp)
        {
            var doc = DocFactory.CreateDoc(file);
            return doc.Process(docProp);
        }

        public virtual DocCollection Process(QDocPropertyCollection docPropCollection)
        {
            DocCollection docCollection = new DocCollection();
            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var doc = this.DocFactory.CreateDoc(file);
                var result = doc?.Process(docPropCollection);
                doc.PropertiesCollection = result;
                docCollection.Add(doc);
            }

            return docCollection;
        }


        public virtual DocCollection Process(QDocProperty docProp)
        {
            DocCollection docCollection = new DocCollection();

            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var doc = this.DocFactory.CreateDoc(file);
                Result<QDocProperty> result = doc?.Process(docProp);
                doc.PropertiesCollection.Add(result);
                docCollection.Add(doc);
            }
            return docCollection;
        }

        public DocCollection Inspect(QDocPropertyCollection docPropCollection)
        {
            DocCollection docCollection = new DocCollection();
            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var doc = this.DocFactory.CreateDoc(file);
                if (doc != null)
                {
                    foreach (var prop in docPropCollection)
                    {
                        var propResult = doc.Inspect(prop);
                        doc.PropertiesCollection.Add(propResult);
                    }
                    docCollection.Add(doc);
                }
            }
            return docCollection;
        }

        public DocCollection Inspect(QDocProperty docProp)
        {
            DocCollection docCollection = new DocCollection();

            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var doc = this.DocFactory.CreateDoc(file);
                Result<QDocProperty> result = doc?.Inspect(docProp);
                doc.PropertiesCollection.Add(result);
                docCollection.Add(doc);
            }
            return docCollection;
        }

        public virtual DocCollection ToUnprocessedDocCollection()
        {

            var docs = new DocCollection();
            foreach(var file in fileManager.ProcessingFiles)
            {
                var doc = docFactory.CreateDoc(file);
                if(doc !=null)
                {
                    docs.Add(doc);
                }
            }

            return docs;
        }

        #endregion
    }

}
