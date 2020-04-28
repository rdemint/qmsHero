
using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QDoc.Interfaces;
using QmsDoc.Docs.Common;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using QmsDoc.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public abstract class DocProperty : QDocProperty, INotifyPropertyChanged
    {
        public DocProperty()
        {
        }

        public DocProperty(object state) : base(state)
        {
        }

        #region visitor
        public override Result<QDocProperty> Read(object doc, object config)
        {
            //Visitor Pattern
            WordprocessingDocument wdoc = doc as WordprocessingDocument;
            WordDocConfig wdocConfig = config as WordDocConfig;

            SpreadsheetDocument sdoc = doc as SpreadsheetDocument;
            ExcelDocConfig sdocConfig = config as ExcelDocConfig;

            FileInfo file = doc as FileInfo;
            DocConfig docConfig = config as DocConfig;


            if(wdoc!=null && wdocConfig!=null)
            {
                return this.Read(wdoc, wdocConfig);
            }

            else if(sdoc!=null && sdocConfig!=null)
            {
                return this.Read(sdoc, sdocConfig);
            }

            else if(file!=null && docConfig !=null)
            {
                return this.Read(file, docConfig);
            }

            else
            {
                return Results.Fail(
                        new Error("Could not read the doc")
                            .CausedBy(new DocProcessingException())
                    );
            }

        }

        public override Result<QDocProperty> Write(object doc, IDocConfig config)
        {
            //Visitor Pattern
            //switch(doc)
            //{
            //    case WordprocessingDocument wdoc:
            //        return this.Write(wdoc, config);
            //        break;
            //    default:
            //        return Results.Fail(new Error("Cannot write to this document type"));
            //}

            //Maybe this will work?
            //return this.Write(doc, config);


            WordprocessingDocument wdoc = doc as WordprocessingDocument;
            WordDocConfig wdocConfig = config as WordDocConfig;

            SpreadsheetDocument sdoc = doc as SpreadsheetDocument;
            ExcelDocConfig sdocConfig = config as ExcelDocConfig;

            FileInfo file = doc as FileInfo;
            DocConfig docConfig = config as DocConfig;

            if (wdoc != null && wdocConfig != null)
            {
                return this.Write(wdoc, wdocConfig);
            }

            else if (sdoc != null && sdocConfig != null)
            {
                return this.Write(sdoc, sdocConfig);
            }

            else if (file != null && docConfig != null)
            {
                return this.Write(file, docConfig);
            }

            else
            {
                return Results.Fail(
                        new Error("Could not write to the doc")
                            .CausedBy(new DocProcessingException())
                    );
            }
        }

        #endregion

        #region word

        public virtual Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig config)
        {
            throw new NotImplementedException();
        }


        public virtual Result<QDocProperty> Read(FileInfo file, DocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual Result<QDocProperty> Write(FileInfo file, DocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig config)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region excel
        public virtual Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            throw new NotImplementedException();
        }

        public virtual Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
