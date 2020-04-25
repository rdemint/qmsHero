using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using Vml = DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Packaging;

namespace QmsDocXml.Common
{
    static class ExcelXml
    {
        public static Xdr.WorksheetDrawing GetWorksheetDrawing()
        {
            var imageData = new Vml.ImageData();
            imageData.RelationshipId = "rId1";
            var xDrawing = new Xdr.WorksheetDrawing();
            return xDrawing;
        }

        //public static VmlDrawingPart NewVmlDrawingPart(WorksheetPart worksheetPart, string imageRelationshipId)
        //{
        //    VmlDrawingPart myPart = worksheetPart.AddNewPart<VmlDrawingPart>();
        //    var imageData = new Vml.ImageData();
        //    imageData.RelationshipId = imageRelationshipId;
        //    var myPartDrawing = GetWorksheetDrawing(imageRelationshipId); 
        //}

        public static int GetNewVmlPartDrawingPartId(WorksheetPart worksheetPart, IEnumerable<VmlDrawingPart> vmlDrawingParts)
        {
            List<int> currentIds = new List<int>();
            foreach(var part in vmlDrawingParts)
            {
                string tempStr = worksheetPart.GetIdOfPart(part).Replace("rId", "");
                int tempInt = Convert.ToInt32(tempStr);
                currentIds.Add(tempInt);
            }
            int newVal = currentIds.Max() + 1;
            return newVal;
        }

        public static string XElementAttributeValueSearch(System.Xml.Linq.XElement el, string attrLocalName)
        {
            return el.Attributes(attrLocalName).ToString();
        }

    }
}
