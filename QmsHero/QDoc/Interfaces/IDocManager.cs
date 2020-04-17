using System.Collections.Generic;
using System.IO;

namespace QDoc.Core
{
    public interface IDocManager
    {
        DirectoryInfo Dir { get; set; }
        List<FileInfo> DirFiles { get; set; }
        QDocManagerConfig DocManagerConfig { get; set; }
        DirectoryInfo ProcessingDir { get; set; }
        List<FileInfo> ProcessingDirFiles { get; set; }

        bool CanProcessFiles();
        void ConfigDir(string dirPath, string processingDirName = "Processing");
        void DeleteProcessingDir();
        bool DirIsValid(string path);
        void ProcessFiles(IDocState docEdit);
        void ProcessFiles(QDocProperty docProp);
    }
}