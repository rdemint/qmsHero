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
        public ExcelDoc() { }

        public ExcelDoc(FileInfo fileInfo) : base(fileInfo) { }

        public ExcelDoc(FileInfo fileInfo, IDocConfig docConfig) : base(fileInfo, docConfig) { }

        public override void Process(QDocProperty prop)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, false))
            {
                prop.Write(doc, DocConfig);
            }
        }
        public override QDocProperty Inspect(QDocProperty prop)
        {
            QDocProperty result = null;
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(this.FileInfo.FullName, false))
            {
                
                if(prop as IReadFileInfo != null)
                {
                    result = prop.Read(FileInfo, DocConfig);
                }
                else
                {
                    result = prop.Read(doc, DocConfig);
                }
            }
            return result;
        }

        public override IDocState Inspect(IDocState docState)
        {
            return base.Inspect(docState);
        }


    }
}
