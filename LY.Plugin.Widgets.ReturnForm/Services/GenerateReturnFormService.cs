using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Svg.Converter;
using LY.Plugin.Widgets.ReturnForm.Services.Contracts;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Services.Localization;
using System.IO;

namespace LY.Plugin.Misc.ReturnForm.Services
{
    public class GenerateReturnFormService : IGenerateReturnFormService
    {
        #region Fields

        private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public GenerateReturnFormService(ILocalizationService localizationService)
        {
            this._localizationService = localizationService;
        }

        #endregion
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

            var titleParagraph = new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.TitlePragragh"));
            titleParagraph.SetBold();
            titleParagraph.SetFontSize(20);
            titleParagraph.SetTextAlignment(TextAlignment.RIGHT);

            document.Add(titleParagraph);

            var titleDetails = new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.TitleDetailsPragragh"));
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


            var cellNameContent = new Paragraph(new Text(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Information.Name")).SetBold())
                .Add(new Text(order.BillingAddress.FirstName + " " + order.BillingAddress.LastName));
            var informationCellName = new Cell()
                .SetPadding(10)
                .Add(cellNameContent);

            informationTable.AddCell(informationCellName);


            var cellOrderContent = new Paragraph(new Text(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Information.Ordre")).SetBold())
                .Add(new Text(order.Id.ToString()));
            var informationCellOrderNumber = new Cell()
                .SetPadding(10)
                .Add(cellOrderContent);

            informationTable.AddCell(informationCellOrderNumber);

            document.Add(informationTable);

            #endregion

            #region Products 

            var productsTitle = new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Product.Title"))
               .SetFontSize(16);
            document.Add(productsTitle);

            var productsHeaderCellOne = new Cell()
                .SetPadding(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Product.TableHeaderOne")));

            var productsHeaderCellTwo = new Cell()
                .SetPadding(10)
                .SetBold()
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Product.TableHeaderTwo")));

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

            var messageTitle = new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Message.Title"))
                .SetFontSize(16);
            document.Add(messageTitle);

            var messageTable = new Table(UnitValue.CreatePercentArray(2))
                .UseAllAvailableWidth();

            var doYouWantMonyBackCell = new Cell()
                .SetPadding(10)
                .Add(new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Message.TableColumnOne")));

            messageTable.AddCell(doYouWantMonyBackCell);


            var whyYouWantMonyBackCell = new Cell()
                .SetPadding(10)
                .Add(new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Message.TableColumnTwo")));

            messageTable.AddCell(whyYouWantMonyBackCell);

            document.Add(messageTable);

            #endregion

            #region Important

            var importantTitle = new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Important.Title"))
               .SetFontSize(16);
            document.Add(importantTitle);

            var importantMessage = new Paragraph(_localizationService.GetResource("Plugins.Widgets.ReturnForm.Document.Important.Details"));
            document.Add(importantMessage);

            #endregion

            document.Close();
        }
    }
}