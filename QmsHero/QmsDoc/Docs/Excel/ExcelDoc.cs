using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QDoc.Interfaces;
using QmsDoc.Interfaces;

namespace QmsDoc.Docs.Excel
{
    public class ExcelDoc : Doc
    {
        ExcelDocConfig docConfig;
        static List<string> fileExtensions = new List<string> { ".xlsx", ".xls", ".xlsm" };

        public ExcelDoc() {
        }

        public ExcelDoc(FileInfo fileInfo) : this() 
        {
            this.FileInfo = fileInfo;
            DocConfig = new ExcelDocConfig();
        }

        public ExcelDoc(FileInfo fileInfo, ExcelDocConfig docConfig) : this()
        {
            this.FileInfo = fileInfo;
            DocConfig = docConfig;
        }
        public ExcelDocConfig DocConfig { get => docConfig; set => docConfig = value; }

        public override Result<QDocProperty> Process(QDocProperty prop)
        {
            Result<QDocProperty> result;

            try
            {
                if(prop as IWriteFileInfo != null)
                {
                    result = prop.Write(FileInfo, DocConfig);
                }
                else
                {
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
                    {
                       result = prop.Write(doc, DocConfig);
                    }
                }

                return result;
            }

            catch (Exception e)
            {
                return Results.Fail<QDocProperty>(
                    new Error("Failed to process the document")
                    .CausedBy(e)
                    );
            }
            
            
        }

        public override Result<QDocProperty> Inspect(QDocProperty prop)
        {
            Result<QDocProperty> result;
             
            try
            {
                if(prop as IReadFileInfo != null)
                    {
                        result = prop.Read(FileInfo, DocConfig);
                    }
                 else
                    {
                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, false))
                        {
                       result = prop.Read(doc, DocConfig);
                        }
                    }
                return result;
            }

            catch (Exception e)
            {
                return Results.Fail<QDocProperty>(
                    new Error("Failed to inspect the document")
                    .CausedBy(e)
                );
            }
        }

        public static List<string> Extensions()
        {
            return fileExtensions;
        }

    }
}
