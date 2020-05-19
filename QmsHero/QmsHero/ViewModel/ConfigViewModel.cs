using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Interfaces;
using QDoc.Core;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using QmsHero.Services;
using System.Security.AccessControl;
using System.IO;
using QFileUtil.Exceptions;
using FluentResults;
using MahApps.Metro.Controls.Dialogs;
using GalaSoft.MvvmLight.Command;

namespace QmsHero.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        string referenceDirPath;
        string processingDirPath;
        int processingFilesCount;
        int referenceFilesCount;
        bool dirsHaveChanged;
        DocManager manager;
        string viewDisplayName;
        IAsyncDialogService dialogService;
        RelayCommand updateDirsCommand;

        public ConfigViewModel(IAsyncDialogService asyncDialogService)
        {
            dialogService = SimpleIoc.Default.GetInstance<IAsyncDialogService>();
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.viewDisplayName = "Configure Project Directories";
            updateDirsCommand = new RelayCommand(() => UpdateDirs(), ()=> CanUpdateDirs());

            //TestData
            ReferenceDirPath = @"C:\Users\raine\Desktop\qmsProcessing\Dot Cup\QMS";
            ProcessingDirPath = @"C:\Users\raine\Desktop\qmsProcessing\Dot Cup\Processing";
        }

        private bool CanUpdateDirs()
        {
            return dirsHaveChanged;
        }

        private void UpdateDirs()
        {
            var referenceCountResult = manager.FileManager.SetReferenceDir(referenceDirPath);
            if (referenceCountResult.IsFailed)
            {
                this.ReferenceFilesCount = 0;
                EvaluateDirErrorsResult(referenceCountResult, referenceDirPath);
                //this.dialogService.ShowMessageAsync(
                //    $"Could not set the Reference Directory to {referenceDirPath}", $"{countResult.Errors.ToString()}.  It likely does not exist.");
            }
            
            var processingCountResult = manager.FileManager.SetProcessingDir(processingDirPath);
            if (processingCountResult.IsFailed)
            {
                this.ProcessingFilesCount = 0;
                EvaluateDirErrorsResult(processingCountResult, processingDirPath);

            }
            dirsHaveChanged = false;
            UpdateDirsCommand.RaiseCanExecuteChanged();
        }

        public string ReferenceDirPath
        {
            get => referenceDirPath;
            set
            {
               if(referenceDirPath!=value)
                {
                Set<string>(
                    () => ReferenceDirPath, ref referenceDirPath, value
                    );
                    var newDir = new DirectoryInfo(value);
                    if(newDir.Exists)
                    {
                        ReferenceFilesCount = newDir.GetFiles("*", SearchOption.AllDirectories).Count();
                    }

                        else
                    {
                        ReferenceFilesCount = 0;
                    }
                    dirsHaveChanged = true;
                    UpdateDirsCommand.RaiseCanExecuteChanged();
                }
            }
                
        }

        public string ProcessingDirPath
        {
            get => processingDirPath;
            set
            {
                if(processingDirPath != value)
                {
                     Set<string>(
                        () => ProcessingDirPath, ref processingDirPath, value
                        );
                        var newDir = new DirectoryInfo(value);
                        if(newDir.Exists)
                        {
                            ProcessingFilesCount = newDir.GetFiles("*", SearchOption.AllDirectories).Count();
                        }

                        else
                        {
                            this.ProcessingFilesCount = 0;
                        }
                    dirsHaveChanged = true;
                    UpdateDirsCommand.RaiseCanExecuteChanged();

                }
            }

        }


        private  async void EvaluateDirErrorsResult(Result<int> countResult, string newDirPath)
        {
            if (countResult.HasError<DirectoryDoesNotExistResultError>())
            {
                var resultEnum = await dialogService.AskQuestionAsync(
                    $"Create Directory?", 
                    $"The directory at {newDirPath} does not exist, would you like to create it?"
                    );
                if(resultEnum == MessageDialogResult.Affirmative)
                {
                    var updatedResult = manager.FileManager.CreateProcessingDirIfDoesNotExistAndUpdateWithReferenceFilesAndNewFileCount();
                    if(updatedResult.IsSuccess)
                    {
                        this.ProcessingDirPath = newDirPath;
                        this.ProcessingFilesCount = updatedResult.Value;
                    }
                }

            }

            else
            {
                await this.dialogService.ShowMessageAsync(
                    $"Error in setting the Processing Directory to {newDirPath}", $"{countResult.Errors.ToString()}");
            }
        }

        public int ProcessingFilesCount
        {
            get => processingFilesCount;
            set { Set<int>(()=> ProcessingFilesCount, ref processingFilesCount, value); }
        }
        public int ReferenceFilesCount { 
            get => referenceFilesCount;
            set { Set<int>(() => ReferenceFilesCount, ref referenceFilesCount, value); } }

        public RelayCommand UpdateDirsCommand { get => updateDirsCommand; set => updateDirsCommand = value; }

        public bool ProcessingDirIsValid()
        {
            return manager.DirIsValid(processingDirPath);
        }

        public bool ReferenceDirIsValid()
        {
            return manager.DirIsValid(referenceDirPath);
        }

    }
}
