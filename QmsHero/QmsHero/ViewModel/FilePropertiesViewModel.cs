using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDocXml.Common.PropertyGroups;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Command;
using QmsHero.View;

namespace QmsHero.ViewModel
{
    public class FilePropertiesViewModel : ViewModelBase
    {
        DocumentPropertyPropertyGroup documentPropertyPropertyGroup;
        FileProcessingViewModel fileProcessingViewModel;
        ConfigViewModel configViewModel;
        RelayCommand processFilesCommand;

        public FilePropertiesViewModel()
        {
            this.fileProcessingViewModel = SimpleIoc.Default.GetInstance<FileProcessingViewModel>();
            this.configViewModel = SimpleIoc.Default.GetInstance<ConfigViewModel>();
            this.processFilesCommand = new RelayCommand(
                () => ProcessFiles(),
                () => CanProcessFiles()
                );
            this.documentPropertyPropertyGroup = new DocumentPropertyPropertyGroup();

            //TestData
            this.documentPropertyPropertyGroup.DocumentPropertyCompany.State = "Lean RAQA Systems";
            this.documentPropertyPropertyGroup.DocumentPropertyCreator.State = "Michelle Lott";
            this.documentPropertyPropertyGroup.DocumentPropertyLastModifiedBy.State = "Raines Demint";
            //TestData
        }
        public DocumentPropertyPropertyGroup DocumentPropertyPropertyGroup { get => documentPropertyPropertyGroup; set => documentPropertyPropertyGroup = value; }
        public RelayCommand ProcessFilesCommand { get => processFilesCommand; set => processFilesCommand = value; }

        public void ProcessFiles()
        {
            this.fileProcessingViewModel.Process(DocumentPropertyPropertyGroup.ToCollection());
        }

        public bool CanProcessFiles()
        {
            return this.configViewModel.ProcessingDirIsValid() && this.configViewModel.ReferenceDirIsValid();
        }
    }
}
