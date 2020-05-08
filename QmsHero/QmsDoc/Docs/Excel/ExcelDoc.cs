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
using QmsDoc.Core;
using QmsDoc.Interfaces;

namespace QmsDoc.Docs.Excel
{
    public class ExcelDoc : Doc
    {
        ExcelDocConfig docConfig;
        static List<string> fileExtensions = new List<string> { ".xlsx", ".xls", ".xlsm" };

        public ExcelDoc(): base() {
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

        public override QDocPropertyResultCollection Process(QDocActionManager action)
        {
            return action.Process(this);
        }
        
        
        public override Result<QDocProperty> Process(QDocProperty prop)
        {
            Result<QDocProperty> result;

            if(prop as IWriteFileInfo != null)
            {
                result = prop.Write(FileInfo, DocConfig);
            }
                
            else if(prop as IWriteDocRegex !=null)
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
                {
                    result = prop.Write(doc);
                }
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

        public QDocPropertyResultCollection Inspect(QDocActionManager action)
        {
            return action.Inspect(this);
        }
            
        
        public override Result<QDocProperty> Inspect(QDocProperty prop)
        {
            Result<QDocProperty> result;
             
            if(prop as IReadFileInfo != null)
                {
                    result = prop.Read(FileInfo, DocConfig);
                }

            else if (prop as IReadDocRegex != null)
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
                {
                    result = prop.Read(doc);
                }
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

        public static List<string> Extensions()
        {
            return fileExtensions;
        }

    }
}
