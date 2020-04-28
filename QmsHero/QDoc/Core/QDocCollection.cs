using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    class QDocCollection : ObservableCollection<IDoc>
    {
        public QDocCollection()
        {
        }

        public QDocCollection(List<IDoc> list) : base(list)
        {
        }

        public QDocCollection(IEnumerable<IDoc> collection) : base(collection)
        {
        }
    }
}
