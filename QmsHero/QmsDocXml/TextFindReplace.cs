﻿using DocumentFormat.OpenXml.Packaging;
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
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;

namespace QmsDocXml
{
    public class TextFindReplace : DocProperty, IReadDocRegex, IWriteDocRegex
    {
        Regex regex;
        int count;

        private TextFindReplace(): base()
        {
        }

        public TextFindReplace(string state) : this()
        {
            this.State = state;
            this.regex = new Regex(state);
        }

        private TextFindReplace(string state, Regex rx) : base(state)
        {
            this.regex = rx;
        }

        private TextFindReplace(string state, Regex rx, int replacedCount): base(state)
        {
            this.regex = rx;
            this.count = replacedCount;
        }

        public Regex Regex { get => regex; }
        public int Count { get => count; }

        public override Result<QDocProperty> Read(WordprocessingDocument doc)
        {
            int count = TextXml.SearchCount(doc, regex);

            if (count > 0)
            {
                return Results.Ok<QDocProperty>(TextFindReplace.Create((string)this.State, regex.ToString(), count));
            }
            else
            {
                return Results.Ok<QDocProperty>(TextFindReplace.Create((string)this.State, regex.ToString(), count));
            }
        }
        
        public override Result<QDocProperty> Write(WordprocessingDocument doc)
        {

            int referenceCount = TextXml.SearchCount(doc, this.regex);

            int replacedCount = 0;
            //main
            var mainTextEls = doc.MainDocumentPart.Document.Descendants<Wxml.Paragraph>().ToList();
            replacedCount += TextXml.ReplaceParagraphElementText(mainTextEls, this.regex, (string)this.State);

            //header
            foreach(var header in doc.MainDocumentPart.HeaderParts)
            {
                MatchCollection matches = this.regex.Matches(header.Header.InnerText);
                if(matches.Count > 0)
                    {
                        var textEls = header.RootElement.Descendants<Wxml.Paragraph>().Where(textEl=> this.regex.Matches(textEl.InnerText).Count > 0);
                        replacedCount += TextXml.ReplaceParagraphElementText(textEls, this.regex, (string)this.State);
                    }
            }
            //footer
            foreach (var footer in doc.MainDocumentPart.FooterParts)
            {
                MatchCollection matches = this.regex.Matches(footer.Footer.InnerText);
                if (matches.Count > 0)
                {
                    var textEls = footer.RootElement.Descendants<Wxml.Paragraph>().Where(textEl => this.regex.Matches(textEl.InnerText).Count > 0);
                    replacedCount += TextXml.ReplaceParagraphElementText(textEls, this.regex, (string)this.State);
                }
            }

            //final check
            //-1 to account for potential occurence in FileName
            if (referenceCount == replacedCount-1 | referenceCount == replacedCount)
                return Results.Ok<QDocProperty>(TextFindReplace.Create(this.Regex.ToString(), (string)this.State, replacedCount));

            else
            {
                return Results.Fail(new Error($"{referenceCount} suspected occurences of pattern '{this.regex.ToString()}', and {replacedCount} were replaced with new text {(string)this.State}."));
            }
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc)
        {
            int count = TextXml.SearchCount(doc, this.regex);
            return Results.Ok<QDocProperty>(TextFindReplace.Create((string)this.State, regex.ToString(), count));
        }
        public override Result<QDocProperty> Write(SpreadsheetDocument doc)
        {
            int count = 0;

            var referenceCountResult = this.Read(doc).Value as TextFindReplace;
            int referenceCount = referenceCountResult.Count;



            foreach (var worksheetPart in doc.WorkbookPart.WorksheetParts)
            {
                foreach (var worksheet in worksheetPart.Worksheet)
                {
                    foreach(var header in worksheetPart.Worksheet.Elements<Sxml.HeaderFooter>())
                    {
                        Match currentNameMatch = this.regex.Match(header.OddHeader.Text);
                        if (currentNameMatch.Success)
                        {
                            string newHeaderText = header.OddHeader.Text.Replace(currentNameMatch.ToString(), (string)this.State);
                            header.OddHeader.Text = newHeaderText;
                            count++;
                        }

                    }
                    var worksheetMatches = this.regex.Matches(worksheet.InnerText);
                    count += worksheetMatches.Count;
                    }
                }
            if(referenceCount-1 == count | referenceCount == count)
                return Results.Ok<QDocProperty>(TextFindReplace.Create(this.regex.ToString(), (string)this.State, count));
            else
            {
                return Results.Fail(new Error($"{referenceCount} occurences of {this.regex.ToString()} suspected and {count} were replace."));
            }
        }

        public static TextFindReplace Create(string findPattern, string replacementText)
        {
            return new TextFindReplace(replacementText, new Regex(findPattern));
        }

        public static TextFindReplace Create(string findPattern)
        {
            return new TextFindReplace(findPattern);
        }

        private static TextFindReplace Create(string findPattern, string replacementText, int count)
        {
            return new TextFindReplace(replacementText, new Regex(findPattern), count);
        }

    }
}