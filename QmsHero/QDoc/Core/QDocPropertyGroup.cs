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
    public abstract class QDocPropertyGroup: IToQDocState, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public QDocPropertyCollection ToDocState(bool filter = true)
        {
            var state = new QDocPropertyCollection();
            var docProps = this.GetType().GetProperties();
            foreach (PropertyInfo docProp in docProps)
            {
                state.Add((QDocProperty)docProp.GetValue(this));
            }
            if (filter)
            {
                state = FilterDocState(state);
            }
            return state;
        }

        public static QDocPropertyCollection FilterDocState(QDocPropertyCollection state)
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
    }
}
