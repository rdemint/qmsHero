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
        QFileChangeInspector fileChangeInspector;


        public QDocManager()
        {
            docManagerConfig = new QDocManagerConfig();
            fileManager = new FileCopyManager();
            fileChangeInspector = new QFileChangeInspector();
        }

        #region Properties
        public IQDocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public QDocFactory DocFactory { get => docFactory; set => docFactory = value; }
        public IFileCopyManager FileManager { get => fileManager; set => fileManager = value; }
        internal QFileChangeInspector FileChangeInspector { get => fileChangeInspector; set => fileChangeInspector = value; }

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
            return this.fileManager.ReferenceDirAndProcessingDirAreNotNullandExist();
        }

        
        
        //private void UpdateModifiedAndProcessingErrors(Doc doc, byte[] initialHash, bool hasErrors)
        //{
        //    doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);
        //    doc.HasProcessingErrors = doc.InspectForPropertyProcessingErrors();

        //}
        
        public virtual Result<QDocProperty> Process(FileInfo file, QDocProperty docProp)
        {
            Result<QDocProperty> docPropResult;
            var docResult = DocFactory.CreateDoc(file);
            if (docResult.IsSuccess)
            {
                var doc = docResult.Value;
                var initialHash = fileChangeInspector.GetFileHash(doc.FileInfo);
                docPropResult = docResult.Value.Process(docProp);
                doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);
                doc.UpdatePropertyProcessingErrors();
            }
            else
            {
                return Results.Fail(new Error("Could not process file"));
            }
            return docPropResult;
        }


        public virtual DocCollection Process(QDocActionManager docActionManager)
        {
            DocCollection docCollection = new DocCollection();
            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var docResult = this.DocFactory.CreateDoc(file);
                if (docResult.IsSuccess)
                {
                    var doc = docResult.Value;
                    var initialHash = fileChangeInspector.GetFileHash(doc.FileInfo);
                    doc.PropertyResultCollection = doc.Process(docActionManager);
                    doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);
                    doc.UpdatePropertyProcessingErrors();

                    docCollection.Add(doc);
                }
            }
            return docCollection;
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
                    var initialHash = fileChangeInspector.GetFileHash(doc.FileInfo);
                    doc.PropertyResultCollection = doc.Process(docPropCollection);
                    doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);
                    doc.UpdatePropertyProcessingErrors();

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
                    var doc = docResult.Value;
                    var initialHash = fileChangeInspector.GetFileHash(doc.FileInfo);
                    Result<QDocProperty> result = doc.Process(docProp);
                    doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);

                    docResult.Value.PropertyResultCollection.Add(result);
                    doc.UpdatePropertyProcessingErrors();
                    docCollection.Add(docResult.Value);

                }
            }
            return docCollection;
        }

        public virtual DocCollection Inspect(QDocPropertyCollection docPropCollection)
        {
            DocCollection docCollection = new DocCollection();
            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var docResult = this.DocFactory.CreateDoc(file);
                if (docResult.IsSuccess)
                {
                    var doc = docResult.Value;
                    var initialHash = fileChangeInspector.GetFileHash(doc.FileInfo);

                    doc.PropertyResultCollection = doc.Inspect(docPropCollection);
                    doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);
                    doc.UpdatePropertyProcessingErrors();

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
                    var initialHash = fileChangeInspector.GetFileHash(doc.FileInfo);

                    Result<QDocProperty> result = doc.Inspect(docProp);
                    doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);

                    doc.PropertyResultCollection.Add(result);
                    doc.UpdatePropertyProcessingErrors();

                    docCollection.Add(doc);
                }
            }

            return docCollection;
        }

        public DocCollection Inspect(QDocActionManager actionManager)
        {
            DocCollection docCollection = new DocCollection();

            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var docResult = this.DocFactory.CreateDoc(file);
                if (docResult.IsSuccess)
                {
                    var doc = docResult.Value;
                    var initialHash = fileChangeInspector.GetFileHash(doc.FileInfo);

                    var resultCollection = doc.Inspect(actionManager);
                    doc.IsModified = fileChangeInspector.FileHashHasChanged(initialHash, doc.FileInfo);

                    doc.PropertyResultCollection = resultCollection;
                    doc.UpdatePropertyProcessingErrors();

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
