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
            foreach(var headerPart in doc.MainDocumentPart?.HeaderParts)
            {
                foreach(var table in headerPart.RootElement.Elements<Table>())
                {
                
                    if(table != null && table.Elements<TableRow>().Count() > row)
                    {
                        var tableRows = table.Descendants<TableRow>().ToList();
                        TableRow r = table.Elements<TableRow>().ElementAt(row);
                        if(r != null)
                        {
                             TableCell cell = r.Elements<TableCell>().ElementAt(col);
                            if(cell != null)
                            {
                                return Results.Ok<TableCell>(cell);
                            }
                        }
                    }
                }
            }
            return Results.Fail(new Error("Did find not the table cell in the header of the document."));
        }
    }
}
