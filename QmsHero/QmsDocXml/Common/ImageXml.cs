using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Wd = DocumentFormat.OpenXml.Wordprocessing;
using Xl = DocumentFormat.OpenXml.Spreadsheet;
using A = DocumentFormat.OpenXml.Drawing;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Windows.Media.Imaging;

namespace QmsDocXml.Common
{
    static class ImageXml
    {
        public static void Add(WordprocessingDocument wordDoc, string relationshipId, Wd.Paragraph par, FileInfo newImageFile)
        {
            long[] imageSizeXY = ScaleImageSizeToTarget(newImageFile);
            {
                var element =
                     new Wd.Drawing(
                         new DW.Inline(
                             new DW.Extent() { Cx = imageSizeXY[0], Cy = 365760L },
                             new DW.EffectExtent()
                             {
                                 LeftEdge = 0L,
                                 TopEdge = 0L,
                                 RightEdge = 0L,
                                 BottomEdge = 0L
                             },
                             new DW.DocProperties()
                             {
                                 Id = (UInt32Value)1U,
                                 Name = newImageFile.Name
                             },
                             new DW.NonVisualGraphicFrameDrawingProperties(
                                 new A.GraphicFrameLocks() { NoChangeAspect = true }),
                             new A.Graphic(
                                 new A.GraphicData(
                                     new PIC.Picture(
                                         new PIC.NonVisualPictureProperties(
                                             new PIC.NonVisualDrawingProperties()
                                             {
                                                 Id = (UInt32Value)0U,
                                                 Name = newImageFile.Name
                                             },
                                             new PIC.NonVisualPictureDrawingProperties()),
                                         new PIC.BlipFill(
                                             new A.Blip(
                                                 new A.BlipExtensionList(
                                                     new A.BlipExtension()
                                                     {
                                                         Uri =
                                                         "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                                     })
                                             )
                                             {
                                                 Embed = relationshipId,
                                                 CompressionState =
                                               A.BlipCompressionValues.Print
                                             },
                                             new A.Stretch(
                                                 new A.FillRectangle())),
                                         new PIC.ShapeProperties(
                                             new A.Transform2D(
                                                 new A.Offset() { X = 0L, Y = 0L },
                                                 new A.Extents() { Cx = imageSizeXY[0], Cy = 365760L }),

                                             new A.PresetGeometry(
                                                 new A.AdjustValueList()
                                             )
                                             { Preset = A.ShapeTypeValues.Rectangle }))
                                 )
                                 { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                         )
                         {
                             DistanceFromTop = (UInt32Value)0U,
                             DistanceFromBottom = (UInt32Value)0U,
                             DistanceFromLeft = (UInt32Value)0U,
                             DistanceFromRight = (UInt32Value)0U,
                             EditId = "50D07946"
                         });

                Wd.Run firstRun = par.Elements<Wd.Run>().First();
                Wd.Run newRun = new Wd.Run(element);
                par.ReplaceChild<Wd.Run>(newRun, firstRun);

            }
        }


        public static Xdr.WorksheetDrawing GetWorksheetDrawing(string relationshipId, FileInfo newImageFile)
        {
            long[] imageSizeXY = ScaleImageSizeToTarget(newImageFile);
            var element = new Xdr.WorksheetDrawing();
                
            return element;
        }

        public static long[] ScaleImageSizeToTarget(FileInfo filename, long targetHeightEmus=365760)
        {
            var img = new BitmapImage(new Uri(filename.FullName, UriKind.RelativeOrAbsolute));
            var widthPx = img.PixelWidth;
            var heightPx = img.PixelHeight;
            //var ratio = widthPx / heightPx;
            var horzRezDpi = img.DpiX;
            var vertRezDpi = img.DpiY;

            const float emusPerInch = (float)914400;
            const float emusPerCm = 360000;


            var widthEmus = (long)(widthPx * 9525);
            var heightEmus = (long)(heightPx * 9525);

            var newWidthEmus = widthEmus * targetHeightEmus/heightEmus;
            long[] result = new long[] { newWidthEmus, targetHeightEmus };
            return result;
        }
    }
}
