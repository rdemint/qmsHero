using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
using QDoc.Docs;
using System.Runtime.CompilerServices;
using QDoc.Interfaces;

namespace QDoc.Core
{
    public abstract class QDocActionManager
    {
        //Coordinates updates to QDocProperties
        protected object currentState;
        protected object targetState;
        string name;
        protected int count;
        protected QDocActionManager()
        {
            this.name = this.GetType().Name;
        }

        protected QDocActionManager(object currentState): this()
        {
            this.currentState = currentState;
        }

        protected QDocActionManager(object currentState, object targetState) : this(currentState)
        {
            this.targetState = targetState;
        }

        protected QDocActionManager(object currentState, int foundCount): this(currentState)        {
            this.count = foundCount;
        }

        protected QDocActionManager(object currentState, object targetState, int count): this(currentState, targetState)
        {
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

    }
}
