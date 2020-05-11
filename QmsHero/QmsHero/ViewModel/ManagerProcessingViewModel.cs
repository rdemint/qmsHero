using GalaSoft.MvvmLight;
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
        DialogService dialogService;
        public ManagerProcessingViewModel()
        {
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.resultsViewModel = SimpleIoc.Default.GetInstance<ResultsViewModel>();
            this.dialogService = SimpleIoc.Default.GetInstance<DialogService>();


        }

        public async void Process(QDocPropertyCollection docPropertyCollection)
        {

            var controller = await dialogService.ShowProgressAsync("In Progress", "Processing Files...");
            var docCollection = this.manager.Process(docPropertyCollection);
            int errorCount = docCollection.Where(doc => doc.PropertyResultCollection.Any(result => result.IsSuccess == false)).Count();
            resultsViewModel.DocCollection = docCollection;
            await controller.CloseAsync();
            if(docCollection.HasErrors())
            {
                await dialogService.ShowProgressAsync("Finished", "Errors in processing the documents");
            }
            else
            {
                await dialogService.ShowProgressAsync("Finished", "No errors identified");
            }
            //MessageBox.Show($"Finished Processing the files. {docCollection.Count} files were processed and {errorCount} files had errors.");

        }
    }
}
