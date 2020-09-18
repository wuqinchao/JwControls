using Jw.Winform.Ctrls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Forms
{
    public partial class JwDialogBase : Form
    {
        /// <summary>
        /// 边圆角大小
        /// </summary>
        private int _Radius = 10;
        private Color _BorderColor = Color.Transparent;
        private Color _HeadColor = Color.Transparent;
        private int _BorderWidth = 0;
        private ButtonBorderStyle _BorderStyle = ButtonBorderStyle.Solid;
        private ThemeType _Theme = ThemeType.Primary;
        private bool _ShowCloseButton = true;
        private bool _CanMoveWindow = true;
        /// <summary>
        /// 边框宽度
        /// </summary>
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
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("JwDialogBase")]
        public Color BorderColor
        {
            get
            {
                return this._BorderColor;
            }
            set
            {
                this._BorderColor = value;
                if (BorderWidth > 0)
                {
                    this.Refresh();
                }
            }
        }
        /// <summary>
        /// 边框样式
        /// </summary>
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
        [Description("边框圆角"), Category("JwDialogBase")]
        public int Radius
        {
            get
            {
                return this._Radius;
            }
            set
            {
                this._Radius = Math.Max(value, 1);
                Refresh();
            }
        }
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色"), Category("JwDialogBase")]
        public Color HeadColor
        {
            get
            {
                return this._HeadColor;
            }
            set
            {
                this._HeadColor = value;
                this.Refresh();
            }
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
                Refresh();
            }
        }
        [Description("是否能移动对话框"), Category("JwDialogBase")]
        public bool CanMoveWindow
        { 
            get => _CanMoveWindow;
            set
            {
                _CanMoveWindow = value;
            }
        }
        public string TitleIcon
        {
            get => JwHead.IconText;
            set => JwHead.IconText = value;
        }
        [Description("对话框标题"), Category("JwDialogBase")]
        public string Title
        {
            get => JwHead.Text;
            set
            {
                JwHead.Text = value;
                Refresh();
            }
        }
        public JwDialogBase()
        {
            InitializeComponent();
            ApplyTheme();
            PanHeadBox.MouseDown += c_MouseDown;
            JwHead.MouseDown += c_MouseDown;
        }
        protected virtual void ApplyTheme()
        {
            JwHead.Theme = BtnClose.Theme = Theme;
            PanHeadBox.BackColor = GetBackColor();
        }
        private Color GetBordColor()
        {
            if (!Enabled) return JwTheme.JwButtonThemeDict[Theme].Disable.Border;
            if (this.Focused) return JwTheme.JwButtonThemeDict[Theme].Active.Border;
            return JwTheme.JwButtonThemeDict[Theme].Normal.Border;
        }
        private Color GetBackColor()
        {
            if (!Enabled) return JwTheme.JwButtonThemeDict[Theme].Disable.Back;
            if (this.Focused) return JwTheme.JwButtonThemeDict[Theme].Active.Back;
            return JwTheme.JwButtonThemeDict[Theme].Normal.Back;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Radius>0)
            {
                Rectangle rect = new Rectangle(-1, -1, Width + 1, Height);
                GraphicsPath path = new GraphicsPath();
                path = GdiPlus.RoundedFormPath(rect, Radius);
                base.Region = new Region(path);
            }
            base.OnPaint(e);
            if (BorderWidth > 0)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                    GetBordColor(), BorderWidth, BorderStyle,
                    GetBordColor(), BorderWidth, BorderStyle,
                    GetBordColor(), BorderWidth, BorderStyle,
                    GetBordColor(), BorderWidth, BorderStyle);
            }
        }
        void c_MouseDown(object sender, MouseEventArgs e)
        {
            if (CanMoveWindow && e.Button == MouseButtons.Left)
                DoMouseDown(this.Handle);
        }
        #region 窗体拖动
        /// <summary>
        /// 通过Windows的API控制窗体的拖动
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        public static void DoMouseDown(IntPtr hwnd)
        {
            WinApi.ReleaseCapture();
            WinApi.SendMessage(hwnd, (int)WinApi.Messages.WM_SYSCOMMAND, (int)WinApi.Messages.SC_MOVE + (int)WinApi.HitTest.HTCAPTION, 0);
        }
        #endregion
        protected virtual void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
