
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using Directory = System.IO.Directory;
using DirectoryInfo = System.IO.DirectoryInfo;
using FileInfo = System.IO.FileInfo;

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
   
        public FileCopyManager(DirectoryInfo referenceDir, DirectoryInfo processingDir): base()
        {
            ReferenceDir = referenceDir;
            ProcessingDir = processingDir;
        }

         public DirectoryInfo ReferenceDir
        {
            get => referenceDir;
            set
            {
                referenceDir = value;
                UpdateFiles();

            }
        }
        public List<FileInfo> ReferenceFiles { get => referenceFiles;}
    
        public DirectoryInfo ProcessingDir { 
            get => processingDir;
            set
            {
                processingDir = value;
                UpdateFiles();
            }
            }
        public List<FileInfo> ProcessingFiles { get => processingFiles; }

        public virtual void UpdateFiles()
        {
            if(IsReadyToCopy())
            {
                FileUtil.DirectoryCopy(ReferenceDir, ProcessingDir, true);
            }
        }
        public virtual FileInfo Copy(FileInfo file)
        {
            FileInfo fileCopy = FileUtil.FileCopy(file, ProcessingDir, true);
            UpdateFiles();
            return fileCopy;
        }
        
        public virtual bool IsReadyToCopy()
        {
            if (ReferenceDir != null &&
                ProcessingDir != null &&
                ReferenceDir.Exists &&
                ProcessingDir.Exists &&
                ReferenceFiles?.Count >= 1
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
