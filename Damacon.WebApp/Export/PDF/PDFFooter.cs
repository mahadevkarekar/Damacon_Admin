using iTextSharp.text;
using iTextSharp.text.pdf;
using System;

namespace Damacon.WebApp.Export.PDF
{
    public class PDFFooter : PdfPageEventHelper
    {

        PdfContentByte cb;
        PdfTemplate template;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            cb = writer.DirectContent;
            template = cb.CreateTemplate(50, 50);
        }

        public override void OnEndPage(PdfWriter writer, Document doc)
        {
            Font font = new Font(FontFactory.GetFont("Arial", 8, Font.NORMAL));
            BaseFont baseFont = font.GetCalculatedBaseFont(true);
            Paragraph footer = new Paragraph(DateTime.Now.ToString("yyyy-MM-dd hh:mm") + " Stored - Web Application by Damacon srl"
                , font);
            footer.Alignment = Element.ALIGN_RIGHT;
            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = doc.PageSize.Width;
            footerTbl.WidthPercentage = 100;
            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell = new PdfPCell(footer);
            cell.Border = 0;
            cell.PaddingLeft = 5;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            footerTbl.AddCell(cell);

            footerTbl.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);

            base.OnEndPage(writer, doc);

            int pageN = writer.PageNumber;
            String text = pageN.ToString() + " di ";
            float len = baseFont.GetWidthPoint(text, 8);

            Rectangle pageSize = doc.PageSize;
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(baseFont, 8);
            cb.SetTextMatrix(doc.PageSize.Width - doc.RightMargin - len, pageSize.GetBottom(10));
            cb.ShowText(text);

            cb.EndText();

            cb.AddTemplate(template, doc.PageSize.Width - doc.RightMargin, pageSize.GetBottom(10));
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            Font font = new Font(FontFactory.GetFont("Arial", 8, Font.NORMAL));
            BaseFont baseFont = font.GetCalculatedBaseFont(true);
            template.BeginText();
            template.SetFontAndSize(baseFont, font.Size);
            template.SetTextMatrix(0, 0);
            template.ShowText("" + (writer.PageNumber));
            template.EndText();
        }
    }
}