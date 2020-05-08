using FluentResults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class QDocPropertyResultCollection : ObservableCollection<Result<int>>
    {
        public QDocPropertyResultCollection()
        {
        }

        public QDocPropertyResultCollection(List<Result<int>> list) : base(list)
        {
        }

        public QDocPropertyResultCollection(IEnumerable<Result<int>> collection) : base(collection)
        {
        }

        public bool HasErrors()
        {
            return this.Any(result => result.IsFailed == true);
        }

        public bool EachItemSharesCount()
        {
            var stateReference = this.First().Value;
            bool boolResult = this.All(result => (int)result.Value == (int)stateReference);

            foreach(var result in this)
            {
                bool tempResult = (string)result.Value == (string)stateReference;
            }
            return boolResult;
        }
    }
}
