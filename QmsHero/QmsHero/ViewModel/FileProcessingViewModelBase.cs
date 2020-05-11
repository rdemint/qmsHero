using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
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
        FileProcessingViewModel fileProcessingViewModel;
        ConfigViewModel configViewModel;
        DocManager manager;
        RelayCommand processFilesCommand;


        public FileProcessingViewModelBase(): base()
        {
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.fileProcessingViewModel = SimpleIoc.Default.GetInstance<FileProcessingViewModel>();
            this.ProcessFilesCommand = new RelayCommand(() => ProcessFiles(), () => CanProcessFiles());
        }
        public RelayCommand ProcessFilesCommand { get => processFilesCommand; set => processFilesCommand = value; }
        public string ViewDisplayName { get => viewDisplayName; set => viewDisplayName = value; }

        public abstract void ProcessFiles();

        public virtual bool CanProcessFiles()
        {
            return this.configViewModel.ProcessingDirIsValid() && this.configViewModel.ReferenceDirIsValid();
        }
    }
}
