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
using System.IO;
using System.Diagnostics;

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
        string logoPath;
        ResultsViewModel resultsViewModel;
        public CustomProcessingViewModel()
        {

            this.ViewDisplayName = "Custom";
            this.Manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.resultsViewModel = SimpleIoc.Default.GetInstance<ResultsViewModel>();
            this.HeaderPropertyGroup = new HeaderPropertyGroup();
            
            this.processFilesCommand = new RelayCommand(
                        () => ProcessFiles(),
                        () => ProcessingDirIsValid() && ReferenceDirIsValid()
                        );
            //
            this.HeaderPropertyGroup.HeaderLogo.State = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Reference\\qaladder_logo.jpg";
            this.referenceDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Reference";
            this.processingDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Processing";
            this.ProcessFiles();
            //
        }

        public RelayCommand ProcessFilesCommand {
            get {
                  return this.processFilesCommand;
            }
            set {
                this.processFilesCommand = value;
            } 
        }
        private void ProcessFiles()
        {

            this.manager.FileManager.SetProcessingDir(this.processingDirPath);
            this.manager.FileManager.SetReferenceDir(this.referenceDirPath);
            var docCollection = this.manager.Process(headerPropertyGroup.ToCollection());
            int errorCount = docCollection.Where(doc => doc.PropertyResultCollection.Any(result => result.IsSuccess == false)).Count();
            resultsViewModel.DocCollection = docCollection;
            MessageBox.Show($"Finished Processing the files. {docCollection.Count} files were processed and {errorCount} files had errors.");

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
        public string LogoPath { 
            get => logoPath;
            set {
                Set<string>(
                    () => LogoPath, ref logoPath, value
                    );
            } }
    }
}
