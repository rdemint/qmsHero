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
    public abstract class DocPropertyGroupManager
    {
        //Coordinates updates to QDocProperties
        object currentState;
        object targetState;
        string name;
        QDocPropertyResultCollection resultCollection;

        protected DocPropertyGroupManager()
        {
            this.name = this.GetType().Name;
            this.resultCollection = new QDocPropertyResultCollection();
        }

        protected DocPropertyGroupManager(object currentState, object targetState) : this()
        {
            CurrentState = currentState;
            TargetState = targetState;
        }

        protected DocPropertyGroupManager(object currentState, QDocPropertyResultCollection resultCollection): this()        {
            CurrentState = currentState;
            ResultCollection = resultCollection;
        }

        protected DocPropertyGroupManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection): this()
        {
            ResultCollection = resultCollection;
            CurrentState = currentState;
            TargetState = targetState;
        }

        public object CurrentState { get => currentState; set => currentState = value; }
        public object TargetState { get => targetState; set => targetState = value; }
        public QDocPropertyResultCollection ResultCollection { get => resultCollection; set => resultCollection = value; }
        public string Name { get => name;}

        public abstract Result<DocPropertyGroupManager> Inspect(ExcelDoc doc);

        public abstract Result<DocPropertyGroupManager> Inspect(WordDoc doc);

        public abstract Result<DocPropertyGroupManager> Process(WordDoc doc);

        public abstract Result<DocPropertyGroupManager> Process(ExcelDoc doc);

    }
}
