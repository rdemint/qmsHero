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
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDocXml.DocPropertyGroupManagers
{
    public class DocNameManager : DocPropertyGroupManager

    {
        private DocNameManager(object currentState): base(currentState)
        {
        }

        private DocNameManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        private DocNameManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection, int replacementCount) : base(currentState, targetState, resultCollection, replacementCount)
        {
        }

        private DocNameManager(object currentState, QDocPropertyResultCollection resultCollection, int replacementCount) : base(currentState, resultCollection, replacementCount)
        {
        }

        public override Result<DocPropertyGroupManager> Inspect(ExcelDoc doc)
        {
            //Ensures consistency between the names
            var fileResult = doc.Inspect(new FileDocName((string)this.CurrentState));
            ResultCollection.Add(fileResult);
            if(fileResult.IsSuccess && fileResult.Value.State.ToString() == (string)this.CurrentState)
            {
                Count += 1;
            }
            
            var result = doc.Inspect(TextFindReplace.Create((string)this.CurrentState));
            var findResult = result.Value as TextFindReplace;
            ResultCollection.Add(result);
            Count += findResult.Count;
            if (ResultCollection.HasErrors()) {

                return Results.Fail(new Error("Errors occured in inspecting this document."));
            }
            else
            {
                return Results.Ok<DocPropertyGroupManager>(DocNameManager
                    .Create(
                    (string)ResultCollection.First().Value.State.ToString(),
                    ResultCollection,
                    Count)); 
            }

            //ResultCollection.Add(doc.Inspect(new HeaderName()));
            //ResultCollection.Add(doc.Inspect(new FileDocName()));
            //if (ResultCollection.EachItemSharesState())
            //{
            //    return Results.Ok<DocPropertyGroupManager>(DocNameManager
            //        .Create(
            //        (string)ResultCollection.First().Value.State.ToString(), 
            //        ResultCollection));
            //}

            //else 
            //{
            //    return Results.Fail(new Error($"The document file name and header name are not consistent for {doc.FileInfo.FullName}."));
            //}
        }

        public override Result<DocPropertyGroupManager> Inspect(WordDoc doc)
        {
            ResultCollection.Add(doc.Inspect(new HeaderName()));
            ResultCollection.Add(doc.Inspect(new FileDocName()));
            if (ResultCollection.EachItemSharesState())
            {
                return Results.Ok<DocPropertyGroupManager>(DocNameManager
                    .Create(
                    (string)ResultCollection.First().Value.State.ToString(),
                    ResultCollection));
            }

            else
            {
                return Results.Fail(new Error($"The document file name and header name are not consistent for {doc.FileInfo.FullName}."));
            }
        }

        public override Result<DocPropertyGroupManager> Process(WordDoc doc)
        {
            return CommonProcess(doc);
            //var fileDocName = doc.Inspect(new FileDocName()).Value.State.ToString();
            //if (fileDocName == (string)this.CurrentState)
            //{
            //    ResultCollection.Add(doc.Process(new FileDocName((string)this.TargetState)));
            //    Count++;
            //}

            //var result = doc.Process(
            //    TextFindReplace.Create(
            //        (string)this.CurrentState,
            //        (string)this.TargetState)
            //    );
            //ResultCollection.Add(result);
            //var replaceResult = result.Value as TextFindReplace;
            //Count += replaceResult.Count;

            //if (ResultCollection.HasErrors())
            //{
            //    return Results.Fail(new Error($"The action {this.Name} did not succeed."));
            //}

            //else
            //{
            //    return Results.Ok<DocPropertyGroupManager>(
            //        DocNameManager.Create(
            //            (string)this.CurrentState, 
            //            (string)this.TargetState, 
            //            ResultCollection,
            //            Count));
            //}
        }

        public override Result<DocPropertyGroupManager> Process(ExcelDoc doc)
        {
            return CommonProcess(doc);
        }

        public Result<DocPropertyGroupManager> CommonProcess(Doc doc)
        {
            var fileDocName = doc.Inspect(new FileDocName()).Value.State.ToString();
            if (fileDocName == (string)this.CurrentState)
            {
                ResultCollection.Add(doc.Process(new FileDocName((string)this.TargetState)));
                Count++;
            }

            var result = doc.Process(
                TextFindReplace.Create(
                    (string)this.CurrentState,
                    (string)this.TargetState)
                );
            ResultCollection.Add(result);
            var replaceResult = result.Value as TextFindReplace;
            Count += replaceResult.Count;

            if (ResultCollection.HasErrors())
            {
                return Results.Fail(new Error($"The action {this.Name} did not succeed."));
            }

            else
            {
                return Results.Ok<DocPropertyGroupManager>(
                    DocNameManager.Create(
                        (string)this.CurrentState,
                        (string)this.TargetState,
                        ResultCollection,
                        Count));
            }
            //headerName = ResultCollection.Add(doc.Process(new HeaderName((string)this.State)));
            //QDocPropertyResultCollection collection = new QDocPropertyResultCollection();
            //var fileDocName = doc.Inspect(new FileDocName()).Value.State.ToString();
            //if (fileDocName == (string)this.CurrentState)
            //    collection.Add(doc.Process(new FileDocName((string)this.TargetState)));

            //collection.Add(doc.Process(
            //    TextFindReplace.Create(
            //        (string)this.CurrentState,
            //        (string)this.TargetState))
            //    );
            //return collection;
        }

        public static DocPropertyGroupManager Create(object currentState)
        {
            return new DocNameManager(currentState);
        }

        public static DocPropertyGroupManager Create(object currentState, object targetState)
        {
            return new DocNameManager(currentState, targetState);
        }

        public static DocPropertyGroupManager Create(object currentState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocNameManager(currentState, resultCollection, replacementCount);
        }

        public static DocPropertyGroupManager Create(object currentStateToFind, object targetState, QDocPropertyResultCollection resultCollection, int replacementCount)
        {
            return new DocNameManager(currentStateToFind, targetState, resultCollection, replacementCount);
        }

    }
}
