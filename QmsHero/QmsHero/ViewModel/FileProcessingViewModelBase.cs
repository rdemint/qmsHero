using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using QDoc.Core;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsHero.ViewModel
{
    public abstract class FileProcessingViewModelBase : ViewModelBase
    {
        string viewDisplayName;
        protected ManagerProcessingViewModel managerProcessingViewModel;
        protected ConfigViewModel configViewModel;
        protected DocManager manager;


        public FileProcessingViewModelBase(): base()
        {
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.managerProcessingViewModel = SimpleIoc.Default.GetInstance<ManagerProcessingViewModel>();
            this.configViewModel = SimpleIoc.Default.GetInstance<ConfigViewModel>();
        }
        public string ViewDisplayName { get => viewDisplayName; set => viewDisplayName = value; }

        protected virtual void ProcessFiles(QDocActionManager actionManager)
        {
            managerProcessingViewModel.Process(actionManager);
        }

       
        protected virtual void InspectFiles(QDocActionManager actionManager)
        {
            managerProcessingViewModel.Inspect(actionManager);

        }

        protected virtual void ProcessFiles(QDocPropertyCollection docPropCollection)
        {
            managerProcessingViewModel.Process(docPropCollection);
        }

        protected virtual void InspectFiles(QDocPropertyCollection docPropCollection)
        {
            managerProcessingViewModel.Inspect(docPropCollection);
        }
        
        protected virtual bool CanProcessFiles()
        {
            return this.configViewModel.ProcessingDirIsValid() && this.configViewModel.ReferenceDirIsValid();
        }
    }
}
