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
        DocState docEdit;
        DocHeader docHeader;
        ObservableCollection<DocProperty> docProps;
        string originalDirPath;

        public CustomProcessingViewModel()
        {

            this.ViewDisplayName = "Custom";
            this.Manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.OriginalDirPath = null;
            this.DocEdit = new DocState();
            //this.DocHeader = new DocHeader();
        }

        public RelayCommand ProcessFilesCommand {
            get {
                  this.processFilesCommand = new RelayCommand(
                        () => ProcessFiles(),
                        () => DirIsValid()
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
            //var docEdit = new DocEdit(this.DocHeader);
            this.manager.ProcessFiles(this.DocEdit);
        }

        private bool DirIsValid()
        {
            return this.Manager.DirIsValid(OriginalDirPath);
        }

        public string ViewDisplayName { 
            get => viewDisplayName; 
            set => viewDisplayName = value; }
        public DocManager Manager { get => manager; set => manager = value; }
        public string OriginalDirPath { 
            get => originalDirPath;
            set {
                Set<string>(
                    () => OriginalDirPath, ref originalDirPath, value
                    );
             
            } }

        //public DocHeader DocHeader { get => docHeader; set => docHeader = value; }
        public DocState DocEdit { get => docEdit; set => docEdit = value; }
    }
}
