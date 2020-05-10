using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDocXml.Common.PropertyGroups;

namespace QmsHero.ViewModel
{
    public class FilePropertiesViewModel : ViewModelBase
    {
        DocumentPropertyPropertyGroup filePropertiesGroup;
        public FilePropertiesViewModel()
        {
        }

        public DocumentPropertyPropertyGroup FilePropertiesGroup { get => filePropertiesGroup; set => filePropertiesGroup = value; }


    }
}
