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

        public ObservableCollection<DocProperty> DocPropertiesCollection {
            get { 
                if (this.DocPropertiesCollection == null)
                {
                    this.DocPropertiesCollection = new ObservableCollection<DocProperty>();
                }
                return this.DocPropertiesCollection;
            }
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
