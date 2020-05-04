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
using System.Threading.Tasks;

namespace QmsDocXml.Actions
{
    public class DocNameManager : DocPropertyGroupManager
    {
        public DocNameManager(): base()
        {
        }

        public DocNameManager(object currentState) : base(currentState)
        {
        }

        public DocNameManager(object currentState, object targetState, QDocPropertyResultCollection resultCollection) : base(currentState, targetState, resultCollection)
        {
        }

        public DocNameManager(object currentState, QDocPropertyResultCollection resultCollection) : base(currentState, resultCollection)
        {
        }

        public override Result<DocPropertyGroupManager> Audit(ExcelDoc doc)
        {
            //Ensures consistency between the names
            ResultCollection.Add(doc.Inspect(new HeaderName()));
            ResultCollection.Add(doc.Inspect(new FileDocName()));
            if (ResultCollection.EachItemSharesState())
            {
                return Results.Ok<DocPropertyGroupManager>(new DocNameManager((string)this.CurrentState, ResultCollection));
            }

            else 
            {
                return Results.Fail(new Error($"The action {this.Name} did not succeed."));
            }
        }

        public override Result<DocPropertyGroupManager> Audit(WordDoc doc)
        {
            throw new NotImplementedException();
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

            collection.Add(doc.Process(new TextFindReplace((string)this.CurrentState)));
            return collection;
        }

        //public QDocPropertyResultCollection CommonAudit(Doc doc)
        //{
        //    QDocPropertyResultCollection collection = new QDocPropertyResultCollection();

        //}
    }
}
