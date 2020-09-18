using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jw.Winform.Ctrls
{
    public static class GdiPlus
    {
        public static Graphics HighQuality(this Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            return g;
        }
        public static GraphicsPath RoundedFormPath(Rectangle rect, int radius)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Rectangle region = new Rectangle(rect.Location, new Size(radius, radius));
            graphicsPath.AddArc(region, 180f, 90f);
            region.X = rect.Right - radius;
            graphicsPath.AddArc(region, 270f, 90f);
            region.Y = rect.Bottom - radius;
            region.Width += 1;
            region.Height += 1;
            graphicsPath.AddArc(region, 360f, 90f);
            region.X = rect.Left;
            graphicsPath.AddArc(region, 90f, 90f);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }
        public static GraphicsPath RoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            roundedRect.AddLine(rect.X + radius, rect.Y, rect.Right - radius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + radius * 2, rect.Right, rect.Y + rect.Height - radius * 2);
            roundedRect.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - radius * 2, rect.Bottom, rect.X + radius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - radius * 2, rect.X, rect.Y + radius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
        public static GraphicsPath RoundedRectPath(RectangleF rect, int radius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            roundedRect.AddLine(rect.X + radius, rect.Y, rect.Right - radius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + radius * 2, rect.Right, rect.Y + rect.Height - radius * 2);
            roundedRect.AddArc(rect.X + rect.Width - radius * 2, rect.Y + rect.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - radius * 2, rect.Bottom, rect.X + radius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - radius * 2, rect.X, rect.Y + radius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }
    }
}
