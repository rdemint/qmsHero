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

        private DocRevisionActionManager(object currentState, int foundCount) : base(currentState, foundCount)
        {
        }

        private DocRevisionActionManager(object currentState, object targetState, int count) : base(currentState, targetState, count)
        {
        }


        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var col = new QDocPropertyResultCollection();
            var fileResult = doc.Inspect(new FileRevision());
            if (fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count ++;
            }
            col.Add(fileResult);

            var headerResult = doc.Inspect(new HeaderRevision());
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

            if((string)this.currentState == "*")
            {
                var fileResult = doc.Process(new FileRevision((string)this.TargetState));
                if(fileResult.IsSuccess)
                    count++;
                col.Add(fileResult);
            
                var headerResult = doc.Process(new HeaderRevision((string)this.TargetState));
                if(headerResult.IsSuccess)
                    count++;
                col.Add(headerResult);
            }
            else
            {
                throw new NotImplementedException();
            }
            return col;
        }
    }
}
