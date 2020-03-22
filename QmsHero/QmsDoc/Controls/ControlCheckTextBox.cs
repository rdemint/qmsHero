using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Controls
{
    public class ControlCheckTextBox: ControlBase
    {
        public ControlCheckTextBox(): base()
        {

        }
        public ControlCheckTextBox(string docActionName, object docActionVal, bool controlIsEnabled=true) : base(docActionName, docActionVal, controlIsEnabled) { }
    }
}
