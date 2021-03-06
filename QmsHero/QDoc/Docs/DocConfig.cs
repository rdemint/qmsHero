﻿using QDoc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QDoc.Docs
{
    public class DocConfig: IDocConfig
    {
        string fileRevisionText;
        Regex fileRevisionRegex;
        Regex fileSopNumberRegex;
        Regex fileSopNumberAndFirstThreeLettersRegex;
        Regex fileFormNumberRegex;
        Regex fileFormNumberAndFirstThreeLettersNameRegex;
        Regex fileNumberRegex;
        string lastModifiedByText;
        DateTime modifiedTime;
        DateTime createdTime;
        string creatorText;

        public DocConfig()
        {
            this.FileRevisionText = "Rev";
            this.FileSopNumberRegex = new Regex(@"SOP-\d\d\d");
            this.FileSopNumberAndFirstThreeLettersRegex = new Regex(@"SOP-\d\d\d,?\s\w\w\w");
            this.FileFormNumberRegex = new Regex(@"F-\d\d\d\w");
            this.FileFormNumberAndFirstThreeLettersNameRegex = new Regex(@"F-\d\d\d,?\s\w\w\w");
            this.FileNumberRegex = new Regex(@"-\d\d\d");
            this.LastModifiedByText = "Lean RAQA Systems";
            this.CreatorText = "Lean RAQA Systems";
            this.ModifiedTime = DateTime.Now;
            this.CreatedTime = DateTime.Now;
        }

        public Regex FileSopNumberRegex { get => fileSopNumberRegex; set => fileSopNumberRegex = value; }
        public Regex FileFormNumberRegex { get => fileFormNumberRegex; set => fileFormNumberRegex = value; }
        public string FileRevisionText { get => fileRevisionText;
            set { 
                
                fileRevisionText = value;
                this.FileRevisionRegex = new Regex(Regex.Escape(fileRevisionText) + @"\d{1,2}");
            }
        }
        public Regex FileRevisionRegex { get => fileRevisionRegex; set => fileRevisionRegex = value; }
        public Regex FileNumberRegex { get => fileNumberRegex; set => fileNumberRegex = value; }
        public string LastModifiedByText { get => lastModifiedByText; set => lastModifiedByText = value; }
        public DateTime ModifiedTime { get => modifiedTime; set => modifiedTime = value; }
        public DateTime CreatedTime { get => createdTime; set => createdTime = value; }
        public string CreatorText { get => creatorText; set => creatorText = value; }
        public Regex FileFormNumberAndFirstThreeLettersNameRegex { get => fileFormNumberAndFirstThreeLettersNameRegex; set => fileFormNumberAndFirstThreeLettersNameRegex = value; }
        public Regex FileSopNumberAndFirstThreeLettersRegex { get => fileSopNumberAndFirstThreeLettersRegex; set => fileSopNumberAndFirstThreeLettersRegex = value; }
    }
}
