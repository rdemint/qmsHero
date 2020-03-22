using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Controls
{
    class ControlTextBox: ControlBase
    {
        public ControlTextBox(): base()
        {

        }

        public ControlTextBox(string Name, object Value): base(Name, Value) { }
    }
}
