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
using GalaSoft.MvvmLight;

namespace QmsDoc.Core
{
    public abstract class DocPropertyGroupManager: ObservableObject
    {
        //Coordinates updates to QDocProperties
        object currentState;
        object targetState;
        string name;
        int count;

        QDocPropertyResultCollection resultCollection;

        protected DocPropertyGroupManager()
        {
            this.name = this.GetType().Name;
            this.resultCollection = new QDocPropertyResultCollection();
        }

        protected DocPropertyGroupManager(object currentState): this()
        {
            this.currentState = currentState;
        }

        protected DocPropertyGroupManager(object currentState, object targetState) : this()
        {
            CurrentState = currentState;
            TargetState = targetState;
        }

        protected DocPropertyGroupManager(object currentState, QDocPropertyResultCollection resultCollection, int foundCount): this()        {
            CurrentState = currentState;
            ResultCollection = resultCollection;
            Count = foundCount;
        }

        protected DocPropertyGroupManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count): this()
        {
            ResultCollection = resultCollection;
            CurrentState = currentState;
            TargetState = targetState;
            Count = count;
        }

        public object CurrentState
        {
            get => currentState;
            set { 
                Set<object>(() => CurrentState, ref currentState, value);
            }
        }
        public object TargetState
        {
            get => targetState;
            set { 
                Set<object>(() => TargetState, ref targetState, value);
            }
        }
        public QDocPropertyResultCollection ResultCollection { get => resultCollection; set => resultCollection = value; }
        public string Name { get => name;}
        public int Count
        {
            get => count;
            set {
                Set<int>(() => Count, ref count, value);

            }
        }

        public abstract Result<DocPropertyGroupManager> Inspect(ExcelDoc doc);

        public abstract Result<DocPropertyGroupManager> Inspect(WordDoc doc);

        public abstract Result<DocPropertyGroupManager> Process(WordDoc doc);

        public abstract Result<DocPropertyGroupManager> Process(ExcelDoc doc);

        public int SuccessCount()
        {
            return this.ResultCollection.Where(result => result.IsSuccess).Count();
        }

        public int FailureCount()
        {
            return this.ResultCollection.Where(result => result.IsFailed).Count();
        }
    }
}
