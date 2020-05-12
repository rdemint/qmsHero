
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using Directory = System.IO.Directory;
using DirectoryInfo = System.IO.DirectoryInfo;
using FileInfo = System.IO.FileInfo;
using QFileUtil.Exceptions;

namespace QFileUtil
{
    public class FileCopyManager : IFileCopyManager
    {
        //Provids blueprint functionality to create and clean a Fixture directory
        //within the current or specified directory.
        //Use in conjunction with Types that have functionality to copy and edit files
        DirectoryInfo processingDir;
        DirectoryInfo referenceDir;
        List<FileInfo> referenceFiles;
        List<FileInfo> processingFiles;

        public FileCopyManager()
        {
            referenceFiles = new List<FileInfo>();
            processingFiles = new List<FileInfo>();
        }
   
        public FileCopyManager(DirectoryInfo referenceDir, DirectoryInfo processingDir): this()
        {
            SetReferenceDir(referenceDir);
            SetProcessingDir(processingDir);
        }

         public DirectoryInfo ReferenceDir
        {
            get => referenceDir;

        }
        public List<FileInfo> ReferenceFiles { get => referenceFiles;}
    
        public DirectoryInfo ProcessingDir { 
            get => processingDir;
            }
        public List<FileInfo> ProcessingFiles { get => processingFiles; }

        
        public virtual int UpdateProcessingDirFilesIfNecessary()
        {

            if (
                ReferenceDirAndProcessingDirAreNotNullandExist() &&
                referenceDir.FullName != processingDir.FullName &&
                referenceFiles.Count > 0
                )
                {
                    CleanProcessingDir();
                    FileUtil.DirectoryCopy(ReferenceDir.FullName, ProcessingDir.FullName, true);
                    var refFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                    //referenceFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                    processingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();

                return processingFiles.Count;
                }
            else
            {
                return -1;
            }


        }
       
        
       
        public virtual FileInfo CopyToProcessingDir(FileInfo file)
        {
            FileInfo fileCopy = FileUtil.FileCopy(file, ProcessingDir, true);
            UpdateProcessingDirFilesIfNecessary();
            return fileCopy;
        }
        
        
        public int SetReferenceDir(string path)
        {
            var dir = new DirectoryInfo(path);
            return SetReferenceDir(dir);

        }

        public int SetReferenceDir(DirectoryInfo dir)
        {
            this.referenceDir = dir;
            this.referenceFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
            
                UpdateProcessingDirFilesIfNecessary();
        
            return this.ReferenceFiles.Count;
        }


        public int SetProcessingDir(string path)
        {
            var dir = new DirectoryInfo(path);
            return SetProcessingDir(dir);
        }

        public int SetProcessingDir(DirectoryInfo dir)
        {
            this.processingDir = dir;
            if (ReferenceDirAndProcessingDirAreNotNullandExist())
            {
                CleanProcessingDir();
                FileUtil.DirectoryCopy(ReferenceDir.FullName, ProcessingDir.FullName, true);
                UpdateProcessingDirFilesIfNecessary();
            }
            UpdateProcessingDirFilesIfNecessary();
            return this.ProcessingFiles.Count;
        }
        
        public virtual bool ReferenceDirAndProcessingDirAreNotNullandExist()
        {
            if (ReferenceDir != null &&
                ProcessingDir != null &&
                ReferenceDir.Exists &&
                ProcessingDir.Exists
                )
            {
                return true;
            }
            else { return false; }
        }

        public virtual bool ProcessingDirIsClean()
        {
            if (
                ProcessingDir != null &&
                ProcessingDir.Exists &&
                ProcessingFiles?.Count == 0
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual int CleanProcessingDir()
        {
            var count = FileUtil.CleanDirectoryAndChildren(ProcessingDir);
            return count;
        }

    }
}
