using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Svg.Converter;
using LY.Plugin.Widgets.ReturnForm.Services.Contracts;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using System.IO;

namespace LY.Plugin.Misc.ReturnForm.Services
{
    public class GenerateReturnFormService: IGenerateReturnFormService
    {
        public void CreateDocument(Order order, in MemoryStream stream)
        {

            PdfWriter writer = new PdfWriter(stream, new WriterProperties().SetPdfVersion(PdfVersion.PDF_2_0));
            PdfDocument pdfDocument = new PdfDocument(writer);

            Document document = new Document(pdfDocument);

            #region Header

            Image img = new Image(ImageDataFactory.Create("plugins/widgets.returnform/images/logo.png"))
                .SetFixedPosition(50, 710)
                .SetWidth(220);

            document.Add(img);

            var titleParagraph = new Paragraph("RETURSEDDEL");
            titleParagraph.SetBold();
            titleParagraph.SetFontSize(20);
            titleParagraph.SetTextAlignment(TextAlignment.RIGHT);

            document.Add(titleParagraph);

            var titleDetails = new Paragraph("lycopenhagen.dk\nDanmark\nJernbanegade 7 A\n6400, Sønderborg");
            titleDetails.SetUnderline();
            titleDetails.SetFontSize(12);
            titleDetails.SetTextAlignment(TextAlignment.RIGHT);

            document.Add(titleDetails);

            #endregion

            #region Information

            var informationTitle = new Paragraph("Information:")
                .SetFontSize(16);
            document.Add(informationTitle);

            var informationTable = new Table(UnitValue.CreatePercentArray(1))
                .UseAllAvailableWidth();


            var cellNameContent = new Paragraph(new Text("Navn: ").SetBold())
                .Add(new Text(order.BillingAddress.FirstName + " " + order.BillingAddress.LastName));
            var informationCellName = new Cell()
                .SetPadding(10)
                .Add(cellNameContent);

            informationTable.AddCell(informationCellName);


            var cellOrderContent = new Paragraph(new Text("Ordre: ").SetBold())
                .Add(new Text(order.Id.ToString()));
            var informationCellOrderNumber = new Cell()
                .SetPadding(10)
                .Add(cellOrderContent);

            informationTable.AddCell(informationCellOrderNumber);

            document.Add(informationTable);

            #endregion

            #region Products 

            var productsTitle = new Paragraph("\nVarer der skal returneres:")
               .SetFontSize(16);
            document.Add(productsTitle);

            var productsHeaderCellOne = new Cell()
                .SetPadding(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Vare"));

            var productsHeaderCellTwo = new Cell()
                .SetPadding(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph("Byttes til str"));

            var productsTable = new Table(UnitValue.CreatePercentArray(2))
                .UseAllAvailableWidth()
                .AddHeaderCell(productsHeaderCellOne)
                .AddHeaderCell(productsHeaderCellTwo);

            foreach (var item in order.OrderItems)
            {
                var productNameCell = new Cell()
                .SetPadding(10)
                .SetTextAlignment(TextAlignment.LEFT)
                .Add(new Paragraph(item.Product.Name + " - " + (string.IsNullOrWhiteSpace(item.AttributeDescription) ? "O/S" : item.AttributeDescription.Split(':')[1].Trim())));

                productsTable.AddCell(productNameCell);
                productsTable.AddCell(new Cell());
            }

            document.Add(productsTable);

            #endregion

            #region Message

            var messageTitle = new Paragraph("\nBesked til LY Copenhagen:")
                .SetFontSize(16);
            document.Add(messageTitle);

            var messageTable = new Table(UnitValue.CreatePercentArray(2))
                .UseAllAvailableWidth();

            var doYouWantMonyBackCell = new Cell()
                .SetPadding(10)
                .Add(new Paragraph("ØNSKER DU PENGENE RETUR? SKAL DU SKRIVE ”JA” HER:\n\n\n"));

            messageTable.AddCell(doYouWantMonyBackCell);


            var whyYouWantMonyBackCell = new Cell()
                .SetPadding(10)
                .Add(new Paragraph("ÅRSAG TIL ØNSKNING AF PENGE RETUR:\n\n\n\n"));

            messageTable.AddCell(whyYouWantMonyBackCell);
           
            document.Add(messageTable);

            #endregion

            #region Important

            var importantTitle = new Paragraph("\nVIGTIGT:")
               .SetFontSize(16);
            document.Add(importantTitle);

            var importantMessage = new Paragraph("Denne seddel skal vedlægges sammen med pakken, når den sendes retur.");
            document.Add(importantMessage);

            #endregion

            document.Close();
        }
    }
}