using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Core;
using QmsHero.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async void Inspect(QDocActionManager actionManager)
        {
            var controller = await dialogService.ShowProgressAsync("In Progress", "Processing Files");
            var docCollection = this.manager.Inspect(actionManager);
            await controller.CloseAsync();
            ProcessBase(docCollection);
        }

        public async void Process(QDocActionManager actionManager)
        {
            var controller = await dialogService.ShowProgressAsync("In Progress", "Processing Files...");
            var docCollection = this.manager.Process(actionManager);
            await controller.CloseAsync();
            InspectBase(docCollection);
        }

        private async void InspectBase(DocCollection docCollection)
        {
            int errorCount = docCollection.Where(doc => doc.PropertyResultCollection.Any(result => result.IsSuccess == false)).Count();
            resultsViewModel.DocCollection = docCollection;

            if (docCollection.Count == 0)
            {
                await dialogService.ShowMessageAsync("Something is not right...", "No documents were inspected.  Please check your directories settings and files.");
            }

            else if (docCollection.HasErrors())
            {
                await dialogService.ShowMessageAsync(
                    "Something is not right...",
                    $"Finished inspecting {docCollection.Count} documents with {docCollection.CountErrors()} errors. Please review the results for details.");
            }
            else
            {
                await dialogService.ShowMessageAsync(
                    "Success.",
                    $"Finished inspecting {docCollection.Count} documents with no errors.");
            }


        }

        public void Inspect(QDocPropertyCollection docPropertyCollection)
        {
            throw new NotImplementedException();
        }

        public async void Process(QDocPropertyCollection docPropertyCollection)
        {

            var controller = await dialogService.ShowProgressAsync("In Progress", "Processing Files...");
            var docCollection = this.manager.Process(docPropertyCollection);
            await controller.CloseAsync();
            ProcessBase(docCollection);

        }

        private async void ProcessBase(DocCollection docCollection)
        {
            int errorCount = docCollection.Where(doc => doc.PropertyResultCollection.Any(result => result.IsSuccess == false)).Count();
            resultsViewModel.DocCollection = docCollection;

            if (docCollection.Count == 0)
            {
                await dialogService.ShowMessageAsync("Something is not right...", "No documents were processed.  Please check your directories settings and files.");
            }

            else if (docCollection.HasErrors())
            {
                await dialogService.ShowMessageAsync(
                    "Something is not right...",
                    $"Finished processing {docCollection.Count} documents with {docCollection.CountErrors()} errors. Please review the results for details.");
            }
            else
            {
                var dialogResult = await dialogService.AskQuestionAsync(
                    $"Success.  Finished processing {docCollection.Count} documents with no errors.", "Would you like to make the results your new project directory, and create a new processing directory?");
                if (dialogResult == MahApps.Metro.Controls.Dialogs.MessageDialogResult.Affirmative)
                {
                    var configViewModel = SimpleIoc.Default.GetInstance<ConfigViewModel>();
                    manager.FileManager.MakeCurrentProcessingDirTheReferenceDirAndCreateNewProcessingDirWithTimeSuffix();
                    configViewModel.ReferenceDirPath = manager.FileManager.ReferenceDir.FullName;
                    configViewModel.ProcessingDirPath = manager.FileManager.ProcessingDir.FullName;
                }
            }
        }
    }
}
