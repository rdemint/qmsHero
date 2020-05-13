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
                    }

                }

                else if(isValidSopNumberAndFirstThreeLetterMatch.Success)
                {
                    MatchCollection matches;
                    using (WordprocessingDocument wdoc = WordprocessingDocument.Open(doc.FileInfo.FullName, false))
                    {
                        matches = TextXml.Search(wdoc, doc.DocConfig.FileSopNumberAndFirstThreeLettersRegex);
                    }
                }

                else
                {
                    col.Add(Results.Fail(new Error(
                        $"Did not match the input pattern {(string)currentState} to {(string)sopRx} or {(string)formRx}.")))
                }

            }
            return col;
        }
    }
}
