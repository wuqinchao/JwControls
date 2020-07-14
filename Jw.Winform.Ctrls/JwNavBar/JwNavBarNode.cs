using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls
{
    public class JwNavBarNode : TreeNode
    {
        private string _Icon;
        public string Icon
        {
            get => _Icon;
            set
            {
                _Icon = value;
                TreeView.Refresh();
            }
        }

        public bool MouseIn()
        {
            var test = this.TreeView.HitTest(this.TreeView.PointToClient(TreeView.MousePosition));
            if (test.Node == null) return false;
            return this.Equals(test.Node);
        }
    }
}
