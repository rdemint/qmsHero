using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using DrawingPictures = DocumentFormat.OpenXml.Drawing.Pictures;
using QmsDoc.Core;
using QmsDoc.Docs.Word;
using QmsDocXml.Exceptions;
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
                GraphicData graphicData = drawing.Descendants<GraphicData>().First();
                var els = graphicData.Elements().ToList();
                var picture = graphicData.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>().First();

                //var pictures = cell.Descendants<DocumentFormat.OpenXml.Drawing.Pictures.Picture>();
                //if(!pictures.Any())
                //{
                //    throw new ElementNotFoundException();
                //}
                //else if(pictures.Count() > 1)
                //{
                //    throw new MultipleElementsExistException();
                //}
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

            //https://stackoverflow.com/questions/43320452/removing-images-in-header-with-openxml-sdk

            IEnumerable<Drawing> drawings = cell.Descendants<Drawing>()
                .Where(
                    d => d.Descendants<DrawingPictures.Picture>()
                      .Any(p => p.BlipFill.Blip.Embed != null)
                );

            if(drawings.Any())
            {
                var drawing = drawings.First();
                var nvDprop = drawing.Descendants<DrawingPictures.NonVisualDrawingProperties>()
                    .Where(nvDp => nvDp.Name != null).First();
                var currentImageUri = new Uri("/word/media/image1.jpg", UriKind.Relative);

                var currentImagePart = doc.Package.GetPart(currentImageUri);
                doc.Package.DeletePart(currentImageUri);
                var newUri = new Uri(System.IO.Path.Combine("/word/media", imageFile.Name, imageFile.Extension), UriKind.Relative);
                var packagePart = doc.Package.CreatePart(newUri, "Image/jpeg");
            }

            foreach(var drawing in drawings)
            {
                drawing.Remove();
            }

        }
    }
}
