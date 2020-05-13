using FluentResults;
using System.Collections.Generic;
using System.IO;

namespace QFileUtil
{
    public interface IFileCopyManager
    {
        DirectoryInfo ProcessingDir { get; set;  }
        DirectoryInfo ReferenceDir { get; set; }
        List<FileInfo> ProcessingFiles { get; set; }
        List<FileInfo> ReferenceFiles { get; set; }

        FileInfo CopyToProcessingDir(FileInfo file);
        int CleanProcessingDir();

        Result<int> SetProcessingDir(string path);
        Result<int> SetProcessingDir(DirectoryInfo dir);
        Result<int> SetReferenceDir(string path);
        Result<int> SetReferenceDir(DirectoryInfo dir);
        bool ProcessingDirIsClean();
        bool ReferenceDirAndProcessingDirAreNotNullandExist();

        Result<int> CopyReferenceToProcessingIfPossible();
        Result<int> CreateProcessingDirIfDoesNotExistAndUpdateWithReferenceFilesAndNewFileCount();
        Result<int> MakeCurrentProcessingDirTheReferenceDirAndCreateNewProcessingDirWithTimeSuffix();
    }
}