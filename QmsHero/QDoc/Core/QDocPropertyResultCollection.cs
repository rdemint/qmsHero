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
        
        public int CountErrors()
        {
            return this.Where(result => result.IsFailed).Count();
        }

        public bool EachItemSharesState()
        {
            var stateReference = this.First().Value.State;
            bool boolResult = this.All(result => (string)result.Value.State == (string)stateReference);

            foreach(var result in this)
            {
                bool tempResult = (string)result.Value.State == (string)stateReference;
            }
            return boolResult;
        }

        public int TotalStateCount()
        {
            int totalCount = 0;
            foreach( var result in this)
            {
                totalCount += result.Value.StateCount;
            }
            return totalCount;
        }
    }
}
