
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
    public class DocState: INotifyPropertyChanged, IToDocPropertyCollection

    {

        public DocState()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<DocProperty> ToCollection(bool filter=true)
        {
            var col = new ObservableCollection<DocProperty>();
            var docProps = this.GetType().GetProperties();
            foreach (PropertyInfo docProp in docProps)
            {
                col.Add((DocProperty)docProp.GetValue(this));
            }
            if(filter)
            {
            col = FilterCollection(col);
            }
            return col;
        }

        public ObservableCollection<DocProperty> FilterCollection(ObservableCollection<DocProperty> docProps)
        {
            var query = docProps.Where(prop => prop.Value!=null);
            if(query.Any())
            {
                return new ObservableCollection<DocProperty>(query);
            }
            else
            {
                return new ObservableCollection<DocProperty>();
            }
        }
        private void AddCollectionRange(ObservableCollection<DocProperty> baseCollection, ObservableCollection<DocProperty> props)
        {
            foreach(DocProperty prop in props)
            {
                baseCollection.Add(prop);
            }
        }
    }
}
