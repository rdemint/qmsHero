using QDoc.Core;
using QmsDoc.Core;
using QmsDoc.Docs.Common.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs.Common.PropertyGroups
{
    public class FilePropertyGroup: QDocPropertyGroup
    {
        FileDocName fileDocName;
        FileDocNumber fileDocNumber;
        FileRevision fileRevision;
        public FilePropertyGroup()
        {
            this.fileDocName = new FileDocName();
            this.fileDocNumber = new FileDocNumber();
            this.fileRevision = new FileRevision();
        }

        public FileRevision FileRevision { get => fileRevision; set => fileRevision = value; }
        public FileDocNumber FileDocNumber { get => fileDocNumber; set => fileDocNumber = value; }
        public FileDocName FileDocName { get => fileDocName; set => fileDocName = value; }
    }
}
