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
    }
}
