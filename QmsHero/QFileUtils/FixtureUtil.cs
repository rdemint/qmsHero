
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using Directory = System.IO.Directory;
using DirectoryInfo = System.IO.DirectoryInfo;
using FileInfo = System.IO.FileInfo;

namespace QFileUtil
{
    public abstract class FixtureUtil
    {
        //Provids blueprint functionality to create and clean a "Processing" directory
        //within the current or specified directory.
        //Use in conjunction with Types that have functionality to copy and edit files
        DirectoryInfo processingDir;
        DirectoryInfo dir;
        DirectoryInfo projectDir;
        List<FileInfo> files;
        List<FileInfo> processingFiles;

        public FixtureUtil()
        {
            Initialize();
        }
        
        public FixtureUtil(string fixtureDirName, string processingDirName)
        {
            Initialize(fixtureDirName, processingDirName);
        }

        public FixtureUtil(DirectoryInfo fixtureDir, DirectoryInfo processingDir)
        {
            Contract.Requires(fixtureDir.Exists);
            this.Dir = fixtureDir;
            this.ProcessingDir = processingDir;
        }
        public DirectoryInfo Dir { get => dir; set => dir = value; }
        public List<FileInfo> Files { get { return Dir.GetFiles("*", SearchOption.AllDirectories).ToList(); } }
        public DirectoryInfo ProcessingDir { get => processingDir; set => processingDir = value; }
        public List<FileInfo> ProcessingFiles { get { return Dir.GetFiles("*", SearchOption.AllDirectories).ToList(); } }

        
        public virtual void Initialize(string fixtureDirName="Fixtures", string processingDirName="Processing")
        { 
            var unittestDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            this.Dir = new DirectoryInfo(Path.Combine(unittestDir.Parent.Parent.FullName, fixtureDirName));
            Contract.Requires(this.Dir.Exists);
            this.ProcessingDir = FileUtil.CreateOrCleanSubDirectory(Dir, processingDirName);
            Contract.Requires(ProcessingDir.Exists);
        }

        public virtual bool IsValid()
        {
            if(
                Dir.Exists && 
                ProcessingDir.Exists &&
                Files.Count >=1
                )
            {
                return true;
            }
            else { return false; }
        }

        public virtual bool IsClean()
        {
            if (
                ProcessingDir.Exists &&
                ProcessingFiles.Count == 0
                ) 
            {
                return true;
                    }
            else
            {
                return false;
            }
        }
        
    }
}
