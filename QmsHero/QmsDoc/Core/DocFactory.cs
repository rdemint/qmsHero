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
using QDoc.Interfaces;

namespace QmsDoc.Core
{
    public class DocFactory: IQDocFactory
    {
        public IDoc CreateDoc(FileInfo file)
        {
            if (WordDoc.Extensions().Contains(file.Extension))
            {
                return new WordDoc(file);
            }

            else if (ExcelDoc.Extensions().Contains(file.Extension))
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
