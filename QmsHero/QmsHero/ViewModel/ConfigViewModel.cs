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

namespace QmsHero.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        string referenceDirPath;
        string processingDirPath;
        int processingFilesCount;
        int referenceFilesCount;
        DocManager manager;
        string viewDisplayName;
        IAsyncDialogService dialogService;

        public ConfigViewModel()
        {
            this.viewDisplayName = "Configure Project Directories";
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.ReferenceDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Dot Cup\\QMS";
            this.ProcessingDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Dot Cup\\QMS_Processing1";
            this.dialogService = SimpleIoc.Default.GetInstance<IAsyncDialogService>();
        }

        public string ReferenceDirPath
        {
            get => referenceDirPath;
            set
            {
               var countResult = manager.FileManager.SetReferenceDir(value);
                if(countResult.IsSuccess)
                {
                    this.ReferenceFilesCount = countResult.Value;
                    Set<string>(
                    () => ReferenceDirPath, ref referenceDirPath, value
                );
                }
                else
                {
                    this.dialogService.ShowMessageAsync(
                        $"Error in setting the Reference Directory to {value}", $"{countResult.Errors.ToString()}");
                }
            }
        }
        public string ProcessingDirPath
        {
            get => processingDirPath;
            set
            {
                var countResult = manager.FileManager.SetProcessingDir(value);
                if(countResult.IsSuccess)
                {
                    this.ProcessingFilesCount = countResult.Value;
                    Set<string>(
                    () => ProcessingDirPath, ref processingDirPath, value
                );
                }
                else
                {
                    EvaluateDirErrorsResult(countResult, value);
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
                    manager.FileManager.CreateProcessingDirThatDoesNotExist();
                }

            }

            else
            {
                await this.dialogService.ShowMessageAsync(
                    $"Error in setting the Reference Directory to {newDirPath}", $"{countResult.Errors.ToString()}");
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
