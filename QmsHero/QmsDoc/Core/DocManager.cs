using FluentResults;
using GalaSoft.MvvmLight.Ioc;
using QDoc.Core;
using QDoc.Docs;
using QDoc.Interfaces;
using QFileUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocManager : QDocManager
    {
        [PreferredConstructor]
        public DocManager(): base()
        {
            this.FileManager = new FileCopyManager();
            this.DocManagerConfig = new DocManagerConfig();
            this.DocFactory = new DocFactory();
        }

        public DocManager(IFileCopyManager fManager): base()
        {
            //This Constructor useful in unit tests where concrete fixture can be passed
            this.FileManager = fManager;
            this.DocManagerConfig = new DocManagerConfig();
            this.DocFactory = new DocFactory();

        }

        public virtual DocCollection Process(DocPropertyGroupManager docPropManager)
        {
            DocCollection docCollection = new DocCollection();
            foreach (var file in this.FileManager.ProcessingFiles)
            {
                var docResult = this.DocFactory.CreateDoc(file);
                if (docResult.IsSuccess)
                {
                    var doc = docResult.Value;
                    doc.PropertyResultCollection = doc.Process(docPropManager);
                    docCollection.Add(doc);
                }
            }

            return docCollection;
        }

        //public override DocCollection Process(QDocPropertyCollection docPropCollection)
        //{

        //    DocCollection docCollection = new DocCollection();
        //    foreach (var file in this.FileManager.ProcessingFiles)
        //    {
        //        var docResult = this.DocFactory.CreateDoc(file);
        //        if(docResult.IsSuccess)
        //        {
        //            var doc = docResult.Value;
        //            var processResult = doc.Process(docPropCollection);
        //            foreach(var res in processResult)
        //            {
        //                doc.PropertyResultCollection.Add(res);
        //            }
        //            docCollection.Add(doc);
        //        }
        //    }

        //    return docCollection;
        //}
        //public override DocCollection Process(QDocProperty docProp)
        //{
        //    DocCollection docCollection = new DocCollection();
        //    foreach (var file in this.FileManager.ProcessingFiles)
        //    {
        //        var docResult = this.DocFactory.CreateDoc(file);
        //        if (docResult.IsSuccess)
        //        {
        //            var doc = docResult.Value;
        //            var processResult = doc.Process(docProp);
        //            doc.PropertyResultCollection.Add(processResult);
        //            docCollection.Add(doc);
        //        }
        //    }

        //    return docCollection;
        //}

        //public override DocCollection Inspect(QDocPropertyCollection collection)
        //{
        //    DocCollection docCollection = new DocCollection();
        //    foreach (var file in this.FileManager.ProcessingFiles)
        //    {
        //        var docResult = this.DocFactory.CreateDoc(file);
        //        if (docResult.IsSuccess)
        //        {
        //            var doc = docResult.Value;
        //            foreach (var prop in collection)
        //            {
        //                var propResult = doc.Inspect(prop);
        //                doc.PropertyResultCollection.Add(propResult);
        //            }
        //            docCollection.Add(doc);
        //        }
        //    }
        //    return docCollection;
        //}

    }
}
