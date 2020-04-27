using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
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

        public override void Process(QDocProperty prop)
        {
            if(prop as IWriteFileInfo != null)
            {
                prop.Write(FileInfo, DocConfig);
            }
            else
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
                {
                    prop.Write(doc, DocConfig);
                }
            }
            
            
        }

        public override void Process(QDocState docState)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, true))
            {
                foreach(QDocProperty prop in docState)
                {
                    Process(prop);
                }
            }
        }
        public override QDocProperty Inspect(QDocProperty prop)
        {
            QDocProperty result = null;
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

        public override QDocState Inspect(QDocState docState)
        {
            QDocState returnState = new QDocState();
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, false))
            {
                foreach(QDocProperty prop in docState)
                {
                    returnState.Add(Inspect(prop));
                }
            }

            return returnState;
        }

        public static List<string> Extensions()
        {
            return fileExtensions;
        }

    }
}
