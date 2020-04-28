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
using QDoc.Docs;
using FluentResults;

namespace QmsDoc.Core
{
    public class DocFactory: QDocFactory
    {
        public override Result<Doc> CreateDoc(FileInfo file)
        {
            if (WordDoc.Extensions().Contains(file.Extension))
            {
                return Results.Ok<Doc>(new WordDoc(file));
            }

            else if (ExcelDoc.Extensions().Contains(file.Extension))
            {
                return Results.Ok<Doc>(new ExcelDoc(file));
            }

            else
            {
                return Results.Fail(
                    new Error($"This factory does not have capability to process files with {file.Extension} extension"));
            }
        }
    }
}
