using QDoc.Docs;
using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public abstract class QDocFactory
    {
        List<string> wordDocExtensions;
        List<string> excelDocExtensions;
        List<string> pdfExtensions;

        public QDocFactory()
        {
            wordDocExtensions = new List<string> { ".docx", ".doc" };
            excelDocExtensions = new List<string> { ".xlsx", ".xls", ".xlsm" };
            pdfExtensions = new List<string> { ".pdf" };
        }

        public List<string> WordDocExtensions { get => wordDocExtensions; set => wordDocExtensions = value; }
        public List<string> ExcelDocExtensions { get => excelDocExtensions; set => excelDocExtensions = value; }
        public List<string> PdfExtensions { get => pdfExtensions; set => pdfExtensions = value; }

        public abstract Doc CreateDoc(FileInfo file);

    }
}
