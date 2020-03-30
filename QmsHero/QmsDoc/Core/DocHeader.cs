using QmsDoc.Interfaces;
using GalaSoft.MvvmLight;

namespace QmsDoc.Core
{
    public class DocHeader : ObservableObject, IDocHeader
    {
        string revision;
        string effectiveDate;
        string logoPath;
        string logoText;

        public string LogoText { 
            get => logoText;
            set { Set<string>(this.LogoText, ref this.logoText, value); } 
        }
        public string LogoPath { 
            get => logoPath;
            set {
                Set<string>(this.LogoPath, ref this.logoPath, value);
            } }
        public string EffectiveDate { 
            get => effectiveDate;
            set {
                Set<string>(this.EffectiveDate, ref this.effectiveDate, value);
            } }
        public string Revision { 
            get => revision;
            set {
                Set<string>(this.Revision, ref this.revision, value);
            } }
    }
}