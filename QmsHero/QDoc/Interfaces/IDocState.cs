using System.Collections.ObjectModel;
using System.ComponentModel;
using QDoc.Core;

namespace QDoc.Interfaces
{
    public interface IDocState
    {
        event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<QDocProperty> FilterCollection(ObservableCollection<QDocProperty> docProps);
        ObservableCollection<QDocProperty> ToCollection(bool filter = true);
    }
}