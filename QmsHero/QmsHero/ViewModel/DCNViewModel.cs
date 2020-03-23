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

        public string EffectiveDate { 
            get => effectiveDate;
            set {
                Set<string>(() => this.EffectiveDate, ref this.effectiveDate, value);
            } }
    }
}
