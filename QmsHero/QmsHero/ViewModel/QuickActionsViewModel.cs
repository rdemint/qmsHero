using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Core;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace QmsHero.ViewModel
{
    public class QuickActionsViewModel: FileProcessingViewModelBase
    {
        string viewDisplayName;
        DocManager manager;
        RelayCommand inspectFileNameCommand; 
        RelayCommand processFileNameCommand;
        string currentDocumentName;
        string newDocumentName;
        RelayCommand inspectFileRevisionCommand;
        RelayCommand processFileRevisionCommand;
        string currentFileRevision;
        string newFileRevision;
        RelayCommand inspectFileNumberCommand;
        RelayCommand processFileNumberCommand;
        string currentFileNumber;
        string newFileNumber;
        RelayCommand inspectFileTextCommand;
        RelayCommand processFileTextCommand;
        string currentFileText;
        string newFileText;

        string referenceDirPath;
        string processingDirPath;
        ResultsViewModel resultsViewModel;

        public QuickActionsViewModel()
        {
            this.ViewDisplayName = "Quick Actions";
            this.Manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.resultsViewModel = SimpleIoc.Default.GetInstance<ResultsViewModel>();

            this.inspectFileNameCommand = new RelayCommand(
                    () => InspectFilesName(),
                    () => CanInspectFiles()
                );
            this.processFileNameCommand = new RelayCommand(
                        () => ProcessFilesName(),
                        () => CanProcessFiles()
                        );

            this.inspectFileNumberCommand = new RelayCommand(
                ()=> InspectFilesNumber(),
                ()=> CanInspectFiles()
               );

            this.processFileNumberCommand = new RelayCommand(
                () => ProcessFilesNumber(),
                () => CanProcessFiles()
                );

            this.inspectFileRevisionCommand = new RelayCommand(
                () => InspectFilesRevision(),
                () => CanInspectFiles());

            this.processFileRevisionCommand = new RelayCommand(() => ProcessFilesRevision(), () => CanProcessFiles());

            this.inspectFileTextCommand = new RelayCommand(
                () => InspectFilesText(),
                () => CanInspectFiles());

            this.processFileTextCommand = new RelayCommand(() => ProcessFilesText(), () => CanProcessFiles());

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

        public RelayCommand ProcessFileNameCommand {
            get { 
                return processFileNameCommand;
            }
            set
            {
                if (processFileNameCommand != value)
                { processFileNameCommand = value; }
            }
        } 
        public string NewDocumentName { 
            get => newDocumentName;
            set{ 
                Set<string>(() => NewDocumentName, ref newDocumentName, value);
                ProcessFileNameCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }}
        public string CurrentDocumentName
        {
            get => currentDocumentName;
            set { 
                Set<string>(()=> CurrentDocumentName, ref currentDocumentName, value);
                InspectFileNameCommand.RaiseCanExecuteChanged();
            }}

        public RelayCommand InspectFileNameCommand
        {
            get
            {
                return inspectFileNameCommand;
            }
            set
            {
                if (inspectFileNameCommand != value)
                {
                    inspectFileNameCommand = value;
                }
            }
        }

        public RelayCommand InspectFileRevisionCommand { get => inspectFileRevisionCommand; set => inspectFileRevisionCommand = value; }
        public RelayCommand ProcessFileRevisionCommand { get => processFileRevisionCommand; set => processFileRevisionCommand = value; }
        public RelayCommand InspectFileNumberCommand { get => inspectFileNumberCommand; set => inspectFileNumberCommand = value; }
        public RelayCommand ProcessFileNumberCommand { get => processFileNumberCommand; set => processFileNumberCommand = value; }
        public RelayCommand InspectFileTextCommand { get => inspectFileTextCommand; set => inspectFileTextCommand = value; }
        public RelayCommand ProcessFileTextCommand { get => processFileTextCommand; set => processFileTextCommand = value; }
        public string CurrentFileRevision { get => currentFileRevision; set { Set<string>(() => CurrentFileRevision, ref currentFileRevision, value); }}
        public string NewFileRevision { get => newFileRevision; set { Set<string>(() => NewFileRevision, ref newFileRevision, value); } }
        public string CurrentFileNumber { get => currentFileNumber; set { Set<string>(() => CurrentFileNumber, ref currentFileNumber, value); }}
        public string NewFileNumber { get => newFileNumber; set { Set<string>(() => NewFileNumber, ref newFileNumber, value); }}
        public string CurrentFileText { get => currentFileText; set { Set<string>(() => CurrentFileText, ref currentFileText, value); }}
        public string NewFileText { get => newFileText; set { Set<string>(() => NewFileText, ref newFileText, value);} }

        private void InspectFilesName()
        {
            var docNameManager = new DocNameActionManager(currentDocumentName);
            InspectFiles(docNameManager);
        }
        
        
        private void ProcessFilesName()
        {
            var docNameManager = new DocNameActionManager(currentDocumentName, newDocumentName);
            ProcessFiles(docNameManager);
        }

        private void InspectFilesText()
        {
            var actionManager = new TextActionManager(currentFileText);
            InspectFiles(actionManager);
        }

        private void ProcessFilesText()
        {
            var actionManager = new TextActionManager(currentFileText, newFileText);
            ProcessFiles(actionManager);


        }

        private void InspectFilesRevision()
        {
            var actionManager = new DocRevisionActionManager(currentFileRevision);
            InspectFiles(actionManager);
            
        }

        private void ProcessFilesRevision()
        {
            var actionManager = new DocRevisionActionManager(currentFileRevision, newFileRevision);
            ProcessFiles(actionManager);

        }

        private void InspectFilesNumber()
        {
            var actionManager = new DocNumberActionManager(currentFileNumber);
            InspectFiles(actionManager);

        }

        private void ProcessFilesNumber()
        {
            var actionManager = new DocNumberActionManager(currentFileNumber, newFileNumber);
            ProcessFiles(actionManager);

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
            return ProcessingDirIsValid() &&
                ReferenceDirIsValid(); 

        }

        private bool CanInspectFiles()
        {
            var result1 = ProcessingDirIsValid();
            var result2 = ReferenceDirIsValid();
            return result1 && result2;
        }

    }
}
