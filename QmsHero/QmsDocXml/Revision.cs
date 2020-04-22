using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QDoc.Core;
using QDoc.Interfaces;
using QmsDoc.Docs.Word;
using QmsDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDocXml
{
    public class Revision: DocProperty
    {
        public Revision()
        {
        }

        public Revision(object state) : base(state)
        {
        }

        public Paragraph FetchRevisionPart(WordprocessingDocument doc, WordDocConfig config)
        {
            TableCell cell = WordPartHeaderTableCell.Get(doc, config.RevisionRow, config.RevisionCol);
            return cell.Elements<Paragraph>().First();
        }
        
        public override QDocProperty Read(object doc, object docConfig)
        {
            WordprocessingDocument wdoc = (WordprocessingDocument)doc;
            WordDocConfig config = (WordDocConfig)docConfig;
            Paragraph par = FetchRevisionPart(wdoc, config);
            Match match = config.RevisionRegex.Match(par.InnerText);
            return new Revision(match.ToString());
        }

        public override void Write(object doc, IDocConfig docConfig)
        {
            WordprocessingDocument wdoc = (WordprocessingDocument)doc;
            WordDocConfig config = (WordDocConfig)docConfig;
            Paragraph par = FetchRevisionPart(wdoc, config);
            par.RemoveAllChildren();
            Run run = new Run();
            Text text = new Text();
            text.Text = config.RevisionText + (string)this.State;
            run.Append(text);
            par.Append(run);
            OnPropertyChanged(); ;
        }

        public override bool IsValid(IDocConfig config)
        {
            var rx = ((WordDocConfig)config).EffectiveDateRegex;
            var match = rx.Match(this.State.ToString());
            if (
                match.Success &&
                base.IsValid(config)
                ) { return true; }
            else { return false; }
        }
    }
}
