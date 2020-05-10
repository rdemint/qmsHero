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
            this.referenceDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Reference";
            this.processingDirPath = "C:\\Users\\raine\\Desktop\\qmsProcessing\\Test\\Processing";
        }

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
