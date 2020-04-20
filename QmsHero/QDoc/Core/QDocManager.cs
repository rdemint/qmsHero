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
    public class QDocManager
    {
        IQDocManagerConfig docManagerConfig;
        IFileCopyManager fileManager;
        public QDocManager()
        {
            docManagerConfig = new QDocManagerConfig();
            fileManager = new FileCopyManager();
        }

        #region Properties
        public IQDocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        
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

        public virtual void ProcessFiles(IDocState docState)
        {
            Contract.Requires(this.CanProcessFiles() == true);

            throw new NotImplementedException();
        }

        public virtual void ProcessFiles(QDocProperty docProp)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

}
