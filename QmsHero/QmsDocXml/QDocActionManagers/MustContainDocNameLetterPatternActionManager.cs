using DocumentFormat.OpenXml.Packaging;
using FluentResults;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Docs.Word;
using QmsDocXml.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDocXml.QDocActionManagers
{
    class MustContainDocNameLetterPatternActionManager : QDocActionManager
    {
        public MustContainDocNameLetterPatternActionManager()
        {
        }

        public MustContainDocNameLetterPatternActionManager(object currentState) : base(currentState)
        {
        }

        public override QDocPropertyResultCollection Inspect(Doc doc)
        {

            var col = new QDocPropertyResultCollection();
            var wordDoc = doc as WordDoc;
            if (wordDoc != null) 
            
            {
                var formRx = doc.DocConfig.FileFormNumberAndFirstThreeLettersNameRegex;
                var sopRx = doc.DocConfig.FileSopNumberAndFirstThreeLettersRegex;
                Match isValidFormNumberAndFirstThreeLetterMatch = formRx.Match((string)currentState);
                Match isValidSopNumberAndFirstThreeLetterMatch = sopRx.Match((string)currentState);

                if(isValidFormNumberAndFirstThreeLetterMatch.Success)
                {
                    MatchCollection matches;
                    using(WordprocessingDocument wdoc = WordprocessingDocument.Open(doc.FileInfo.FullName, false))
                    {
                        matches = TextXml.Search(wdoc, doc.DocConfig.FileFormNumberAndFirstThreeLettersNameRegex);
                        return EvaluateMatches(matches, col);  
                    }
                    

                }

                else if(isValidSopNumberAndFirstThreeLetterMatch.Success)
                {
                    MatchCollection matches;
                    using (WordprocessingDocument wdoc = WordprocessingDocument.Open(doc.FileInfo.FullName, false))
                    {
                        matches = TextXml.Search(wdoc, doc.DocConfig.FileSopNumberAndFirstThreeLettersRegex);
                        return EvaluateMatches(matches, col);
                    }
                }

                else
                {
                    col.Add(Results.Fail(new Error(
                        $"Did not match the input pattern {(string)currentState} to {sopRx.ToString()} or {formRx.ToString()}."
                        )));
                }

            }
            return col;
        }

        private QDocPropertyResultCollection EvaluateMatches(MatchCollection matches, QDocPropertyResultCollection col)
        {
            int count = 0;
            foreach (var match in matches)
            {
                if (match.ToString() != (string)currentState)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                col.Add(Results.Fail(new Error($"Procedure Number Name patterns not matching {currentState.ToString()} were found {count} times in the document. There is likely a misnamed procedured")));
            }

            else
            {
                col.Add(Results.Ok<QDocProperty>(new TextFindReplace()));
            }

            return col;
        }
    }
}
