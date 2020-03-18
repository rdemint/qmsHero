﻿using System;
using System.Collections.Generic;
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
        DirectoryInfo fixture_dir;
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

        public FixtureUtil()
        {
            var unittest_dir_path = Directory.GetCurrentDirectory();
            this.unittest_dir = new DirectoryInfo(unittest_dir_path);
            var parent1 = this.unittest_dir.Parent;
            var parent = parent1.Parent;
            this.qmsHero_dir = parent;
            this.fixture_dir = new DirectoryInfo(System.IO.Path.Combine(this.qmsHero_dir.FullName, "Fixtures"));
            DirectoryInfo[] dirs = this.fixture_dir.GetDirectories();
            this.activeQMSDocuments = dirs[0];
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
        
    }
}
