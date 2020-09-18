using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace Jw.Winform.Ctrls
{
    public class JwNavbarDesigner : ControlDesigner
    {
        private readonly DesignerVerbCollection designerVerbs = new DesignerVerbCollection();

        private IDesignerHost designerHost;

        private ISelectionService selectionService;
        public override SelectionRules SelectionRules
        {
            get
            {
                return base.SelectionRules;
            }
        }
        public IDesignerHost DesignerHost
        {
            get
            {
                return designerHost ?? (designerHost = (IDesignerHost)(GetService(typeof(IDesignerHost))));
            }
        }
        public ISelectionService SelectionService
        {
            get
            {
                return selectionService ?? (selectionService = (ISelectionService)(GetService(typeof(ISelectionService))));
            }
        }
        public override DesignerVerbCollection Verbs
        {
            get
            {
                return designerVerbs;
            }
        }
        public JwNavbarDesigner()
        {
            var verb1 = new DesignerVerb("编辑节点", OnEditNodes);
            designerVerbs.AddRange(new[] { verb1 });
        }
        //protected override bool GetHitTest(Point pt)
        //{
        //    TreeView tree = this.Control as TreeView;
        //    if(tree.ClientRectangle.Contains(tree.PointToClient(pt)))
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        //protected override void WndProc(ref Message m)
        //{
        //    if (m.Msg == (int)WinApi.Messages.WM_VSCROLL)
        //    {
        //        JwDialog.AlertInfo($"MESSAGe;{m.Msg} {m.WParam} {m.LParam}");
        //    }
        //    base.WndProc(ref m);
        //}
        public override GlyphCollection GetGlyphs(GlyphSelectionType selectionType)
        {
            GlyphCollection glyphs = base.GetGlyphs(selectionType);
            if (SelectionService != null)
            {
                if (SelectionService.PrimarySelection == this.Control)
                    glyphs.Add(new EventGlyph(this.BehaviorService, this.Control));
            }
            return glyphs;
        }
        private void OnEditNodes(Object sender, EventArgs e)
        {
            TreeView tree = this.Control as TreeView;
            using(TreeNodeEditor editor = new TreeNodeEditor(tree.Nodes))
            {
                if(editor.ShowDialog() == DialogResult.OK)
                {
                    tree.Nodes.Clear();
                    foreach (TreeNode node in editor.Nodes)
                    {
                        tree.Nodes.Add(node.Clone() as TreeNode);
                        RaiseComponentChanged(TypeDescriptor.GetProperties(tree)["Nodes"],
                                  null, tree.Nodes);
                    }
                }
            }
        }
    }
    public class EventGlyph : Glyph
    {
        Control control;
        BehaviorService behaviorSvc;

        public EventGlyph(BehaviorService behaviorSvc, Control control)
            : base(new EventBehavior(control))
        {
            this.behaviorSvc = behaviorSvc;
            this.control = control;
        }

        public override Rectangle Bounds
        {
            get
            {
                Point edge = behaviorSvc.ControlToAdornerWindow(control);
                Size size = control.Size;
                Rectangle bounds = new Rectangle(edge.X, edge.Y, size.Width, size.Height);

                return bounds;
            }
        }

        public override Cursor GetHitTest(System.Drawing.Point p)
        {
            var tree = (TreeView)control;
            var test = tree.HitTest(tree.PointToClient(Cursor.Position));
            return (test.Node == null) ? null : Cursors.Hand;
        }
        public override void Paint(PaintEventArgs pe)
        {            
        }
    }

    public class EventBehavior : Behavior
    {
        bool isPressed = false;
        Control control;

        public EventBehavior(Control control)
        {
            this.control = control;
        }

        public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
        {
            isPressed = true;
            return true;
        }

        public override bool OnMouseUp(Glyph g, MouseButtons button)
        {
            if (isPressed)
                OnClick();

            isPressed = false;
            return true;
        }

        //public override bool OnMouseEnter(Glyph g)
        //{
        //    this.control.BackColor = Color.LightPink;
        //    return true;
        //}

        //public override bool OnMouseLeave(Glyph g)
        //{
        //    this.control.BackColor = Color.FromKnownColor(KnownColor.Control);
        //    return true;
        //}

        public void OnClick()
        {
            var tree = (TreeView)control;
            var test = tree.HitTest(tree.PointToClient(Cursor.Position));
            if (test.Node == null) return;
            if (!test.Node.IsExpanded && test.Node.Nodes.Count > 0)
                test.Node.Expand();
            else if (test.Node.IsExpanded)
                test.Node.Collapse();
        }
    }
}
