using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;
using QmsDoc.Core;
using QmsDoc.Controls;
using System.Windows.Controls;

namespace QmsHero.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        //DocActionControlBase logoPath;
        //string logoText;
        //string effectiveDate;
        //string revision;
        List<IDocActionControl> controlList;

        public ConfigViewModel()
        {
            var manager = new DocConfigControlManager();
            this.ControlList = manager.ToControlList();
        }

        public List<IDocActionControl> ControlList { get => controlList; set => controlList = value; }

    }
}
