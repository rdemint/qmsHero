using GalaSoft.MvvmLight;
using QDoc.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsHero.Model
{
    class ProcessingResultsStore : ObservableObject
    {
        DocCollection docCollection;
        public ProcessingResultsStore(): base()
        {
            docCollection = new DocCollection();
        }

        public DocCollection DocCollection { 
            get => docCollection;
            set {
                Set<DocCollection>(
                    () => DocCollection, ref docCollection, value
                    ); ;
            } }
    }
}
