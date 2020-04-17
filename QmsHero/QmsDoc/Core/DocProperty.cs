using DocumentFormat.OpenXml.Packaging;
using QDoc.Core;
using QDoc.Interfaces;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public abstract class DocProperty : QDocProperty
    {
        public DocProperty()
        {
        }

        public DocProperty(object state) : base(state)
        {
        }

        #region visitor
        public override QDocProperty Read(object doc, object docConfig)
        {
            //Visitor Pattern
            WordprocessingDocument wdoc = doc as WordprocessingDocument;
            WordDocConfig wdocConfig = docConfig as WordDocConfig;

            SpreadsheetDocument sdoc = doc as SpreadsheetDocument;
            ExcelDocConfig sdocConfig = docConfig as ExcelDocConfig;
            
            if(wdoc!=null && wdocConfig!=null)
            {
                return this.Read(wdoc, wdocConfig);
            }

            else if(sdoc!=null && sdocConfig!=null)
            {
                return this.Read(sdoc, sdocConfig);
            }

            else
            {
                throw new ReadDocumentNotValidException();
            }

        }

        public override void Write(object doc, IDocConfig docConfig, object state)
        {
            //Visitor Pattern
            WordprocessingDocument wdoc = doc as WordprocessingDocument;
            WordDocConfig wdocConfig = docConfig as WordDocConfig;

            SpreadsheetDocument sdoc = doc as SpreadsheetDocument;
            ExcelDocConfig sdocConfig = docConfig as ExcelDocConfig;

            if (wdoc != null && wdocConfig != null)
            {
                this.Write(wdoc, wdocConfig);
            }

            else if (sdoc != null && sdocConfig != null)
            {
                this.Write(sdoc, sdocConfig, state);
            }

            else
            {
                throw new ReadDocumentNotValidException();
            }
        }

        #endregion

        #region word

        public virtual DocProperty Read(WordprocessingDocument doc, WordDocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(WordprocessingDocument doc, WordDocConfig config)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region excel
        public virtual DocProperty Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual void Write(SpreadsheetDocument doc, ExcelDocConfig config, object state)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
