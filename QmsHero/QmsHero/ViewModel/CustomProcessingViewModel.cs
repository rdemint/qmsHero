using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
        string originalDirPath;

        public CustomProcessingViewModel()
        {

            this.ViewDisplayName = "Custom";
            this.Manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.docEdit = new DocEdit();
            this.OriginalDirPath = null;
        }

        public RelayCommand ProcessFilesCommand {
            get {
                  this.processFilesCommand = new RelayCommand(
                        () => ProcessFiles(),
                        () => CanProcessFiles()
                        );
                return this.processFilesCommand;
            }
            set {
                this.processFilesCommand = value;
            } 
        }
        private void ProcessFiles()
        {
            this.manager.ConfigDir(this.OriginalDirPath);
            this.manager.ProcessFiles(this.DocEdit);
        }

        private bool CanProcessFiles()
        {
            return this.Manager.CanProcessFiles(this.OriginalDirPath);
        }

        public string ViewDisplayName { 
            get => viewDisplayName; 
            set => viewDisplayName = value; }
        public DocEdit DocEdit { get => docEdit; set => docEdit = value; }
        public DocManager Manager { get => manager; set => manager = value; }
        public string OriginalDirPath { 
            get => originalDirPath;
            set {
                Set<string>(
                    () => OriginalDirPath, ref originalDirPath, value
                    );
             
            } }
    }
}
