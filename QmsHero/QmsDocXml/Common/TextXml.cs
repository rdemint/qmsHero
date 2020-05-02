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

        public static Match Search(WordprocessingDocument doc, Regex rx)
        {
            string docText = null;
            using (StreamReader sr = new StreamReader(doc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();
            }

            return rx.Match(docText);
            
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
        }
    }
