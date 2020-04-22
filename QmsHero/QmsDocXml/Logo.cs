using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
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

        //public string LogoPath
        //{
        //    get => this.logoPath;
        //    set
        //    {
        //        //this.logoPath = value;
        //        //var cell = this.HeaderFooterTable
        //        //    .Cell(
        //        //    this.DocConfig.LogoRow,
        //        //    this.DocConfig.LogoCol
        //        //    );
        //        //cell.Range.Delete();
        //        //var picture = cell.Range.InlineShapes.AddPicture(value, false, true);
        //        //picture.LockAspectRatio = Microsoft.Office.Core.MsoTriState.msoTrue;
        //        //picture.Height = 28;
        //        this.logoPath = value;
        //        this.OnPropertyChanged();
        //    }
        //}
        //public Paragraph FetchLogoPart(WordprocessingDocument doc, WordDocConfig config)
        //{
        //    TableCell cell = WordPartHeaderTableCell.Get(doc, config.LogoRow, config.LogoCol);
        //    var images = doc.MainDocumentPart.ImageParts.ToList();
        //    ImagePart image1 = images.First();
        //    doc.MainDocumentPart.GetIdOfPart(image1);
        //    return 
        //}

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

        public override void Write(WordprocessingDocument doc, WordDocConfig docConfig, object state)
        {
            //https://stackoverflow.com/questions/2810138/replace-image-in-word-doc-using-openxml
            //https://stackoverflow.com/questions/43320452/removing-images-in-header-with-openxml-sdk

            FileInfo imageFile = new FileInfo((string)state);

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
                //var drawing = drawings.First();
                //OpenXmlPart imagePart = doc.MainDocumentPart.GetPartById(
                //    drawing.Descendants<DrawingPictures.Picture>().First().BlipFill.Blip.Embed);
                //using (var writer = new BinaryWriter(imagePart.GetStream()))
                //{
                //    writer.Write(File.ReadAllBytes(imageFile.FullName));
                //}

                //var nvDprop = drawing.Descendants<DrawingPictures.NonVisualDrawingProperties>()
                //    .Where(nvDp => nvDp.Name != null).First();


                //new picture
                MainDocumentPart mainPart = doc.MainDocumentPart;
                ImagePart imagePart = mainPart.HeaderParts.First().AddImagePart(ImagePartType.Jpeg);
                //ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);
                using (FileStream stream = new FileStream(imageFile.FullName, FileMode.Open))
                {
                    imagePart.FeedData(stream);
                }

                string imageId = mainPart.HeaderParts.First().GetIdOfPart(imagePart);
                AddImage.Add(doc, imageId, cellPar);
            }
        }
    }
}
