using DocumentFormat.OpenXml.Packaging;
using Wxml = DocumentFormat.OpenXml.Wordprocessing;
using Sxml = DocumentFormat.OpenXml.Spreadsheet;
using FluentResults;
using QDoc.Core;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using QmsDocXml.Common;
using QmsDoc.Interfaces;

namespace QmsDocXml
{
    public class TextIsClean : DocProperty, IReadDocRegex
    {
        Regex regex;

        private TextIsClean(): base()
        {
        }

        public TextIsClean(string state) : this()
        {
            this.State = state;
            this.regex = new Regex(state);
        }

        private TextIsClean(string state, Regex rx) : base(state)
        {
            this.regex = rx;
        }

        public Regex Regex { get => regex; }

        public override Result<QDocProperty> Read(WordprocessingDocument doc)
        {
            MatchCollection matches = TextXml.Search(doc, regex);
            if (matches.Count > 0)
                return Results.Fail(new Error($"The document contains {matches.Count} matches for '{matches.ToString()}'"));
            else
            {
                return Results.Ok<QDocProperty>(new TextIsClean((string)this.State, regex));
            }
        }
        
        public override Result<QDocProperty> Write(WordprocessingDocument doc)
        {
            TextXml.SearchAndReplace(doc, this.Regex, this.State.ToString());
            return Results.Ok<QDocProperty>(TextIsClean.Instance((string)this.State, (string)this.State));
        }

        public static TextIsClean Instance(string regexFindPattern, string textToFindOrInsert)
        {
            return new TextIsClean(textToFindOrInsert, new Regex(regexFindPattern));
        }

        public static TextIsClean Instance(string textToFind)
        {
            return new TextIsClean(textToFind);
        }
    }
}
