using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace nnrr
{
    public class ModernButton : Button
    {
        public int BorderRadius { get; set; } = 0; // Square by default
        public Color HoverColor { get; set; } = Theme.ButtonHover;
        public Color ClickColor { get; set; } = Theme.ButtonDown;
        public Color NormalColor { get; set; } = Theme.Secondary;
        public Color BorderColor { get; set; } = Theme.Accent;

        public ModernButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(100, 40);
            this.BackColor = NormalColor;
            this.ForeColor = Theme.Text;
            this.Resize += (s, e) => { if (this.Region != null) this.Region.Dispose(); this.Region = null; };
        }

        private GraphicsPath GetFigurePath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            if (radius < 1)
            {
                path.AddRectangle(rect);
            }
            else
            {
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            }
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF rectSurface = new RectangleF(0, 0, this.Width, this.Height);
            RectangleF rectBorder = new RectangleF(1, 1, this.Width - 2, this.Height - 2);

            using (GraphicsPath pathSurface = GetFigurePath(rectSurface, BorderRadius))
            using (GraphicsPath pathBorder = GetFigurePath(rectBorder, BorderRadius - 1))
            using (Pen penBorder = new Pen(BorderColor, 1.5f))
            {
                penBorder.Alignment = PenAlignment.Inset;
                
                // 1. Draw Parent Background to "erase" corners (Simulate Transparency)
                if (this.Parent != null)
                {
                    using (SolidBrush parentBrush = new SolidBrush(this.Parent.BackColor))
                    {
                        pevent.Graphics.FillRectangle(parentBrush, rectSurface);
                    }
                }
                // 2. Fill Button Background
                if (Control.MouseButtons == MouseButtons.Left && this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    using SolidBrush clickBrush = new SolidBrush(ClickColor);
                    pevent.Graphics.FillPath(clickBrush, pathSurface);
                }
                else if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
                {
                    using SolidBrush hoverBrush = new SolidBrush(HoverColor);
                    pevent.Graphics.FillPath(hoverBrush, pathSurface);
                }
                else
                {
                    using SolidBrush normalBrush = new SolidBrush(NormalColor);
                    pevent.Graphics.FillPath(normalBrush, pathSurface);
                }

                pevent.Graphics.DrawPath(penBorder, pathSurface);
                
                // Draw Text
                TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
        
        // Handle Paint Conflicts
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            this.Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            this.Invalidate();
        }
    }
}
