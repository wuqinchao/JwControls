using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public partial class TreeNodeEditor : Form
    {
        private int last = 1;
        public TreeNodeCollection Nodes
        {
            get { return tree.Nodes; }
        }
        public TreeNodeEditor(TreeNodeCollection Nodes)
        {
            InitializeComponent();
            tree.ItemDrag += Tree_ItemDrag;
            tree.DragEnter += Tree_DragEnter;
            tree.DragDrop += Tree_DragDrop;
            if (Nodes == null) return;
            foreach(TreeNode node in Nodes)
            {
                tree.Nodes.Add(node.Clone() as TreeNode);
            }
        }

        private void Tree_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the drop location.
            Point targetPoint = tree.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node that was dragged.
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Sanity check
            if (draggedNode == null)
            {
                return;
            }

            // Retrieve the node at the drop location.
            TreeNode targetNode = tree.GetNodeAt(targetPoint);

            // Did the user drop the node 
            if (targetNode == null)
            {
                draggedNode.Remove();
                tree.Nodes.Add(draggedNode);
                draggedNode.Expand();
            }
            else
            {
                TreeNode parentNode = targetNode;

                // Confirm that the node at the drop location is not 
                // the dragged node and that target node isn't null
                // (for example if you drag outside the control)
                if (!draggedNode.Equals(targetNode) && targetNode != null)
                {
                    bool canDrop = true;
                    while (canDrop && (parentNode != null))
                    {
                        canDrop = !Object.ReferenceEquals(draggedNode, parentNode);
                        parentNode = parentNode.Parent;
                    }

                    if (canDrop)
                    {
                        // Have to remove nodes before you can move them.
                        draggedNode.Remove();

                        // Is the user holding down shift?
                        if (e.KeyState == 4)
                        {
                            // Is the targets parent node null?
                            if (targetNode.Parent == null)
                            {
                                // The target node has no parent. That means 
                                // the target node is at the root level. We'll 
                                // insert the node at the root level below the 
                                // target node.
                                tree.Nodes.Insert(targetNode.Index + 1, draggedNode);
                            }
                            else
                            {
                                // The target node has a valid parent so we'll 
                                // drop the node into it's index.
                                targetNode.Parent.Nodes.Insert(targetNode.Index + 1, draggedNode);
                            }
                        }
                        else
                        {
                            targetNode.Nodes.Add(draggedNode);
                        }

                        targetNode.Expand();
                        tree.SelectedNode = draggedNode;
                    }
                }
            }
        }

        private void Tree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Tree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void BtnAddRoot_Click(object sender, EventArgs e)
        {
            tree.Nodes.Add(new TreeNode($"节点{last++}"));
        }

        private void BtnAddChild_Click(object sender, EventArgs e)
        {
            if(tree.SelectedNode!=null && tree.SelectedNode.Level == 0)
            {
                tree.SelectedNode.Nodes.Add(new TreeNode($"节点{last++}"));
                if(!tree.SelectedNode.IsExpanded)
                {
                    tree.SelectedNode.Expand();
                }
            }
        }

        private void BtnUp_Click(object sender, EventArgs e)
        {
            var node = tree.SelectedNode;
            if (tree.SelectedNode.MoveUp())
                tree.SelectedNode = node;
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            var node = tree.SelectedNode;
            if(tree.SelectedNode.MoveDown())
                tree.SelectedNode = node;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if(tree.SelectedNode != null)
            {
                tree.SelectedNode.Remove();
                if (tree.Nodes.Count == 0)
                {
                    property.SelectedObject = null;
                    BtnAddChild.Enabled = BtnUp.Enabled = BtnDown.Enabled = BtnDelete.Enabled = false;
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(tree.SelectedNode == null)
            {
                BtnAddChild.Enabled = BtnUp.Enabled = BtnDown.Enabled = BtnDelete.Enabled = false;
                return;
            }
            property.SelectedObject = tree.SelectedNode;
            if (tree.SelectedNode.Level == 0)
            {
                BtnAddChild.Enabled = true;
            }
            BtnUp.Enabled = BtnDown.Enabled = BtnDelete.Enabled = true;
        }
    }
}
