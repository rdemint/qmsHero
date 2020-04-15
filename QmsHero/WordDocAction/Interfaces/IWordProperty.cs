using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QmsDoc.Docs;

namespace QmsDoc.Word.Interfaces
{
    interface IWordProperty
    {
        string Get(WordprocessingDocument doc, WordDocConfig config);
        void Set(WordprocessingDocument doc, WordDocConfig config);

    }
}
