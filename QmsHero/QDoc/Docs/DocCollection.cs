using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Docs
{
    public class DocCollection : ObservableCollection<Doc>
    {
        public DocCollection()
        {
        }

        public DocCollection(List<Doc> list) : base(list)
        {
        }

        public DocCollection(IEnumerable<Doc> collection) : base(collection)
        {
        }

        public bool HasErrors()
        {
            foreach(Doc doc in this)
            {
                if(doc.PropertyResultCollection.HasErrors())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
