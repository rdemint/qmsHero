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
        private DocNameManager(): base()
        {
        }

        private DocNameManager(object currentState, object targetState) : base(currentState, targetState)
        {
        }

        private DocNameManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection) : base(currentState, targetState, resultCollection)
        {
        }

        private DocNameManager(object currentState, QDocPropertyResultCollection resultCollection) : base(currentState, resultCollection)
        {
        }

        public override Result<DocPropertyGroupManager> Inspect(ExcelDoc doc)
        {
            //Ensures consistency between the names
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
            ResultCollection = CommonProcess(doc);
            if (ResultCollection.HasErrors())
            {
                return Results.Fail(new Error($"The action {this.Name} did not succeed."));
            }

            else
            {
                return Results.Ok<DocPropertyGroupManager>(new DocNameManager((string)this.CurrentState, (string)this.TargetState, ResultCollection));
            }
        }

        public override Result<DocPropertyGroupManager> Process(ExcelDoc doc)
        {
            ResultCollection = CommonProcess(doc);
            if (ResultCollection.HasErrors())
            {
                return Results.Fail(new Error($"The action {this.Name} did not succeed."));
            }

            else
            {
                return Results.Ok<DocPropertyGroupManager>(new DocNameManager((string)this.CurrentState, (string)this.TargetState, ResultCollection));
            }
        }

        public QDocPropertyResultCollection CommonProcess(Doc doc)
        {
            //headerName = ResultCollection.Add(doc.Process(new HeaderName((string)this.State)));
            QDocPropertyResultCollection collection = new QDocPropertyResultCollection();
            var fileDocName = doc.Inspect(new FileDocName()).Value.State.ToString();
            if(fileDocName == (string)this.CurrentState)
                collection.Add(doc.Process(new FileDocName((string)this.TargetState)));

            collection.Add(doc.Process(
                TextFindReplace.Create(
                    (string)this.CurrentState, 
                    (string)this.TargetState))
                );
            return collection;
        }

        public static DocPropertyGroupManager Create()
        {
            return new DocNameManager();
        }

        public static DocPropertyGroupManager Create(object currentState, object targetState)
        {
            return new DocNameManager(currentState, targetState);
        }

        public static DocPropertyGroupManager Create(object currentState, QDocPropertyResultCollection resultCollection)
        {
            return new DocNameManager(currentState, resultCollection);
        }

        public static DocPropertyGroupManager Create(object currentStateToFind, object targetState, QDocPropertyResultCollection resultCollection)
        {
            return new DocNameManager(currentStateToFind, targetState, resultCollection);
        }

    }
}
