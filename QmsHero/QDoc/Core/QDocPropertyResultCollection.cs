using FluentResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class QDocPropertyResultCollection : ObservableCollection<Result<QDocProperty>>
    {
        public QDocPropertyResultCollection()
        {
        }

        public QDocPropertyResultCollection(List<Result<QDocProperty>> list) : base(list)
        {
        }

        public QDocPropertyResultCollection(IEnumerable<Result<QDocProperty>> collection) : base(collection)
        {
        }

        public bool HasErrors()
        {
            return this.Any(result => result.IsFailed == true);
        }

        public bool EachItemSharesState()
        {
            object stateReference = this.First().Value.State;
            return this.All(result => result.Value.State == stateReference);
        }
    }
}
