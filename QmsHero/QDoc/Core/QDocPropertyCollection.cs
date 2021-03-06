﻿
using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QDoc.Core
{
    public class QDocPropertyCollection : ObservableCollection<QDocProperty>, IEquatable<QDocPropertyCollection>
    {
        
    public QDocPropertyCollection()
        {

        }

        public QDocPropertyCollection(List<QDocProperty> list) : base(list)
        {
        }

        public QDocPropertyCollection(IEnumerable<QDocProperty> collection) : base(collection)
        {
        }

        public bool Equals(QDocPropertyCollection other)
        {
            IEnumerable<QDocProperty> diffProps = this.Except(other);
            if(diffProps.Any())
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }
}
