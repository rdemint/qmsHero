using QDoc.Core;
using QDoc.Docs;
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

        private TextActionManager(object currentState, QDocPropertyResultCollection resultCollection, int foundCount) : base(currentState, resultCollection, foundCount)
        {
        }

        private TextActionManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int count) : base(currentState, targetState, resultCollection, count)
        {
        }

        //public static QDocActionManager Create(object currentState)
        //{
        //    return new TextActionManager(currentState);
        //}

        //public static QDocActionManager Create(object currentState, object targetState)
        //{
        //    return new TextActionManager(currentState, targetState);
        //}

        //protected static QDocActionManager Create(object currentState, QDocPropertyResultCollection resultCollection, int replacementCount)
        //{
        //    return new TextActionManager(currentState, resultCollection, replacementCount);
        //}

        //protected static QDocActionManager Create(object currentStateToFind, object targetState, QDocPropertyResultCollection resultCollection, int replacementCount)
        //{
        //    return new TextActionManager(currentStateToFind, targetState, resultCollection, replacementCount);
        //}

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var result = doc.Inspect(
                TextFindReplace.Create(
                    (string)this.CurrentState)
                );
            var findResult = result.Value as TextFindReplace;
            PropertyResultCollection.Add(result);
            count += findResult.Count;
            return PropertyResultCollection;
        }

        public override QDocPropertyResultCollection Process(Doc doc)
        {
            var result = doc.Process(
                TextFindReplace.Create(
                    (string)this.CurrentState,
                    (string)this.TargetState)
                );
            PropertyResultCollection.Add(result);
            if (result.IsSuccess)
            {
                var replaceResult = result.Value as TextFindReplace;
                count += replaceResult.Count;
            }

            return PropertyResultCollection;
        }


    }
}
