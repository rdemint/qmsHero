using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace QmsHero.ViewModel
{
    public class DCNViewModel: ViewModelBase
    {
        string effectiveDate;
        string logoPath;
        string logoText;

        public DCNViewModel()
        {
            this.EffectiveDate = "2020-03-20";
        }

        public string EffectiveDate { 
            get => effectiveDate;
            set {
                Set<string>(() => this.EffectiveDate, ref this.effectiveDate, value);
            } }

        public string LogoPath
        {
            get => logoPath;
            set { Set<string>(() => this.LogoPath, ref this.logoPath, value); }
        }
        public string LogoText { 
            get => logoText;
            set { Set<string>(() => this.LogoText, ref this.logoText, value); } 
        }
    }
}
