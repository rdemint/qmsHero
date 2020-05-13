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
using FluentResults;
using System.Drawing;

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

        private HeaderLogo(object state, int stateCount) : base(state, stateCount)
        {
        }

        public override Result<QDocProperty> Read(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            var tableCellResult = WordPartHeaderTableCell.Get(doc, docConfig.HeaderLogoRow, docConfig.HeaderLogoCol);
            if(tableCellResult.IsFailed)
            {
                return Results.Fail(new Error("Did not identify the table cell for the logo in the document."));
            }
            IEnumerable<Drawing> drawings = tableCellResult.Value.Descendants<Drawing>()
                .Where(
                    d => d.Descendants<DrawingPictures.Picture>()
                      .Any(p => p.BlipFill.Blip.Embed != null)
                );

            if(drawings.Any())
            {
                Drawing drawing = tableCellResult.Value.Descendants<Drawing>().First();
                DRAWING.GraphicData graphicData = drawing.Descendants<DRAWING.GraphicData>().First();
                var els = graphicData.Elements().ToList();
                var picture = graphicData.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>().First();

                
                var pictureProperties = picture.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties>().First();
                return Results.Ok<QDocProperty>(new HeaderLogo(pictureProperties.Name.ToString(), 1));
            }

            else
            {
                return Results.Fail(new Error("Did not identify any drawings in the document."));
            }
        }

        public override Result<QDocProperty> Read(SpreadsheetDocument doc, ExcelDocConfig config)
        {
            var workSheetPart = doc.WorkbookPart.WorksheetParts.First();

            var pageSetup = workSheetPart.Worksheet.Elements<Xl.PageSetup>().First();

            var vmlDrawingParts = workSheetPart.VmlDrawingParts;

            if (vmlDrawingParts.Count() > 1)
            {
                return Results.Fail(new Error("Multiple drawings identified in the worksheet."));
            }

            else
            {
                var imageParts = vmlDrawingParts.First().ImageParts;
                if (imageParts.Count() > 1)
                {
                    return Results.Fail(new Error("Multiple images identified in the worksheet."));
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
                            return Results.Fail(new Error("Could not identify the Shape element in the worksheet drawing."));
                        }
                    }

                    if (myShape != null)
                    {
                        var myImageData = myShape.Elements<Ovml.ImageData>().First();
                        return Results.Ok<QDocProperty>(new HeaderLogo(myImageData.Title.Value, 1));
                    }

                    else
                    {
                        return Results.Fail(new Error("Could not identify the Shape element in the worksheet drawing."));
                    }


                }

                
            }

        }

        public override Result<QDocProperty> Write(WordprocessingDocument doc, WordDocConfig docConfig)
        {
            //https://stackoverflow.com/questions/2810138/replace-image-in-word-doc-using-openxml
            //https://stackoverflow.com/questions/43320452/removing-images-in-header-with-openxml-sdk

            FileInfo imageFile = new FileInfo((string)this.State);
            if(!imageFile.Exists)
            {
                return Results.Fail(new Error($"The image at {imageFile.FullName} does not exist."));
            }
            
            var tableCellResult = WordPartHeaderTableCell.Get(doc, docConfig.HeaderLogoRow, docConfig.HeaderLogoCol);
            if(tableCellResult.IsFailed)
            {
                return Results.Fail(new Error($"Did not identify a table cell at row {docConfig.HeaderLogoRow} and column {docConfig.HeaderLogoCol} in the document for the logo."));
            }
            Paragraph cellPar = tableCellResult.Value.Descendants<Paragraph>().First();
            //https://stackoverflow.com/questions/43320452/removing-images-in-header-with-openxml-sdk

            IEnumerable<Drawing> drawings = tableCellResult.Value.Descendants<Drawing>()
                .Where(
                    d => d.Descendants<DrawingPictures.Picture>()
                      .Any(p => p.BlipFill.Blip.Embed != null)
                );

            if(drawings.Any())
            {
                MainDocumentPart mainPart = doc.MainDocumentPart;
                ImagePart imagePart;
                if(imageFile.Extension == ".jpeg" | imageFile.Extension == ".jpg")
                {
                    imagePart = mainPart.HeaderParts.First().AddImagePart(ImagePartType.Jpeg);
                }
                else if(imageFile.Extension == ".png")
                {
                    imagePart = mainPart.HeaderParts.First().AddImagePart(ImagePartType.Png);
                }
                else
                {
                    return Results.Fail(new Error($"Image file type {imageFile.Extension} can not be processed."));
                }
                //Alternative does not work
                //ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    imagePart.FeedData(stream);
                }

                string imageId = mainPart.HeaderParts.First().GetIdOfPart(imagePart);
                ImageXml.Add(doc, imageId, cellPar, imageFile);
            }
            return Results.Ok<QDocProperty>(new HeaderLogo(this.State, 1));


        }

        public override Result<QDocProperty> Write(SpreadsheetDocument doc, ExcelDocConfig config)
        {

            bool foundImage = false;
            int foundCount = 0;
            FileInfo imageFile = new FileInfo(this.State.ToString());

            if (!imageFile.Exists)
            {
                return Results.Fail($"The image at {imageFile.FullName} does not exist.");
            }

            //if (imageFile.Extension != ".jpg" && imageFile.Extension != ".jpeg")
            //{
            //    return Results.Fail("The image must be a .jpg or .jpeg type.");
            //}


            foreach (var workSheetPart in doc.WorkbookPart.WorksheetParts) {
                var vmlParts = workSheetPart.VmlDrawingParts;
                if (vmlParts.Any())
                {
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

                    var imageNode = xEl.Descendants().Where(d => d.Name.LocalName == "imagedata").First();
                    var imageNodeAttrs = imageNode.Attributes();
                    var imageAttr = imageNode.Attributes().Where(attr => attr.Name.LocalName == "title").First();

                    //get current image height in string
                    Match widthMatch = Regex.Match(styleAttr.Value, @"width:.*?pt");
                    Match heightMatch = Regex.Match(styleAttr.Value, @"height:.*?pt");
                    if (!widthMatch.Success && !heightMatch.Success)
                    {
                        return Results.Fail(new Error("Could not find the width and height specification for the current image."));
                    }
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
                    using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        imagePart.FeedData(stream);
                    }
                    foundImage = true;
                    foundCount++;
                }
            }

            //Done
            if (foundImage == true)
            {
                return Results.Ok<QDocProperty>(new HeaderLogo(this.State, foundCount));
            }

            else
            {
                return Results.Fail(new Error("Did not find any images in the document"));
            }
        }
    }
}
