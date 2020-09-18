using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public class JwSilderBar : Control, IJwTheme
    {
        public delegate void ValueChangedEvent(object sender, float value);
        public JwSilderBar()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | 
                          ControlStyles.DoubleBuffer | 
                          ControlStyles.ResizeRedraw | 
                          ControlStyles.Selectable | 
                          ControlStyles.SupportsTransparentBackColor | 
                          ControlStyles.UserPaint, true);

            this.MouseDown += JwSilderBar_MouseDown;
            this.MouseMove += JwSilderBar_MouseMove;
            this.MouseUp += JwSilderBar_MouseUp;
            this.FontChanged += JwSilderBar_FontChanged;
            this.GotFocus += JwSilderBar_GotFocus;
            this.LostFocus += JwSilderBar_LostFocus;
            this.MouseEnter += JwSilderBar_MouseEnter;
            this.MouseLeave += JwSilderBar_MouseLeave;
        }

        //private Color _LineColor = Color.FromArgb(0x4C, 0x4C, 0x4C);
        //private Color _LeftColor = Color.FromArgb(0x33, 0x99, 0xFF);
        //private Color _ThumbColor = Color.FromArgb(255, 255, 255);
        //private Color _ThumbOutColor = Color.FromArgb(0x33, 0x99, 0xFF);
        private bool MLeftDown = false;
        private JwSilderBarValueType _ValueType = JwSilderBarValueType.Percent;
        private float _MinValue = 0f;
        private float _MaxValue = 100f;
        private float _Value = 0f;
        private bool _ThumbVisble = true;
        private float _ThumbWidth = 13f;
        private float _ThumbOutWidth = 3f;
        private float _LineHight = 5f;
        private int _DecimalPlaces = 0;

        private bool _TextVisable = false;
        private string _ValueFormat = "";
        private int _TextLeftMargin = 8;

        private RectangleF LineRect;
        private RectangleF ValueRect;
        private RectangleF ThumbRect;
        private SizeF TextRect;

        private int _radius = 2;

        public event ValueChangedEvent ValueChanged;
        public event EventHandler ValueChangeStart;
        public event EventHandler ValueChangeFinish;

        private bool _ReadOnly = false;
        private bool _MouseIn = false; 
        
        private ThemeType _Theme = ThemeType.Primary;
        [Description("配色方案"), Category("JwSilderBar.Theme")]
        public ThemeType Theme
        {
            get => _Theme;
            set
            {
                _Theme = value;
                Refresh();
            }
        }
        [Description("是否能改变SliderBar的值"), Category("JwSilderBar")]
        public bool ReadOnly
        {
            get => _ReadOnly; set => _ReadOnly = value;
        }
        [Description("圆角半径,0为不使用圆角"), Category("JwSilderBar")]
        public int Radius
        {
            get => _radius;
            set
            {
                if (_radius == value) return;
                _radius = value;
                Refresh();
            }
        }
        [Description("值文字左间距"), Category("JwSilderBar")]
        public int TextLeftMargin
        {
            get => _TextLeftMargin;
            set
            {
                if (value < 0) return;
                _TextLeftMargin = value;
                Refresh();
            }
        }
        [Description("保留小数位数"), Category("JwSilderBar.Value")]
        public int DecimalPlaces
        {
            get => _DecimalPlaces;
            set
            {
                if (value < 0) return;
                _DecimalPlaces = value;
                this.Refresh();
            }
        }
        [Description("表示类型[百分比,实际值]"), Category("JwSilderBar.Value")]
        public JwSilderBarValueType ValueType
        {
            get { return _ValueType; }
            set 
            {
                if (_ValueType == value) return;
                _ValueType = value;
                if(value == JwSilderBarValueType.Percent)
                {
                    var p = InnerValue / _MaxValue;
                    _MinValue = 0;
                    _MaxValue = 100;
                    InnerValue = p * 100;
                }
                this.Refresh();
            }
        }
        [Description("最小值"), Category("JwSilderBar.Value")]
        public float MinValue
        {
            get => _MinValue;
            set
            {
                if (_ValueType != JwSilderBarValueType.Absolute) return;
                if (value > _MaxValue) return;
                _MinValue = value;
                if (InnerValue < value) InnerValue = value;
                this.Refresh();
            }
        }
        [Description("最大值 "), Category("JwSilderBar.Value")]
        public float MaxValue
        {
            get => _MaxValue;
            set
            {
                if (_ValueType != JwSilderBarValueType.Absolute) return;
                if (value < _MinValue) return;
                _MaxValue = value;
                if (InnerValue > value) InnerValue = value;
                this.Refresh();
            }
        }
        [Description("是否显示Thumb"), Category("JwSilderBar")]
        public bool ThumbVisble
        {
            get => _ThumbVisble;
            set
            {
                _ThumbVisble = value;
                this.Refresh();
            }
        }
        [Description("Thumb宽高度"), Category("JwSilderBar")]
        public float ThumbWidth
        {
            get { return _ThumbWidth; }
            set
            {
                if (value < this.Size.Height && value >= 0)
                {
                    _ThumbWidth = value;
                    this.Refresh();
                }
            }
        }
        [Description("Thumb边框宽度"), Category("JwSilderBar")]
        public float ThumbOutWidth
        {
            get { return _ThumbOutWidth; }
            set
            {
                if (value < _ThumbWidth && value >= 0)
                {
                    _ThumbOutWidth = value;
                    this.Refresh();
                }
            }
        }
        [Description("是否显示值文字"), Category("JwSilderBar")]
        public bool TextVisable
        {
            get => _TextVisable;
            set
            {
                _TextVisable = value;
                this.Refresh();
            }
        }
        [Description("值文字显示格式"), Category("JwSilderBar.Value")]
        public string ValueFormat { 
            get => _ValueFormat;
            set
            {
                _ValueFormat = value;
                this.Refresh();
            }
        }
        private float InnerValue
        {
            get => _Value;
            set
            {
                if (value != _Value && value >= _MinValue && value <= _MaxValue)
                {
                    _Value = (float)System.Math.Round((double)value, DecimalPlaces);
                    this.Refresh();
                    ValueChanged?.Invoke(this, _Value);
                }
            }
        }
        [Description("当前值"), Category("JwSilderBar.Value")]
        public float Value
        {
            get => _Value;
            set
            {
                if (!MLeftDown && value != _Value && value >= _MinValue && value <= _MaxValue)
                {
                    _Value = (float)System.Math.Round((double)value, DecimalPlaces);
                    this.Refresh();
                }
            }
        }
        [Description("总刻度高度"), Category("JwSilderBar")]
        public float LineHight
        {
            get => _LineHight;
            set
            {
                if (value > this.Size.Height) return;
                _LineHight = value;
                this.Refresh();
            }
        }
        private string ValueText
        {
            get
            {
                if (string.IsNullOrEmpty(_ValueFormat))
                {
                    return InnerValue.ToString();
                }
                else
                {
                    return string.Format(_ValueFormat, InnerValue);
                }
            }
        }
        private int HalfThumbW
        {
            get { return this.Size.Height / 2; }
        }
        /// <summary>
        /// 精确度
        /// </summary>
        private float precision
        {
            get
            {
                var offset = MaxValue - MinValue;
                if (offset > 10000) return 0.0001f;
                else if (offset > 1000) return 0.001f;
                else return 0.01f;
            }
        }

        #region theme
        [Description("总刻度背景色"), Category("JwSilderBar.Theme")]
        public Color LineColor
        {
            get => CustomTheme.Normal.Line;
            set
            {
                CustomTheme.Normal.Line = value;
                this.Refresh();
            }
        }
        [Description("值的背景色"), Category("JwSilderBar.Theme")]
        public Color LeftColor
        {
            get => CustomTheme.Normal.Value;
            set
            {
                CustomTheme.Normal.Value = value;
                this.Refresh();
            }
        }
        [Description("Thumb背景色"), Category("JwSilderBar.Theme")]
        public Color ThumbColor
        {
            get => CustomTheme.Normal.Thumb;
            set
            {
                CustomTheme.Normal.Thumb = value;
                this.Refresh();
            }
        }
        [Description("Thumb边框色"), Category("JwSilderBar.Theme")]
        public Color ThumbOutColor
        {
            get => CustomTheme.Normal.ThumbBorder;
            set
            {
                CustomTheme.Normal.ThumbBorder = value;
                this.Refresh();
            }
        }
        [Description("总刻度背景色"), Category("JwSilderBar.Theme")]
        public Color LineActiveColor
        {
            get => CustomTheme.Active.Line;
            set
            {
                CustomTheme.Active.Line = value;
                this.Refresh();
            }
        }
        [Description("值的背景色"), Category("JwSilderBar.Theme")]
        public Color LeftActiveColor
        {
            get => CustomTheme.Active.Value;
            set
            {
                CustomTheme.Active.Value = value;
                this.Refresh();
            }
        }
        [Description("Thumb背景色"), Category("JwSilderBar.Theme")]
        public Color ThumbActiveColor
        {
            get => CustomTheme.Active.Thumb;
            set
            {
                CustomTheme.Active.Thumb = value;
                this.Refresh();
            }
        }
        [Description("Thumb边框色"), Category("JwSilderBar.Theme")]
        public Color ThumbOutActiveColor
        {
            get => CustomTheme.Active.ThumbBorder;
            set
            {
                CustomTheme.Active.ThumbBorder = value;
                this.Refresh();
            }
        }
        [Description("总刻度背景色"), Category("JwSilderBar.Theme")]
        public Color LineDisableColor
        {
            get => CustomTheme.Disable.Line;
            set
            {
                CustomTheme.Disable.Line = value;
                this.Refresh();
            }
        }
        [Description("值的背景色"), Category("JwSilderBar.Theme")]
        public Color LeftDisableColor
        {
            get => CustomTheme.Disable.Value;
            set
            {
                CustomTheme.Disable.Value = value;
                this.Refresh();
            }
        }
        [Description("Thumb背景色"), Category("JwSilderBar.Theme")]
        public Color ThumbDisableColor
        {
            get => CustomTheme.Disable.Thumb;
            set
            {
                CustomTheme.Disable.Thumb = value;
                this.Refresh();
            }
        }
        [Description("Thumb边框色"), Category("JwSilderBar.Theme")]
        public Color ThumbOutDisableColor
        {
            get => CustomTheme.Disable.ThumbBorder;
            set
            {
                CustomTheme.Disable.ThumbBorder = value;
                this.Refresh();
            }
        }
        public JwSilderBarThemeStatus CustomTheme
        {
            get => _CustomTheme;
            set
            {
                if (value == null) return;
                _CustomTheme = value;
                if(Theme == ThemeType.None)
                {
                    Refresh();
                }
            }
        }
        private JwSilderBarThemeStatus _CustomTheme = new JwSilderBarThemeStatus()
        {
            Normal = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#007bff".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#007bff".HexToColor() },
            Active = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#007bff".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#007bff".HexToColor() },
            Disable = new JwSilderBarThemeInfo() { Line = "#C0C0C0".HexToColor(), Value = "#007bff".HexToColor(), Thumb = "#ffffff".HexToColor(), ThumbBorder = "#007bff".HexToColor() },
        };
        protected JwSilderBarThemeStatus ThemePrivate
        {
            get
            {
                if (Theme != ThemeType.None)
                {
                    return JwTheme.JwSilderBarThemeDict[Theme];
                }
                else
                {
                    return CustomTheme;
                }
            }
        }
        #endregion
        public void ThemeToCustom(ThemeType type)
        {
            CustomTheme = JwTheme.JwSilderBarThemeDict[type].Clone();
        }
        private void JwSilderBar_MouseLeave(object sender, System.EventArgs e)
        {
            _MouseIn = false;
            Refresh();
        }
        private void JwSilderBar_MouseEnter(object sender, System.EventArgs e)
        {
            _MouseIn = true;
            Refresh();
        }
        private void JwSilderBar_LostFocus(object sender, System.EventArgs e)
        {
            Refresh();
        }
        private void JwSilderBar_GotFocus(object sender, System.EventArgs e)
        {
            Refresh();
        }
        private void JwSilderBar_FontChanged(object sender, System.EventArgs e)
        {
            this.Refresh();
        }
        private void JwSilderBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (ReadOnly || !this.Enabled) return;
            var tempLineRect = LineRect;
            if(ThumbWidth> tempLineRect.Height)
            {
                tempLineRect.Y -= (ThumbWidth - tempLineRect.Height) / 2;
                tempLineRect.Height += ThumbWidth - tempLineRect.Height;
            }
            if (tempLineRect.Contains(e.Location) || ThumbRect.Contains(e.Location))
            {
                this.Focus();
                MLeftDown = true;
                ValueChangeStart?.Invoke(this, null);
                if(tempLineRect.Contains(e.Location) && !ThumbRect.Contains(e.Location))
                {
                    var x = e.Location.X - LineRect.X;
                    var percent = x / LineRect.Width;
                    if(_ValueType == JwSilderBarValueType.Percent)
                    {
                        InnerValue = percent * 100;
                    }
                    else
                    {
                        InnerValue = percent * _MaxValue;
                    }
                    Refresh();
                }
            }
        }
        private void JwSilderBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (MLeftDown)
            {
                var x = e.Location.X - LineRect.X;
                if (x < 0) x = 0;
                if (x > LineRect.Width) x = LineRect.Width;
                var percent = x / LineRect.Width;
                if (_ValueType == JwSilderBarValueType.Percent)
                {
                    InnerValue = percent * 100;
                }
                else
                {
                    InnerValue = percent * _MaxValue;
                }
                Refresh();
            }
        }
        private void JwSilderBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (MLeftDown)
            {
                MLeftDown = false;
                ValueChangeFinish?.Invoke(this, null);
            }
        }
        private float TotalWidth(SizeF TextSize)
        {
            return this.Size.Width - this.Size.Height - (TextSize.IsEmpty ? 0 : TextSize.Width + _TextLeftMargin);
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;
            g.HighQuality();
            var theme = ThemePrivate.Normal;
            if (!Enabled)
            {
                theme = ThemePrivate.Disable;
            }
            else if (Focused || _MouseIn)
            {
                theme = ThemePrivate.Active;
            }
            TextRect = _TextVisable ? g.MeasureString(ValueText, this.Font) : SizeF.Empty;
            LineRect = new RectangleF(HalfThumbW, (this.Size.Height - _LineHight) / 2, TotalWidth(TextRect), _LineHight);
            using (var brush = new SolidBrush(theme.Line))
            {
                using (var lineBorder = new Pen(theme.Value, 0.1f))
                {
                    if (this.Radius == 0)
                    {
                        g.FillRectangle(brush, LineRect);
                        if (this.Focused || _MouseIn)
                        {
                            g.DrawRectangle(lineBorder, LineRect.X, LineRect.Y, LineRect.Width, LineRect.Height);
                        }
                    }
                    else
                    {
                        var path = RoundedRectPath(LineRect, Radius);
                        g.FillPath(brush, path);
                        if (this.Focused || _MouseIn)
                        {
                            g.DrawPath(lineBorder, path);
                        }
                    }
                }
            }

            if(_TextVisable)
            {
                using (var brush = new SolidBrush(ForeColor))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    g.DrawString(ValueText, this.Font, brush, new RectangleF(LineRect.X + LineRect.Width + _TextLeftMargin, (this.Size.Height - TextRect.Height) / 2, TextRect.Width, TextRect.Height));
                }
            }
            var v = InnerValue;
            v = _ValueType == JwSilderBarValueType.Percent ? v / 100 : v / _MaxValue;
            var vw = LineRect.Width * v;
            ValueRect = new RectangleF(HalfThumbW, (this.Size.Height - _LineHight) / 2, vw, _LineHight);
            if (InnerValue > _MinValue)
            {                
                using (var brush = new SolidBrush(theme.Value))
                {
                    if (this.Radius == 0)
                    {
                        g.FillRectangle(brush, ValueRect);
                    }
                    else
                    {
                        var path = RoundedRectPath(ValueRect, Radius);
                        g.FillPath(brush, path);
                    }
                }
            }

            ThumbRect = new RectangleF(ValueRect.X + ValueRect.Width - _ThumbWidth / 2, (this.Size.Height - _ThumbWidth) / 2, _ThumbWidth, _ThumbWidth);
            if (ThumbVisble)
            {
                var ThumbInnerRect = new RectangleF(ThumbRect.X + _ThumbOutWidth, ThumbRect.Y + _ThumbOutWidth, ThumbRect.Width - _ThumbOutWidth * 2, ThumbRect.Width - _ThumbOutWidth * 2);

                using (var brush = new SolidBrush(theme.ThumbBorder))
                {
                    g.FillEllipse(brush, ThumbRect);
                }
                using (var brush = new SolidBrush(theme.Thumb))
                {
                    g.FillEllipse(brush, ThumbInnerRect);
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!ReadOnly && this.Enabled)
            {
                if (keyData == Keys.Left)
                {
                    if (InnerValue > MinValue)
                    {
                        float offset = (MaxValue - MinValue) * precision;
                        if (InnerValue - offset < MinValue)
                        {
                            InnerValue = MinValue;
                        }
                        else
                        {
                            InnerValue -= offset;
                        }
                    }
                    return true;
                }
                if (keyData == Keys.Right)
                {
                    if (InnerValue < _MaxValue)
                    {
                        var offset = (MaxValue - MinValue) * precision;
                        if (InnerValue + offset > MaxValue)
                        {
                            InnerValue = MaxValue;
                        }
                        else
                        {
                            InnerValue += offset;
                        }
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
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
    public enum JwSilderBarValueType
    {
        Percent,
        Absolute,
    }
}
