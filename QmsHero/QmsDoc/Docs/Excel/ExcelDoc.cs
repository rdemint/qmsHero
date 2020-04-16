using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Docs.Excel;
namespace QmsDoc.Docs.Excel
{
    public class QmsExcelDoc : QExcelDoc
    {
        public QmsExcelDoc(FileInfo fileInfo) : base(fileInfo)
        {
        }
    }
}
