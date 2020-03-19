using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class ControlTextBox: ControlBase
    {
        public ControlTextBox(): base()
        {

        }
        public ControlTextBox(string docActionName, object docActionVal, bool controlIsEnabled=true) : base(docActionName, docActionVal, controlIsEnabled) { }
    }
}
