using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace QmsDocXml.Docs.Word.Properties
{
    public static class HeaderTableCell
    {
        public static TableCell Get(WordprocessingDocument doc, int row, int col)
        {
            var table = doc.MainDocumentPart?.HeaderParts.FirstOrDefault().RootElement.Elements<Table>().FirstOrDefault();
            TableRow r = table.Elements<TableRow>().ElementAt(row);
            TableCell cell = r.Elements<TableCell>().ElementAt(col);
            return cell;
        }
    }
}
