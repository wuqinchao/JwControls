using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class JwNavbarNode : TreeNode
    {
        private string _Key = string.Empty;

        public string Key { get => _Key; set => _Key = value; }
        public JwNavbarNode():base()
        { }
        public JwNavbarNode(string text) : base(text)
        {
        }
        public JwNavbarNode(string text, string key) : base(text)
        {
            _Key = key;
        }
        public JwNavbarNode(string text, string key, TreeNode[] children) : base(text, children)
        {
            _Key = key;
        }
    }

    public static class JwNavbarNodeExtensions
    {
        public static bool MoveUp(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                    return true;
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                    return true;
                }
            }
            return false;
        }

        public static bool MoveDown(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                    return true;
                }
            }
            else if (view != null && view.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index < view.Nodes.Count - 1)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                    return true;
                }
            }
            return false;
        }
    }
}
