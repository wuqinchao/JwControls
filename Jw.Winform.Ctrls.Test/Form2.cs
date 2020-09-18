using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls.Test
{
    public partial class Form2 : Form
    {
        //private JwNotifyForm fnotify = null;
        public Form2()
        {
            InitializeComponent();
        }

        private void jwButton1_Click(object sender, EventArgs e)
        {
            TreeNodeEditor f = new TreeNodeEditor(null);
            f.ShowDialog();
        }

        private void jwNavbar2_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void jwNavbar1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        private string[] tt = new string[] {
            "Bacon ipsum dolor sit amet salami venison chicken flank fatback doner",
            "11-7-2014	Approved"
        };
        int ii = 0;
        private void jwIcon1_Click(object sender, EventArgs e)
        {
            //JwNotify.Error("jwIcon1_Click", true, 3000, true);
        }

        private void jwButton1_Click_1(object sender, EventArgs e)
        {
            //JwNotify.Succ("jwIcon1_Click", true, 3000, true);
        }

        private void jwNavbar1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            MessageBox.Show(e.Node.ToString());
        }
    }
}
