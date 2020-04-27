using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Interfaces
{
    interface IToQDocState
    {
        QDocPropertyCollection ToDocState(bool filter);
    }
}
