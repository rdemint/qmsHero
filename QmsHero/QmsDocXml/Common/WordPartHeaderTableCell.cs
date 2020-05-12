using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FluentResults;

namespace QmsDocXml
{
    public static class WordPartHeaderTableCell
    {
        public static Result<TableCell> Get(WordprocessingDocument doc, int row, int col)
        {
            foreach(var table in doc.MainDocumentPart?.HeaderParts.FirstOrDefault().RootElement.Elements<Table>())
            {
                
                if(table != null && table.Elements<TableRow>().Count() > row)
                {
                    var tableRows = table.Descendants<TableRow>().ToList();
                            TableRow r = table.Elements<TableRow>().ElementAt(row);
                            TableCell cell = r.Elements<TableCell>().ElementAt(col);
                            return Results.Ok<TableCell>(cell);
                }
            }
            return Results.Fail(new Error("Did not the table cell in the header of the document."));
        }
    }
}
