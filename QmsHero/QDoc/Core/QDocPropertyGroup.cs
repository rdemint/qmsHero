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
    public abstract class QDocPropertyGroup: IToQDocState, INotifyPropertyChanged, IEquatable<QDocPropertyGroup>
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual QDocPropertyCollection ToCollection(bool filter = true)
        {
            var collection = new QDocPropertyCollection();
            var docProps = this.GetType().GetProperties();
            foreach (PropertyInfo docProp in docProps)
            {
                collection.Add((QDocProperty)docProp.GetValue(this));
            }
            if (filter)
            {
                collection = FilterCollection(collection);
            }
            return collection;
        }

        public static QDocPropertyCollection FilterCollection(QDocPropertyCollection state)
        {
            var query = state.Where(prop => prop.State != null);
            if (query.Any())
            {
                return new QDocPropertyCollection(query);
            }
            else
            {
                return new QDocPropertyCollection();
            }
        }

        public bool Equals(QDocPropertyGroup other)
        {
            QDocPropertyCollection collection = ToCollection();
            QDocPropertyCollection otherCollection = other.ToCollection();
            if(collection == otherCollection)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
