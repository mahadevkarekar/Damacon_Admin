using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Telerik.Documents.SpreadsheetStreaming;

namespace Damacon.WebApp.Export.Excel
{
    public abstract class ExcelExportBase<T>
    {
        protected const string EuroSymbol = " €";

        public abstract FileStreamResult Export(IList<T> data, string title, IList<KeyValuePair<string, string>> filters, string reportLayout);

        protected void CreateFilterRow(IWorksheetExporter worksheet, string reportTitle, IList<KeyValuePair<string, string>> filters, int numberOfColumns, int numberOfColumnsForFirstCell = 2, bool bShowLabel = false)
        {
            using (IRowExporter row = worksheet.CreateRowExporter())
            {
                row.SetHeightInPixels(28);
                using (ICellExporter cell = row.CreateCellExporter())
                {
                    cell.SetValue(reportTitle);
                    cell.SetFormat(new SpreadCellFormat()
                    {
                        HorizontalAlignment = SpreadHorizontalAlignment.Left,
                        VerticalAlignment = SpreadVerticalAlignment.Top,
                        FontSize = 14,
                        IsBold = true,
                        WrapText = true
                    });
                }


                SkipCells(row, numberOfColumnsForFirstCell - 1);

                string filterCellText = string.Empty;
                foreach (var filter in filters)
                {
                  if (bShowLabel)
                    filterCellText += "         " + filter.Key + ": " + filter.Value;
                  else
                    filterCellText += "         " + filter.Value;
                }

                using (ICellExporter cell = row.CreateCellExporter())
                {
                    cell.SetValue(filterCellText);
                    cell.SetFormat(new SpreadCellFormat()
                    {
                        HorizontalAlignment = SpreadHorizontalAlignment.Right,
                        VerticalAlignment = SpreadVerticalAlignment.Center,
                        IsBold = true,
                    });
                }
            }

            worksheet.SkipRows(1);
        }

        protected void CreateFilterRowWithHeaders(IWorksheetExporter worksheet, string reportTitle, IList<KeyValuePair<string, string>> filters, int numberOfColumns, int filtersInOneRow = 4, string groupText = "")
        {
            using (IRowExporter row = worksheet.CreateRowExporter())
            {
                row.SetHeightInPixels(28);
                using (ICellExporter cell = row.CreateCellExporter())
                {
                    if (!string.IsNullOrEmpty(groupText))
                    {
                        reportTitle = groupText + " › " + reportTitle;
                    }
                    cell.SetValue(reportTitle);
                    cell.SetFormat(new SpreadCellFormat()
                    {
                        HorizontalAlignment = SpreadHorizontalAlignment.Left,
                        VerticalAlignment = SpreadVerticalAlignment.Center,
                        FontSize = 14,
                        IsBold = true,
                        WrapText = true,
                        Fill = SpreadPatternFill.CreateSolidFill(new SpreadColor(14, 167, 229)),
                        ForeColor = new SpreadThemableColor(new SpreadColor(255, 255, 255))
                    });
                }
            }

            int filterStart = 0;
            var filtersForRow = filters.Skip(filterStart).Take(filtersInOneRow);

            while (filtersForRow.Any())
            {
                using (IRowExporter row = worksheet.CreateRowExporter())
                {
                    row.SetHeightInPixels(24);
                    using (ICellExporter cell = row.CreateCellExporter())
                    {
                        var filterText = string.Join("       ", filtersForRow.Select(filter => filter.Key + " : " + filter.Value));
                        cell.SetValue(filterText);
                        cell.SetFormat(new SpreadCellFormat()
                        {
                            HorizontalAlignment = SpreadHorizontalAlignment.Left,
                            VerticalAlignment = SpreadVerticalAlignment.Center,
                            FontSize = 11,
                            WrapText = true,
                            Fill = SpreadPatternFill.CreateSolidFill(new SpreadColor(14, 167, 229)),
                            ForeColor = new SpreadThemableColor(new SpreadColor(255, 255, 255))
                        });
                    }
                }
                filterStart += filtersInOneRow;
                filtersForRow = filters.Skip(filterStart).Take(filtersInOneRow);
            }
            worksheet.SkipRows(1);
        }

