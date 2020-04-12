namespace QmsDoc.Core
{
    public interface IDocHeader
    {
        DocProperty EffectiveDate { get; set; }
        DocProperty LogoPath { get; set; }
        DocProperty LogoText { get; set; }
        DocProperty Revision { get; set; }
    }
}