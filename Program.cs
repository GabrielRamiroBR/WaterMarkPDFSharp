// See https://aka.ms/new-console-template for more information

using PdfSharp.Charting;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;

namespace PdfSharp
{
    class Program
    {
        public static void LoadCenteredTiltedWatermark(PdfPage page, XGraphics graphics, XImage image)
        {
            // Define a rotation transformation at the center of the page.
            graphics.TranslateTransform(page.Width / 2, page.Height / 2);
            graphics.RotateTransform(-Math.Atan(page.Height / page.Width) * 180 / Math.PI);
            graphics.TranslateTransform(-page.Width / 2, -page.Height / 2);

            //Load image
            graphics.DrawImage(image,(page.Width / 2) - 220, (page.Height / 2) - 105, 500, 170);

            // Define Transparency to a overlayed translucid white rectangle
            XSolidBrush brush = new XSolidBrush(XColor.FromArgb(200, 255, 255, 255));
            graphics.DrawRectangle(brush, 0, 0, page.Width, page.Height);
        }

        static void Main (string[] args)
        {
            //Creates a pdf doc, a page and the Xgraphics to draw on the page
            using PdfDocument doc = PdfReader.Open("C:\\Users\\gabmes61\\source\\repos\\TestesPDF\\TestesPDF\\TEST.pdf");
            var page = doc.Pages[0];
            var graphics = PdfSharp.Drawing.XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Prepend);
            XImage image = XImage.FromFile("C:\\Users\\gabmes61\\source\\repos\\TestesPDF\\TestesPDF\\ComosLogo.png");

            //Load watermark on page
            LoadCenteredTiltedWatermark(page, graphics, image);

            //Line of code to avoid a NotSupportedException to be thrown when saviung the doc
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            doc.Save("testFileWatermark.pdf");

            //modificar para ler carregar várias páginas
 
        }
    }
}

