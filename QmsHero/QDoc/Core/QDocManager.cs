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

namespace QDoc.Core
{
    public abstract class QDocManager: IQDocManager
    {
        IQDocManagerConfig docManagerConfig;
        IFileCopyManager fileManager;
        IQDocFactory docFactory;
        public QDocManager()
        {
            docManagerConfig = new QDocManagerConfig();
            fileManager = new FileCopyManager();
        }

        #region Properties
        public IQDocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public IQDocFactory DocFactory { get => docFactory; set => docFactory = value; }
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

        public virtual void Process(FileInfo file, QDocProperty docProp)
        {
            var doc = DocFactory.CreateDoc(file);
            doc.Process(docProp);
        }

        public abstract void Process(QDocPropertyCollection docState);


        public abstract void Process(QDocProperty docProp);

        public DocCollection DocCollection()
        {

            var docs = new DocCollection();
            foreach(var file in fileManager.ProcessingFiles)
            {
                docs.Add(docFactory.CreateDoc(file));
            }

            return docs;
        }

        #endregion
    }

}
