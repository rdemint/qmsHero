using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class QDocConfig<T>
    {
        T _value;
        public QDocConfig(T t)
        {
            this._value = t;
        }

        //public new Type GetType()
        //{
        //    return (Type)_value;
        //}
    }
}
