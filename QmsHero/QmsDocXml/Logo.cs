using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Xl = DocumentFormat.OpenXml.Spreadsheet;
using DRAWING = DocumentFormat.OpenXml.Drawing;
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
                AddImage.Add(doc, imageId, cellPar, imageFile);
            }
        }

        public override void Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            
            var workSheetPart = doc.WorkbookPart.WorksheetParts.First();

            //legacy drawing
            var legDrawHF = workSheetPart.Worksheet.Elements<Xl.LegacyDrawingHeaderFooter>().First();
            var legId = legDrawHF.First().Id;
            var vmlDraw = workSheetPart.VmlDrawingParts.First();



            var draws = workSheetPart.Worksheet.Elements<Xl.DrawingHeaderFooter>();

            //var pageSetup = workSheetParts.Worksheet.Elements<Xl.PageSetup>().First();
            //string relationshipId = pageSetup.Id;

            //var packageRelationship = doc.Package.GetRelationship(relationshipId);

            var vmlDrawingParts = workSheetPart.VmlDrawingParts;

            if (vmlDrawingParts.Count() > 1)
            {
                throw new MultipleElementsExistException();
            }

            else
            {
                var imageParts = vmlDrawingParts.First().ImageParts;
                if (imageParts.Count() > 1)
                {
                    throw new MultipleElementsExistException();
                }
                else
                {
                    ImagePart currentImagePart = imageParts.First();

                    var newImagePart = vmlDrawingParts.First().AddImagePart(ImagePartType.Jpeg);

                    using (FileStream stream = new FileStream((string)this.State, FileMode.Open))
                    {
                        newImagePart.FeedData(stream);
                    }
                }

            }
        }
    }
}
