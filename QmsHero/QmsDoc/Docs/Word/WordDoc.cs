using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using QDoc.Core;
using QDoc.Docs;
using QmsDoc.Core;
using QmsDoc.Exceptions;
using QmsDoc.Interfaces;

namespace QmsDoc.Docs.Word
{
    public class WordDoc: Doc
    {
        WordDocConfig docConfig;
        static List<string> fileExtensions = new List<string> { ".docx", ".doc", ".docm", ".dotm" };

        public WordDoc() {
        }

        public WordDoc(FileInfo fileInfo) : this()
        {
            this.FileInfo = fileInfo;
            DocConfig = new WordDocConfig();
        }

        public WordDoc(FileInfo fileInfo, WordDocConfig docConfig) : this() {

            this.FileInfo = fileInfo;
            this.DocConfig = docConfig;
        }

        public new WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }
        public List<string> FileExtensions { get => fileExtensions; }

        public override void Process(QDocProperty qprop)
        {
            if(qprop as IWriteFileInfo != null)
            {
                qprop.Write(FileInfo, DocConfig);
            }

            else
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
                {
                    var prop = qprop as DocProperty;
                    prop.Write(doc, DocConfig);
                }
            }
        }

        public override QDocProperty Inspect(QDocProperty prop) 
        {

            QDocProperty result = null;

            if(prop as IReadFileInfo != null)
                 {
                    result = prop.Read(FileInfo, DocConfig);
                 }
            else
              {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
                  {
                    result = prop.Read(doc, DocConfig);
                  }
              }
            return result;
        }

        public static List<string> Extensions()
        {
            return fileExtensions;
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
