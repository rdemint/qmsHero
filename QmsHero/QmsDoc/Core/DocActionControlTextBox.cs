using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocActionControlTextBox: DocActionControlBase
    {
        public DocActionControlTextBox(): base()
        {

        }
        public DocActionControlTextBox(string docActionName, object docActionVal) : base(docActionName, docActionVal) { }
    }
}
