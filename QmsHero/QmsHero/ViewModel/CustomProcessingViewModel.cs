using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using QmsDoc.Core;
using QmsDoc.Interfaces;

namespace QmsHero.ViewModel
{
    public class CustomProcessingViewModel: ViewModelBase
    {
        DocManager manager;
        DocActionControls docActions;
        List<IDocActionControl> actionList;
        RelayCommand processFilesCommand;

        public CustomProcessingViewModel()
        {
            this.docActions = new DocActionControls();
            this.actionList = docActions.ToDocActionControlList();
            this.manager = new DocManager();
            this.manager.ConfigDir("C:\\Users\\raine\\Documents\\Dev\\qmsHero\\QmsHero\\QmsDoc.Test\\Fixtures\\Active QMS Documents\\SOP-001 Quality Manual Documents");
        }

        public DocActionControls DocActions { get => docActions; set => docActions = value; }
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

        public bool CanProcessFiles()
        {
            return true;
        }
    }
}
