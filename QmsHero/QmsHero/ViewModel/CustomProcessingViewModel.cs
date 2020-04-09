using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QmsDoc.Core;
using QmsDoc.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;
using System.Windows;

namespace QmsHero.ViewModel
{
    public class CustomProcessingViewModel: ViewModelBase
    {
        string viewDisplayName;
        DocManager manager;
        RelayCommand processFilesCommand;
        DocEdit docEdit;
        ObservableCollection<DocProperty> docProps;
        DocHeader docHeader;
        public CustomProcessingViewModel()
        {

            this.ViewDisplayName = "Custom";
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.docEdit = new DocEdit();
        }

        public RelayCommand ProcessFilesCommand {
            get {
                if (this.processFilesCommand == null)
                {
                    this.processFilesCommand = new RelayCommand(
                        () => ProcessFiles(),
                        () => CanProcessFiles()
                        );
                }
                return this.processFilesCommand;
            }
            set {
                this.processFilesCommand = value;
            } 
        }
        private void ProcessFiles()
        {
            this.manager.ProcessFiles(this.DocEdit);
        }

        private bool CanProcessFiles()
        {
            return this.manager.CanProcessFiles;
        }

        public string ViewDisplayName { get => viewDisplayName; set => viewDisplayName = value; }
        public DocEdit DocEdit { get => docEdit; set => docEdit = value; }
        public DocManager Manager { get => manager; set => manager = value; }
    }
}
