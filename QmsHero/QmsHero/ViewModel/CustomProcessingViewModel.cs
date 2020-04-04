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
        DocActionControlManager docActions;
        List<IDocActionControl> actionList;
        RelayCommand processFilesCommand;

        public CustomProcessingViewModel()
        {

            this.ViewDisplayName = "Custom";
            this.docActions = SimpleIoc.Default.GetInstance<DocActionControlManager>();
            this.actionList = docActions.ToControlList();
            this.manager = SimpleIoc.Default.GetInstance<DocManager>();
            this.manager.ConfigDir("C:\\Users\\raine\\Documents\\Dev\\qmsHero\\QmsHero\\QmsDoc.Test\\Fixtures\\Active QMS Documents\\SOP-001 Quality Manual Documents");
        }

        public DocActionControlManager DocActions { get => docActions; set => docActions = value; }
        public List<IDocActionControl> ActionList { get => actionList; set => actionList = value; }
        public RelayCommand ProcessFilesCommand {
            get { 
                if (this.processFilesCommand == null)
                {
                    this.processFilesCommand = new RelayCommand(
                            () => this.manager.ProcessFiles(ActionList, true),
                            this.CanProcessFiles()
                        ) ;
                }
                return this.processFilesCommand;
            } 
            set => processFilesCommand = value; }

        public string ViewDisplayName { get => viewDisplayName; set => viewDisplayName = value; }

        public bool CanProcessFiles()
        {
            return true;
        }

        public void ProcessFiles()
        {
            try
            {
                this.manager.ProcessFiles(this.ActionList, true);
            }
            catch (Exception e)
            {
                this.manager.CloseApps();
                throw e;
            }
        }
    }
}
