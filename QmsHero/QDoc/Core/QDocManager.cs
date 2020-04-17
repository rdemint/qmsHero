﻿using System;
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
    public class QDocManager : IQDocManager, IDisposable
    {
        DirectoryInfo dir;
        DirectoryInfo processingDir;
        List<FileInfo> dirFiles;
        List<FileInfo> processingDirFiles;
        bool disposed = false;

        QDocManagerConfig docManagerConfig;

        public QDocManager()
        {
           
            this.docManagerConfig = new QDocManagerConfig();
            this.DirFiles = new List<FileInfo>();
            this.ProcessingDirFiles = new List<FileInfo>();
        }
        
        #region Properties
        public QDocManagerConfig DocManagerConfig { get => docManagerConfig; set => docManagerConfig = value; }
        public List<FileInfo> DirFiles {
            get => this.dirFiles;
            set => this.dirFiles = value;
        }
        public DirectoryInfo Dir { get => dir; set => dir = value; }

        public List<FileInfo> ProcessingDirFiles { 
            get => this.processingDirFiles;
            set => this.processingDirFiles = value;
            }

        public DirectoryInfo ProcessingDir { 
            get => processingDir; 
            set => processingDir = value; }
        

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
            if(this.DirIsValid(this.Dir.FullName))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void ConfigDir(string dirPath, string processingDirName="Processing")
        {
            this.Dir = new DirectoryInfo(dirPath);
            this.ProcessingDir = FileUtil.DirectoryCopy(this.Dir, processingDirName, true);
            this.DirFiles = this.Dir.GetFiles("*", SearchOption.AllDirectories).ToList();
            this.ProcessingDirFiles = this.ProcessingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
        }


        private DirectoryInfo DirectoryCopy(DirectoryInfo dir, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var destDirPath = Path.Combine(dir.Parent.FullName, destDirName);
            var dirCopy = new DirectoryInfo(destDirPath);
            if(dirCopy.Exists)
            {
                //Delete current processing directory, if one exists
                dirCopy.Delete(true);
            }

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + dir.FullName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirPath))
            {
                Directory.CreateDirectory(destDirPath);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            if (files.Length >= 30)
            {
                throw new Exception("The number of files is conspicuously large.  An error has been thrown to ensure the directory is correct.");
            }

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirPath, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirPath, subdir.Name);
                    DirectoryCopy(subdir, temppath, copySubDirs);
                }
            }

            return new DirectoryInfo(destDirPath);
        }

        public void ProcessFiles(QDocState docEdit) 
        {
            Contract.Requires(this.CanProcessFiles() == true);

            foreach (FileInfo file in this.ProcessingDirFiles)
            {
                ProcessDoc(file, docEdit);   
            }
        }

        #endregion
    }

}
