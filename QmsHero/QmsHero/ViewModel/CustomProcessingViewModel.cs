using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System.Collections.ObjectModel;
using System.Windows;
using QmsDoc.Core;
using QDoc.Core;
using QmsDocXml.Common.PropertyGroups;

namespace QmsHero.ViewModel
{
    public class CustomProcessingViewModel: ViewModelBase
    {
        string viewDisplayName;
        DocManager manager;
        RelayCommand processFilesCommand;
        HeaderPropertyGroup headerPropertyGroup;
        string referenceDirPath;
        string processingDirPath;

        public CustomProcessingViewModel()
        {

            this.ViewDisplayName = "Custom";
            this.Manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.HeaderPropertyGroup = new HeaderPropertyGroup();
        }

        public RelayCommand ProcessFilesCommand {
            get {
                  this.processFilesCommand = new RelayCommand(
                        () => ProcessFiles(),
                        () => ProcessingDirIsValid()
                        );
                return this.processFilesCommand;
            }
            set {
                this.processFilesCommand = value;
            } 
        }
        private void ProcessFiles()
        {
            
            //var docEdit = new DocEdit(this.DocHeader);
            this.manager.Process(headerPropertyGroup.ToCollection());
        }

        private bool ProcessingDirIsValid()
        {
            return manager.DirIsValid(processingDirPath);
        }

        private bool ReferenceDirIsValid()
        {
            return manager.DirIsValid(referenceDirPath);
        }

        private bool CanProcessFiles()
        {
            return manager.CanProcessFiles();
        }

        public string ViewDisplayName { 
            get => viewDisplayName; 
            set => viewDisplayName = value; }
        public DocManager Manager { get => manager; set => manager = value; }

        public string ReferenceDirPath
        {
            get => referenceDirPath;
            set
            {
                Set<string>(
                    () => ReferenceDirPath, ref referenceDirPath, value
                );
                manager.FileManager.SetReferenceDir(value);
            }
        }
        public string ProcessingDirPath { 
            get => processingDirPath;
            set {
                Set<string>(
                    () => ProcessingDirPath, ref processingDirPath, value
                );
                manager.FileManager.SetProcessingDir(value);
            }
                
        }

        public HeaderPropertyGroup HeaderPropertyGroup { get => headerPropertyGroup; set => headerPropertyGroup = value; }
    }
}
