namespace QmsDoc.Interfaces
{
    public interface IDocActionControl
    {
        bool ControlIsEnabled { get; set; }
        string DocActionName { get; set; }
        object DocActionVal { get; set; }
    }
}