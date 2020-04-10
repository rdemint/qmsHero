using QmsDoc.Interfaces;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QmsDoc.Core
{
    public class DocHeader : INotifyPropertyChanged, IDocHeader, IToDocPropertyCollection
    {
        DocProperty revision;
        DocProperty effectiveDate;
        DocProperty logoPath;
        DocProperty logoText;

        public DocHeader()
        {
            this.Revision = new DocProperty() { Name = "Revision", Value = null };
            this.EffectiveDate = new DocProperty() { Name = "Effective Date", Value = null };
            this.LogoPath = new DocProperty() { Name = "Logo Path", Value = null };
            this.LogoText = new DocProperty() { Name = "Logo Text", Value = null };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public DocProperty Revision { 
            get => revision;
            set {
                this.revision = value;
                NotifyPropertyChanged();
            } 
        }
        public DocProperty EffectiveDate { 
            get => effectiveDate;
            set {
                this.effectiveDate = value;
                NotifyPropertyChanged();
            } }
        public DocProperty LogoPath { 
            get => logoPath;
            set {
                this.logoPath = value;
                NotifyPropertyChanged();
            } }
        public DocProperty LogoText { 
            get => logoText;
            set {
                this.logoText = value;
                NotifyPropertyChanged();
            } }

        public ObservableCollection<DocProperty> ToCollection()
        {
            var col = new ObservableCollection<DocProperty>();
            col.Add(Revision);
            col.Add(EffectiveDate);
            col.Add(LogoPath);
            col.Add(logoText);
            return col;
        }
    }
}