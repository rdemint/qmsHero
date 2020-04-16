using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QFileUtil
{
    public static class QFileUtil
    {
        
        public static FileInfo FileCopy(FileInfo file, DirectoryInfo targetDir, bool allowOverWrite=true)
        {
            if(!targetDir.Exists)
            {
                throw new DirectoryNotFoundException();
            }
            string temppath = Path.Combine(targetDir.FullName, file.Name);
            file.CopyTo(temppath, allowOverWrite);
            return new FileInfo(temppath);
        }
        public static DirectoryInfo DirectoryCopy(DirectoryInfo dir, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            var destDirPath = Path.Combine(dir.Parent.FullName, destDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + dir.FullName);
            }


            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirPath))
            {
                Directory.CreateDirectory(destDirPath);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            if (files.Length >= 30)
            {
                throw new Exception("The number of files is conspicuously large.  An error has been thrown to ensure the directory is correct.");
            }

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirPath, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirPath, subdir.Name);
                    DirectoryCopy(subdir, temppath, copySubDirs);
                }
            }

            return new DirectoryInfo(destDirPath);
        }

        public static DirectoryInfo CreateOrCleanSubDirectory(DirectoryInfo dir, string subDirName) 
        {
            var targetDir = new DirectoryInfo(Path.Combine(dir.FullName, subDirName));
            if (targetDir.Exists)
            {
                targetDir.Delete(true);
            }
                        
            var cleanDir = dir.CreateSubdirectory(subDirName);
            return cleanDir;

        }
    }
}
