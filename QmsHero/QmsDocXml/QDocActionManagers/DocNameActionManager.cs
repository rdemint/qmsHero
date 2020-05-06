using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Core;
using QmsDoc.Docs.Common.Properties;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDocXml.QDocActionManagers
{
    public class DocNameActionManager : QDocActionManager

    {
        public DocNameActionManager(object currentState): base(currentState)
        {
        }

        public DocNameActionManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        private DocNameActionManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int replacementCount) : base(currentState, targetState, resultCollection, replacementCount)
        {
        }

        private DocNameActionManager(object currentState, QDocPropertyResultCollection resultCollection, int replacementCount) : base(currentState, resultCollection, replacementCount)
        {
        }

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var fileResult = doc.Inspect(new FileDocName((string)this.CurrentState));
            if (fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count += 1;
            }

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
            var fileDocName = doc.Inspect(new FileDocName()).Value.State.ToString();
            if (fileDocName == (string)this.CurrentState)
            {
                PropertyResultCollection.Add(doc.Process(new FileDocName((string)this.TargetState)));
                count++;
            }

            var result = doc.Process(
                TextFindReplace.Create(
                    (string)this.CurrentState,
                    (string)this.TargetState)
                );
            PropertyResultCollection.Add(result);
            if(result.IsSuccess)
            {
                var replaceResult = result.Value as TextFindReplace;
                count += replaceResult.Count;
            }

            return PropertyResultCollection;
        }

        public static QDocActionManager Create(string currentState)
        {
            return new DocNameActionManager(currentState);
        }

        public static QDocActionManager Create(string currentState, string targetState)
        {
            return new DocNameActionManager(currentState, targetState);
        }

        protected static QDocActionManager Create(string currentState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocNameActionManager(currentState, resultCollection, replacementCount);
        }

        protected static QDocActionManager Create(string currentStateToFind, string targetState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocNameActionManager(currentStateToFind, targetState, resultCollection, replacementCount);
        }

    }
}
