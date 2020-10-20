using Damacon.DAL.i18n;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Windows;

namespace Damacon.WebApp.Export.PDF
{
    public abstract class PDFExportBase<T>
    {
        public abstract FileStreamResult Export(IList<T> data, string title, IList<KeyValuePair<string, string>> filters,string reportLayout);

        protected void AddWaterMark(PdfWriter writer)
        {
            //water mark
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ITALIC, BaseFont.CP1252, false);
            BaseColor bc = new BaseColor(0, 0, 0, 35);
            Font times = new Font(bfTimes, 145.5F, Font.ITALIC, bc);

            ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase("StoreD", times), 245.5F, 480.0F, -55);
            //End water mark
        }

        protected BaseFont GetBaseFont()
        {
            return BaseFont.CreateFont(System.Web.HttpContext.Current.Server.MapPath("~\\Fonts\\Arial Narrow.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        }

        protected void SetDocumentPageSizeRotation(Document document, string reportLayout)
        {
            if (reportLayout == Resources.Landscape)
            {
                document.SetPageSize(document.PageSize.Rotate());
            }
        }

        protected byte[] RotateDocument(MemoryStream document, string reportLayout)
        {
            //if (reportLayout == Resources.Landscape)
            //{
            //    PdfReader reader = new PdfReader(document.ToArray());
            //    int pagesCount = reader.NumberOfPages;

            //    for (int n = 1; n <= pagesCount; n++)
            //    {
            //        PdfDictionary page = reader.GetPageN(n);
            //        PdfNumber rotate = page.GetAsNumber(PdfName.ROTATE);
            //        int rotation = rotate == null ? 270 : (rotate.IntValue + 270) % 360;
            //        page.Put(PdfName.ROTATE, new PdfNumber(rotation));
            //    }
            //    MemoryStream stream = new MemoryStream();
            //    PdfStamper stamper = new PdfStamper(reader, stream);
            //    stamper.Close();
            //    reader.Close();
            //    return stream.ToArray();
            //}
            return document.ToArray();
        }

        protected PdfPCell GetSalesHeaderCell(string text, Font fontHeader, int colSpan, int rowSpan = 1)
        {
            // Adding headers
            PdfPCell cellHeader = new PdfPCell(new Phrase(text, fontHeader));
            cellHeader.BorderWidth = Rectangle.NO_BORDER;
            cellHeader.Colspan = colSpan;
            cellHeader.Rowspan = rowSpan;
            cellHeader.HorizontalAlignment = Element.ALIGN_LEFT;
            cellHeader.VerticalAlignment = Element.ALIGN_MIDDLE;
            return cellHeader;
        }

        protected PdfPCell GetTitleCell(string text, Font font, int horizontalAlignment, bool isFill = true)
        {
            PdfPCell cellTitle = new PdfPCell(new Phrase(text, font));
            cellTitle.BorderWidth = Rectangle.NO_BORDER;
            if (isFill)
            {
                cellTitle.BackgroundColor = new BaseColor(192, 192, 192);
            }
            cellTitle.HorizontalAlignment = horizontalAlignment;
            cellTitle.VerticalAlignment = Element.ALIGN_TOP;
            cellTitle.Padding = 0f;
            cellTitle.FixedHeight = 18.0f;
            return cellTitle;
        }

        protected PdfPCell GetCellWithSpacing(string text, Font fontHeader, bool isFill = false, int horizontalAlignment = Element.ALIGN_LEFT, int colSpan = 1)
        {
            PdfPCell cell = GetSalesHeaderCell(text, fontHeader, 1);
            cell.CellEvent = new CellSpacingEvent(2, isFill);
            cell.HorizontalAlignment = horizontalAlignment;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 4;
            cell.PaddingBottom = 6;
            cell.Colspan = colSpan;
            return cell;
        }

        protected PdfPCell GetBorderedCell(string text, Font fontHeader, int colSpan, BaseColor fillColor, int horizontalAlignment = Element.ALIGN_LEFT, int rowSpan = 1, float? padding = null, string rightText = null)
        {
            Phrase phrase = new Phrase(text, fontHeader);
            if(!string.IsNullOrWhiteSpace(rightText))
            {
                Chunk glue = new Chunk(new VerticalPositionMark());
                phrase.Add(new Chunk(glue));
                phrase.Add(new Chunk(rightText, fontHeader));
            }

            PdfPCell borderedCell = new PdfPCell(phrase);
            borderedCell.BorderWidth = 0.5f;
            borderedCell.Colspan = colSpan;
            borderedCell.Rowspan = rowSpan;
            borderedCell.HorizontalAlignment = horizontalAlignment;
            borderedCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            if (padding.HasValue)
            {
                borderedCell.UseAscender = true;
                borderedCell.UseDescender = true;
                borderedCell.Padding = padding.Value;
                borderedCell.PaddingTop = 2f;
            }
            
            if (fillColor != null)
            {
                borderedCell.BackgroundColor = fillColor;
            }
            return borderedCell;
        }

        protected void AddFilterCell(IList<KeyValuePair<string, string>> filters, Font fontTitleRight, PdfPTable table, int filtersInOneRow = 4)
        {
            Phrase phraseFilter = new Phrase();
            int filterCounter = 1;
            foreach (var filter in filters)
            {
                Chunk chunk = new Chunk(filter.Key + " : " + filter.Value + "       ", fontTitleRight);
                phraseFilter.Add(chunk);
                if (filterCounter % filtersInOneRow == 0 || filterCounter == filters.Count)
                {
                    PdfPCell cellFilter = new PdfPCell(phraseFilter);
                    cellFilter.PaddingBottom = 2;
                    cellFilter.PaddingLeft = 5f;
                    if (filterCounter == filters.Count)
                    {
                        cellFilter.PaddingBottom = 5;
                    }
                    cellFilter.BorderWidth = Rectangle.NO_BORDER;
                    cellFilter.BackgroundColor = new BaseColor(192, 192, 192);
                    table.AddCell(cellFilter);
                    phraseFilter = new Phrase();
                }
                filterCounter++;
            }
        }
    }
}