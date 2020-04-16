using DocumentFormat.OpenXml.Packaging;
using QmsDoc.Core;
using QmsDoc.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Docs
{
    public class WordDocProperty : DocProperty
    {
        public WordDocProperty():base() { }

        public WordDocProperty(string value) : base(value){ }


    public virtual WordDocProperty Get(WordprocessingDocument doc)
    {
        throw new NotImplementedException();
    }

    public virtual void Set(WordprocessingDocument doc)
    {
        throw new NotImplementedException();

    }
   }
}
