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
        public static DirectoryInfo DirectoryCopy(DirectoryInfo dir, string destDirName, bool copySubDirs)
        {
            var destDirPath = Path.Combine(dir.Parent.FullName, destDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(dir.FullName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destDirPath))
            {
                Directory.CreateDirectory(destDirPath);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirPath, file.Name);
                file.CopyTo(temppath, true);
            }

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
