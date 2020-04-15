using QmsDoc.Core;
using QmsDoc.Docs;
using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWordDoc
{
    class WordDocActionManager
    {

        public void Process(string actionName)
        {
            var action = Type.GetType("Revision");
            
        }

        public DocProperty Do(WordDoc doc, IDocCheck check) 
        {
            
        }
    }
}
