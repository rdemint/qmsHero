using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocConfigControlManager: DocControlManagerBase

    {


        public DocConfigControlManager(): base()
        {
            this.Initialize();
        }



        public void Initialize()
        {
            //this.QmsPwd = new ControlTextBox("QmsPwd", "QMSpwd");
            //this.LeaveDocumentsOpen = new ControlCheckBox("LeaveDocumentsOpen", null);
            //this.topDir = new ControlFolderPicker("TopDir", null);

        }
    }
}
