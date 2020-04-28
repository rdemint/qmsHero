using QDoc.Core;
using QmsDoc.Docs.Common.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Common.PropertyGroups
{
    public class HeaderPropertyGroup: QDocPropertyGroup
    {
        HeaderEffectiveDate headerEffectiveDate;
        HeaderLogo headerLogo;
        HeaderName headerName;
        HeaderRevision headerRevision;
        //FileRevision fileRevision;

        public HeaderPropertyGroup()
        {
            this.headerEffectiveDate = new HeaderEffectiveDate();
            this.headerLogo = new HeaderLogo();
            this.headerName = new HeaderName();
            this.headerRevision = new HeaderRevision();
            //this.fileRevision = new FileRevision();
        }

        public HeaderEffectiveDate HeaderEffectiveDate { get => headerEffectiveDate; set => headerEffectiveDate = value; }
        public HeaderLogo HeaderLogo { get => headerLogo; set => headerLogo = value; }
        public HeaderName HeaderName { get => headerName; set => headerName = value; }
        public HeaderRevision HeaderRevision { get => headerRevision; set => headerRevision = value; }
        //public FileRevision FileRevision { get => fileRevision; set => fileRevision = value; }
    }
}
