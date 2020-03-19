using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;

namespace QmsDoc.Core
{
    public class DocActionControlFolderPicker: DocActionControlBase
    {
        public DocActionControlFolderPicker(): base()  {}
        public DocActionControlFolderPicker(string docActionName, object docActionVal): base(docActionName, docActionVal) { }
    }
}
