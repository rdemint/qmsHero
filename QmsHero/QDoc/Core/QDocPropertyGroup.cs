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

        public QDocState ToCollection(bool filter = true)
        {
            var state = new QDocState();
            var docProps = this.GetType().GetProperties();
            foreach (PropertyInfo docProp in docProps)
            {
                state.Add((QDocProperty)docProp.GetValue(this));
            }
            if (filter)
            {
                state = FilterCollection(state);
            }
            return state;
        }

        public QDocState FilterCollection(QDocState state)
        {
            var query = state.Where(prop => prop.State != null);
            if (query.Any())
            {
                return new QDocState(query);
            }
            else
            {
                return new QDocState();
            }
        }
    }
}
