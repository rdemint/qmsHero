
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using Directory = System.IO.Directory;
using DirectoryInfo = System.IO.DirectoryInfo;
using FileInfo = System.IO.FileInfo;
using QFileUtil.Exceptions;
using FluentResults;
using System.Diagnostics;

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

        
        public virtual Result<int> UpdateProcessingDirFilesIfNecessaryAndGetResultCount()
        {
            //Copies the References files to the ProcessingFiles, or resets ProcessingFiles as needed
            if (processingDir != null && !processingDir.Exists)
            {
                processingFiles = new List<FileInfo>();
                return Results.Fail(new Error($"The directory at {processingDir.FullName} does not exist.")
                    .CausedBy(new DirectoryDoesNotExistResultError()));
            }

            if (
                ReferenceDirAndProcessingDirAreNotNullandExist() &&
                referenceDir.FullName != processingDir.FullName &&
                referenceFiles.Count > 0
                )
                {
                    CleanProcessingDir();
                    FileUtil.DirectoryCopy(ReferenceDir.FullName, ProcessingDir.FullName, true);
                    var refFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                    processingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                }
            return Results.Ok<int>(processingFiles.Count);


        }
       
        
       
        public virtual FileInfo CopyToProcessingDir(FileInfo file)
        {
            FileInfo fileCopy = FileUtil.FileCopy(file, ProcessingDir, true);
            UpdateProcessingDirFilesIfNecessaryAndGetResultCount();
            return fileCopy;
        }
        
        
        public Result<int> SetReferenceDir(string path)
        {
            var dir = new DirectoryInfo(path);
            return SetReferenceDir(dir);

        }

        public Result<int> SetReferenceDir(DirectoryInfo dir)
        {
            this.referenceDir = dir;
            this.referenceFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
            UpdateProcessingDirFilesIfNecessaryAndGetResultCount();
            return Results.Ok<int>(referenceFiles.Count);
        }


        public Result<int> SetProcessingDir(string path)
        {
            var dir = new DirectoryInfo(path);
            return SetProcessingDir(dir);
        }

        public Result<int> SetProcessingDir(DirectoryInfo dir)
        {
            this.processingDir = dir;
            return UpdateProcessingDirFilesIfNecessaryAndGetResultCount();
        }

        public Result<int> CreateProcessingDirIfDoesNotExistAndUpdateWithReferenceFilesAndNewFileCount()
        {
            if(!processingDir.Exists)
            {
                processingDir.Create();
                return UpdateProcessingDirFilesIfNecessaryAndGetResultCount();
            }
            else
            {
                return Results.Ok<int>(processingFiles.Count);
            }
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
