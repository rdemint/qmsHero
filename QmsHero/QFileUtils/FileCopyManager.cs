
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
using System.ComponentModel;

namespace QFileUtil
{
    public class FileCopyManager : INotifyPropertyChanged, IFileCopyManager
    {
        //Provids blueprint functionality to create and clean a Fixture directory
        //within the current or specified directory.
        //Use in conjunction with Types that have functionality to copy and edit files
        DirectoryInfo processingDir;
        DirectoryInfo referenceDir;
        List<FileInfo> referenceFiles;
        List<FileInfo> processingFiles;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged( String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

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
            set
            {
                referenceDir=value;
                if(referenceDir.Exists)
                {
                    referenceFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                    OnPropertyChanged("ReferenceFiles");
                }
                OnPropertyChanged("ReferenceDir");
            }

        }
        public List<FileInfo> ReferenceFiles { 
            get => referenceFiles;
            set
            {
                referenceFiles = value;
                OnPropertyChanged("ReferenceFiles");
            }
        }
    
        public DirectoryInfo ProcessingDir { 
            get => processingDir;
            set
            {
                processingDir =value;
                if(processingDir.Exists)
                {
                    
                    processingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                    OnPropertyChanged("ProcessingFiles");
                    CopyReferenceToProcessingIfPossible();

                }
                OnPropertyChanged("ProcessingDir");
            }
            }
        public List<FileInfo> ProcessingFiles { 
            get => processingFiles; 
            set
            {
                processingFiles = value;
                OnPropertyChanged("ProcessingFiles");
            }
        }

        
        public virtual Result<int> UpdateFiles()
        {
            if(referenceDir!=null && referenceDir.Exists) {
                ReferenceFiles = referenceDir.GetFiles("*", SearchOption.AllDirectories).ToList();
            }
            if(processingDir!=null && processingDir.Exists) {
                ProcessingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
            }
            return Results.Ok<int>(referenceFiles.Count);

        }
        
        public virtual Result<int> CopyReferenceToProcessingIfPossible()
        {
            //Copies the References files to the ProcessingFiles, or resets ProcessingFiles as needed
            //if (processingDir != null && !processingDir.Exists)
            //{
            //    processingFiles = new List<FileInfo>();
            //    return Results.Fail(new Error($"The directory at {processingDir.FullName} does not exist.")
            //        .CausedBy(new DirectoryDoesNotExistResultError()));
            //}

            //if(processingDir != null && processingDir.Exists)
            //{
            //    processingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
            //}

            if (
                ReferenceDirAndProcessingDirAreNotNullandExist() &&
                referenceDir.FullName != processingDir.FullName &&
                referenceFiles.Count > 0 
                )
                {
                    CleanProcessingDir();
                    FileUtil.DirectoryCopy(ReferenceDir.FullName, ProcessingDir.FullName, true);
                    ProcessingFiles = processingDir.GetFiles("*", SearchOption.AllDirectories).ToList();
                }
            return Results.Ok<int>(processingFiles.Count);


        }
       
        
       
        public virtual FileInfo CopyToProcessingDir(FileInfo file)
        {
            FileInfo fileCopy = FileUtil.FileCopy(file, ProcessingDir, true);
            CopyReferenceToProcessingIfPossible();
            return fileCopy;
        }
        
        
        public Result<int> SetReferenceDir(string path)
        {
            var dir = new DirectoryInfo(path);
            return SetReferenceDir(dir);

        }

        public Result<int> SetReferenceDir(DirectoryInfo dir)
        {
            ReferenceDir = dir;
            if(!referenceDir.Exists)
            {
                return Results.Fail(new Error("The Directory does not exist").CausedBy(new DirectoryDoesNotExistResultError()));
            }
                CopyReferenceToProcessingIfPossible();
            return Results.Ok<int>(ReferenceDir.GetFiles("*", SearchOption.AllDirectories).ToList().Count);
        }


        public Result<int> SetProcessingDir(string path)
        {
            var dir = new DirectoryInfo(path);
            return SetProcessingDir(dir);
        }

        public Result<int> SetProcessingDir(DirectoryInfo dir)
        {
           ProcessingDir = dir;
                CopyReferenceToProcessingIfPossible();
            if(ProcessingDir.Exists)
            {
                return Results.Ok<int>(ProcessingDir.GetFiles("*", SearchOption.AllDirectories).ToList().Count());
            }
            else
            {
                return Results.Fail(new Error("Directory does not exist").CausedBy(new DirectoryDoesNotExistResultError()));
            }
        }

        public Result<int> CreateProcessingDirIfDoesNotExistAndUpdateWithReferenceFilesAndNewFileCount()
        {
            if(!processingDir.Exists)
            {
                processingDir.Create();
                ProcessingDir = new DirectoryInfo(processingDir.FullName);
                return CopyReferenceToProcessingIfPossible();
            }
            else
            {
                return Results.Ok<int>(processingFiles.Count);
            }
        }


        public Result<int> MakeCurrentProcessingDirTheReferenceDirAndCreateNewProcessingDirWithTimeSuffix()
        {
            var oldProcessingDirPath = processingDir.FullName;
            var tempDir = new DirectoryInfo(Path.Combine(referenceDir.FullName, "_temp"));
            var newDirName = processingDir.Name + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            var newDirPath = Path.Combine(processingDir.Parent.FullName, newDirName);
            SetProcessingDir(newDirPath);
            SetReferenceDir(oldProcessingDirPath);
            return CreateProcessingDirIfDoesNotExistAndUpdateWithReferenceFilesAndNewFileCount();

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
