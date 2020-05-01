using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using QDoc.Docs;
using QmsHero.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsHero.ViewModel
{
    public class ResultsViewModel : ViewModelBase
    {
        DocCollection docCollection;

        public ResultsViewModel() : base()
        {
            this.DocCollection = SimpleIoc.Default.GetInstance<ProcessingResultsStore>().DocCollection;
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
    }
}
