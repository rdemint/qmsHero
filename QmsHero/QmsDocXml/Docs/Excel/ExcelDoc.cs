using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Docs.Excel;

namespace QmsDocXml.Docs.Excel
{
    public class ExcelDoc : QmsExcelDoc
    {
        public ExcelDoc(FileInfo fileInfo) : base(fileInfo)
        {
        }
    }
}
