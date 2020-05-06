using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Core;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QmsHero.ViewModel
{
    public class QuickActionsViewModel: ViewModelBase
    {
        string viewDisplayName;
        DocManager manager;
        RelayCommand processFilesCommand;
        string referenceDirPath;
        string processingDirPath;
        ResultsViewModel resultsViewModel;
        DocNameManager docNameManager;
        string currentDocumentName;
        string newDocumentName;

        public QuickActionsViewModel()
        {
            this.ViewDisplayName = "Quick Actions";
            this.Manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.resultsViewModel = SimpleIoc.Default.GetInstance<ResultsViewModel>();

            this.processFilesCommand = new RelayCommand(
                        () => ProcessFiles(),
                        () => ProcessingDirIsValid() && ReferenceDirIsValid()
                        );
            this.referenceDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Reference";
            this.processingDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Processing";
        }

        public string ViewDisplayName { get => viewDisplayName; set => viewDisplayName = value; }
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
        public string ProcessingDirPath
        {
            get => processingDirPath;
            set
            {
                Set<string>(
                    () => ProcessingDirPath, ref processingDirPath, value
                );
                manager.FileManager.SetProcessingDir(value);
            }

        }

        public RelayCommand ProcessFilesCommand { get => processFilesCommand; set => processFilesCommand = value; }
        public DocNameManager DocNameManager { get => docNameManager; set => docNameManager = value; }
        public string NewDocumentName { 
            get => newDocumentName;
            set{ Set<string>(() => NewDocumentName, ref newDocumentName, value); }}
        public string CurrentDocumentName
        {
            get => currentDocumentName;
            set { Set<string>(()=> CurrentDocumentName, ref currentDocumentName, value);}}

        
        private void ConfigManagerDir()
        {
            this.manager.FileManager.SetProcessingDir(this.processingDirPath);
            this.manager.FileManager.SetReferenceDir(this.referenceDirPath);
        }
        
        private void ProcessFiles()
        {
            this.ConfigManagerDir();
            var docNameManager = DocNameManager.Create(currentDocumentName);
            var docCollection = this.manager.Process(docNameManager);
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
            return manager.CanProcessFiles() && DocNameManager.CurrentState !=null ;
        }

        private bool CanInspectFiles()
        {
            return manager.CanProcessFiles() &&
                DocNameManager.CurrentState != null &&
                DocNameManager.TargetState != null;
        }
    }
}
