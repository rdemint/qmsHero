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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QFileUtil;

namespace QDoc.Core
{
    public class QDocManager : IDocManager
    {
        DirectoryInfo dir;
        DirectoryInfo processingDir;
        List<FileInfo> dirFiles;
        List<FileInfo> processingDirFiles;
        bool disposed = false;

        QDocManagerConfig docManagerConfig;

        public QDocManager()
        {
            this.DirFiles = new List<FileInfo>();
            this.ProcessingDirFiles = new List<FileInfo>();
        }

        #region Properties
        public QDocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public List<FileInfo> DirFiles
        {
            get => this.dirFiles;
            set => this.dirFiles = value;
        }
        public DirectoryInfo Dir { get => dir; set => dir = value; }

        public List<FileInfo> ProcessingDirFiles
        {
            get => this.processingDirFiles;
            set => this.processingDirFiles = value;
        }

        public DirectoryInfo ProcessingDir
        {
            get => processingDir;
            set => processingDir = value;
        }


        #endregion


        #region Methods

        public void DeleteProcessingDir()
        {
            this.ProcessingDir?.Delete(true);
        }

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
            if (this.DirIsValid(this.Dir.FullName))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void ConfigDir(string dirPath, string processingDirName = "Processing")
        {
            this.Dir = new DirectoryInfo(dirPath);
            this.ProcessingDir = FileUtil.DirectoryCopy(this.Dir, processingDirName, true);
            this.DirFiles = this.Dir.GetFiles("*", SearchOption.AllDirectories).ToList();
            this.ProcessingDirFiles = this.ProcessingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
        }

        public virtual void ProcessFiles(IDocState docEdit)
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
