using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Controls;

namespace QmsDoc.Core
{
    public class DocConfigControlManager: DocControlManagerBase

    {
        ControlFolderPicker topDir;
        ControlTextBox qmsPwd;
        ControlCheckBox leaveDocumentsOpen;


        public DocConfigControlManager(): base()
        {
            this.Initialize();
        }

        public ControlTextBox QmsPwd { get => qmsPwd; set => qmsPwd = value; }
        public ControlCheckBox LeaveDocumentsOpen { get => leaveDocumentsOpen; set => leaveDocumentsOpen = value; }
        public ControlFolderPicker TopDir { get => topDir; set => topDir = value; }

        public void Initialize()
        {
            this.QmsPwd = new ControlTextBox("QmsPwd", "QMSpwd");
            this.LeaveDocumentsOpen = new ControlCheckBox("LeaveDocumentsOpen", null);
            this.topDir = new ControlFolderPicker("TopDir", null);
        }
    }
}
