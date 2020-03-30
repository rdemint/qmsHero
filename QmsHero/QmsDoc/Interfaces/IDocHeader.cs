namespace QmsDoc.Core
{
    public interface IDocHeader
    {
        string EffectiveDate { get; set; }
        string LogoPath { get; set; }
        string LogoText { get; set; }
        string Revision { get; set; }
    }
}