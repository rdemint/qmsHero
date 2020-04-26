using DocumentFormat.OpenXml.Packaging;
using QmsDoc.Core;
using QmsDoc.Docs.Excel;
using QmsDoc.Docs.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml
{
    class HeaderName : DocProperty
    {
        public HeaderName()
        {
        }

        public HeaderName(object state) : base(state)
        {
        }

        public override DocProperty Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            return base.Read(doc, config);
        }

        public override DocProperty Read(WordprocessingDocument doc, WordDocConfig config)
        {
            return base.Read(doc, config);
        }


    }
}
