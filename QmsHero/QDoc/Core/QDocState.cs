
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
    public class QDocState : INotifyPropertyChanged, IToDocPropertyCollection, IDocState
    {

        public QDocState()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<QDocProperty> ToCollection(bool filter = true)
        {
            var col = new ObservableCollection<QDocProperty>();
            var docProps = this.GetType().GetProperties();
            foreach (PropertyInfo docProp in docProps)
            {
                col.Add((QDocProperty)docProp.GetValue(this));
            }
            if (filter)
            {
                col = FilterCollection(col);
            }
            return col;
        }

        public ObservableCollection<QDocProperty> FilterCollection(ObservableCollection<QDocProperty> docProps)
        {
            var query = docProps.Where(prop => prop.State != null);
            if (query.Any())
            {
                return new ObservableCollection<QDocProperty>(query);
            }
            else
            {
                return new ObservableCollection<QDocProperty>();
            }
        }
    }
}
