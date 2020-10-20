using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.SharedWeb.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.IO;
using System.Web.Mvc;

namespace Damacon.SharedWeb.Export.PDF
{
    public class PDFExportClientLoginDetail
    {
        public FileStreamResult Export(Client client)
        {
            // step 1: creation of a document-object
            var document = new Document(PageSize.A4, 150, 150, 10, 10);

            //step 2: we create a memory stream that listens to the document
            var output = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, output);

            //step 3: we open the document
            document.Open();

            BaseFont baseFont = BaseFont.CreateFont(System.Web.HttpContext.Current.Server.MapPath("~\\Fonts\\Arial Narrow.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Font fontLarge = new Font(baseFont, 14);
            Font fontLargeBold = new Font(fontLarge);
            fontLargeBold.SetStyle(1);

            Font fontNormal = new Font(baseFont, 12);
            Font fontNormalBold = new Font(fontNormal);
            fontNormalBold.SetStyle(1);
            DottedLineSeparator dottedLineSeparator = new DottedLineSeparator();
            dottedLineSeparator.Offset = 0;

            //if (store != null)
            //{
            //    document.Add(CreateParagraph(store.WebName, fontLargeBold));
            //    document.Add(CreateParagraph(store.WebAddress, fontLarge));
            //    document.Add(CreateParagraph("Tel. " + store.Phone, fontLarge));
            //}
            document.Add(CreateParagraph(Resources.Date + ": " + DateTime.Now.ToString(UIExtensions.DateFormat), fontLarge));

            document.Add(new Chunk(dottedLineSeparator));
            document.Add(CreateParagraph(Resources.LOGIN, fontLargeBold));
            document.Add(new Chunk(dottedLineSeparator));
            document.Add(new Chunk("\n\n"));

            document.Add(CreateParagraph(Resources.LoginPrintLine1, fontNormal));
            document.Add(CreateParagraph("clienti.gruppozanzuri.it", fontNormalBold));
            document.Add(CreateParagraph(Resources.LoginPrintLine2, fontNormal));
            document.Add(CreateParagraph(Resources.LoginPrintLine3 + " € 50,00 ", fontNormalBold));
            document.Add(CreateParagraph(Resources.LoginPrintLine4 , fontNormal));
            document.Add(new Chunk("\n"));

            document.Add(CreateParagraph(Resources.LoginPrintLine5, fontNormal, Element.ALIGN_LEFT));
            PdfPTable credentialsTable = new PdfPTable(2);
            credentialsTable.WidthPercentage = 100;
            credentialsTable.SetWidths(new float[2] { 0.6f, 1f });
            credentialsTable.DefaultCell.Border = Rectangle.NO_BORDER;
            credentialsTable.AddCell(new Phrase(Resources.Username, fontNormal));
            credentialsTable.AddCell(new Phrase(client.Username, fontNormalBold));
            credentialsTable.AddCell(new Phrase(Resources.Password, fontNormal));
            credentialsTable.AddCell(new Phrase(client.ClientPassword, fontNormalBold));
            document.Add(credentialsTable);



            document.Add(CreateParagraph(Resources.LoginPrintLine6, fontNormal, Element.ALIGN_LEFT));
            document.Add(new Chunk("\n\n"));

            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.SetWidths(new float[2] { 0.6f, 1f });
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.AddCell(new Phrase(Resources.Client, fontNormal));
            table.AddCell(new Phrase(client.FullName, fontNormalBold));
            table.AddCell(new Phrase(Resources.Phone, fontNormal));
            table.AddCell(new Phrase(client.MobilePhone, fontNormalBold));
            document.Add(table);

            //This is important don't forget to close the document
            document.Close();

            // send the memory stream as File
            Stream newStream = new MemoryStream(output.ToArray());
            var fileStreamResult = new FileStreamResult(newStream, "application/pdf");
            fileStreamResult.FileDownloadName = "LoginDetails.pdf";
            fileStreamResult.FileStream.Seek(0, SeekOrigin.Begin);
            return fileStreamResult;
        }

        private Paragraph CreateParagraph(string text, Font font, int horizontalAlignment = Element.ALIGN_CENTER)
        {
            Paragraph paragraph = new Paragraph(text, font);
            paragraph.Alignment = horizontalAlignment;
            paragraph.SetLeading(0.0f, 1.2f);
            return paragraph;
        }
    }
}