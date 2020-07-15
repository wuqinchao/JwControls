using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    [DefaultEvent("Click")]
    [DefaultProperty("IconText")]
    [Designer("Jw.Winform.Ctrls.JwIconDesigner")]
    public class JwIcon : Control
    {
        public JwIcon()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

            this.Width = 80;
            this.Height = 32;
            this.BackColor = Color.Transparent;

            TextChanged += JwIcon_TextChanged;
            FontChanged += JwIcon_TextChanged;
            ForeColorChanged += JwIcon_TextChanged;
            PaddingChanged += JwIcon_TextChanged;
            BackColorChanged += JwIcon_TextChanged;
            SizeChanged += JwIcon_TextChanged;
        }

        private string iconText = Iconfont.TypeDict.Keys.First();
        private int _IconSize = 20;
        private bool _CententCenter = true;

        private Color _IconColor = Color.Black;
        private Color _IconActiveColor = Color.Black;
        private Color _IconDisableColor = Color.Black;

        private Color _BgColor = Color.Transparent;
        private Color _BgActiveColor = Color.Transparent;
        private Color _BgDisableColor = Color.Transparent;

        private Color _ForeActiveColor = Color.Black;
        private Color _ForeDisableColor = Color.Black;

        private Color _BorderColor = Color.Black;
        private Color _BorderActiveColor = Color.Black;
        private Color _BorderDisableColor = Color.Black;

        private bool _ShowFocused = true;

        private int _TextMarginLeft = 4;
        private int _AdjustIconTop = 0;
        private bool _AutoResize = false;
        private int _Radius = 3;

        private int _BorderWidth = 1;

        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFocused = false;

        private ThemeType _Theme = ThemeType.primary;

        public new event EventHandler Click;

        [Description("圆角"), Category("JwIcon")]
        public int Radius
        {
            get => _Radius;
            set
            {
                _Radius = value;
                Refresh();
            }
        }
        [Description("配色方案"), Category("JwIcon")]
        public ThemeType Theme
        {
            get => _Theme;
            set
            {
                _Theme = value;
                Refresh();
            }
        }
        private void JwIcon_TextChanged(object sender, EventArgs e)
        {
            SetSize();
        }
        [Description("图标大小"), Category("JwIcon")]
        public int IconSize
        {
            get => _IconSize;
            set
            {
                if (_IconSize == value) return;
                _IconSize = value;
                SetSize();
            }
        }
        [Description("图标颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color IconColor
        {
            get => _IconColor;
            set
            {
                if (value == _IconColor) return;
                _IconColor = value;
                Refresh();
            }
        }
        [Description("激活时图标颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color IconActiveColor
        {
            get => _IconActiveColor;
            set
            {
                if (value == _IconActiveColor) return;
                _IconActiveColor = value;
                Refresh();
            }
        }
        [Description("激活时图标颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color IconDisableColor
        {
            get => _IconDisableColor;
            set
            {
                if (value == _IconDisableColor) return;
                _IconDisableColor = value;
                Refresh();
            }
        }
        [Description("背景颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color BgColor
        {
            get => _BgColor;
            set
            {
                if (value == _BgColor) return;
                _BgColor = value;
                Refresh();
            }
        }
        [Description("激活时背景颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color BgActiveColor
        {
            get => _BgActiveColor;
            set
            {
                if (value == _BgActiveColor) return;
                _BgActiveColor = value;
                Refresh();
            }
        }
        [Description("禁用时背景颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color BgDisableColor
        {
            get => _BgDisableColor;
            set
            {
                if (value == _BgDisableColor) return;
                _BgDisableColor = value;
                Refresh();
            }
        }
        [Description("激活时文字颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color ForeActiveColor
        {
            get => _ForeActiveColor;
            set
            {
                if (value == _ForeActiveColor) return;
                _ForeActiveColor = value;
                Refresh();
            }
        }
        [Description("禁用时文字颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color ForeDisableColor
        {
            get => _ForeDisableColor;
            set
            {
                if (value == _ForeDisableColor) return;
                _ForeDisableColor = value;
                Refresh();
            }
        }
        [Description("边框颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color BorderColor
        {
            get => _BorderColor;
            set
            {
                if (value == _BorderColor) return;
                _BorderColor = value;
                Refresh();
            }
        }
        [Description("激活时边框颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color BorderActiveColor
        {
            get => _BorderActiveColor;
            set
            {
                if (value == _BorderActiveColor) return;
                _BorderActiveColor = value;
                Refresh();
            }
        }
        [Description("禁用时边框颜色,不使用Theme时有效果"), Category("JwIcon")]
        public Color BorderDisableColor
        {
            get => _BorderDisableColor;
            set
            {
                if (value == _BorderDisableColor) return;
                _BorderDisableColor = value;
                Refresh();
            }
        }
        [Description("边框宽度"), Category("JwIcon")]
        public int BorderWidth
        {
            get => _BorderWidth;
            set
            {
                if (value == _BorderWidth) return;
                _BorderWidth = value;
                SetSize();
            }
        }

        [Description("图标"), Category("JwIcon")]
        public string IconText
        {
            get => iconText;
            set
            {
                if (!string.IsNullOrEmpty(value) && !Iconfont.TypeDict.ContainsKey(value)) return;
                iconText = value;
                SetSize();
            }
        }
        [Description("图标与文字间距(有文字是有效)"), Category("JwIcon")]
        public int TextMarginLeft
        {
            get => _TextMarginLeft;
            set
            {
                if (value == _TextMarginLeft) return;
                _TextMarginLeft = value;
                SetSize();
            }
        }
        [Description("是否自动设置控件大小"), Category("JwIcon")]
        public bool AutoResize
        {
            get => _AutoResize;
            set
            {
                _AutoResize = value;
                SetSize();
            }
        }
        [Description("内容是否水平居中"),Category("JwIcon")]
        public bool CententCenter
        {
            get => _CententCenter;
            set
            {
                _CententCenter = value;
                SetSize();
            }
        }
        [Description("是否显示获得焦点状态"),Category("JwIcon")]
        public bool ShowFocused
        {
            get => _ShowFocused;
            set
            {
                _ShowFocused = value;
                Refresh();
            }
        }
        [Description("图标与上边距离微调"), Category("JwIcon")]
        public int AdjustIconTop
        {
            get => _AdjustIconTop;
            set
            {
                _AdjustIconTop = value;
                SetSize();
            }
        }
        private string RealText
        {
            get
            {
                if (!string.IsNullOrEmpty(IconText) && Iconfont.TypeDict.ContainsKey(IconText))
                    return Iconfont.TypeDict[IconText];
                else
                    return string.Empty;
            }
        }
        private Font GetIconFont()
        {
            var size = IconSize * (3f / 4f);
            var font = new Font(Iconfont.Family, size, FontStyle.Regular, GraphicsUnit.Point);
            return font;
        }
        private void SetSize()
        {
            if (!AutoResize)
            {
                Refresh();
                return;
            }
            using (var g = CreateGraphics())
            {
                int w, h, iw, ih, tw = 0, th = 0;
                InitIconGraphics(g);
                using (var iconFont = GetIconFont())
                {
                    var isize = g.MeasureString(RealText, iconFont, int.MaxValue, StringFormat.GenericTypographic);
                    w = iw = (int)Math.Ceiling(isize.Width);
                    h = ih = (int)Math.Ceiling(isize.Height);
                }

                if (!string.IsNullOrEmpty(Text))
                {
                    var tsize = g.MeasureString(Text, this.Font, int.MaxValue, StringFormat.GenericTypographic);
                    tw = (int)Math.Ceiling(tsize.Width);
                    th = (int)Math.Ceiling(tsize.Height);
                }

                h = Math.Max(ih, th) + Padding.Top + Padding.Bottom;
                w = iw + Padding.Left + Padding.Bottom + (tw > 0 ? tw + TextMarginLeft : 0);

                if (w != this.Size.Width || h != this.Size.Height)
                {
                    this.Size = new Size(w, h);
                }
            }
        }
        private bool ExistText
        {
            get { return !string.IsNullOrEmpty(Text); }
        }
        private bool ExistIcon
        {
            get { return !string.IsNullOrEmpty(IconText) && Iconfont.TypeDict.ContainsKey(IconText); }
        }
        private float DrawRectWidth
        {
            get { return Size.Width - Padding.Left - Padding.Right; }
        }
        private float DrawRectHeight
        {
            get { return Size.Height - Padding.Top - Padding.Bottom; }
        }
        private void InitIconGraphics(Graphics g)
        {
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    var backColor = GetBackColor();
        //    Console.WriteLine(ColorTranslator.ToHtml(backColor));
        //    if (backColor.A == 255 && BackgroundImage == null)
        //    {
        //        if (Radius < 1)
        //        {
        //            e.Graphics.Clear(backColor);
        //        }
        //        else
        //        {
        //            e.Graphics.Clear(Color.Transparent);
        //            using (var brush = new SolidBrush(backColor))
        //            {
        //                e.Graphics.FillPath(brush, RoundedRectPath(ClientRectangle, Radius));
        //            }
        //        }
        //        return;
        //    }

        //    base.OnPaintBackground(e);
        //}
        protected override void OnPaint(PaintEventArgs e)
        {
            SizeF iconSize = SizeF.Empty;
            SizeF textSize = SizeF.Empty;
            var span = TextMarginLeft;
            var press = isPressed ? 1 : 0;
            var g = e.Graphics;
            InitIconGraphics(g);

            if(!ExistIcon || !ExistText) span = 0;
            if (ExistText) textSize = g.MeasureString(Text, this.Font, int.MaxValue, StringFormat.GenericTypographic);
            // 背景
            var backColor = GetBackColor();
            if (backColor.A == 255 && BackgroundImage == null)
            {
                if (Radius < 1)
                {
                    e.Graphics.Clear(backColor);
                }
                else
                {
                    using (var brush = new SolidBrush(backColor))
                    {
                        e.Graphics.FillPath(brush, GdiPlus.RoundedRectPath(ClientRectangle, Radius));
                    }
                }
            }
            // 边框
            if (BorderWidth > 0)
            {
                if (Radius < 1)
                {
                    using (var pen = new Pen(GetBordColor(), BorderWidth))
                    {
                        g.DrawRectangle(pen, 0, 0, this.Size.Width, this.Size.Height);
                    }
                }
                else
                {
                    using (var pen = new Pen(GetBordColor(), BorderWidth))
                    {
                        g.DrawPath(pen, GdiPlus.RoundedRectPath(ClientRectangle, Radius));
                    }
                }
            }
            // 图标
            if (ExistIcon)
            {
                using (var font = GetIconFont())
                {
                    iconSize = g.MeasureString(RealText, font, int.MaxValue, StringFormat.GenericTypographic);
                    using (var brush = new SolidBrush(GetIconColor()))
                    {
                        if (CententCenter)
                        {
                            g.DrawString(RealText, font, brush, press + Padding.Left + ((DrawRectWidth - textSize.Width - iconSize.Width - span) / 2), press + Padding.Top + ((DrawRectHeight - iconSize.Height) / 2) + AdjustIconTop, StringFormat.GenericTypographic);
                        }
                        else
                        {
                            g.DrawString(RealText, font, brush, press + Padding.Left, press + Padding.Top + ((DrawRectHeight - iconSize.Height) / 2) + AdjustIconTop, StringFormat.GenericTypographic);
                        }
                    }
                }
            }
            // 文字
            if (ExistText)
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                using (var tbrush = new SolidBrush(GetForeColor()))
                {
                    if (CententCenter)
                    {
                        g.DrawString(Text, this.Font, tbrush, press + Padding.Left + iconSize.Width + span + ((DrawRectWidth - iconSize.Width - span - textSize.Width) / 2), press + Padding.Top + ((DrawRectHeight - textSize.Height) / 2), StringFormat.GenericTypographic);
                    }
                    else
                    {
                        g.DrawString(Text, this.Font, tbrush, press + Padding.Left + iconSize.Width + span, press + Padding.Top + ((DrawRectHeight - textSize.Height) / 2), StringFormat.GenericTypographic);
                    }
                }
            }
            // 焦点
            if (ShowFocused && isFocused)
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);
        }
        private Color GetIconColor()
        {
            if (!Enabled) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? IconDisableColor : JwTheme.JwIconThemeDict[Theme].ForeDisableColor;
            if (isHovered || isPressed || isFocused) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? IconActiveColor : JwTheme.JwIconThemeDict[Theme].ForeActiveColor;
            return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? IconColor : JwTheme.JwIconThemeDict[Theme].ForeColor;
        }
        private Color GetForeColor()
        {
            if (!Enabled) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? IconDisableColor : JwTheme.JwIconThemeDict[Theme].ForeDisableColor;
            if (isHovered || isPressed || isFocused) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? IconActiveColor : JwTheme.JwIconThemeDict[Theme].ForeActiveColor;
            return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? IconColor : JwTheme.JwIconThemeDict[Theme].ForeColor;
        }
        private Color GetBackColor()
        {
            if (!Enabled) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? BgDisableColor : JwTheme.JwIconThemeDict[Theme].BackDisableColor;
            if ((isHovered || isPressed || isFocused) && ShowFocused) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? BgActiveColor : JwTheme.JwIconThemeDict[Theme].BackActiveColor;
            return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? BgColor : JwTheme.JwIconThemeDict[Theme].BackColor;
        }
        private Color GetBordColor()
        {
            if (!Enabled) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? BorderDisableColor : JwTheme.JwIconThemeDict[Theme].BordDisableColor;
            if (isHovered || isPressed || isFocused) return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? BorderActiveColor : JwTheme.JwIconThemeDict[Theme].BordActiveColor;
            return (Theme == ThemeType.none || !JwTheme.JwIconThemeDict.ContainsKey(Theme)) ? BorderColor : JwTheme.JwIconThemeDict[Theme].BordColor;
        } 

        #region Focus Methods

        protected override void OnGotFocus(EventArgs e)
        {
            isFocused = true;
            Refresh();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isFocused = false;
            Refresh();

            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            isHovered = true;
            Refresh();

            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isHovered = false;
            Refresh();

            base.OnLeave(e);
        }

        #endregion

        #region Keyboard Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                isPressed = true;
                Click?.Invoke(this, e);
                Refresh();
            }

            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            isPressed = false;
            Refresh();

            base.OnKeyUp(e);
        }

        #endregion

        #region Mouse Methods

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Refresh();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                Refresh();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Refresh();

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;

            Refresh();

            base.OnMouseLeave(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            Click?.Invoke(this, e);
            base.OnMouseClick(e);
        }

        #endregion        
    }
}
