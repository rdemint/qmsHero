using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QDoc.Core
{
    public interface IDocState
    {
        event PropertyChangedEventHandler PropertyChanged;
        ObservableCollection<QDocProperty> FilterCollection(ObservableCollection<QDocProperty> docProps);
        ObservableCollection<QDocProperty> ToCollection(bool filter = true);
    }
}