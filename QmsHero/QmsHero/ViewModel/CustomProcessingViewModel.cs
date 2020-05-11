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
        string logoPath;
        ResultsViewModel resultsViewModel;
        FileProcessingViewModel fileProcessingViewModel;
        ConfigViewModel configViewModel;
        public CustomProcessingViewModel(): base()
        {

            this.ViewDisplayName = "Custom";
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.fileProcessingViewModel = SimpleIoc.Default.GetInstance<FileProcessingViewModel>();
            
            this.HeaderPropertyGroup = new HeaderPropertyGroup();
            this.ProcessFilesCommand = new RelayCommand(
                        () => ProcessFiles(),
                        () => CanProcessFiles()
                        );
            //TestData
            this.HeaderPropertyGroup.HeaderLogo.State = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Reference\\qaladder_logo.jpg";
            //
        }

        public RelayCommand ProcessFilesCommand {
            get {
                  return this.processFilesCommand;
            }
            set {
                if(this.processFilesCommand != value)
                {
                    this.processFilesCommand = value;
                }
            } 
        }
        private void ProcessFiles()
        {

            this.fileProcessingViewModel.Process(headerPropertyGroup.ToCollection());
        }

        public bool CanProcessFiles()
        {
            return this.configViewModel.ProcessingDirIsValid() && this.configViewModel.ReferenceDirIsValid();
        }

        
        public string ViewDisplayName { 
            get => viewDisplayName; 
            set => viewDisplayName = value; }

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
