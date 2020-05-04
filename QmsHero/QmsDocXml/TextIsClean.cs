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
            int count = TextXml.SearchCount(doc, regex);

            if (count > 0)
            {
                return Results.Fail(new Error($"The document contains {count} matches for '{regex.ToString()}'"));
            }
            else
            {
                return Results.Ok<QDocProperty>(new TextIsClean((string)this.State, regex));
            }
        }
        
        public override Result<QDocProperty> Write(WordprocessingDocument doc)
        {

            int referenceCount = TextXml.SearchCount(doc, this.regex);

            int replacedCount = 0;
            //main
            foreach(var header in doc.MainDocumentPart.HeaderParts)
            {
                MatchCollection matches = this.regex.Matches(header.Header.InnerText) {
                    if(matches.Count > 0)
                    {
                        var textEls = header.RootElement.Elements<Wxml.Text>().Where(textEl=> this.regex.Matches(textEl.Text).Count > 0);
                        
                    }
                }
            }

            //header

            //footer

            //image
        }

        public static TextIsClean Create(string regexFindPattern, string textToFindOrInsert)
        {
            return new TextIsClean(textToFindOrInsert, new Regex(regexFindPattern));
        }

        public static TextIsClean Create(string textToFind)
        {
            return new TextIsClean(textToFind);
        }
    }
}
