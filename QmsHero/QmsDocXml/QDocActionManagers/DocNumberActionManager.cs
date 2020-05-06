using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Docs.Common.Properties;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.QDocActionManagers
{
    public class DocNumberActionManager : TextActionManager
    {
        public DocNumberActionManager(object currentState) : base(currentState)
        {
        }

        public DocNumberActionManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        protected DocNumberActionManager(object currentState, QDocPropertyResultCollection resultCollection, int foundCount) : base(currentState, resultCollection, foundCount)
        {
        }

        protected DocNumberActionManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count) : base(currentState, targetState, resultCollection, count)
        {
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
