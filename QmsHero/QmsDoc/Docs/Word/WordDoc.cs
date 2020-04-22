using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using QDoc.Core;
using QDoc.Docs;
using QDoc.Interfaces;
using QmsDoc.Core;

namespace QmsDoc.Docs.Word
{
    public class WordDoc: Doc
    {
        WordprocessingDocument doc;
        MainDocumentPart mainDocumentPart;
        WordDocConfig docConfig;

        public WordDoc() { }

        public WordDoc(FileInfo fileInfo) : base(fileInfo) 
        {
            DocConfig = new WordDocConfig();
        }

        public WordDoc(FileInfo fileInfo, WordDocConfig docConfig) : base(fileInfo, docConfig) { }

        public new WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public override void Process(QDocProperty qprop)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
            {
                var prop = qprop as DocProperty;
                prop.Write(doc, DocConfig, prop.State);
            }
        }

        public override QDocProperty Inspect (QDocProperty prop)
        {
            QDocProperty result = null;
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
            {
                result = prop.Read(doc, DocConfig);
            }
            return result;
        }

        public override QDocProperty Inspect(QDocProperty prop, FileInfo file) 
        {
            
        }
        //public override IDocState Inspect (IDocState docState)
        //{

        //    return base.Inspect(docState);
        // Create a new instance of IQDocState and set the property values on it. 

        //var docProps = docState.ToCollection();
        //using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
        //{
        //    object[] methodParams = new object[1];
        //    methodParams[0] = doc;
        //    methodParams[1] = DocConfig;
        //    foreach (QDocProperty docProp in docProps)
        //    {

        //        var getMethod = docProp.GetType().GetMethod("Read");
        //        string result = (string)getMethod?.Invoke(docProp, methodParams);
        //        var stateProperty = docState.GetType().GetProperty(docProp.Name);
        //        QDocProperty dp = (QDocProperty)stateProperty.GetValue(docState);
        //        var propertyInfoValue = dp.GetType().GetProperty("Value");
        //        propertyInfoValue.SetValue(dp, result);
        //    }
        //return result;
        //}
    }
}
