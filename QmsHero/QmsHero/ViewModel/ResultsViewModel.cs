using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using QDoc.Docs;
using QmsHero.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsHero.ViewModel
{
    public class ResultsViewModel : ViewModelBase
    {
        DocCollection docCollection;
        RelayCommand<string> openFileCommand;
        string name;

        public ResultsViewModel() : base()

        {

            this.DocCollection = SimpleIoc.Default.GetInstance<ProcessingResultsStore>().DocCollection;
            this.openFileCommand = new RelayCommand<string>(
                file => Process.Start(file)
                );
            this.Name = "ResultsView";
        }

        public DocCollection DocCollection
        {
            get => docCollection;
            set
            {
                docCollection = value;
                RaisePropertyChanged();
            }

            //public DocCollection InitializeCollection ()
            //{
            //    DocCollection.Add()
            //}
        }

        public RelayCommand<string> OpenFileCommand { get => openFileCommand; set => openFileCommand = value; }
        public string Name { get => name; set => name = value; }
    }
}
