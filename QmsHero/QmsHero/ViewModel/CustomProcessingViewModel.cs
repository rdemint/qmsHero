using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QmsDoc.Core;
using QmsDoc.Interfaces;
using GalaSoft.MvvmLight.Ioc;


namespace QmsHero.ViewModel
{
    public class CustomProcessingViewModel: ViewModelBase
    {
        string viewDisplayName;
        DocManager manager;
        RelayCommand processFilesCommand;
        DocEdit docEdit;

        public CustomProcessingViewModel()
        {

            this.ViewDisplayName = "Custom";
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.manager.ConfigDir("C:\\Users\\raine\\Documents\\Dev\\qmsHero\\QmsHero\\QmsDoc.Test\\Fixtures\\Active QMS Documents\\SOP-001 Quality Manual Documents");
        }

        public RelayCommand ProcessFilesCommand {
            get { 
                if (this.processFilesCommand == null)
                {
                    this.processFilesCommand = new RelayCommand(
                            () => this.manager.ProcessFiles(this.DocEdit, true),
                            this.CanProcessFiles()
                        ) ;
                }
                return this.processFilesCommand;
            } 
            set => processFilesCommand = value; }

        public string ViewDisplayName { get => viewDisplayName; set => viewDisplayName = value; }
        public DocEdit DocEdit { get => docEdit; set => docEdit = value; }

        public bool CanProcessFiles()
        {
            return true;
        }

        public void ProcessFiles()
        {
            try
            {
                this.manager.ProcessFiles(this.DocEdit, true);
            }
            catch (Exception e)
            {
                this.manager.CloseApps();
                throw e;
            }
        }
    }
}
