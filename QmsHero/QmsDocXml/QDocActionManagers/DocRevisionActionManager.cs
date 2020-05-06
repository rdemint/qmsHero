using DocumentFormat.OpenXml.Drawing;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Docs.Common.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.QDocActionManagers
{
    public class DocRevisionActionManager : QDocActionManager
    {
        public DocRevisionActionManager()
        {
        }

        public DocRevisionActionManager(object currentState) : base(currentState)
        {
        }

        public DocRevisionActionManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        private DocRevisionActionManager(object currentState, QDocPropertyResultCollection resultCollection, int foundCount) : base(currentState, resultCollection, foundCount)
        {
        }

        private DocRevisionActionManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count) : base(currentState, targetState, resultCollection, count)
        {
        }

        public static QDocActionManager Create(string currentState)
        {
            return new DocRevisionActionManager(currentState);
        }

        public static QDocActionManager Create(string currentState, string targetState)
        {
            return new DocRevisionActionManager(currentState, targetState);
        }

        protected static QDocActionManager Create(string currentState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocRevisionActionManager(currentState, resultCollection, replacementCount);
        }

        protected static QDocActionManager Create(string currentStateToFind, string targetState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocRevisionActionManager(currentStateToFind, targetState, resultCollection, replacementCount);
        }

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var col = new QDocPropertyResultCollection();
            var fileResult = doc.Inspect(new FileDocName((string)this.CurrentState));
            if (fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count ++;
            }
            col.Add(fileResult);

            var headerResult = doc.Inspect(new HeaderRevision((string)this.CurrentState));
            if(headerResult.IsSuccess && headerResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count++;
            }
            col.Add(headerResult);

            return col;
        }

        public override QDocPropertyResultCollection Process(Doc doc)
        {
            var col = new QDocPropertyResultCollection();

            var fileResult = doc.Process(new FileDocName((string)this.TargetState));
            if(fileResult.IsSuccess)
                count++;
            col.Add(fileResult);
            
            var headerResult = doc.Process(new HeaderRevision((string)this.TargetState));
            if(headerResult.IsSuccess)
                count++;
            col.Add(headerResult);
            return col;
        }
    }
}
