using QDoc.Core;
using QmsDoc.Core;
using QmsDoc.Docs.Common.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.StateGroups
{
    class FileStateGroup: QDocStateGroup
    {
        FileDocName fileDocName;
        FileDocNumber fileDocNumber;
        FileRevision filerevision;
        public FileStateGroup()
        {
            this.fileDocName = new FileDocName();
            this.fileDocNumber = new FileDocNumber();
            this.filerevision = new FileRevision();
        }

        public FileRevision Filerevision { get => filerevision; set => filerevision = value; }
        public FileDocNumber FileDocNumber { get => fileDocNumber; set => fileDocNumber = value; }
        public FileDocName FileDocName { get => fileDocName; set => fileDocName = value; }
    }
}
