using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Documents.SpreadsheetStreaming;

namespace Damacon.WebApp.Export.Excel
{
    public class ExcelExportTable<T> : ExcelExportBase<T>
    {
        private string columns;
        private string selectedOptions;
        private IList<string> rightAlignedColumns;
        public ExcelExportTable(string columns, string selectedOptions, IList<string> rightAlignedColumns)
        {
            this.columns = columns;
            this.selectedOptions = selectedOptions;
            this.rightAlignedColumns = rightAlignedColumns;
        }

        public override FileStreamResult Export(IList<T> data, string title, IList<KeyValuePair<string, string>> filters, string reportLayout)
        {
            var columnsData = JsonConvert.DeserializeObject<List<ExportColumnSettingsEx>>(HttpUtility.UrlDecode(columns));
            dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(selectedOptions));
            SpreadDocumentFormat exportFormat = SpreadDocumentFormat.Xlsx;
            string fileName = string.Format("{0}.{1}", title, options.format);
            string mimeType = Kendo.Mvc.Export.Helpers.GetMimeType(exportFormat);

            //Remove edit,delete column
            columnsData.RemoveAt(columnsData.Count - 1);
            columnsData.RemoveAll(x => x.Hidden);

            Stream stream = new MemoryStream();
            {
                using (IWorkbookExporter workbook = SpreadExporter.CreateWorkbookExporter(SpreadDocumentFormat.Xlsx, stream))
                {
                    using (IWorksheetExporter worksheet = workbook.CreateWorksheetExporter("Sheet 1"))
                    {
                        foreach (var column in columnsData)
                        {
                            using (IColumnExporter iColumnExporter = worksheet.CreateColumnExporter())
                            {
                                if (column.ExcelColumnWidth < 100)
                                  iColumnExporter.SetWidthInPixels(100);
                                else
                                  iColumnExporter.SetWidthInPixels(column.ExcelColumnWidth);
                                }
                            }

                        CreateFilterRow(worksheet, title, filters, columnsData.Capacity, (filters.Count * 2) + 1, true);

                        using (IRowExporter row = worksheet.CreateRowExporter())
                        {
                            foreach (var column in columnsData)
                            {
                                CreateNewHeaderCell(row, column.Title, horizontalAlignment: SpreadHorizontalAlignment.Center);

                                //using (ICellExporter cell = row.CreateCellExporter())
                                //{
                                //    cell.SetValue(column.Title);
                                //    cell.SetFormat(new SpreadCellFormat()
                                //    {
                                //        HorizontalAlignment = SpreadHorizontalAlignment.Center,
                                //        VerticalAlignment = SpreadVerticalAlignment.Center
                                //    });
                                //}
                            }
                        }

                        foreach (var item in data)
                        {
                            using (IRowExporter row = worksheet.CreateRowExporter())
                            {
                                row.SetHeightInPixels(30);
                                foreach (var column in columnsData)
                                {
                                    CreateNewCell(row, ReflectionExtensions.GetPropertyValue(item, column.Field, column.Values), applyDecimalFormat: true);

                                    //using (ICellExporter cell = row.CreateCellExporter())
                                    //{
                                    //    cell.SetValue(ReflectionExtensions.GetPropertyValue(item, column.Field, column.Values));
                                    //    if (rightAlignedColumns != null && rightAlignedColumns.Any(x => x.Equals(column.Field)))
                                    //    {
                                    //        SpreadCellFormat cellFormat = new SpreadCellFormat();
                                    //        cellFormat.NumberFormat = "#,##0.00";
                                    //        cellFormat.HorizontalAlignment = SpreadHorizontalAlignment.Right;
                                    //        cell.SetFormat(cellFormat);
                                    //    }
                                    //}
                                }
                            }
                        }
                    }
                }
            }

            var fileStreamResult = new FileStreamResult(stream, mimeType);
            fileStreamResult.FileDownloadName = fileName;
            fileStreamResult.FileStream.Seek(0, SeekOrigin.Begin);

            return fileStreamResult;
        }
    }
}