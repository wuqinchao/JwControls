using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class JwLabel : Label
    {
        public new event EventHandler Click; 
        public JwLabel()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable |
                     ControlStyles.UserPaint, true);

            this.GotFocus += JwLabel_GotFocus; ;
            this.LostFocus += JwLabel_GotFocus;
            this.MouseEnter += JwLabel_MouseEnter;
            this.MouseLeave += JwLabel_MouseLeave;
            this.MouseClick += JwLabel_MouseClick;

            this.Padding = new Padding(3, 5, 3, 5);
            this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AutoSize = true;
        }

        private void JwLabel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Clickable) return;
            Click?.Invoke(this, e);
        }

        private bool _MouseIn = false;
        private bool _Clickable = false;
        private Color _FocusBackColor = Color.Black;
        private Color _FocusTextColor = Color.White;

        [Description("获得焦点时的背景色"), Category("JwLabel")]
        public Color FocusBackColor
        {
            get => _FocusBackColor;
            set
            {
                _FocusBackColor = value;
                Refresh();
            }
        }

        [Description("获得焦点时的前景色"), Category("JwLabel")]
        public Color FocusTextColor
        {
            get => _FocusTextColor;
            set
            {
                _FocusTextColor = value;
                Refresh();
            }
        }

        [Description("是否可点击"), Category("JwLabel")]
        public bool Clickable { get => _Clickable; set => _Clickable = value; }
        private void JwLabel_MouseLeave(object sender, EventArgs e)
        {
            _MouseIn = false;
            if (Clickable) this.Cursor = Cursors.Default;
            Refresh();
        }
        private void JwLabel_MouseEnter(object sender, EventArgs e)
        {
            _MouseIn = true;
            if (Clickable) this.Cursor = Cursors.Hand;
            Refresh();
        }
        private void JwLabel_GotFocus(object sender, EventArgs e)
        {
            Refresh();
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                Color backColor = BackColor;

                if (_MouseIn || this.Focused)
                {
                    backColor = FocusBackColor;
                }

                if (backColor.A == 255 && BackgroundImage == null)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);
            }
            catch
            {
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }
        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            Color foreColor = ForeColor;

            if (_MouseIn || this.Focused)
            {
                foreColor = FocusTextColor;
            }
            var TextRect = new Rectangle(this.Padding.Left, this.Padding.Top, ClientSize.Width - this.Padding.Left - this.Padding.Right, ClientSize.Height - this.Padding.Top - this.Padding.Bottom);
            TextRenderer.DrawText(e.Graphics, Text,this.Font, TextRect, foreColor, TextFormatFlags.EndEllipsis| LabelTextFmt(TextAlign));
        }
        public static TextFormatFlags LabelTextFmt(System.Drawing.ContentAlignment textAlign)
        {
            TextFormatFlags controlFlags = TextFormatFlags.Default;
            switch (textAlign)
            {
                case System.Drawing.ContentAlignment.TopLeft:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case System.Drawing.ContentAlignment.TopCenter:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case System.Drawing.ContentAlignment.TopRight:
                    controlFlags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;

                case System.Drawing.ContentAlignment.MiddleLeft:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case System.Drawing.ContentAlignment.MiddleCenter:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;
                case System.Drawing.ContentAlignment.MiddleRight:
                    controlFlags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;

                case System.Drawing.ContentAlignment.BottomLeft:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case System.Drawing.ContentAlignment.BottomCenter:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case System.Drawing.ContentAlignment.BottomRight:
                    controlFlags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
            }
            return controlFlags;
        }
    }
}
