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
        public Form1()
        {
            InitializeComponent();
        }

        private void jwIcon1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("OK");
        }

        private void jwIcon5_Click(object sender, EventArgs e)
        {
            var f = new JwIcons.IconSelection();
            if(f.ShowDialog() == DialogResult.OK)
            {
                Clipboard.SetText(f.SelectKey);
                MessageBox.Show(f.SelectKey);
            }
        }

        private void jwIcon3_Click(object sender, EventArgs e)
        {
            JwDialog.AlertInfo("是不是发生的什么错误");
            JwDialog.AlertSucc("是不是发生的什么错误");
            JwDialog.AlertWarning("是不是发生的什么错误");
            JwDialog.AlertError("是不是发生的什么错误");
        }

        private void jwIcon10_Click(object sender, EventArgs e)
        {
            JwDialog.Confirm("??????????????????");
        }
    }
}
