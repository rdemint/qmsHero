using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Interfaces;

namespace QmsDoc.Core
{
    public class DocAction
    {
        string name;
        string value;
        IDocActionControl docActionControl;
        public DocAction()
        {

        }
    }
}
