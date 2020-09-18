using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    [Designer("Jw.Winform.Ctrls.JwNavbarDesigner")]
    public class JwNavbar : TreeView, IIconfont, IJwTheme
    {
        private int _NodeHeight = 40;
        private Padding _NodePadding = new Padding(10, 0, 5, 0);
        private int _AdjustTopIconTop = 0;
        private int _AdjustChildIconTop = 0;
        private int _IconTopSize = 14;
        private int _IconChildSize = 6;
        private string _IconExpand = "arrow3-down";
        private string _IconContract = "arrow3-up";
        private string _IconChild = "arrow6-right"; 
        private ThemeType _Theme = ThemeType.Dark;
        //private const int GWL_STYLE = -16;
        private string _IconFontName = JwIconfontManager.DefaultFont;

        [Description("配色方案"), Category("JwNavbar")]
        public ThemeType Theme
        {
            get => _Theme;
            set
            {
                if (_Theme == value) return;
                _Theme = value;
                this.BackColor = ThemePrivate.Normal.TopNodeBack;
                Refresh();
            }
        }
        [Description("结点高度"), Category("JwNavbar")]
        public int NodeHeight
        {
            get => _NodeHeight;
            set
            {
                if (base.ItemHeight == value) return;
                base.ItemHeight = _NodeHeight = value;
                Refresh();
            }
        }
        [Description("结点Padding"), Category("JwNavbar")]
        public Padding NodePadding 
        { 
            get => _NodePadding;
            set
            {
                _NodePadding = value;
                Refresh();
            }
        }
        [Description("主结点图标大小"), Category("JwNavbar.TopNode")]
        public int IconTopSize
        {
            get => _IconTopSize;
            set
            {
                if (_IconTopSize == value) return;
                _IconTopSize = value;
                Refresh();
            }
        }
        [Description("主结点图标与上边距离微调"), Category("JwNavbar.TopNode")]
        public int AdjustTopIconTop
        {
            get => _AdjustTopIconTop;
            set
            {
                _AdjustTopIconTop = value;
                Refresh();
            }
        }
        [Description("展开时的图标"), Category("JwNavbar.TopNode")]
        [Editor(typeof(JwIconSelectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string IconExpand 
        { 
            get => _IconExpand;
            set
            {
                _IconExpand = value;
                Refresh();
            }
        }
        [Description("折叠时的图标"), Category("JwNavbar.TopNode")]
        [Editor(typeof(JwIconSelectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string IconContract 
        { 
            get => _IconContract;
            set
            {
                _IconContract = value;
                Refresh();
            }
        }
        [Description("子结点图标大小"), Category("JwNavbar.ChildNode")]
        public int IconChildSize
        {
            get => _IconChildSize;
            set
            {
                if (_IconChildSize == value) return;
                _IconChildSize = value;
                Refresh();
            }
        }
        [Description("子结点图标与上边距离微调"), Category("JwNavbar.ChildNode")]
        public int AdjustChildIconTop
        {
            get => _AdjustChildIconTop;
            set
            {
                _AdjustChildIconTop = value;
                Refresh();
            }
        }
        [Editor(typeof(JwIconSelectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("子结点图标"), Category("JwNavbar.ChildNode")]
        public string IconChild
        { 
            get => _IconChild;
            set
            {
                _IconChild = value;
                Refresh();
            }
        }
        [Editor(typeof(JwFontSelectionEditor), typeof(UITypeEditor))]
        [Description("使用图标字体的名称(注册时的名称)"), Category("JwNavbar")]
        public string IconFontName
        {
            get => _IconFontName;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !JwIconfontManager.ContainsFont(value)) return;
                _IconFontName = value;
            }
        }
        public JwNavbar()
        {
            base.HideSelection = false;
            base.ItemHeight = NodeHeight;
            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            base.DrawNode += new DrawTreeNodeEventHandler(NavBarDrawNode);
            base.NodeMouseClick += new TreeNodeMouseClickEventHandler(NavBarNodeMouseClick);
            base.SizeChanged += new EventHandler(this.NavBarSizeChanged);
            base.FullRowSelect = true;
            base.ShowLines = false;
            base.ShowPlusMinus = false;
            base.ShowRootLines = false;
            this.BackColor = ThemePrivate.Normal.Back;
            this.BorderStyle = BorderStyle.None;
            this.BackColor = ThemePrivate.Normal.TopNodeBack;
            DoubleBuffered = true;
            Font = new System.Drawing.Font("微软雅黑", 9F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        }

        #region custom theme
        private JwNavbarThemeStatus CustomTheme = new JwNavbarThemeStatus()
        {
            Normal = new JwNavbarThemeInfo()
            {
                Back = "#282828".HexToColor(),
                TopNodeBack = "#282828".HexToColor(),
                TopNodeBorder = "#232323".HexToColor(),
                TopNodeFore = "#999999".HexToColor(),
                ClildNodeBack = "#eeeeee".HexToColor(),
                ChildNodeIcon = "#bebebe".HexToColor(),
                ChildNodeFore = "#333333".HexToColor(),
                ChildNodeBorder = "#dddddd".HexToColor(),
            },
            Active = new JwNavbarThemeInfo()
            {
                Back = "#282828".HexToColor(),
                TopNodeBack = "#2c2c2c".HexToColor(),
                TopNodeBorder = "#232323".HexToColor(),
                TopNodeFore = "#ffffff".HexToColor(),
                ClildNodeBack = "#ffffff".HexToColor(),
                ChildNodeIcon = "#bebebe".HexToColor(),
                ChildNodeFore = "#333333".HexToColor(),
                ChildNodeBorder = "#dddddd".HexToColor(),
            }
        };

        [Description("背景颜色"), Category("JwNavbar.Theme.Normal")]
        public Color Backgound
        {
            get => CustomTheme.Normal.Back;
            set { 
                CustomTheme.Normal.Back = value;
                if (Theme == ThemeType.None)
                {
                    this.BackColor = ThemePrivate.Normal.TopNodeBack;
                    Refresh();
                }
            }
        }
        [Description("主结点背景颜色"), Category("JwNavbar.Theme.Normal")]
        public Color TopNodeBackgound
        {
            get => CustomTheme.Normal.TopNodeBack;
            set { CustomTheme.Normal.TopNodeBack = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("主结点边框颜色"), Category("JwNavbar.Theme.Normal")]
        public Color TopNodeBorderColor
        {
            get => CustomTheme.Normal.TopNodeBorder;
            set { CustomTheme.Normal.TopNodeBorder = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("主结点文字及图标颜色"), Category("JwNavbar.Theme.Normal")]
        public Color TopNodeForeColor
        {
            get => CustomTheme.Normal.TopNodeFore;
            set { CustomTheme.Normal.TopNodeFore = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("子结点背景颜色"), Category("JwNavbar.Theme.Normal")]
        public Color ClildNodeBackgound
        {
            get => CustomTheme.Normal.ClildNodeBack;
            set { CustomTheme.Normal.ClildNodeBack = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("子结点文字颜色"), Category("JwNavbar.Theme.Normal")]
        public Color ChildNodeForeColor
        {
            get => CustomTheme.Normal.ChildNodeFore;
            set { CustomTheme.Normal.ChildNodeFore = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("子结点边框颜色"), Category("JwNavbar.Theme.Normal")]
        public Color ChildNodeBorderColor
        {
            get => CustomTheme.Normal.ChildNodeBorder;
            set { CustomTheme.Normal.ChildNodeBorder = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("子结点图标颜色"), Category("JwNavbar.Theme.Normal")]
        public Color ChildNodeIconColor
        {
            get => CustomTheme.Normal.ChildNodeIcon;
            set { CustomTheme.Normal.ChildNodeIcon = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]背景颜色"), Category("JwNavbar.Theme.Active")]
        public Color BackgoundActive
        {
            get => CustomTheme.Active.Back;
            set { CustomTheme.Active.Back = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]主结点背景颜色"), Category("JwNavbar.Theme.Active")]
        public Color TopNodeBackgoundActive
        {
            get => CustomTheme.Active.TopNodeBack;
            set { CustomTheme.Active.TopNodeBack = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]主结点边框颜色"), Category("JwNavbar.Theme.Active")]
        public Color TopNodeBorderColorActive
        {
            get => CustomTheme.Active.TopNodeBorder;
            set { CustomTheme.Active.TopNodeBorder = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]主结点文字及图标颜色"), Category("JwNavbar.Theme.Active")]
        public Color TopNodeForeColorActive
        {
            get => CustomTheme.Active.TopNodeFore;
            set { CustomTheme.Active.TopNodeFore = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]子结点背景颜色"), Category("JwNavbar.Theme.Active")]
        public Color ClildNodeBackgoundActive
        {
            get => CustomTheme.Active.ClildNodeBack;
            set { CustomTheme.Active.ClildNodeBack = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]子结点文字颜色"), Category("JwNavbar.Theme.Active")]
        public Color ChildNodeForeColorActive
        {
            get => CustomTheme.Active.ChildNodeFore;
            set { CustomTheme.Active.ChildNodeFore = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]子结点边框颜色"), Category("JwNavbar.Theme.Active")]
        public Color ChildNodeBorderColorActive
        {
            get => CustomTheme.Active.ChildNodeBorder;
            set { CustomTheme.Active.ChildNodeBorder = value; if (Theme == ThemeType.None) Refresh(); }
        }
        [Description("[活动状态时]子结点图标颜色"), Category("JwNavbar.Theme.Active")]
        public Color ChildNodeIconColorActive
        {
            get => CustomTheme.Active.ChildNodeIcon;
            set { CustomTheme.Active.ChildNodeIcon = value; if (Theme == ThemeType.None) Refresh(); }
        }
        #endregion
        //private TreeNode _MouseNode = null;
        //protected override void OnMouseHover(EventArgs e)
        //{
        //    var node = this.GetNodeAt(PointToClient(Cursor.Position));
        //    if (node == null) return;
        //    if (_MouseNode == null || !_MouseNode.Equals(node))
        //    {
        //        if(_MouseNode != null)
        //        {
        //            //Invalidate(_MouseNode.RowBounds);
        //        }
        //        _MouseNode = node;
        //        Refresh();
        //    }
        //    base.OnMouseHover(e);
        //}
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (uint)WinApi.Messages.WM_ERASEBKGND) return;
            base.WndProc(ref m);
        }
        private string RealIconChild
        {
            get
            {
                if (!string.IsNullOrEmpty(IconChild) && JwIconfontManager.ContainsIcon(IconChild, IconFontName))
                    return JwIconfontManager.GetFontIcon(IconChild, IconFontName);
                else
                    return string.Empty;
            }
        }
        private string RealIconExpand
        {
            get
            {
                if (!string.IsNullOrEmpty(IconExpand) && JwIconfontManager.ContainsIcon(IconExpand, IconFontName))
                    return JwIconfontManager.GetFontIcon(IconExpand, IconFontName);
                else
                    return string.Empty;
            }
        }
        private string RealIconContract
        {
            get
            {
                if (!string.IsNullOrEmpty(IconContract) && JwIconfontManager.ContainsIcon(IconContract, IconFontName))
                    return JwIconfontManager.GetFontIcon(IconContract, IconFontName);
                else
                    return string.Empty;
            }
        }
        private void NavBarSizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }
        private void NavBarNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    if (e.Node.Nodes.Count > 0)
                    {
                        if (e.Node.IsExpanded)
                        {
                            e.Node.Collapse();
                        }
                        else
                        {
                            e.Node.Expand();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InitIconGraphics(Graphics g)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }
        private void NavBarDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics;
            //InitIconGraphics(g);
            if(e.Node.Level == 0)
            {
                NavBarDrawTopNode(e);
            }
            else
            {
                NavBarDrawChildNode(e);
            }
        }
        private bool IsVerticalScrollBarVisible()
        {
            return base.IsHandleCreated && (WinApi.GetWindowLong(base.Handle, WinApi.GWL_STYLE) & (uint)WinApi.WinStyle.WS_VSCROLL) != 0;
        }
        private bool IsMouseIn(TreeNode node)
        {
            var hit = this.HitTest(PointToClient(MousePosition));
            if (hit.Node == null) return false;
            return hit.Node.Equals(node);
        }
        public JwNavbarThemeStatus ThemePrivate
        {
            get
            {
                if(Theme!= ThemeType.None)
                {
                    return JwTheme.JwNavbarThemeDict[Theme];
                }
                else
                {
                    return CustomTheme;
                }
            }
        }
        private void NavBarDrawTopNode(DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics.HighQuality();

            var theme = ThemePrivate.Normal;
            if (e.State == TreeNodeStates.Selected || e.State == TreeNodeStates.Focused || IsMouseIn(e.Node))
            {
                theme = ThemePrivate.Active;
            }

            var scrollbar = IsVerticalScrollBarVisible() ? 20 : 0;
            var nodeRect = new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height));
            var drawRect = new Rectangle(
                nodeRect.X + NodePadding.Left,
                nodeRect.Y + NodePadding.Top,
                nodeRect.Width - NodePadding.Left - NodePadding.Right - scrollbar,
                nodeRect.Height - NodePadding.Top - NodePadding.Bottom);
            // 背景
            using(var bgBrush = new SolidBrush(theme.TopNodeBack))
            {
                g.FillRectangle(bgBrush, nodeRect);
            }
            // 图标
            if (e.Node.Nodes.Count > 0)
            {
                using (var font = JwIconfontManager.GetIconFont(IconFontName, IconTopSize))
                {
                    var iconSize = g.MeasureString(e.Node.IsExpanded ? RealIconContract : RealIconExpand, font, int.MaxValue, StringFormat.GenericTypographic);
                    var iconRect = new RectangleF(new PointF(drawRect.X + drawRect.Width - iconSize.Width, drawRect.Y + (drawRect.Height - iconSize.Height) / 2f + AdjustTopIconTop), iconSize);
                    using (var brush = new SolidBrush(theme.TopNodeFore))
                    {
                        g.DrawString(RealIconExpand, font, brush, iconRect.Location, StringFormat.GenericTypographic);
                    }
                }
            }
            // 文字
            var textSize = g.MeasureString(e.Node.Text, Font, base.Width);
            var textRect = new RectangleF(new PointF(drawRect.X, drawRect.Y + (drawRect.Height - textSize.Height) / 2f), textSize);
            using (var foreBrush = new SolidBrush(theme.TopNodeFore))
            {
                g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                g.DrawString(e.Node.Text, Font, foreBrush, textRect, StringFormat.GenericTypographic);
            }
            // 边框
            if (!e.Node.IsExpanded)
            {
                using (var pen = new Pen(theme.TopNodeBorder, 1))
                {
                    g.DrawLine(pen, nodeRect.X, nodeRect.Y + nodeRect.Height, nodeRect.X + nodeRect.Width, nodeRect.Y + nodeRect.Height);
                }
            }
            
        }
        private void NavBarDrawChildNode(DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics.HighQuality();

            var theme = ThemePrivate.Normal;
            if(e.State == TreeNodeStates.Selected || e.State == TreeNodeStates.Focused || IsMouseIn(e.Node))
            {
                theme = ThemePrivate.Active;
            }

            var scrollbar = IsVerticalScrollBarVisible() ? 20 : 0;
            var nodeRect = new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height));
            var drawRect = new Rectangle(
                nodeRect.X + NodePadding.Left,
                nodeRect.Y + NodePadding.Top,
                nodeRect.Width - NodePadding.Left - NodePadding.Right - scrollbar,
                nodeRect.Height - NodePadding.Top - NodePadding.Bottom);
            // 背景
            using (var bgBrush = new SolidBrush(theme.ClildNodeBack))
            {
                g.FillRectangle(bgBrush, nodeRect);
            }
            // 图标
            using (var font = JwIconfontManager.GetIconFont(IconFontName, IconChildSize))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                var iconSize = g.MeasureString(RealIconChild, font, int.MaxValue, StringFormat.GenericTypographic);
                var iconRect = new RectangleF(new PointF(drawRect.X, drawRect.Y + (drawRect.Height - iconSize.Height) / 2f + AdjustChildIconTop), iconSize);
                using (var foreBrush = new SolidBrush(theme.ChildNodeIcon))
                {
                    g.DrawString(RealIconChild, font, foreBrush, iconRect, StringFormat.GenericTypographic);
                }
                // 文字
                var textSize = g.MeasureString(e.Node.Text, Font, base.Width);
                var textRect = new RectangleF(new PointF(drawRect.X + iconSize.Width + 10, drawRect.Y + (drawRect.Height - textSize.Height) / 2f), textSize);
                using (var foreBrush = new SolidBrush(theme.ChildNodeFore))
                {
                    g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    g.DrawString(e.Node.Text, Font, foreBrush, textRect, StringFormat.GenericTypographic);
                }
            }
            // 边框
            using (var pen = new Pen(theme.ChildNodeBorder, 1))
            {
                g.DrawLine(pen, nodeRect.X, nodeRect.Y + nodeRect.Height, nodeRect.X + nodeRect.Width, nodeRect.Y + nodeRect.Height);
                g.DrawLine(pen, nodeRect.X + nodeRect.Width, nodeRect.Y, nodeRect.X + nodeRect.Width, nodeRect.Y + nodeRect.Height);
            }
        }
    }
}
