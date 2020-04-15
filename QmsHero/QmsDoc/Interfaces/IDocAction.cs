using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Interfaces
{
    public interface IDocAction
    {
        void Config();
        //tell where the EffectiveDaterow is, for instance

        string Inspect();
        //get the current value of a document

        void Do();
        //Perform the action

    }
}
