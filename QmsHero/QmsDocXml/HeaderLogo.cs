using Oxml = DocumentFormat.OpenXml;
using System.Xml.Serialization;
using Xlinq = System.Xml.Linq;
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
using System.Xml;
using QDoc.Core;
using System.IO;
using QmsDoc.Docs.Excel;
using QmsDoc.Exceptions;
using System.Text.RegularExpressions;
using System.IO.Packaging;
using System.Globalization;

namespace QmsDocXml
{
    public class HeaderLogo : DocProperty
    {
         public HeaderLogo()
        {
        }

        public HeaderLogo(object state) : base(state)
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
                return new HeaderLogo(pictureProperties.Name.ToString());
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
                    
                    
                    var xmlPartReader = DocumentFormat.OpenXml.OpenXmlPartReader.Create(vmlDrawingParts.First().GetStream());

                    Ovml.Shape myShape = null;

                    while (xmlPartReader.Read())
                    {
                        var currentEl = xmlPartReader.LoadCurrentElement();
                        if (currentEl.HasChildren)
                        {
                            foreach (var child in currentEl.ChildElements)
                            {
                                //var asShapeType = child as Ovml.Shapetype;
                                //var asShape = child as Ovml.Shape;
                                //var asShapeLayout = child as Ovml.Office.ShapeLayout;

                                try
                                {
                                    myShape = new Ovml.Shape(child.OuterXml);

                                }

                                catch
                                {
                                    //Element not of interest
                                }
                            }

                        }
                        else
                        {
                            throw new DocProcessingException();
                        }
                    }

                    if (myShape != null)
                    {
                        var myImageData = myShape.Elements<Ovml.ImageData>().First();
                        return new HeaderLogo(myImageData.Title.Value);
                    }

                    else
                    {
                        throw new DocReadException();
                    }


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

            //get the content of the vmlPart 
            string vmlContent = null;
            using (StreamReader streamReader = new StreamReader(vmlPart.GetStream()))
            {
                vmlContent = streamReader.ReadToEnd();
                
            }
            var xEl = Xlinq.XElement.Parse(vmlContent);
            var shapeNode = xEl.Descendants().Where(d => d.Name.LocalName == "shape").First();
            var styleAttr = shapeNode.Attributes("style").First();

            var imageNode = xEl.Descendants().Where(d=> d.Name.LocalName == "imagedata").First();
            var imageNodeAttrs = imageNode.Attributes();
            var imageAttr = imageNode.Attributes().Where(attr => attr.Name.LocalName == "title").First();

            //get current image height in string
            Match widthMatch = Regex.Match(styleAttr.Value, @"width:.*?pt");
            Match heightMatch = Regex.Match(styleAttr.Value, @"height:.*?pt");
            if(widthMatch.Success && heightMatch.Success)
            {
                string hStr = heightMatch.ToString().Replace("height:", "").Replace("pt", "");
                double imageHeight;
                bool rH = double.TryParse(hStr, out imageHeight);

                double targetRatio = ImageXml.GetImageHeightWidthRatio(imageFile);
                double newWidth = Math.Round(imageHeight / targetRatio, 1);
                string newStyleWidth = "width:" + newWidth.ToString() + "pt";
                string newStyleAttr = styleAttr.Value.Replace(widthMatch.ToString(), newStyleWidth);


                //set new values
                styleAttr.Value = newStyleAttr;
                imageAttr.Value = imageFile.Name;

                //generate new content and feed to VmlDrawingPart
                string newVmlContent = xEl.ToString();

                var vmlStream = new MemoryStream();
                var writer = new StreamWriter(vmlStream);
                writer.Write(newVmlContent);
                writer.Flush();
                vmlStream.Position = 0;
                vmlPart.FeedData(vmlStream);

                //overwrite current image with the new image
                ImagePart imagePart = vmlPart.ImageParts.First();
                using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open))
                {
                    imagePart.FeedData(stream);
                }
            }
            else
            {
                throw new DocWriteException("Could not find both the height and width specification of the current document image");
            }


            //******************************************************
            //FileInfo imageFile = new FileInfo(this.State.ToString());

            //var workSheetPart = doc.WorkbookPart.WorksheetParts.First();
            //var vmlParts = workSheetPart.VmlDrawingParts;
            //var vmlPart = workSheetPart.VmlDrawingParts.First();

            ////get the content of the vmlPart 

            //string vmlContent = null;
            //using (StreamReader streamReader = new StreamReader(vmlPart.GetStream()))
            //{
            //    vmlContent = streamReader.ReadToEnd();

            //}
            //var xEl = Xlinq.XElement.Parse(vmlContent);
            //var shapeNode = xEl.Descendants().Where(d => d.Name.LocalName == "shape").First();
            //var styleAttr = shapeNode.Attributes("style").First();

            //var imageNode = shapeNode.Descendants().Where(d => d.Name.LocalName == "imageData").First();
            //var imageAttr = imageNode.Attributes("title").First();


            ////get current image height in string
            //Match widthMatch = Regex.Match(styleAttr.Value, @"width:.*?pt");
            //Match heightMatch = Regex.Match(styleAttr.Value, @"height:.*?pt");
            //if (widthMatch.Success && heightMatch.Success)
            //{
            //    string hStr = heightMatch.ToString().Replace("height:", "").Replace("pt", "");
            //    double imageHeight;
            //    bool rH = double.TryParse(hStr, out imageHeight);

            //    double targetRatio = ImageXml.GetImageHeightWidthRatio(imageFile);
            //    double newWidth = Math.Round(imageHeight / targetRatio, 1);
            //    string newStyleWidth = "width:" + newWidth.ToString() + "pt";

            //    //generate new content to feed to part
            //    string newVmlContent = vmlContent.Replace(widthMatch.ToString(), newStyleWidth);

            //    var vmlStream = new MemoryStream();
            //    var writer = new StreamWriter(vmlStream);
            //    writer.Write(newVmlContent);
            //    writer.Flush();
            //    vmlStream.Position = 0;
            //    vmlPart.FeedData(vmlStream);

            //    ImagePart imagePart = vmlPart.ImageParts.First();
            //    using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open))
            //    {
            //        imagePart.FeedData(stream);
            //    }
            //}
            //else
            //{
            //    throw new DocWriteException("Could not find both the height and width specification of the current document image");
            //}

        }
    }
}
