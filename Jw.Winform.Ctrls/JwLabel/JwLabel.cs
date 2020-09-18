using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class JwLabel : Label, IJwTheme
    {
        private bool _MouseIn = false;
        private bool _Clickable = false;
        private int _Radius = 4;
        private Color _FocusBackColor = Color.Black;
        private Color _FocusTextColor = Color.White; 
        private int _BorderWidth = 0;
        private ThemeType _Theme = ThemeType.Primary;
        //private bool _WordWrap = false;

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

        [Description("圆角"), Category("JwLabel")]
        public int Radius
        {
            get => _Radius;
            set
            {
                _Radius = value;
                Refresh();
            }
        }
        [Description("是否可点击"), Category("JwLabel")]
        public bool Clickable { get => _Clickable; set => _Clickable = value; }
        [Description("边框宽度"), Category("JwLabel")]
        public int BorderWidth
        {
            get => _BorderWidth;
            set
            {
                if (value == _BorderWidth) return;
                _BorderWidth = value;
                Refresh();
            }
        }

        #region theme
        [Description("配色方案"), Category("JwIcon.Theme")]
        public ThemeType Theme
        {
            get => _Theme;
            set
            {
                _Theme = value;
                Refresh();
            }
        }
        [Description("背景颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color BgColor
        {
            get => CustomTheme.Normal.Back;
            set
            {
                if (value == CustomTheme.Normal.Back) return;
                CustomTheme.Normal.Back = value;
                Refresh();
            }
        }
        [Description("激活时背景颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color BgActiveColor
        {
            get => CustomTheme.Active.Back;
            set
            {
                if (value == CustomTheme.Active.Back) return;
                CustomTheme.Active.Back = value;
                Refresh();
            }
        }
        [Description("禁用时背景颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color BgDisableColor
        {
            get => CustomTheme.Disable.Back;
            set
            {
                if (value == CustomTheme.Disable.Back) return;
                CustomTheme.Disable.Back = value;
                Refresh();
            }
        }
        [Description("激活时文字颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public new Color ForeColor
        {
            get => CustomTheme.Normal.Fore;
            set
            {
                if (value == CustomTheme.Normal.Fore) return;
                CustomTheme.Normal.Fore = value;
                Refresh();
            }
        }
        [Description("激活时文字颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color ForeActiveColor
        {
            get => CustomTheme.Active.Fore;
            set
            {
                if (value == CustomTheme.Active.Fore) return;
                CustomTheme.Active.Fore = value;
                Refresh();
            }
        }
        [Description("禁用时文字颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color ForeDisableColor
        {
            get => CustomTheme.Disable.Fore;
            set
            {
                if (value == CustomTheme.Disable.Fore) return;
                CustomTheme.Disable.Fore = value;
                Refresh();
            }
        }
        [Description("边框颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color BorderColor
        {
            get => CustomTheme.Normal.Border;
            set
            {
                if (value == CustomTheme.Normal.Border) return;
                CustomTheme.Normal.Border = value;
                Refresh();
            }
        }
        [Description("激活时边框颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color BorderActiveColor
        {
            get => CustomTheme.Active.Border;
            set
            {
                if (value == CustomTheme.Active.Border) return;
                CustomTheme.Active.Border = value;
                Refresh();
            }
        }
        [Description("禁用时边框颜色,不使用Theme时有效果"), Category("JwIcon.Theme")]
        public Color BorderDisableColor
        {
            get => CustomTheme.Disable.Border;
            set
            {
                if (value == CustomTheme.Disable.Border) return;
                CustomTheme.Disable.Border = value;
                Refresh();
            }
        }
        protected JwLabelThemeStatus CustomTheme = new JwLabelThemeStatus()
        {
            Normal = new JwLabelThemeInfo() { Back = "#007bff".HexToColor(), Border = "#007bff".HexToColor(), Fore = "#ffffff".HexToColor() },
            Active = new JwLabelThemeInfo() { Back = "#0069d9".HexToColor(), Border = "#0062cc".HexToColor(), Fore = "#ffffff".HexToColor() },
            Disable = new JwLabelThemeInfo() { Back = "#007bff".HexToColor(), Border = "#007bff".HexToColor(), Fore = "#ffffff".HexToColor() },
        };
        protected JwLabelThemeStatus ThemePrivate
        {
            get
            {
                if (Theme != ThemeType.None)
                {
                    return JwTheme.JwLabelThemeDict[Theme];
                }
                else
                {
                    return CustomTheme;
                }
            }
        }
        #endregion

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
            var g = e.Graphics;
            g.HighQuality();
            var theme = ThemePrivate.Normal;
            if (!Enabled)
            {
                theme = ThemePrivate.Disable;
            }
            else if (_MouseIn || this.Focused)
            {
                theme = ThemePrivate.Active;
            }
            // back
            if (theme.Back.A == 255 && BackgroundImage == null)
            {
                if (Radius < 1)
                {
                    e.Graphics.Clear(theme.Back);
                }
                else
                {
                    using (var brush = new SolidBrush(theme.Back))
                    {
                        e.Graphics.FillPath(brush, GdiPlus.RoundedRectPath(ClientRectangle, Radius));
                    }
                }
            }
            // border
            if (BorderWidth > 0)
            {
                using (var pen = new Pen(theme.Border, BorderWidth))
                {
                    if (Radius < 1)
                        g.DrawRectangle(pen, 0, 0, this.Size.Width, this.Size.Height);
                    else
                        g.DrawPath(pen, GdiPlus.RoundedRectPath(ClientRectangle, Radius));
                }
            }
            // text
            var TextRect = new Rectangle(this.Padding.Left, this.Padding.Top, ClientSize.Width - this.Padding.Left - this.Padding.Right, ClientSize.Height - this.Padding.Top - this.Padding.Bottom);
            TextRenderer.DrawText(e.Graphics, Text,this.Font, TextRect, theme.Fore, TextFormatFlags.EndEllipsis| LabelTextFmt(TextAlign));
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
