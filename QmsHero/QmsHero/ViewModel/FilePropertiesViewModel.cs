﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDocXml.Common.PropertyGroups;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Command;
using QmsHero.View;

namespace QmsHero.ViewModel
{
    public class FilePropertiesViewModel : FileProcessingViewModelBase
    {
        DocumentPropertyPropertyGroup documentPropertyPropertyGroup;
        RelayCommand processFilesCommand;

        public FilePropertiesViewModel(): base()
        {
            this.documentPropertyPropertyGroup = new DocumentPropertyPropertyGroup();
            processFilesCommand = new RelayCommand(() => ProcessFiles(documentPropertyPropertyGroup.ToCollection()));

            //TestData
            this.DocumentPropertyPropertyGroup.DocumentPropertyCompany.State = "Lean RAQA Systems";
            this.DocumentPropertyPropertyGroup.DocumentPropertyCreator.State = "Lean RAQA Systems";
            this.DocumentPropertyPropertyGroup.DocumentPropertyLastModifiedBy.State = "Lean RAQA Systems";
            this.DocumentPropertyPropertyGroup.DocumentPropertyManager.State = "Lean RAQA Systems";

            //TestData
        }
        public DocumentPropertyPropertyGroup DocumentPropertyPropertyGroup { get => documentPropertyPropertyGroup; set => documentPropertyPropertyGroup = value; }
        public RelayCommand ProcessFilesCommand { get => processFilesCommand; set => processFilesCommand = value; }
    }
}
