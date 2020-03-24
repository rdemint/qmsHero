using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using QmsDoc.Core;

namespace QmsHero.ViewModel
{
    public class DCNViewModel: ViewModelBase
    {
        string effectiveDate;
        DocManager manager;

        public DCNViewModel()
        {
            this.EffectiveDate = "2020-03-20";
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
        }

        public string EffectiveDate { 
            get => effectiveDate;
            set {
                Set<string>(() => this.EffectiveDate, ref this.effectiveDate, value);
            } }

        //.manager.ConfigDir()
        //. foreach (fileinfo in manager.DirFiles) {
        //.     var doc = manager.CreateDoc(fileinfo)
        //.     doc = manager.ProcessDoc(doc)
        //. 
        //.
        //.
        //.
        //.
        //.
        //.
        //.

    }
}
