using FluentResults;
using DocumentFormat.OpenXml.Packaging;
using Wxml = DocumentFormat.OpenXml.Wordprocessing;
using Sxml = DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QmsDoc.Docs.Word;
using QmsDoc.Docs.Excel;
using QDoc.Core;

namespace QmsDoc.Core
{
    public abstract class DocAction
    {
        //Coordinates updates to QDocProperties
        object state;
        string name;
        QDocPropertyResultCollection resultCollection;

        public DocAction()
        {
            this.name = this.GetType().Name;
            this.resultCollection = new QDocPropertyResultCollection();
        }
        
        public DocAction(object state): this()
        {
            State = state;
        }

        public object State { get => state; set => state = value; }
        public QDocPropertyResultCollection ResultCollection { get => resultCollection; set => resultCollection = value; }
        public string Name { get => name;}

        public abstract Result<DocAction> Inspect(ExcelDoc doc);

        public abstract Result<DocAction> Inspect(WordDoc doc);

        public abstract Result<DocAction> Process(WordDoc doc);

        public abstract Result<DocAction> Process(ExcelDoc doc);

    }
}
