using Jw.Winform.Forms;
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
    public partial class Form1 : Form
    {
        //JwMdiTabbed mdi = null;
        public Form1()
        {
            InitializeComponent();
            //mdi = new JwMdiTabbed();
            //mdi.MdiParent = this;
        }

        private void jwIcon1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
        }

        private void jwIcon3_Click(object sender, EventArgs e)
        {
            JwAlert.Info("是不是发生的什么错误");
            JwAlert.Succ("是不是发生的什么错误");
            JwAlert.Warning("是不是发生的什么错误");
            JwAlert.Error("是不是发生的什么错误");
        }

        private void jwIcon10_Click(object sender, EventArgs e)
        {
            JwAlert.Confirm("??????????????????");
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form f = new Form2();
            f.MdiParent = this;
            f.Show();
        }
    }
}
