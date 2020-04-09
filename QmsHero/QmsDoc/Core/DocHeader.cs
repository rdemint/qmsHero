using QmsDoc.Interfaces;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace QmsDoc.Core
{
    public class DocHeader : ObservableObject, IDocHeader, ToDocPropertyCollection
    {
        DocProperty revision;
        DocProperty effectiveDate;
        DocProperty logoPath;
        DocProperty logoText;
        ObservableCollection<DocProperty> docProperties;

        public DocProperty Revision { 
            get => revision;
            set { Set<DocProperty>(ref this.revision, value);} }
        public DocProperty EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public DocProperty LogoPath { get => logoPath; set => logoPath = value; }
        public DocProperty LogoText { get => logoText; set => logoText = value; }

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