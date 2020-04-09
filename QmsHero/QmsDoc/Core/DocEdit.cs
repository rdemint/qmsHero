using QmsDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocEdit: ToDocPropertyCollection

    {
        DocHeader docheader;
        DocBody docbody;
        //ObservableCollection<DocProperty> docPropertiesCollection;

        public DocEdit()
        {
            //this.DocPropertiesCollection = new ObservableCollection<DocProperty>();
        }

        public DocHeader Docheader { get => docheader; set => docheader = value; }
        public DocBody Docbody { get => docbody; set => docbody = value; }

        //public ObservableCollection<DocProperty> DocPropertiesCollection {
        //    get => docPropertiesCollection;
        //    set => docPropertiesCollection = value; }
        public ObservableCollection<DocProperty> ToCollection()
        {
            var col = new ObservableCollection<DocProperty>();
            AddCollectionRange(col, this.Docheader.ToCollection());
            //AddCollectionRange(col, this.Docbody.ToCollection());
            return col;
        }

        private void AddCollectionRange(ObservableCollection<DocProperty> baseCollection, ObservableCollection<DocProperty> props)
        {
            foreach(DocProperty prop in props)
            {
                baseCollection.Add(prop);
            }
        }
        Dictionary<string, object> ToDict()
        {
            var propDict = new Dictionary<string, object>();
            foreach (DocProperty docProperty in this.DocPropertiesCollection)
            {
                
                propDict.Add(docProperty.Name, docProperty.Value);
            }
            return propDict;
        }
    }
}
