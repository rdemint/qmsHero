using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Directory = System.IO.Directory;
using DirectoryInfo = System.IO.DirectoryInfo;
using FileInfo = System.IO.FileInfo;

namespace QmsDoc.Test
{
    public class FixtureUtil
    {
        DirectoryInfo unittest_dir;
        DirectoryInfo fixtureDir;
        DirectoryInfo baseFixtureDir;
        DirectoryInfo sop1_dir;
        DirectoryInfo qmsHero_dir;
        FileInfo open_file;
        List<FileInfo> files;
        private List<FileInfo> safeFiles;
        public FileInfo WordSample;
        public FileInfo ExcelSample;
        DirectoryInfo activeQMSDocuments;
        DirectoryInfo sop1Documents;

        public DirectoryInfo ActiveQMSDocuments { get => activeQMSDocuments; set => activeQMSDocuments = value; }
        public DirectoryInfo Sop1Documents { get => sop1Documents; set => sop1Documents = value; }
        public List<FileInfo> Files { get => files; set => files = value; }
        public List<FileInfo> SafeFiles { get => safeFiles; set => safeFiles = value; }
        public DirectoryInfo FixtureDir { get => fixtureDir; set => fixtureDir = value; }
        public DirectoryInfo BaseFixtureDir { get => baseFixtureDir; set => baseFixtureDir = value; }

        public FixtureUtil()
        {
            var unittest_dir_path = Directory.GetCurrentDirectory();
            this.unittest_dir = new DirectoryInfo(unittest_dir_path);
            var parent1 = this.unittest_dir.Parent;
            var parent = parent1.Parent;
            this.qmsHero_dir = parent;
            this.BaseFixtureDir = new DirectoryInfo(Path.Combine(this.qmsHero_dir.FullName, "Fixtures"));
            this.FixtureDir = DirectoryCopy(this.BaseFixtureDir, "Processing", true);
            DirectoryInfo[] dirs = this.FixtureDir.GetDirectories();
            this.ActiveQMSDocuments = dirs[0];
            DirectoryInfo[] sub_dirs = dirs[0].GetDirectories();
            this.sop1Documents = sub_dirs[0];
            this.files = new List<FileInfo>();
           foreach (DirectoryInfo subDir in sub_dirs)
            {
                foreach(FileInfo fileInfo in subDir.GetFiles())
                {
                    this.files.Add(fileInfo);
                }
            }
            this.SafeFiles = this.GetSafeFiles(this.files);
            this.WordSample = this.SafeFiles[0];
            this.ExcelSample = this.SafeFiles[1];
        }

        public List<FileInfo> GetSafeFiles(List<FileInfo> files)
        {
            var result = files.Where((file) => file.Name.StartsWith("~") == false).ToList();
            return result;
        }
        
        private DirectoryInfo DirectoryCopy(DirectoryInfo dir, string destDirName, bool copySubDirs)
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

    }
}
