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
        public DocNameActionManager(object currentState) : base(currentState)
        {
        }

        public DocNameActionManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        private DocNameActionManager(object currentState, object targetState, int replacementCount) : base(currentState, targetState, replacementCount)
        {
        }

        private DocNameActionManager(object currentState, int replacementCount) : base(currentState, replacementCount)
        {
        }

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {
            var col = new QDocPropertyResultCollection();

            var fileResult = doc.Inspect(new FileDocName());
            if (fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                count += 1;
            }

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

            var fileNameResult = doc.Inspect(new FileDocName());
            if (fileNameResult.IsSuccess)
            {
                if (fileNameResult.IsSuccess && (string)this.currentState == fileNameResult.Value.State.ToString())
                {
                    var fileResult = doc.Process(new FileDocName((string)this.TargetState));
                    if (fileResult.IsSuccess)
                    {
                        count++;
                        col.Add(fileResult);
                    }
                }

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

            }
            return col;
        }
    }

    }
