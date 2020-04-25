using Oxml = DocumentFormat.OpenXml;
using System.Xml.Serialization;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xl = DocumentFormat.OpenXml.Spreadsheet;
using Ovml = DocumentFormat.OpenXml.Vml;
using DRAWING = DocumentFormat.OpenXml.Drawing;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DrawingPictures = DocumentFormat.OpenXml.Drawing.Pictures;
using QmsDoc.Core;
using QmsDoc.Docs.Word;
using QmsDocXml.Exceptions;
using QmsDocXml.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QDoc.Core;
using System.IO;
using QmsDoc.Docs.Excel;
using QmsDoc.Exceptions;
using System.Text.RegularExpressions;
using System.IO.Packaging;

namespace QmsDocXml
{
    public class Logo : DocProperty
    {
         public Logo()
        {
        }

        public Logo(object state) : base(state)
        {
        }

        public override DocProperty Read(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            DocumentFormat.OpenXml.Wordprocessing.TableCell cell = WordPartHeaderTableCell.Get(doc, docConfig.LogoRow, docConfig.LogoCol);

            IEnumerable<Drawing> drawings = cell.Descendants<Drawing>()
                .Where(
                    d => d.Descendants<DrawingPictures.Picture>()
                      .Any(p => p.BlipFill.Blip.Embed != null)
                );

            if(drawings.Any())
            {
                Drawing drawing = cell.Descendants<Drawing>().First();
                DRAWING.GraphicData graphicData = drawing.Descendants<DRAWING.GraphicData>().First();
                var els = graphicData.Elements().ToList();
                var picture = graphicData.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>().First();

                
                var pictureProperties = picture.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties>().First();
                return new Logo(pictureProperties.Name.ToString());
            }

            else
            {
                return null;
            }
        }

        public override DocProperty Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetPart = doc.WorkbookPart.WorksheetParts.First();

            var pageSetup = workSheetPart.Worksheet.Elements<Xl.PageSetup>().First();
            string relationshipId = pageSetup.Id;


            var vmlDrawingParts = workSheetPart.VmlDrawingParts;

            if(vmlDrawingParts.Count() > 1)
            {
                throw new MultipleElementsExistException();
            }

            else
            {
                var imageParts = vmlDrawingParts.First().ImageParts;
                if(imageParts.Count() > 1)
                {
                    throw new MultipleElementsExistException();
                }
                else
                {
                    
                    
                    string imageName = Path.GetFileName(imageParts.First().Uri.ToString());
                    return new Logo(imageName);
                }

            }

        }

        public override void Write(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            //https://stackoverflow.com/questions/2810138/replace-image-in-word-doc-using-openxml
            //https://stackoverflow.com/questions/43320452/removing-images-in-header-with-openxml-sdk

            FileInfo imageFile = new FileInfo((string)this.State);

            DocumentFormat.OpenXml.Wordprocessing.TableCell cell = WordPartHeaderTableCell.Get(doc, docConfig.LogoRow, docConfig.LogoCol);
            Paragraph cellPar = cell.Descendants<Paragraph>().First();
            //https://stackoverflow.com/questions/43320452/removing-images-in-header-with-openxml-sdk

            IEnumerable<Drawing> drawings = cell.Descendants<Drawing>()
                .Where(
                    d => d.Descendants<DrawingPictures.Picture>()
                      .Any(p => p.BlipFill.Blip.Embed != null)
                );

            if(drawings.Any())
            {
                MainDocumentPart mainPart = doc.MainDocumentPart;
                ImagePart imagePart = mainPart.HeaderParts.First().AddImagePart(ImagePartType.Jpeg);
                //Alternative does not work
                //ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open))
                {
                    imagePart.FeedData(stream);
                }

                string imageId = mainPart.HeaderParts.First().GetIdOfPart(imagePart);
                ImageXml.Add(doc, imageId, cellPar, imageFile);
            }
        }

