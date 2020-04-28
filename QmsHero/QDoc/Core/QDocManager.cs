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
            var docResult = DocFactory.CreateDoc(file);
            if (docResult.IsSuccess)
                return docResult.Value.Process(docProp);
            else
            {
                return Results.Fail(new Error("Could not process file"));
            }
        }

        public virtual DocCollection Process(QDocPropertyCollection docPropCollection)
        {
            DocCollection docCollection = new DocCollection();
            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var docResult = this.DocFactory.CreateDoc(file);
                if (docResult.IsSuccess)
                {
                    var doc = docResult.Value;
                    doc.PropertiesCollection = doc.Process(docPropCollection);
                    docCollection.Add(doc);
                }
            }

            return docCollection;
        }


        public virtual DocCollection Process(QDocProperty docProp)
        {
            DocCollection docCollection = new DocCollection();

            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var docResult = this.DocFactory.CreateDoc(file);
                if(docResult.IsSuccess)
                {
                    Result<QDocProperty> result = docResult.Value.Process(docProp);
                    docResult.Value.PropertiesCollection.Add(result);
                    docCollection.Add(docResult.Value);

                }
            }
            return docCollection;
        }

        public DocCollection Inspect(QDocPropertyCollection docPropCollection)
        {
            DocCollection docCollection = new DocCollection();
            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var docResult = this.DocFactory.CreateDoc(file);
                if (docResult.IsSuccess)
                {
                    var doc = docResult.Value;
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
                var docResult = this.DocFactory.CreateDoc(file);
                if(docResult.IsSuccess)
                {
                    var doc = docResult.Value;
                    Result<QDocProperty> result = doc.Inspect(docProp);
                    doc.PropertiesCollection.Add(result);
                    docCollection.Add(doc);
                }
            }
            return docCollection;
        }

        public virtual DocCollection ToUnprocessedDocCollection()
        {

            var docs = new DocCollection();
            foreach(var file in fileManager.ProcessingFiles)
            {
                var docResult = docFactory.CreateDoc(file);
                if(docResult.IsSuccess)
                {
                   docs.Add(docResult.Value);
                }
            }

            return docs;
        }

        #endregion
    }

}
