using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QDoc.Core;
using QDoc.Interfaces;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDocXml.Docs.Word.Properties
{
    class Revision: QDocProperty
    {
        public Revision()
        {
        }

        public Revision(string state) : base(state)
        {
        }

        public Paragraph FetchRevisionPart(WordprocessingDocument doc, WordDocConfig config)
        {
            TableCell cell = WordPartHeaderTableCell.Get(doc, config.RevisionRow, config.RevisionCol);
            return cell.Elements<Paragraph>().First();
        }
        public string FetchRevision(WordprocessingDocument doc, WordDocConfig config)
        {
            Paragraph par = FetchRevisionPart(doc, config);
            Match match = Regex.Match(par.InnerText, @"\d{1,2}");
            var result = match.ToString().Replace(config.RevisionText, "");
            return result;
        }

        public override QDocProperty Read(object doc, object docConfig)
        {
            WordprocessingDocument wdoc = (WordprocessingDocument)doc;
            WordDocConfig config = (WordDocConfig)docConfig;
            Paragraph par = FetchRevisionPart(wdoc, config);
            Match match = config.RevisionRegex.Match(par.InnerText);
            return new Revision(match.ToString());
        }

        public override void Write(object doc, IDocConfig docConfig, string value)
        {
            WordprocessingDocument wdoc = (WordprocessingDocument)doc;
            WordDocConfig config = (WordDocConfig)docConfig;
            Paragraph par = FetchRevisionPart(wdoc, config);
            par.RemoveAllChildren();
            Run run = new Run();
            Text text = new Text();
            text.Text = config.RevisionText + value;
            run.Append(text);
            par.Append(run);
            OnPropertyChanged(); ;
        }

        public override bool IsValid(IDocConfig config)
        {
            var rx = ((WordDocConfig)config).EffectiveDateRegex;
            var match = rx.Match(this.State);
            //Match match = Regex.Match(this.Value, @"d{1,2}");
            if (
                match.Success &&
                base.IsValid(config)
                ) { return true; }
            else { return false; }
        }
    }
}
