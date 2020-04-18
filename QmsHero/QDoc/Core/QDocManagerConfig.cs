
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class QDocManagerConfig : IQDocManagerConfig
    {
        int safeProcessingLength;
        List<string> wordDocExtensions;
        List<string> excelDocExtensions;
        List<string> pdfExtensions;

        public QDocManagerConfig()
        {
            Initialize();
        }

        public int SafeProcessingLength { get => safeProcessingLength; set => safeProcessingLength = value; }
        public List<string> ExcelDocExtensions { get => excelDocExtensions; set => excelDocExtensions = value; }
        public List<string> WordDocExtensions { get => wordDocExtensions; set => wordDocExtensions = value; }
        public List<string> PdfExtensions { get => pdfExtensions; set => pdfExtensions = value; }

        public virtual void Initialize()
        {
            this.SafeProcessingLength = 15;
            this.WordDocExtensions = new List<string> { ".docx", ".doc" };
            this.ExcelDocExtensions = new List<string> { ".xlsx", ".xls", ".xlsm" };
            this.PdfExtensions = new List<string> { ".pdf" };
        }
    }
}

