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
        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] String propertyName = "")
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
                OnPropertyChanged();
            }

        }
        public List<FileInfo> ReferenceFiles { 
            get => referenceFiles;
        }
    
        public DirectoryInfo ProcessingDir { 
            get => processingDir;
            set
            {
                processingDir =value;
                OnPropertyChanged();
            }
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
                referenceFiles.Count > 0 && 
                referenceFiles.Count != processingFiles.Count
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
            this.ReferenceDir = dir;
            if(!referenceDir.Exists)
            {
                return Results.Fail(new Error("The Directory does not exist").CausedBy(new DirectoryDoesNotExistResultError()));
            }
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
            this.ProcessingDir = dir;
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

        public Result<int> MakeCurrentProcessingDirTheReferenceDirAndCreateNewProcessingDirWithTimeSuffix()
        {
            SetReferenceDir(processingDir.FullName);
            var newDirName = processingDir.Name + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            var newDirPath = Path.Combine(processingDir.Parent.FullName, newDirName);
            SetProcessingDir(newDirPath);
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
