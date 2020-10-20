using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Damacon.WebApp.Export.PDF
{
    public class CellSpacingEvent : IPdfPCellEvent
    {
        private int cellSpacing;
        private bool isFill;
        public CellSpacingEvent(int cellSpacing, bool isFill)
        {
            this.cellSpacing = cellSpacing;
            this.isFill = isFill;
        }
        void IPdfPCellEvent.CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
        {
            //Grab the line canvas for drawing lines on
            PdfContentByte cb = canvases[PdfPTable.LINECANVAS];

            cb.SetLineWidth((float)0.5);
            //Create a new rectangle using our previously supplied spacing
            cb.Rectangle(
                position.Left + this.cellSpacing,
                position.Bottom + this.cellSpacing,
                (position.Right - this.cellSpacing) - (position.Left + this.cellSpacing),
                (position.Top - this.cellSpacing) - (position.Bottom + this.cellSpacing)
                );

            if (isFill)
            {
                cb.SetColorFill(new BaseColor(192, 192, 192));
            }
            else
            {
                cb.SetColorFill(new BaseColor(255, 255, 255));
            }

            //Set a color
            cb.SetColorStroke(BaseColor.BLACK);
            //Draw the rectangle
            cb.FillStroke();
        }
    }
}