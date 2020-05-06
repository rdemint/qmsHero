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

        protected DocNumberActionManager(object currentState, int foundCount) : base(currentState, foundCount)
        {
        }

        protected DocNumberActionManager(object currentState, object targetState, int count) : base(currentState, targetState, count)
        {
        }

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var col = new QDocPropertyResultCollection();

            var fileResult = doc.Inspect(new FileDocNumber((string)this.CurrentState));
            if (fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count++;
            }
            col.Add(fileResult);
            return base.Inspect(doc, col);
        }

        public override QDocPropertyResultCollection Process(Doc doc)
        {
            var col = new QDocPropertyResultCollection();

            var fileResult = doc.Process(new FileDocNumber((string)this.TargetState));
            if (fileResult.IsSuccess)
                count++;
            col.Add(fileResult);
            return base.Process(doc, col);
        }


    }
}
