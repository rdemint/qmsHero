using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Docs.Word.Properties
{
    class Revision
    {
        public Paragraph FetchRevisionPart()
        {
            TableCell cell = HeaderTableCell.Get(this.DocConfig.RevisionRow, DocConfig.RevisionCol);
            return cell.Elements<Paragraph>().First();
        }
        public string FetchRevision()
        {
            Paragraph par = FetchRevisionPart();
            Match match = Regex.Match(par.InnerText, @"\d{1,2}");
            var result = match.ToString().Replace(DocConfig.RevisionText, "");
            return result;
        }

        public string Revision
        {
            get
            {
                return this.revision;
            }

            set
            {
                Paragraph par = FetchRevisionPart();
                //par.Elements<Run>().Elements<Text>().First()
                par.RemoveAllChildren();
                Run run = new Run();
                Text text = new Text();
                text.Text = DocConfig.RevisionText + value;
                run.Append(text);
                par.Append(run);
                this.revision = value;
                OnPropertyChanged();
            }
        }
    }
}
