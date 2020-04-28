using FluentResults;
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
        public DocManager(): base()
        {
            this.FileManager = new FileCopyManager();
            this.DocManagerConfig = new DocManagerConfig();
            this.DocFactory = new DocFactory();
        }

        public DocManager(IFileCopyManager fManager)
        {
            //This Constructor useful in unit tests where concrete fixture can be passed
            this.FileManager = fManager;
            this.DocManagerConfig = new DocManagerConfig();
            this.DocFactory = new DocFactory();

        }

        //public override DocCollection Process(QDocPropertyCollection docPropCollection)
        //{

        //    DocCollection docCollection = new DocCollection();
        //    foreach(var file in this.FileManager.ProcessingFiles)
        //    {
        //        var doc = this.DocFactory.CreateDoc(file);
        //        var result = doc?.Process(docPropCollection);
        //        doc.PropertiesCollection = result;
        //        docCollection.Add(doc);
        //    }

        //    return docCollection;
        //}
        //public override DocCollection Process(QDocProperty docProp)
        //{
        //    DocCollection docCollection = new DocCollection();

        //    foreach (var file in this.FileManager.ProcessingFiles)
        //    {
        //        var doc = this.DocFactory.CreateDoc(file);
        //        Result<QDocProperty> result = doc?.Process(docProp);
        //        doc.PropertiesCollection.Add(result);
        //        docCollection.Add(doc);
        //    }
        //    return docCollection;
        //}

        //public DocCollection Inspect(QDocPropertyCollection collection)
        //{
        //    DocCollection docCollection = new DocCollection();
        //    foreach (var file in this.FileManager.ProcessingFiles)
        //    {
        //        var doc = this.DocFactory.CreateDoc(file);
        //        if(doc != null)
        //        {
        //            foreach(var prop in collection)
        //            {
        //                var propResult = doc.Inspect(prop);
        //                doc.PropertiesCollection.Add(propResult);
        //            }
        //            docCollection.Add(doc);
        //        }
        //    }
        //    return docCollection;
        //}

    }
}
