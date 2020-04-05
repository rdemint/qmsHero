using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDoc.Core
{
    public class DocEdit

    {
        DocHeader docheader;
        DocBody docbody;
        ObservableCollection<DocProperty> docPropertiesCollection;

        public DocEdit()
        {
            this.DocPropertiesCollection = new ObservableCollection<DocProperty>();
        }

        public ObservableCollection<DocProperty> DocPropertiesCollection {
            get => docPropertiesCollection;
            set => docPropertiesCollection = value; }

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
