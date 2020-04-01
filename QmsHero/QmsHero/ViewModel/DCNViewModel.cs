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
        string revision;
        string logoText;
        DocManager manager;
        DocHeader docHeader;

        public DCNViewModel()
        {
            this.EffectiveDate = "2020-03-20";
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            //this.docHeader = new DocHeader();
            //this.docHeader.Revision = "1";
            //this.docHeader.EffectiveDate = "2020-03-20";
            //this.docHeader.LogoPath = "C:/raine/qmsProcessing";
            this.Revision = "1";
        }

        public string EffectiveDate { 
            get => effectiveDate;
            set {
                Set<string>(() => this.EffectiveDate, ref this.effectiveDate, value);
            } }

        public string Revision { get => revision; set => revision = value; }
        public string LogoText { get => logoText; set => logoText = value; }

        public DocHeader DocHeader
        {
            get => docHeader;
            set
            {
                Set<DocHeader>(
                    () => this.DocHeader, ref this.docHeader, value
                    );
            }
        }

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
