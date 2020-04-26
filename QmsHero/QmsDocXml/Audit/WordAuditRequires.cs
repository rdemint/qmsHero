using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using QmsDoc.Exceptions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Audit
{
    public static class WordAuditRequires
    {

        public static void SingleParagraph(List<Paragraph> pars)
        {
            if(pars.Count > 1)
            {
                throw new MultipleElementsExistException();
            }
        }
    }
}
