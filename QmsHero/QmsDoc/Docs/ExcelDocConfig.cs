using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace QmsDoc.Docs
{
    public class ExcelDocConfig: QmsDocBaseConfig
    {
        Workbook doc;
        HeaderFooter headerFooter;
        Range revision;
        Range effectiveDate;
        Range logo;

        public ExcelDocConfig()
        {

        }



        #region HeaderFooter
        public HeaderFooter HeaderFooter { 
            get => headerFooter; 
            set => headerFooter = value; }
        public Range Logo { 
            get => headerFooter.Tables(1); 
            set => logo = value; }
        public Range EffectiveDate { get => effectiveDate; set => effectiveDate = value; }
        public Range Revision { get => revision; set => revision = value; }

        #endregion
    }
}
