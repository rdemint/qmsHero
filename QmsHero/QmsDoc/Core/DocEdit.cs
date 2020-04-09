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
        DocHeader docHeader;
        DocBody docBody;
        string editPathName;

        public DocEdit()
        {
            this.DocHeader = new DocHeader();
            //this.DocBody = new DocBody();
        }

        public DocHeader DocHeader { get => docHeader; set => docHeader = value; }
        public DocBody DocBody { get => docBody; set => docBody = value; }
        public string EditPathName { get => editPathName; set => editPathName = value; }

        
        public ObservableCollection<DocProperty> ToCollection()
        {
            var col = new ObservableCollection<DocProperty>();
            AddCollectionRange(col, this.DocHeader.ToCollection());
            //AddCollectionRange(col, this.Docbody.ToCollection());
            col = FilterCollection(col);
            return col;
        }

        public ObservableCollection<DocProperty> FilterCollection(ObservableCollection<DocProperty> docProps)
        {
            var query = docProps.Where(prop => prop.IsValid());
            if(query.Any())
            {
                return new ObservableCollection<DocProperty>(query);
            }
            else
            {
                return new ObservableCollection<DocProperty>();
            }
        }
        private void AddCollectionRange(ObservableCollection<DocProperty> baseCollection, ObservableCollection<DocProperty> props)
        {
            foreach(DocProperty prop in props)
            {
                baseCollection.Add(prop);
            }
        }
        //Dictionary<string, object> ToDict()
        //{
        //    var propDict = new Dictionary<string, object>();
        //    foreach (DocProperty docProperty in this.DocPropertiesCollection)
        //    {
                
        //        propDict.Add(docProperty.Name, docProperty.Value);
        //    }
        //    return propDict;
        //}
    }
}
