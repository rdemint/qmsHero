using QmsDoc.Interfaces;
using GalaSoft.MvvmLight;

namespace QmsDoc.Core
{
    public class DocHeader : ObservableObject, IDocHeader
    {
        DocProperty revision;
        DocProperty effectiveDate;
        DocProperty logoPath;
        DocProperty logoText;

        public DocProperty Revision { get => revision; set => revision = value; }
        public DocProperty EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public DocProperty LogoPath { get => logoPath; set => logoPath = value; }
        public DocProperty LogoText { get => logoText; set => logoText = value; }
    }
}