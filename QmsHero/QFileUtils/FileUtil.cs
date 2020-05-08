using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFileUtil
{
    public static class FileUtil
    {
        
        public static FileInfo FileCopy(FileInfo file, DirectoryInfo targetDir, bool allowOverWrite=true)
        {
            if(!targetDir.Exists)
            {
                throw new DirectoryNotFoundException(targetDir.FullName);
            }
            string temppath = Path.Combine(targetDir.FullName, file.Name);
            file.CopyTo(temppath, allowOverWrite);
            return new FileInfo(temppath);
        }

        public static FileInfo FileRename(FileInfo file, string newNameWithExtension)
        {
            string oldFilePath = file.FullName;
            FileInfo newFile = new FileInfo(Path.Combine(file.DirectoryName, newNameWithExtension));
            file.MoveTo(newFile.FullName);

            var oldFile = new FileInfo(oldFilePath);
            if(oldFile.Exists && file.Exists && oldFile.Name != file.Name)
            {
                oldFile.Delete();
            }
            return newFile;
        }


        public static DirectoryInfo SearchSubDirectory(DirectoryInfo directory, string dirName)
        {
            var searchDirs = directory.GetDirectories(dirName, SearchOption.AllDirectories).ToList();
            if (searchDirs.Any())
            {
                return searchDirs[0];
            }
            else
            {
                return null;
            }
        }
       
        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirName);
                }

                DirectoryInfo[] dirs = dir.GetDirectories();
                // If the destination directory doesn't exist, create it.
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, true);
                }

                // If copying subdirectories, copy them and their contents to new location.
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }

        public static DirectoryInfo CreateOrCleanSubDirectory(DirectoryInfo dir, string subDirName) 
        {
            Contract.Requires(dir.Exists);
            var targetDir = new DirectoryInfo(Path.Combine(dir.FullName, subDirName));
            if (targetDir.Exists)
            {
                CleanDirectoryAndChildren(targetDir);
                return targetDir;
            }

            else
            {
                return dir.CreateSubdirectory(subDirName);
            }

        }

        public static int CleanDirectoryAndChildren(DirectoryInfo dir)
        {
            var count = 0;
            foreach (FileInfo file in dir.GetFiles("*", SearchOption.AllDirectories))
            {
                file.Delete();
                count += 1;
            }
            return count;
        }
    }
}