        public override void Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {

            FileInfo imageFile = new FileInfo(this.State.ToString());
            
            var workSheetPart = doc.WorkbookPart.WorksheetParts.First();
            var vmlParts = workSheetPart.VmlDrawingParts;
            var vmlPart = workSheetPart.VmlDrawingParts.First();


            //get the current relationship Id, for resuse in the to-be-created relationship with new ImagePart
            //var myParts = workSheetPart.Parts;
            //var vmlUri = new Uri("/xl/drawings/vmlDrawing1.vml", UriKind.Relative);
            //var myVmlPart = doc.Package.GetPart(vmlUri);

            //get the content of the vmlPart 

            using (StreamReader streamReader = new StreamReader(vmlPart.GetStream()))
            {
                var vmlContent = streamReader.ReadToEnd();
                
            }



            var xmlPartReader = DocumentFormat.OpenXml.OpenXmlPartReader.Create(vmlPart.GetStream());
            List<Oxml.OpenXmlElement> xmlEls = new List<Oxml.OpenXmlElement>();
            while (xmlPartReader.Read())
            {
                var currentEl = xmlPartReader.LoadCurrentElement();
                if (currentEl.HasChildren)
                {
                    foreach(var child in currentEl.ChildElements)
                    {
                        //var asShapeType = child as Ovml.Shapetype;
                        //var asShape = child as Ovml.Shape;
                        //var asShapeLayout = child as Ovml.Office.ShapeLayout;

                        try
                        {
                            var myShape = new Ovml.Shape(child.OuterXml);
                        }

                        catch
                        {
                            //too bad
                        }

                        try
                        {
                            var myShapeType = new Ovml.Shapetype(child.OuterXml);
                        }
                        catch
                        {
                            //too bad
                        }

                        try
                        {
                            var myShapeLayout = new Ovml.Office.ShapeLayout(child.OuterXml);
                        }

                        catch
                        {
                            //too bad
                        }
                        
                        //if (asShapeType != null)
                        //{
                        //    xmlEls.Add(asShapeType);
                        //}
                        //else if(asShape != null)
                        //{
                        //    xmlEls.Add(asShape);
                        //}
                    }
                }
            }
            xmlPartReader.Close();

            //var myRelPair = workSheetPart.Parts.ToList()[0];
            //var myRel = vmlParts.First().GetReferenceRelationship(myRelPair.RelationshipId);
            //string myRelId = myRel.Id;

            //workSheetPart.DeleteReferenceRelationship(myRel.Id);

            //create the new ImagePart.  Create a relationship using the original Reference relationship id
            //var newImagePart = vmlParts.First().AddImagePart(ImagePartType.Jpeg);
            //var newRelId = workSheetPart.CreateRelationshipToPart(newImagePart, myRelId);

            //    using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open))
            //{
            //    newImagePart.FeedData(stream);
            //}



            //3 Just overwrite current imagePart
            //var vmlDrawing = workSheetPart.VmlDrawingParts.First();
            //var myId = workSheetPart.GetIdOfPart(vmlDrawing);



            //using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open))
            //{
            //    currentImagePart.FeedData(stream);
            //}

            //1 Replace legacy with new
            //workSheetPart.Worksheet.RemoveAllChildren<Xl.LegacyDrawingHeaderFooter>();
            //workSheetPart.DeleteParts<VmlDrawingPart>(workSheetPart.VmlDrawingParts);

            //workSheetPart.AddNewPart<DrawingsPart>();
            //ImagePart imagePart = workSheetPart.DrawingsPart.AddImagePart(ImagePartType.Jpeg);
            //string imagePartId = workSheetPart.DrawingsPart.GetIdOfPart(imagePart);

            //var xDrawing = new Xdr.WorksheetDrawing();
            //workSheetPart.DrawingsPart.WorksheetDrawing = xDrawing;

            //var worksheetDrawing = workSheetPart.DrawingsPart.WorksheetDrawing

            ////create new WorksheetDrawing
            //var el = ImageXml.GetWorksheetDrawing(imagePartId, imageFile);
            //workSheetPart.DrawingsPart.WorksheetDrawing = el;

            //var newDrawHf = new Xl.DrawingHeaderFooter();
            ////var idInt32 = Convert.ToUInt32(imagePartId);
            //workSheetPart.Worksheet.Append(newDrawHf);



            //2 Add by legacy




        }
    }
}
