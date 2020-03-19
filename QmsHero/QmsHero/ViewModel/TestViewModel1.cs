using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using QmsDoc.Core;

namespace QmsHero.ViewModel
{
    public class TestViewModel1: ViewModelBase
    {
        DocActions docActions;
        List<DocAction> actionList;

        public TestViewModel1()
        {
            this.docActions = new DocActions();
            this.actionList = docActions.ToDocActionList();
        }

        public DocActions DocActions { get => docActions; set => docActions = value; }
        public List<DocAction> ActionList { get => actionList; set => actionList = value; }
    }
}
