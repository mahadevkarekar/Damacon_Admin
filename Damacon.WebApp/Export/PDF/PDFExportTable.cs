using Damacon.WebApp.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Damacon.SharedWeb.Helpers;

namespace Damacon.WebApp.Export.PDF
{
    public class PDFExportTable<T> : PDFExportBase<T>
    {
        private string columns;
        private string selectedOptions;
        private IList<string> rightAlignedColumns;
        public PDFExportTable(string columns, string selectedOptions, IList<string> rightAlignedColumns)
        {
            this.columns = columns;
            this.selectedOptions = selectedOptions;
            this.rightAlignedColumns = rightAlignedColumns;
        }

        public override FileStreamResult Export(IList<T> data, string title, IList<KeyValuePair<string, string>> filters, string reportLayout)
        {
            var columnsData = JsonConvert.DeserializeObject<IList<ExportColumnSettingsEx>>(HttpUtility.UrlDecode(columns));
            dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));

            //Remove edit,delete column
            columnsData.RemoveAt(columnsData.Count - 1);

            // step 1: creation of a document-object
            var document = new Document(PageSize.A4, 10, 10, 10, 10);
            SetDocumentPageSizeRotation(document, reportLayout);

            //step 2: we create a memory stream that listens to the document
            var output = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, output);

            //step 3: we open the document
            document.Open();

            //step 4: we add content to the document
            //AddWaterMark(writer);

            BaseFont baseFont = GetBaseFont();
            Font fontTitle = new Font(baseFont, 14);
            fontTitle.SetStyle(1);

            if (filters == null || filters.Count == 0)
            {
                Paragraph phraseTitle = new Paragraph(title, fontTitle);
                phraseTitle.Alignment = Element.ALIGN_CENTER;
                document.Add(phraseTitle);
            }
            else
            {
                Font fontTitleRight = new Font(baseFont, 12);
                fontTitleRight.SetStyle(1);
                var titleTable = new PdfPTable((filters.Count * 2) + 2);
                titleTable.WidthPercentage = 100;
                var reportTitleCell = GetTitleCell(title, fontTitle, Element.ALIGN_LEFT);
                reportTitleCell.PaddingLeft = 4f;
                reportTitleCell.Colspan = 2;
                titleTable.AddCell(reportTitleCell);
                foreach (var filter in filters)
                {
                    if (string.IsNullOrWhiteSpace(filter.Key))
                    {
                        var cell = GetTitleCell(filter.Value, fontTitleRight, Element.ALIGN_RIGHT);
                        cell.Colspan = 2;
                        cell.PaddingRight = 4f;
                        titleTable.AddCell(cell);
                    }
                    else
                    {
                        var valueCell = GetTitleCell(filter.Value, fontTitleRight, Element.ALIGN_LEFT);
                        valueCell.Colspan = 2;
                        titleTable.AddCell(valueCell);
                    }
                }
                document.Add(titleTable);
            }

            Font fontDateTime = new Font(baseFont, 10);
            Paragraph phraseDateTime = new Paragraph(DateTime.Now.ToString("dd-MMM-yyy HH:mm:ss"), fontDateTime);
            phraseDateTime.Alignment = Element.ALIGN_RIGHT;
            document.Add(phraseDateTime);

            document.Add(new Paragraph(" "));

            var numOfColumns = columnsData.Count(x => !x.Hidden);
            var dataTable = new PdfPTable(numOfColumns);
            dataTable.WidthPercentage = 100;

            Font fontHeader = new Font(baseFont, 8);
            fontHeader.SetStyle(1);
            Font fontCell = new Font(baseFont, 8);
            dataTable.DefaultCell.Padding = 1;

            dataTable.DefaultCell.BorderWidth = 0.5f;
            dataTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;

            // Adding headers
            foreach (var column in columnsData.Where(x => !x.Hidden))
            {
                PdfPCell cellHeader = new PdfPCell(new Phrase(column.Title, fontHeader));
                cellHeader.BackgroundColor = new BaseColor(245, 245, 245);
                dataTable.AddCell(cellHeader);
            }

            dataTable.HeaderRows = 1;
            dataTable.DefaultCell.BorderWidth = 0.5f;

            foreach (var item in data)
            {
                foreach (var column in columnsData.Where(x => !x.Hidden))
                {
                    PdfPCell cell = new PdfPCell(new Phrase(ReflectionExtensions.GetPropertyValue(item, column.Field, column.Values), fontCell));
                    if (rightAlignedColumns != null && rightAlignedColumns.Any(x => x.Equals(column.Field)))
                    {
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }
                    dataTable.AddCell(cell);
                }
            }

            // Add table to the document
            document.Add(dataTable);

            //This is important don't forget to close the document
            document.Close();

            Stream newStream = new MemoryStream(output.ToArray());
            // send the memory stream as File
            var fileStreamResult = new FileStreamResult(newStream, "application/pdf");
            fileStreamResult.FileDownloadName = "StoreD_" + title + ".pdf";
            fileStreamResult.FileStream.Seek(0, SeekOrigin.Begin);
            return fileStreamResult;
        }
    }
}