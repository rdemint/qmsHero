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
        int currentStateCount;
        object targetState;
        int targetStateCount;
        string name;
        QDocPropertyResultCollection resultCollection;

        public DocPropertyGroupManager()
        {
            this.name = this.GetType().Name;
            this.resultCollection = new QDocPropertyResultCollection();
        }
        
        public DocPropertyGroupManager(object currentState): this()
        {
            CurrentState = currentState;
        }

        public DocPropertyGroupManager(object currentState, QDocPropertyResultCollection resultCollection): this(currentState)
        {
            ResultCollection = resultCollection;
        }

        public DocPropertyGroupManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection): this(currentState)
        {
            ResultCollection = resultCollection;
            TargetState = targetState;
        }

        public object CurrentState { get => currentState; set => currentState = value; }
        public object TargetState { get => targetState; set => targetState = value; }
        public QDocPropertyResultCollection ResultCollection { get => resultCollection; set => resultCollection = value; }
        public string Name { get => name;}
        public int TargetStateCount { get => targetStateCount; set => targetStateCount = value; }
        public int CurrentStateCount { get => currentStateCount; set => currentStateCount = value; }

        public abstract Result<DocPropertyGroupManager> Audit(ExcelDoc doc);

        public abstract Result<DocPropertyGroupManager> Audit(WordDoc doc);

        public abstract Result<DocPropertyGroupManager> Process(WordDoc doc);

        public abstract Result<DocPropertyGroupManager> Process(ExcelDoc doc);

    }
}
