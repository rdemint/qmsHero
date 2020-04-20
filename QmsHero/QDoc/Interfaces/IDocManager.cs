using QDoc.Core;
using QFileUtil;
using System.Collections.Generic;
using System.IO;

namespace QDoc.Interfaces
{
    public interface IDocManager
    {
        DirectoryInfo Dir { get; set; }
        List<FileInfo> DirFiles { get; set; }
        IQDocManagerConfig DocManagerConfig { get; set; }
        DirectoryInfo ProcessingDir { get; set; }
        List<FileInfo> ProcessingDirFiles { get; set; }
        IFileCopyManager FileManager { get; set; }
        bool CanProcessFiles();
        void ConfigDir(string dirPath, string processingDirName = "Processing");
        void DeleteProcessingDir();
        bool DirIsValid(string path);
        void ProcessFiles(IDocState docEdit);
        void ProcessFiles(QDocProperty docProp);
    }
}