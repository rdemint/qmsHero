using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using QDoc.Core;
using QDoc.Docs;

namespace QmsDoc.Docs.Word
{
    public class WordDoc: Doc
    {
        WordprocessingDocument doc;
        MainDocumentPart mainDocumentPart;
        WordDocConfig docConfig;

        public WordDocConfig DocConfig { get => docConfig; set => docConfig = value; }

        public WordDoc()
        {
        }

        public WordDoc(FileInfo fileInfo) : base(fileInfo)
        {
        }

        public void Process(QDocProperty prop)
        {
            Type myPropType = Type.GetType(prop);
            QDocProperty instance = (QDocProperty)Activator.CreateInstance(myPropType);
            QDocProperty result = null;
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, true))
            {
                instance.Write(doc, DocConfig, prop.Value);
            }
        }

        public QDocProperty Inspect (QDocProperty prop)
        {
            Type myPropType = Type.GetType(prop);
            QDocProperty instance = (QDocProperty)Activator.CreateInstance(myPropType);
            QDocProperty result = null;
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
            {
                result = (QDocProperty)instance.Read(doc, DocConfig);
            }
            return result;
        }
        public IQDocState Inspect (IQDocState docState)
        {
            
            
            var docProps = docState.ToCollection();
            using (WordprocessingDocument doc = WordprocessingDocument.Open(this.FileInfo.FullName, false))
            {
                object[] methodParams = new object[1];
                methodParams[0] = doc;
                methodParams[1] = DocConfig;
                foreach (QDocProperty docProp in docProps)
                {

                    var getMethod = docProp.GetType().GetMethod("Read");
                    string result = (string)getMethod?.Invoke(docProp, methodParams);
                    var stateProperty = state.GetType().GetProperty(docProp.Name);
                    DocProperty dp = (DocProperty)stateProperty.GetValue(state);
                    var propertyInfoValue = dp.GetType().GetProperty("Value");
                    propertyInfoValue.SetValue(dp, result);
                }
            }
            return result;

            
            
            
            
        }
    }
}
