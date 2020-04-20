using QDoc.Core;
using QDoc.Interfaces;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    class DocFactory: QDocFactory
    {
        public IDoc CreateDoc(FileInfo file)
        {
            if (WordDocExtensions.Contains(file.Extension))
            {
                return new WordDoc(file);
            }

            else if (ExcelDocExtensions.Contains(file.Extension))
            {
                return new ExcelDoc(file);
            }

            else
            {
                return null;
            }
        }
    }
}
