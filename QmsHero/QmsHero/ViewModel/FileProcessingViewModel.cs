using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using QDoc.Core;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QmsHero.ViewModel
{
    class FileProcessingViewModel : ViewModelBase
    {
        DocManager manager;
        ResultsViewModel resultsViewModel;
        public FileProcessingViewModel()
        {
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.resultsViewModel = SimpleIoc.Default.GetInstance<ResultsViewModel>();


        }

        public void Process(QDocPropertyCollection docPropertyCollection)
        {

            var docCollection = this.manager.Process(docPropertyCollection);
            int errorCount = docCollection.Where(doc => doc.PropertyResultCollection.Any(result => result.IsSuccess == false)).Count();
            resultsViewModel.DocCollection = docCollection;
            MessageBox.Show($"Finished Processing the files. {docCollection.Count} files were processed and {errorCount} files had errors.");

        }
    }
}
