using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using Wxml = DocumentFormat.OpenXml.Wordprocessing;
using Sxml = DocumentFormat.OpenXml.Spreadsheet;
using System.Text.RegularExpressions;
using FluentResults;
using QDoc.Core;
using DocumentFormat.OpenXml;
using System.IO;

namespace QmsDocXml.Common
{
    public static class TextXml
    {

        public static MatchCollection Search(WordprocessingDocument doc, Regex rx)
        {
            string docText = null;
            using (StreamReader sr = new StreamReader(doc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }

            return rx.Matches(docText);
            
        }
        
        public static void SearchAndReplace(WordprocessingDocument doc, Regex rx, string newValue)
        {
                string docText = null;
                using (StreamReader sr = new StreamReader(doc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                docText = rx.Replace(docText, newValue);

                using (StreamWriter sw = new StreamWriter(doc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }
        }

        public static List<MatchCollection> SearchInnerText(WordprocessingDocument doc, Regex rx)
        {
            List<MatchCollection> matches = new List<MatchCollection>();
            matches.Add(rx.Matches(doc.MainDocumentPart.Document.InnerText));
            
            
            if(doc.MainDocumentPart.HeaderParts.Any())
            foreach(var headerPart in doc.MainDocumentPart.HeaderParts)
            {
                matches.Add(rx.Matches(headerPart.Header.InnerText));
            }

            if(doc.MainDocumentPart.FooterParts.Any())
            foreach(var footerPart in doc.MainDocumentPart.FooterParts)
            {
                matches.Add(rx.Matches(footerPart.Footer.InnerText));
            }

            if(doc.MainDocumentPart.ImageParts.Any())
            foreach(var imagePart in doc.MainDocumentPart.ImageParts)
            {
                    if(imagePart.RootElement != null && imagePart.RootElement.InnerText != null)
                    {
                        matches.Add(rx.Matches(imagePart.RootElement.InnerText));
                    }
            }

            return matches;

        }

        public static List<MatchCollection> SearchInnerText(SpreadsheetDocument doc, Regex rx)
        {
            List<MatchCollection> matches = new List<MatchCollection>();

            foreach (var worksheetPart in doc.WorkbookPart.WorksheetParts)
            {
                //header
                foreach (var worksheet in worksheetPart.Worksheet)
                {
                    matches.Add(rx.Matches(worksheet.InnerText));
                }
            }
            return matches;
        }

        public static int SearchCount(WordprocessingDocument doc, Regex rx) {

            var matches = SearchInnerText(doc, rx);
            int count = 0;
            foreach (var match in matches)
            {
                count += match.Count;
            }
            return count;

        }

        public static int SearchCount(SpreadsheetDocument doc, Regex rx)
        {
            var matches = SearchInnerText(doc, rx);
            int count = 0;
            foreach (var match in matches)
            {
                count += match.Count;
            }
            return count;
        }

        public static int ReplaceParagraphElementText(IEnumerable<Wxml.Paragraph> parEls, Regex rx, string replacementText)
        {
            int count = 0;
            //.Elements<Wxml.Run>().First().InnerText != replacementText)
            //{
            var parMatches = parEls.Where(par => rx.IsMatch(par.InnerText));    
            foreach(var par in parMatches)
                {
                    var result = ReplaceRunElementText(par, par.Descendants<Wxml.Run>(), rx, replacementText);
                        if (result.IsSuccess)
                            {
                            count+= (int)result.Value;
                            }
                        else
                            {
                    //match is split over multiple runs. 
                        var runClone = par.Descendants<Wxml.Run>().Where(run => run.Elements<Wxml.Text>().Any() == true).First().Clone() as Wxml.Run;
                        var textClone = (Wxml.Text)runClone.Descendants<Wxml.Text>().First().Clone();

                            string matchText = rx.Match(par.InnerText).ToString();
                            string newInnerText = par.InnerText.Replace(matchText, replacementText);
                            textClone.Text = newInnerText;

                            runClone.RemoveAllChildren<Wxml.Text>();
                            par.RemoveAllChildren<Wxml.Run>();

                            runClone.AppendChild<Wxml.Text>(textClone);
                            par.AppendChild<Wxml.Run>(runClone);
                            count += 1;
                        }

                }
            return count;
        }

        public static Result<int> ReplaceRunElementText(Wxml.Paragraph par, IEnumerable<Wxml.Run> runEls, Regex rx, string replacementText)
        {
            var count = 0;
            foreach(var run in runEls)
            {
                Match runMatch = rx.Match(run.InnerText);
                if(runMatch.Success)
                {
                    var referenceRunTexts = run.Descendants<Wxml.Text>();
                    var result = ReplaceTextElementText(referenceRunTexts, rx, replacementText);
                    if (result.IsSuccess)
                    {
                        count++;
                    }
                    else
                    {
                        var modifyResult = ModifyRunTextChildren(run, rx, replacementText);
                        if (modifyResult.IsSuccess)
                            count++;
                    }
                }
                
            }
            if(count>0)
            {
                return Results.Ok<int>(count);
            }
            else
            {
                return Results.Fail(new Error("No match"));

            }
        }

        private static Result<string> ModifyRunTextChildren(Wxml.Run run, Regex rx, string replacementText)
        {
            var textEls = run.Elements<Wxml.Text>().ToList();
            if (textEls.Any())
            {
                var runTextClone = (Wxml.Text)textEls.First().Clone();
                runTextClone.Text = replacementText;
                string textSum = "";
                for (var i = 0; i < textEls.Count(); i++)
                {
                    textSum += textEls[i].InnerText;
                    Match matchInnerText = rx.Match(textSum);
                    if (matchInnerText.Success)
                    {
                        //The list of Text elements whose concatenated InnerText yields the match.
                        List<Wxml.Text> textElsToDelete = new List<Wxml.Text>();
                        for (var j = 0; j <= i; j++)
                        {
                            textElsToDelete.Add(textEls[j]);
                        }
                        foreach (var el in textElsToDelete)
                        {
                            run.RemoveChild<Wxml.Text>(el);
                        }

                        run.PrependChild<Wxml.Text>(runTextClone);
                        return Results.Ok<string>("Match success");
                    }
                }
            }
            return Results.Fail(new Error("Match not successful"));
        }
        public static Result<string> ReplaceTextElementText(IEnumerable<Wxml.Text> textEls, Regex rx, string replacementText)
        {
            int replacedCount = 0;
            
            foreach (var textEl in textEls)
                {
                    var match = rx.Match(textEl.Text);
                    if (match.Success)
                    {
                        string newText = textEl.Text.Replace(match.ToString(), replacementText);
                        textEl.Text = newText;
                        replacedCount += 1;
                        return Results.Ok<string>(match.ToString());
                    }
                }
            return Results.Fail(new Error("Did not match Text text"));
        }
        }
    }
