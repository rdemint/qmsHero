using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
using QDoc.Docs;

namespace QDoc.Core
{
    public abstract class QDocActionManager
    {
        //Coordinates updates to QDocProperties
        protected object currentState;
        protected object targetState;
        string name;
        protected int count;

        QDocPropertyResultCollection resultCollection;

        protected QDocActionManager()
        {
            this.name = this.GetType().Name;
            this.resultCollection = new QDocPropertyResultCollection();
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
            this.resultCollection = resultCollection;
            this.count = foundCount;
        }

        protected QDocActionManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count): this()
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

        public abstract Result<QDocActionManager> Inspect(Doc doc);

        public abstract Result<QDocActionManager> Process(Doc doc);


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
