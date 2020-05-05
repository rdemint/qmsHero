﻿using FluentResults;
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
    public abstract class DocPropertyGroupManager
    {
        //Coordinates updates to QDocProperties
        protected object currentState;
        protected object targetState;
        string name;
        protected int count;

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
            this.currentState = currentState;
            this.targetState = targetState;
        }

        protected DocPropertyGroupManager(object currentState, QDocPropertyResultCollection resultCollection, int foundCount): this()        {
            this.currentState = currentState;
            this.resultCollection = resultCollection;
            this.count = foundCount;
        }

        protected DocPropertyGroupManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count): this()
        {
            ResultCollection = resultCollection;
            this.currentState = currentState;
            this.targetState = targetState;
            this.count = count;
        }

        public object CurrentState
        {
            get => currentState;
        }
        public object TargetState
        {
            get => targetState;
        }
        public QDocPropertyResultCollection ResultCollection { get => resultCollection; set => resultCollection = value; }
        public string Name { get => name;}
        public int Count
        {
            get => count;
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
