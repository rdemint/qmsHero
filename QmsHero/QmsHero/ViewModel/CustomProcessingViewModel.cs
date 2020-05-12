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
        public CustomProcessingViewModel(): base()
        {

            this.viewDisplayName = "Custom";
            this.HeaderPropertyGroup = new HeaderPropertyGroup();
            
            //TestData
            this.HeaderPropertyGroup.HeaderLogo.State = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Dot Cup\\logo_DotCup.jpg";
            //
        }

        
        protected override void ProcessFiles()
        {
            this.managerProcessingViewModel.Process(headerPropertyGroup.ToCollection());
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