        protected ICellExporter CreateNewHeaderCell(IRowExporter row, string headerText, double fontSize = 10,
            SpreadHorizontalAlignment horizontalAlignment = SpreadHorizontalAlignment.Left, bool checkDoubleValue = true, bool wrapText = false)
        {
            ICellExporter cell;
            SpreadBorder border = new SpreadBorder(SpreadBorderStyle.Thin, new SpreadThemableColor(new SpreadColor(0, 0, 0)));
            using (cell = row.CreateCellExporter())
            {
                SpreadCellFormat cellFormat = new SpreadCellFormat();
                double doubleValue;
                if (checkDoubleValue && double.TryParse(headerText, out doubleValue))
                {
                    cell.SetValue(doubleValue);
                    cellFormat.NumberFormat = "#,##0.00";
                }
                else
                {
                    cell.SetValue(headerText);
                }
                
                cellFormat.HorizontalAlignment = horizontalAlignment;
                cellFormat.VerticalAlignment = SpreadVerticalAlignment.Center;
                cellFormat.FontSize = fontSize;
                cellFormat.IsBold = true;
                cellFormat.Fill = SpreadPatternFill.CreateSolidFill(new SpreadColor(201, 201, 201));
                cellFormat.TopBorder = border;
                cellFormat.BottomBorder = border;
                cellFormat.LeftBorder = border;
                cellFormat.RightBorder = border;
                cellFormat.WrapText = wrapText;
                cell.SetFormat(cellFormat);
            }
            return cell;
        }

    protected ICellExporter CreateNewTotalCell(IRowExporter row, string totalFormula, double fontSize = 10,
        SpreadHorizontalAlignment horizontalAlignment = SpreadHorizontalAlignment.Left, bool applyDecimalFormat = false, bool wrapText = false)
    {
      ICellExporter cell;
      SpreadBorder border = new SpreadBorder(SpreadBorderStyle.Thin, new SpreadThemableColor(new SpreadColor(0, 0, 0)));
      using (cell = row.CreateCellExporter())
      {
        SpreadCellFormat cellFormat = new SpreadCellFormat();
        cell.SetFormula(totalFormula);

        if (applyDecimalFormat)
        {
          cellFormat.NumberFormat = "#,##0.00";
        }

        cellFormat.HorizontalAlignment = horizontalAlignment;
        cellFormat.VerticalAlignment = SpreadVerticalAlignment.Center;
        cellFormat.FontSize = fontSize;
        cellFormat.IsBold = true;
        cellFormat.Fill = SpreadPatternFill.CreateSolidFill(new SpreadColor(201, 201, 201));
        cellFormat.TopBorder = border;
        cellFormat.BottomBorder = border;
        cellFormat.LeftBorder = border;
        cellFormat.RightBorder = border;
        cellFormat.WrapText = wrapText;
        cell.SetFormat(cellFormat);
      }
      return cell;
    }

    protected ICellExporter CreateNewCell(IRowExporter row, string cellText, double fontSize = 9, 
            SpreadHorizontalAlignment horizontalAlignment = SpreadHorizontalAlignment.Left, 
            SpreadVerticalAlignment verticalAlignment = SpreadVerticalAlignment.Center, bool isFill = false, SpreadColor fillColor = null,
            bool isBold = false, bool applyDecimalFormat = false, SpreadColor foreColor = null, bool wrapText = false)
        {
            if (cellText == null)
            {
                cellText = string.Empty;
            }
            ICellExporter cell;
            SpreadBorder border = new SpreadBorder(SpreadBorderStyle.Thin, new SpreadThemableColor(new SpreadColor(0, 0, 0)));
            using (cell = row.CreateCellExporter())
            {
                SpreadCellFormat cellFormat = new SpreadCellFormat();
                double doubleValue;
                long longValue;
                if (double.TryParse(cellText, out doubleValue) && (horizontalAlignment == SpreadHorizontalAlignment.Right))
                {
                    cell.SetValue(doubleValue);
                    if (applyDecimalFormat)
                    {
                        cellFormat.NumberFormat = "#,##0.00";
                    }
                    else
                    {
                        cellFormat.NumberFormat = "#,##0";
                    }
                }
                else if (long.TryParse(cellText, out longValue))
                {
                    cell.SetValue(longValue);
                }
                else
                {
                    cell.SetValue(cellText);
                }
                
                cellFormat.HorizontalAlignment = horizontalAlignment;
                cellFormat.VerticalAlignment = verticalAlignment;
                cellFormat.FontSize = fontSize;
                if (isFill)
                {
                    cellFormat.Fill = SpreadPatternFill.CreateSolidFill(fillColor);
                }
                cellFormat.TopBorder = border;
                cellFormat.BottomBorder = border;
                cellFormat.LeftBorder = border;
                cellFormat.RightBorder = border;
                cellFormat.IsBold = isBold;
                cellFormat.WrapText = wrapText;
                if (foreColor != null)
                {
                    cellFormat.ForeColor = new SpreadThemableColor(foreColor);
                }
                cell.SetFormat(cellFormat);
            }
            return cell;
        }

        protected void SkipCells(IRowExporter row, int numberOfCells)
        {
            for (int counter = 0; counter < numberOfCells; counter++)
            {
                using (ICellExporter cell = row.CreateCellExporter()) { };
            }
        }
    }
}