using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    //[Designer("Jw.Winform.Ctrls.JwNavBarDesigner")]
    public partial class JwNavBar : FlowLayoutPanel
    {
        private int _NodeHeight = 40;
        private ThemeType _TopNodeTheme = ThemeType.dark;
        private ThemeType _ChildNodeTheme = ThemeType.light;
        private List<JwNavBarNode> _Nodes;
        [Description("结点高度"), Category("JwNavBar")]
        public int NodeHeight
        {
            get => _NodeHeight;
            set
            {
                _NodeHeight = value;
            }
        }
        [Description("主结点配色方案"), Category("JwNavBar")]
        public ThemeType TopNodeTheme { get => _TopNodeTheme; set => _TopNodeTheme = value; }
        [Description("子结点配色方案"), Category("JwNavBar")]
        public ThemeType ChildNodeTheme { get => _ChildNodeTheme; set => _ChildNodeTheme = value; }

        public JwNavBar()
        {
            AutoScroll = true;
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
        private void NavBarDrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            if(e.Node.Level == 0)
            {
                NavBarDrawTopNode(e);
            }
            else
            {
                NavBarDrawChildNode(e);
            }
        }
        private void NavBarDrawTopNode(DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics;
            using(var bgBrush = new SolidBrush(JwTheme.ThemeDict[TopNodeTheme].BackColor))
            {
                var nodeRect = new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height));
                var textSize = g.MeasureString(e.Node.Text, Font, base.Width);
                var textRect = new RectangleF(new PointF(0f, e.Bounds.Y + (NodeHeight - textSize.Height) / 2f), textSize);
                g.FillRectangle(bgBrush, nodeRect);
                using (var foreBrush = new SolidBrush(JwTheme.ThemeDict[TopNodeTheme].ForeColor))
                {
                    g.DrawString(e.Node.Text, Font, foreBrush, textRect);
                }
            }
        }
        private void NavBarDrawChildNode(DrawTreeNodeEventArgs e)
        {
            var g = e.Graphics;
            using (var bgBrush = new SolidBrush(JwTheme.ThemeDict[ChildNodeTheme].BackColor))
            {
                var nodeRect = new Rectangle(new Point(0, e.Node.Bounds.Y), new Size(base.Width, e.Node.Bounds.Height));
                var textSize = g.MeasureString(e.Node.Text, Font, base.Width);
                var textRect = new RectangleF(new PointF(0f, e.Bounds.Y + (NodeHeight - textSize.Height) / 2f), textSize);
                g.FillRectangle(bgBrush, nodeRect);
                using (var foreBrush = new SolidBrush(JwTheme.ThemeDict[ChildNodeTheme].ForeColor))
                {
                    g.DrawString(e.Node.Text, Font, foreBrush, textRect);
                }
            }
        }

    }
}
