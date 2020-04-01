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
        public ControlCheckTextBox(string displayValue, bool qInit) : base(displayValue, qInit) { }
    }
}
