﻿
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

        
        public virtual int UpdateFiles()
        {
            if (ReferenceDirAndProcessingDirAreNotNullandExist())
            {
              var refFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                if (refFiles.Any())
                {
                    referenceFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                    processingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                }

                return referenceFiles.Count + processingFiles.Count;
            }

            return -1;

        }
        
        
        public virtual int UpdateFiles2()
        {
            if(ReferenceDirAndProcessingDirAreNotNullandExist())
            {
                
                
                var refFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                if(refFiles.Any())
                {
                    referenceFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                    processingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                }
                
                return referenceFiles.Count + processingFiles.Count;
            }
            return -1;
        }
        public virtual FileInfo CopyToProcessingDir(FileInfo file)
        {
            FileInfo fileCopy = FileUtil.FileCopy(file, ProcessingDir, true);
            UpdateFiles();
            return fileCopy;
        }
        
        
        public void SetReferenceDir(string path)
        {
            var dir = new DirectoryInfo(path);
            SetReferenceDir(dir);
        }

        public void SetReferenceDir(DirectoryInfo dir)
        {
            this.referenceDir = dir;
            if (ReferenceDirAndProcessingDirAreNotNullandExist())
            {
                CleanProcessingDir();
                FileUtil.DirectoryCopy(ReferenceDir.FullName, ProcessingDir.FullName, true);
                UpdateFiles();
            }
        }

        public void SetProcessingDir(string path)
        {
            var dir = new DirectoryInfo(path);
            SetProcessingDir(dir);
        }

        public void SetProcessingDir(DirectoryInfo dir)
        {
            this.processingDir = dir;
            if (ReferenceDirAndProcessingDirAreNotNullandExist())
            {
                CleanProcessingDir();
                FileUtil.DirectoryCopy(ReferenceDir.FullName, ProcessingDir.FullName, true);
                UpdateFiles();
            }
            UpdateFiles();
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
