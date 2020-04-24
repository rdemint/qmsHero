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
        public static Xdr.WorksheetDrawing GetVmlDrawing(string imageRelationshipId)
        {
            var imageData = new Vml.ImageData();
            imageData.RelationshipId = imageRelationshipId;
            var xDrawing = new Xdr.WorksheetDrawing(imageData);
            return xDrawing;
        }

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

    }
}
