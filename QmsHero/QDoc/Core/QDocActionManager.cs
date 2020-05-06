﻿using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
using QDoc.Docs;
using System.Runtime.CompilerServices;

namespace QDoc.Core
{
    public abstract class QDocActionManager
    {
        //Coordinates updates to QDocProperties
        protected object currentState;
        protected object targetState;
        string name;
        protected int count;
        protected QDocPropertyResultCollection propertyResultCollection;

        protected QDocActionManager()
        {
            this.name = this.GetType().Name;
            this.propertyResultCollection = new QDocPropertyResultCollection();
        }

        protected QDocActionManager(object currentState): this()
        {
            this.currentState = currentState;
        }

        protected QDocActionManager(object currentState, object targetState) : this()
        {
            this.currentState = currentState;
            this.targetState = targetState;
        }

        protected QDocActionManager(object currentState, QDocPropertyResultCollection resultCollection, int foundCount): this()        {
            this.currentState = currentState;
            this.propertyResultCollection = resultCollection;
            this.count = foundCount;
        }

        protected QDocActionManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count): this()
        {
            PropertyResultCollection = resultCollection;
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
        public QDocPropertyResultCollection PropertyResultCollection { get => propertyResultCollection; set => propertyResultCollection = value; }
        public string Name { get => name;}
        public int Count
        {
            get => count;
        }

        public virtual QDocPropertyResultCollection Inspect(Doc doc)
        {
            throw new NotImplementedException();
        }

        public virtual QDocPropertyResultCollection Process(Doc doc)
        {
            throw new NotImplementedException();
        }


        public int SuccessCount()
        {
            return this.PropertyResultCollection.Where(result => result.IsSuccess).Count();
        }

        public int FailureCount()
        {
            return this.PropertyResultCollection.Where(result => result.IsFailed).Count();
        }

    }
}
