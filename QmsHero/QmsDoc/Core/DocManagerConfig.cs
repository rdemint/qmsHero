using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocManagerConfig
    {
        string qmsPwd;
        bool leaveDocumentsOpen;

        public DocManagerConfig()
        {
            this.qmsPwd = "QMSpwd";
            this.leaveDocumentsOpen = false;
        }

        public string QmsPwd { get => qmsPwd; set => qmsPwd = value; }
        public bool LeaveDocumentsOpen { get => leaveDocumentsOpen; set => leaveDocumentsOpen = value; }
    }
}
