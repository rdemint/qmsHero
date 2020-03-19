using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using QmsDoc.Core;
using QmsDoc.Interfaces;

namespace QmsHero.ViewModel
{
    public class TestViewModel1: ViewModelBase
    {
        DocActionControls docActions;
        List<IDocActionControl> actionList;

        public TestViewModel1()
        {
            this.docActions = new DocActionControls();
            this.actionList = docActions.ToDocActionControlList();
        }

        public DocActionControls DocActions { get => docActions; set => docActions = value; }
        public List<IDocActionControl> ActionList { get => actionList; set => actionList = value; }
    }
}
