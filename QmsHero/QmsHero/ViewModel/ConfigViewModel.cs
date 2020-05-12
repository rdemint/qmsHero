using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Interfaces;
using QDoc.Core;
using QmsDoc.Core;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;

namespace QmsHero.ViewModel
{
    public class ConfigViewModel : ViewModelBase
    {
        string referenceDirPath;
        string processingDirPath;
        DocManager manager;
        string viewDisplayName;
        public ConfigViewModel()
        {
            this.viewDisplayName = "Configure Project Directories";
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.ReferenceDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Dot Cup\\QMS";
            this.ProcessingDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Dot Cup\\QMS_Processing1";

        }

        public string ReferenceDirPath
        {
            get => referenceDirPath;
            set
            {
                Set<string>(
                    () => ReferenceDirPath, ref referenceDirPath, value
                );

                //this.manager = ServiceLocator.Current.GetInstance<DocManager>();
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
                //this.manager = SimpleIoc.Default.GetInstance<DocManager>();
                manager.FileManager.SetProcessingDir(value);
            }

        }

        public bool ProcessingDirIsValid()
        {
            return manager.DirIsValid(processingDirPath);
        }

        public bool ReferenceDirIsValid()
        {
            return manager.DirIsValid(referenceDirPath);
        }

    }
}
