using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using QDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Exceptions;

namespace QmsDoc.Core
{
    public abstract class DocAudit : QDocAudit
    {
        protected DocAudit(): base()
        {
        }

        public override void Audit(object doc, object docConfig)
        {
            //Visitor Pattern
            WordprocessingDocument wdoc = doc as WordprocessingDocument;
            WordDocConfig wdocConfig = docConfig as WordDocConfig;

            SpreadsheetDocument sdoc = doc as SpreadsheetDocument;
            ExcelDocConfig sdocConfig = docConfig as ExcelDocConfig;

            FileInfo file = doc as FileInfo;

            
            try
            {
                if (wdoc != null && wdocConfig != null)
                {
                    this.Audit(wdoc, wdocConfig);
                }

                else if (sdoc != null && sdocConfig != null)
                {
                    this.Audit(sdoc, sdocConfig);
                }

                else if (file != null && wdocConfig != null)
                {
                    this.Audit(file, wdocConfig);
                }

                else if (file != null && sdocConfig != null)
                {
                    this.Audit(file, sdocConfig);
                }

                else
                {
                    throw new ReadDocumentNotValidException();
                }
            }

            catch (Exception e)
            {
                this.Add(e);
            }
            
        }

        public virtual void Audit(WordprocessingDocument doc, WordDocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual void Audit(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual void Audit(FileInfo doc, WordDocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual void Audit(FileInfo doc, ExcelDocConfig config)
        {
            throw new NotImplementedException();
        }

    }
}
