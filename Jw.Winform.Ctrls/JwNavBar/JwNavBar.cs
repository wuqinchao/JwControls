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

namespace Jw.Winform.Ctrls
{
    //[Designer("Jw.Winform.Ctrls.JwNavBarDesigner")]
    public partial class JwNavBar : TreeView
    {
        private int _NodeHeight = 35;
        private ThemeType _TopNodeTheme = ThemeType.dark;
        private ThemeType _ChildNodeTheme = ThemeType.light;
        private Padding _NodePadding = new Padding(10, 0, 5, 0);
        private int _AdjustIconTop = 0;
        private int _IconTopSize = 18;
        private int _IconChildSize = 18;
        private string _IconExpand = "arrow3-down";
        private string _IconContract = "arrow3-up";
        private string _IconChild = "arrow6-right";
        [Description("结点高度"), Category("JwNavBar")]
        public int NodeHeight
        {
            get => _NodeHeight;
            set
            {
                base.ItemHeight = _NodeHeight = value;
            }
        }
        [Description("主结点配色方案"), Category("JwNavBar")]
        public ThemeType TopNodeTheme { get => _TopNodeTheme; set => _TopNodeTheme = value; }
        [Description("子结点配色方案"), Category("JwNavBar")]
        public ThemeType ChildNodeTheme { get => _ChildNodeTheme; set => _ChildNodeTheme = value; }
        [Description("结点Padding"), Category("JwNavBar")]
        public Padding NodePadding 
        { 
            get => _NodePadding; 
            set => _NodePadding = value;
        }
        [Description("图标大小"), Category("JwIcon")]
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
        [Description("图标大小"), Category("JwIcon")]
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
        [Description("图标与上边距离微调"), Category("JwIcon")]
        public int AdjustIconTop
        {
            get => _AdjustIconTop;
            set
            {
                _AdjustIconTop = value;
                Refresh();
            }
        }

        [Description("展开时的图标"), Category("JwIcon")]
        public string IconExpand 
        { 
            get => _IconExpand;
            set
            {
                _IconExpand = value;
                Refresh();
            }
        }

        [Description("折叠时的图标"), Category("JwIcon")]
        public string IconContract 
        { 
            get => _IconContract;
            set
            {
                _IconContract = value;
                Refresh();
            }
        }
        [Description("子结点图标"), Category("JwIcon")]
        public string IconChild
        { 
            get => _IconChild;
            set
            {
                _IconChild = value;
                Refresh();
            }
        }

