using FluentResults;
using System.Collections.Generic;
using System.IO;

namespace QFileUtil
{
    public interface IFileCopyManager
    {
        DirectoryInfo ProcessingDir { get; set;  }
        DirectoryInfo ReferenceDir { get; set; }
        List<FileInfo> ProcessingFiles { get; }
        List<FileInfo> ReferenceFiles { get; }

        FileInfo CopyToProcessingDir(FileInfo file);
        int CleanProcessingDir();

        Result<int> SetProcessingDir(string path);
        Result<int> SetProcessingDir(DirectoryInfo dir);
        Result<int> SetReferenceDir(string path);
        Result<int> SetReferenceDir(DirectoryInfo dir);
        bool ProcessingDirIsClean();
        bool ReferenceDirAndProcessingDirAreNotNullandExist();

        Result<int> UpdateProcessingDirFilesIfNecessaryAndGetResultCount();
        Result<int> CreateProcessingDirIfDoesNotExistAndUpdateWithReferenceFilesAndNewFileCount();
        Result<int> MakeCurrentProcessingDirTheReferenceDirAndCreateNewProcessingDirWithTimeSuffix();
    }
}