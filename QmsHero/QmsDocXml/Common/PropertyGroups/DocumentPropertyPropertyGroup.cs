using QDoc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDocXml.Common.PropertyGroups
{
    public class DocumentPropertyPropertyGroup : QDocPropertyGroup
    {
        DocumentPropertyCompany documentPropertyCompany;
        DocumentPropertyCreator documentPropertyCreator;
        DocumentPropertyCreatedTime documentPropertyCreatedTime;
        DocumentPropertyLastModifiedBy documentPropertyLastModifiedBy;
        public DocumentPropertyPropertyGroup()
        {
            this.documentPropertyCompany = new DocumentPropertyCompany();
            this.documentPropertyCreator = new DocumentPropertyCreator();
            this.documentPropertyLastModifiedBy = new DocumentPropertyLastModifiedBy();
            this.documentPropertyCreatedTime = new DocumentPropertyCreatedTime();
        }

        public DocumentPropertyCompany DocumentPropertyCompany { get => documentPropertyCompany; set => documentPropertyCompany = value; }
        public DocumentPropertyLastModifiedBy DocumentPropertyLastModifiedBy { get => documentPropertyLastModifiedBy; set => documentPropertyLastModifiedBy = value; }
        public DocumentPropertyCreatedTime DocumentPropertyCreatedTime { get => documentPropertyCreatedTime; set => documentPropertyCreatedTime = value; }
        public DocumentPropertyCreator DocumentPropertyCreator { get => documentPropertyCreator; set => documentPropertyCreator = value; }
    }


}
