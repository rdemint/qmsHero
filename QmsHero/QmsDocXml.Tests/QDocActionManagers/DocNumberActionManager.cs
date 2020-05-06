using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Docs.Common.Properties;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Tests.QDocActionManagers
{
    public class DocNumberActionManager : TextActionManager
    {
        private DocNumberActionManager()
        {
        }

        public DocNumberActionManager(object currentState) : base(currentState)
        {
        }

        public DocNumberActionManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        private DocNumberActionManager(object currentState, QDocPropertyResultCollection resultCollection, int foundCount) : base(currentState, resultCollection, foundCount)
        {
        }

        private DocNumberActionManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count) : base(currentState, targetState, resultCollection, count)
        {
        }

        public static QDocActionManager Create(string currentState)
        {
            return new DocNumberActionManager(currentState);
        }

        public static QDocActionManager Create(string currentState, string targetState)
        {
            return new DocNumberActionManager(currentState, targetState);
        }

        protected static QDocActionManager Create(string currentState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocNumberActionManager(currentState, resultCollection, replacementCount);
        }

        protected static QDocActionManager Create(string currentStateToFind, string targetState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocNumberActionManager(currentStateToFind, targetState, resultCollection, replacementCount);
        }

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var fileResult = doc.Inspect(new FileDocNumber((string)this.CurrentState));
            if (fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count++;
            }
            PropertyResultCollection.Add(fileResult);
            return base.Inspect(doc);
        }

        public override QDocPropertyResultCollection Process(Doc doc)
        {
            var fileResult = doc.Process(new FileDocNumber((string)this.TargetState));
            if (fileResult.IsSuccess)
                count++;
            PropertyResultCollection.Add(fileResult);
            return base.Process(doc);
        }


    }
}
