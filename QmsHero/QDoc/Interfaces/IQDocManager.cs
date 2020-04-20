using QDoc.Core;
using QFileUtil;
using System.Collections.Generic;
using System.IO;

namespace QDoc.Interfaces
{
    public interface IQDocManager
    {
        DirectoryInfo Dir { get; set; }
        List<FileInfo> DirFiles { get; set; }
        IQDocManagerConfig DocManagerConfig { get; set; }
        IFileCopyManager FileManager { get; set; }
        bool CanProcessFiles();
        void ProcessFiles(IDocState docEdit);
        void ProcessFiles(QDocProperty docProp);
    }
}