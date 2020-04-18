﻿
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
    public class FixtureUtil
    {
        //Provids blueprint functionality to create and clean a Fixture directory
        //within the current or specified directory.
        //Use in conjunction with Types that have functionality to copy and edit files
        DirectoryInfo processingDir;
        DirectoryInfo referenceDir;
        DirectoryInfo fixtureDir;

        public FixtureUtil()
        {
            Initialize();
        }
        
        public FixtureUtil(string fixtureDirName, string referenceDirName, string processingDirName)
        {
            Initialize(fixtureDirName, referenceDirName, processingDirName);
        }

        public FixtureUtil(DirectoryInfo fixtureDir, string referenceDirName, string processingDirName)
        {
            Contract.Requires(fixtureDir.Exists);
            FixtureDir = fixtureDir;
            Initialize(FixtureDir.Name, referenceDirName, processingDirName);
        }
        public DirectoryInfo ReferenceDir { get => referenceDir; }
        public List<FileInfo> ReferenceFiles { get { return ReferenceDir.GetFiles("*", SearchOption.AllDirectories).ToList(); } }
        public DirectoryInfo ProcessingDir { get => processingDir; }
        public List<FileInfo> ProcessingFiles { get { return ProcessingDir.GetFiles("*", SearchOption.AllDirectories).ToList(); } }

        public DirectoryInfo FixtureDir { get => fixtureDir; set => fixtureDir = value; }

        public void Initialize(DirectoryInfo fixtureDir, string referenceDirName = "Reference", string processingDirName = "Processing")
        {
            processingDir = FileUtil.CreateOrCleanSubDirectory(FixtureDir, processingDirName);
            Contract.Requires(ProcessingDir.Exists);
            referenceDir = FileUtil.CreateOrCleanSubDirectory(FixtureDir, referenceDirName);
            Contract.Requires(ReferenceDir.Exists);
        }
        
        public void Initialize(string fixtureDirName="Fixtures", string referenceDirName="Reference", string processingDirName="Processing")
        {
            var unittestDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            FixtureDir = new DirectoryInfo(Path.Combine(unittestDir.Parent.Parent.FullName, fixtureDirName));
            Contract.Requires(FixtureDir.Exists);
            Initialize(FixtureDir, referenceDirName, processingDirName);
        }

        public virtual bool IsValid()
        {
            if(
                FixtureDir.Exists &&
                ReferenceDir.Exists && 
                ProcessingDir.Exists &&
                ReferenceFiles.Count >=1
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

        public virtual int CleanProcessingDir()
        {
            var count = FileUtil.CleanDirectoryAndChildren(ProcessingDir);
            return count;
        }
        
    }
}