        public JwNavBar()
        {
            base.HideSelection = false;
            base.ItemHeight = NodeHeight;
            base.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            base.DrawNode += new DrawTreeNodeEventHandler(NavBarDrawNode);
            base.NodeMouseClick += new TreeNodeMouseClickEventHandler(NavBarNodeMouseClick);
            base.SizeChanged += new EventHandler(this.NavBarSizeChanged);
            //base.AfterSelect += new TreeViewEventHandler(this.TreeViewEx_AfterSelect);
            base.FullRowSelect = true;
            base.ShowLines = false;
            base.ShowPlusMinus = false;
            base.ShowRootLines = false;
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.None;
            DoubleBuffered = true;
        }
        private string RealIconChild
        {
            get
            {
                if (!string.IsNullOrEmpty(IconChild) && Iconfont.TypeDict.ContainsKey(IconChild))
                    return Iconfont.TypeDict[IconChild];
                else
                    return string.Empty;
            }
        }
        private string RealIconExpand
        {
            get
            {
                if (!string.IsNullOrEmpty(IconExpand) && Iconfont.TypeDict.ContainsKey(IconExpand))
                    return Iconfont.TypeDict[IconExpand];
                else
                    return string.Empty;
            }
        }
        private string RealIconContract
        {
            get
            {
                if (!string.IsNullOrEmpty(IconContract) && Iconfont.TypeDict.ContainsKey(IconContract))
                    return Iconfont.TypeDict[IconContract];
                else
                    return string.Empty;
            }
        }
        private Font GetIconFont(int insize)
        {
            var size = insize * (3f / 4f);
            var font = new Font(Iconfont.Family, insize, FontStyle.Regular, GraphicsUnit.Point);
            return font;
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
                    //if (base.SelectedNode != null)
                    //{
                    //    if (base.SelectedNode == e.Node && e.Node.IsExpanded)
                    //    {
                    //        if (!this._parentNodeCanSelect)
                    //        {
                    //            if (e.Node.Nodes.Count > 0)
                    //            {
                    //                base.SelectedNode = e.Node.Nodes[0];
                    //            }
                    //        }
                    //    }
                    //}
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
        private const int GWL_STYLE = -16;
        private const int WS_VSCROLL = 0x00200000;
        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);
        private bool IsVerticalScrollBarVisible()
        {
            return base.IsHandleCreated && (GetWindowLong(base.Handle, GWL_STYLE) & WS_VSCROLL) != 0;
        }
        private void NavBarDrawTopNode(DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            var scrollbar = IsVerticalScrollBarVisible() ? 20 : 0;
            var nodeRect = new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height));
            var drawRect = new Rectangle(
                nodeRect.X + NodePadding.Left,
                nodeRect.Y + NodePadding.Top,
                nodeRect.Width - NodePadding.Left - NodePadding.Right - scrollbar,
                nodeRect.Height - NodePadding.Top - NodePadding.Bottom);
            // 背景
            using(var bgBrush = new SolidBrush(JwTheme.ThemeDict[TopNodeTheme].BackColor))
            {
                g.FillRectangle(bgBrush, nodeRect);
            }
            // 图标
            using (var font = GetIconFont(IconTopSize))
            {
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                var iconSize = g.MeasureString(RealIconExpand, font, int.MaxValue, StringFormat.GenericTypographic);
                var iconRect = new RectangleF(new PointF(drawRect.X + drawRect.Width - iconSize.Width, drawRect.Y + (drawRect.Height - iconSize.Height) / 2f), iconSize);
                using (var brush = new SolidBrush(JwTheme.ThemeDict[TopNodeTheme].ForeColor))
                {
                    g.DrawString(RealIconExpand, font, brush, iconRect, StringFormat.GenericTypographic);
                }
            }
            // 文字
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            var textSize = g.MeasureString(e.Node.Text, Font, base.Width);
            var textRect = new RectangleF(new PointF(drawRect.X, drawRect.Y + (drawRect.Height - textSize.Height) / 2f), textSize);
            using (var foreBrush = new SolidBrush(JwTheme.ThemeDict[TopNodeTheme].ForeColor))
            {
                g.DrawString(e.Node.Text, Font, foreBrush, textRect);
            }
        }
        private void NavBarDrawChildNode(DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics;
            var scrollbar = IsVerticalScrollBarVisible() ? 20 : 0;
            var nodeRect = new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height));
            var drawRect = new Rectangle(
                nodeRect.X + NodePadding.Left,
                nodeRect.Y + NodePadding.Top,
                nodeRect.Width - NodePadding.Left - NodePadding.Right - scrollbar,
                nodeRect.Height - NodePadding.Top - NodePadding.Bottom);
            // 背景
            using (var bgBrush = new SolidBrush(JwTheme.ThemeDict[ChildNodeTheme].BackColor))
            {
                g.FillRectangle(bgBrush, nodeRect);
            }
            // 图标
            using (var font = GetIconFont(IconChildSize))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                var iconSize = g.MeasureString(RealIconChild, font, int.MaxValue);
                var iconRect = new RectangleF(new PointF(drawRect.X, drawRect.Y + (drawRect.Height - iconSize.Height) / 2f), iconSize);
                
                // 文字
                //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                var textSize = g.MeasureString(e.Node.Text, Font, base.Width);
                var textRect = new RectangleF(new PointF(drawRect.X + iconSize.Width + 10, drawRect.Y + (drawRect.Height - textSize.Height) / 2f), textSize);
                using (var foreBrush = new SolidBrush(JwTheme.ThemeDict[ChildNodeTheme].ForeColor))
                {
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                    g.DrawString(RealIconChild, font, foreBrush, iconRect);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                    g.DrawString(e.Node.Text,   Font, foreBrush, textRect, StringFormat.GenericTypographic);
                }
            }
        }

    }
}
