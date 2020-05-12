using System.Collections.Generic;
using System.IO;

namespace QFileUtil
{
    public interface IFileCopyManager
    {
        DirectoryInfo ProcessingDir { get; }
        DirectoryInfo ReferenceDir { get; }
        List<FileInfo> ProcessingFiles { get; }
        List<FileInfo> ReferenceFiles { get; }

        FileInfo CopyToProcessingDir(FileInfo file);
        int CleanProcessingDir();

        int SetProcessingDir(string path);
        int SetProcessingDir(DirectoryInfo dir);
        int SetReferenceDir(string path);
        int SetReferenceDir(DirectoryInfo dir);
        bool ProcessingDirIsClean();
        bool ReferenceDirAndProcessingDirAreNotNullandExist();

        int UpdateProcessingDirFilesIfNecessary();
    }
}