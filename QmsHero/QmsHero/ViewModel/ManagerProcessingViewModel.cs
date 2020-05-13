﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using QDoc.Core;
using QmsDoc.Core;
using QmsHero.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QmsHero.ViewModel
{
    public class ManagerProcessingViewModel : ViewModelBase
    {
        DocManager manager;
        ResultsViewModel resultsViewModel;
        IAsyncDialogService dialogService;
        public ManagerProcessingViewModel()
        {
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.resultsViewModel = SimpleIoc.Default.GetInstance<ResultsViewModel>();
            this.dialogService = SimpleIoc.Default.GetInstance<IAsyncDialogService>();


        }

        public async void Process(QDocPropertyCollection docPropertyCollection)
        {

            var controller = await dialogService.ShowProgressAsync("In Progress", "Processing Files...");
            var docCollection = this.manager.Process(docPropertyCollection);
            int errorCount = docCollection.Where(doc => doc.PropertyResultCollection.Any(result => result.IsSuccess == false)).Count();
            resultsViewModel.DocCollection = docCollection;
            await controller.CloseAsync();
            if(docCollection.Count == 0)
            {
                await dialogService.ShowMessageAsync("Something is not right...", "No documents were processed.  Please check your directories settings and files.");
            }
            
            if(docCollection.HasErrors())
            {
                await dialogService.ShowMessageAsync(
                    "Something is not right...",
                    $"Finished processing {docCollection.Count} documents with {docCollection.CountErrors()} errors. Please review the results for details.");
            }
            else
            {
                var dialogResult = await dialogService.AskQuestionAsync(
                    $"Finished processing {docCollection.Count} documents. with no errors.", "Would you like to make the results your new project directory, and create a new processing directory?");
                if(dialogResult == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative)
                {

                }
            }

        }
    }
}
