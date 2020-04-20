using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using DrawingPictures = DocumentFormat.OpenXml.Drawing.Pictures;
using QmsDoc.Core;
using QmsDoc.Docs.Word;
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
            //var headerParts = doc.MainDocumentPart.HeaderParts.ToList();
            //HeaderPart headerPart = headerParts[0];
            //var drawings = headerPart.Header.Elements<Drawing>().ToList();
            //var blips = headerPart.Header.Elements<PIC.Picture>().ToList();
            //var images = headerPart.ImageParts.ToList();
            //var image1 = images[0];

            DocumentFormat.OpenXml.Wordprocessing.TableCell cell = WordPartHeaderTableCell.Get(doc, docConfig.LogoRow, docConfig.LogoCol);
            Drawing drawing = cell.Descendants<Drawing>().First();
            GraphicData graphicData = drawing.Descendants<GraphicData>().First();
            var els = graphicData.Elements().ToList();
            var picture = graphicData.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>().First();
            var pictureProperties = picture.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties>().First();
            return new Logo(pictureProperties.Name.ToString());
        }

        public override void Write(WordprocessingDocument doc, WordDocConfig docConfig, object state)
        {
            //https://stackoverflow.com/questions/2810138/replace-image-in-word-doc-using-openxml

            DocumentFormat.OpenXml.Wordprocessing.TableCell cell = WordPartHeaderTableCell.Get(doc, docConfig.LogoRow, docConfig.LogoCol);
            var picture = cell.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>().First();
            var imageId = picture.BlipFill.Blip.Embed.Value;

            var imagePartResult = doc.MainDocumentPart.GetPartById(imageId);
            var imagePart = imagePartResult as ImagePart;
            doc.DeletePart(imagePart);

            FileInfo logoFile = new FileInfo((string)state);
            if(logoFile.Extension == ".jpg")
            newImagePart = doc.MainDocumentPart.AddImagePart(ImagePartType.Jpeg)

            byte[] imageBytes = File.ReadAllBytes((string)state);
            BinaryWriter writer = new BinaryWriter(imagePart.GetStream());
            writer.Write(imageBytes);
            writer.Close();

        }
    }
}
