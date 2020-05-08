using QDoc.Core;
using QDoc.Docs;
using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.QDocActionManagers
{
    public class TextActionManager : QDocActionManager
    {

        public TextActionManager(object currentState) : base(currentState)
        {
        }

        public TextActionManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        protected TextActionManager(object currentState, int foundCount) : base(currentState, foundCount)
        {
        }

        protected TextActionManager(object currentState, object targetState, int count) : base(currentState, targetState, count)
        {
        }

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var col = new QDocPropertyResultCollection();
            return Inspect(doc, col);
            
        }

        protected QDocPropertyResultCollection Inspect(Doc doc, QDocPropertyResultCollection col)
        {
            var result = doc.Inspect(
                TextFindReplace.Create(
                    (string)this.CurrentState)
                );
            var findResult = result.Value as TextFindReplace;
            col.Add(result);
            count += findResult.StateCount;
            return col;
        }

        public override QDocPropertyResultCollection Process(Doc doc)
        {
            var col = new QDocPropertyResultCollection();
            return Process(doc, col);
        }

        protected QDocPropertyResultCollection Process(Doc doc, QDocPropertyResultCollection col)
        {
            var result = doc.Process(
                TextFindReplace.Create(
                    (string)this.CurrentState,
                    (string)this.TargetState)
                );
            col.Add(result);
            if (result.IsSuccess)
            {
                var replaceResult = result.Value as TextFindReplace;
                count += replaceResult.StateCount;
            }

            return col;
        }


    }
}
