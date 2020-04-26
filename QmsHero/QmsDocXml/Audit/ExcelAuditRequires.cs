using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using QmsDoc.Exceptions;

namespace QmsDocXml.Audit
{
    public static class ExcelAuditRequires
    {
        public static void HeaderSameOddEven(SpreadsheetDocument doc)
        {
            var workSheetParts = doc.WorkbookPart.WorksheetParts.ToList();
            foreach (var workSheetPart in workSheetParts)
            {
                foreach (var header in workSheetPart.Worksheet.Elements<HeaderFooter>().ToList())
                {
                    if (header.DifferentOddEven != null && header.DifferentOddEven)
                    {
                        throw new MultipleHeadersExistException();
                    }
                }
            }
        }
    }
}
