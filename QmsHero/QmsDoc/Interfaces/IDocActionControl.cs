namespace QmsDoc.Interfaces
{
    public interface IDocActionControl
    {
        bool ControlIsValid { get; set; }
        string DisplayValue { get; set; }
        object DocActionVal { get; set; }
    }
}