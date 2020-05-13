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
    public class CustomProcessingViewModel: FileProcessingViewModelBase
    {
        string viewDisplayName;
        HeaderPropertyGroup headerPropertyGroup;
        string logoPath;
        RelayCommand processFilesCommand;
        public CustomProcessingViewModel(): base()
        {

            this.viewDisplayName = "Custom";
            this.HeaderPropertyGroup = new HeaderPropertyGroup();
            processFilesCommand = new RelayCommand(
                () => ProcessFiles(headerPropertyGroup.ToCollection()), 
                ()=>CanProcessFiles() == true);
            
            //TestData
            this.HeaderPropertyGroup.HeaderLogo.State = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Dot Cup\\QMS\\DotLogoFinal.png";
            //
        }

        
        public HeaderPropertyGroup HeaderPropertyGroup { get => headerPropertyGroup; set => headerPropertyGroup = value; }
        public string LogoPath { 
            get => logoPath;
            set {
                Set<string>(
                    () => LogoPath, ref logoPath, value
                    );
            } }

        public RelayCommand ProcessFilesCommand { get => processFilesCommand; set => processFilesCommand = value; }
    }
}
