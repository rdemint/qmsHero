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
                    matches.Add(rx.Matches(imagePart.RootElement.InnerText));
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

        public static int ReplaceParagraphElementText(IEnumerable<Wxml.Paragraph> parEls, Regex rx, string replacementText)
        {
            int count = 0;
            foreach(var par in parEls)
            {
                Match matchPar = rx.Match(par.InnerText);
                if(matchPar.Success)
                {
                    count += ReplaceRunElementText(par.Elements<Wxml.Run>(), rx, replacementText);
                }
            }
            return count;
        }

        public static int ReplaceRunElementText(IEnumerable<Wxml.Run> runEls, Regex rx, string replacementText)
        {
            var referenceRun = (Wxml.Run)runEls.First();
            var referenceRunNewText = (Wxml.Text)referenceRun.Descendants<Wxml.Text>().First().Clone();
            referenceRunNewText.Text = replacementText;
            //Delete current elements
            //par.RemoveAllChildren<Wxml.Run>();

            referenceRun.RemoveAllChildren<Wxml.Run>();
            referenceRun.Append(referenceRunNewText);

            return 1;
        }

        public static int ReplaceTextElementText(IEnumerable<Wxml.Text> textEls, Regex rx, string replacementText)
        {
            int replacedCount = 0;
            
            if (textEls.Any())
            {
                foreach (var textEl in textEls)
                {
                    var match = rx.Match(textEl.Text);
                    if (match.Success)
                    {
                        string newText = textEl.Text.Replace(match.ToString(), replacementText);
                        textEl.Text = newText;
                        replacedCount += 1;
                    }
                }
            }
            return replacedCount;
        }


    }


    }
