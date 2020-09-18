using Jw.Winform.Ctrls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace Jw.Winform.Forms
{
    public partial class JwNotifyForm : Form
    {
        private string _uuid;
        //private int _Radius = 8;
        private int _BorderWidth = 1;
        private ButtonBorderStyle _BorderStyle = ButtonBorderStyle.Solid;
        private bool _ShowCloseButton = false;
        private ThemeType _Theme = ThemeType.Primary;
        private bool _AutoClose = true;
        private int _CloseDelay = 5000;
        public string Uuid
        {
            get { return _uuid; }
        }
        [Description("配色方案"), Category("JwDialogBase")]
        public ThemeType Theme
        {
            get => _Theme;
            set
            {
                if (ThemeType.None == value || value == _Theme) return;
                _Theme = value;
                ApplyTheme();
                Refresh();
            }
        }
        [Description("是否显示关闭按钮"), Category("JwDialogBase")]
        public bool ShowCloseButton
        {
            get => _ShowCloseButton;
            set
            {
                _ShowCloseButton = value;
                ApplyTheme();
                Refresh();
            }
        }
        [Description("边框宽度"), Category("JwDialogBase")]
        public int BorderWidth
        {
            get
            {
                return this._BorderWidth;
            }
            set
            {
                this._BorderWidth = value;
                this.Refresh();
            }
        }
        [Description("边框样式"), Category("JwDialogBase")]
        public ButtonBorderStyle BorderStyle
        {
            get
            {
                return this._BorderStyle;
            }
            set
            {
                this._BorderStyle = value;
                if (BorderWidth > 0)
                {
                    this.Refresh();
                }
            }
        }
        /// <summary>
        /// 边框圆角
        /// </summary>
        //[Description("边框圆角"), Category("JwDialogBase")]
        //public int Radius
        //{
        //    get
        //    {
        //        return this._Radius;
        //    }
        //    set
        //    {
        //        this._Radius = Math.Max(value, 1);
        //        Refresh();
        //    }
        //}
        public string MessageIcon
        {
            get => JwIIcon.IconText;
            set
            {
                JwIIcon.IconText = value;
                Refresh();
            }
        }
        public JwNotifyForm()
        {
            _uuid = Guid.NewGuid().ToString();
            InitializeComponent();
        }
        public string Message
        {
            get => TxtMessage.Text;
            set
            {
                var size = TextRenderer.MeasureText(value, TxtMessage.Font, new Size(TxtMessage.ClientRectangle.Width, int.MaxValue), 
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.WordBreak);
                if (TxtMessage.ClientRectangle.Height < size.Height)
                {
                    Size = new Size(Size.Width, Size.Height + (size.Height - TxtMessage.ClientRectangle.Height));
                }
                TxtMessage.Text = value;
            }
        }

        public bool AutoClose { get => _AutoClose; set => _AutoClose = value; }
        public int CloseDelay { get => _CloseDelay; set => _CloseDelay = value; }

        protected virtual void ApplyTheme()
        {
            BtnClose.Visible = ShowCloseButton;

            var colors = JwTheme.JwDialogThemeDict[Theme].Normal;
            JwIIcon.Theme = BtnClose.Theme = jwSilderBar1.Theme = Theme;
            jwSilderBar1.CustomTheme.Normal.Line = 
            jwSilderBar1.CustomTheme.Active.Line = 
            jwSilderBar1.CustomTheme.Disable.Line = colors.Back;
            jwSilderBar1.CustomTheme.Normal.Value = 
            jwSilderBar1.CustomTheme.Active.Value = 
            jwSilderBar1.CustomTheme.Disable.Value = colors.Fore;
            jwSilderBar1.Theme = ThemeType.None;
            TxtMessage.BackColor = colors.Back;
            TxtMessage.ForeColor = colors.Fore;
            this.BackColor = colors.Back;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (BorderWidth > 0)
            {
                e.Graphics.HighQuality();
                var colors = JwTheme.JwDialogThemeDict[Theme].Normal;
                using(var pen = new Pen(colors.Fore, BorderWidth))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, Width, Height);
                }
            }
        }
        private void JwNotify_Load(object sender, System.EventArgs e)
        {
            ApplyTheme();
            jwSilderBar1.Visible = AutoClose;
            if(AutoClose)
            {
                jwSilderBar1.ValueType = JwSilderBarValueType.Absolute;
                jwSilderBar1.MaxValue = CloseDelay;
                jwSilderBar1.MinValue = 0;
                jwSilderBar1.Value = 0;
                timer1.Interval = 10;
                timer1.Start();
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            this.Close();
        }
        public static void DoMouseDown(IntPtr hwnd)
        {
            WinApi.ReleaseCapture();
            WinApi.SendMessage(hwnd, (int)WinApi.Messages.WM_SYSCOMMAND, (int)WinApi.Messages.SC_MOVE + (int)WinApi.HitTest.HTCAPTION, 0);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (jwSilderBar1.Value < CloseDelay)
                jwSilderBar1.Value += timer1.Interval;
            else
            {
                if (this.Opacity > timer1.Interval / 500d)
                    this.Opacity -= timer1.Interval / 500d;
                else
                {
                    timer1.Stop();
                    this.Close();
                }
            }               
        }

        private void JwNotifyForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                DoMouseDown(this.Handle);
        }
    }
}
