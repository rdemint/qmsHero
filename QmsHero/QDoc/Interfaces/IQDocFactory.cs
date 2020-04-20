using QDoc.Interfaces;
using System.IO;

namespace QDoc.Interfaces
{
    public interface IQDocFactory
    {
        IDoc CreateDoc(FileInfo file);
    }
}