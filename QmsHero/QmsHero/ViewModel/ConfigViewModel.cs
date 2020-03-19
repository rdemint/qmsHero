using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;
using QmsDoc.Core;
using System.Windows.Controls;

namespace QmsHero.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        //DocActionControlBase logoPath;
        //string logoText;
        //string effectiveDate;
        //string revision;
        List<ControlBase> docActionControls;

        public ConfigViewModel()
        {
            var docActions = new DocActionControlManager();
        }

        public List<ControlBase> DocActionControls { get => docActionControls; set => docActionControls = value; }

        //public DocActionControlBase LogoPath {
        //    get => DocActionControlBase.GetControl(this.logoPath); 
        //    set => logoPath = value; }
        //public string LogoText { get => logoText; set => logoText = value; }
        //public string EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        //public string Revision { get => revision; set => revision = value; }
    }
}
