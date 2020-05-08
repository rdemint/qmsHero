using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Docs.Common.Properties;
using QmsDocXml.QDocActionManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            var fileResult = doc.Inspect(new FileDocNumber());
            if (fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count++;
            }
            col.Add(fileResult);
            return base.Inspect(doc, col);
        }

        public override QDocPropertyResultCollection Process(Doc doc)
        {
            //Rename occurences of a Document Number within the filename and word / excel text.  
            var col = new QDocPropertyResultCollection();
            //
            //
            Match fileMatch;
            string patternToInspectFile = (string)this.currentState;
            Match isValidFormPatternMatch = doc.DocConfig.FileFormNumberRegex.Match(patternToInspectFile);
            Match isValidSopPatternMatch = doc.DocConfig.FileSopNumberRegex.Match(patternToInspectFile);
            Match isValidNumberPatternMatch = doc.DocConfig.FileNumberRegex.Match(patternToInspectFile);

            if (isValidFormPatternMatch.Success)
            {
                fileMatch = doc.DocConfig.FileFormNumberRegex.Match(doc.FileInfo.Name);
                if (fileMatch.Success)
                {
                    var fileResult = doc.Process(new FileDocNumber((string)this.TargetState));
                    if (fileResult.IsSuccess)
                        count++;
                    col.Add(fileResult);
                }
            }
            else if (isValidSopPatternMatch.Success)
            {
                fileMatch = doc.DocConfig.FileSopNumberRegex.Match(doc.FileInfo.Name);
                if (fileMatch.Success)
                {
                    var fileResult = doc.Process(new FileDocNumber((string)this.TargetState));
                    if (fileResult.IsSuccess)
                        count++;
                    col.Add(fileResult);
                }
            }

            else if (isValidNumberPatternMatch.Success)
            {
                fileMatch = doc.DocConfig.FileNumberRegex.Match(doc.FileInfo.Name);
                if (fileMatch.Success)
                {
                    var fileResult = doc.Process(new FileDocNumber((string)this.TargetState));
                    if (fileResult.IsSuccess)
                        count++;
                    col.Add(fileResult);
                }

            }

            //
            //
            var fileResultCurrent = doc.Inspect(new FileDocNumber());

            if(fileResultCurrent.IsSuccess)
            {
                
            }
            return base.Process(doc, col);
        }


    }
}
