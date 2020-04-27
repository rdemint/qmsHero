using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Docs
{
    public class DocCollection : ObservableCollection<IDoc>
    {
        public DocCollection()
        {
        }

        public DocCollection(List<IDoc> list) : base(list)
        {
        }

        public DocCollection(IEnumerable<IDoc> collection) : base(collection)
        {
        }
    }
}
