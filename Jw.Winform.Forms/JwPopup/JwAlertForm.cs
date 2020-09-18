using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Forms
{
    public partial class JwAlertForm : JwDialogBase
    {
        public string Message
        {
            get => TxtMessage.Text;
            set
            {
                using (var g = TxtMessage.CreateGraphics())
                {
                    var size = g.MeasureString(value, TxtMessage.Font, TxtMessage.ClientRectangle.Width, StringFormat.GenericTypographic);
                    var h = (int)Math.Ceiling(size.Height);
                    if (TxtMessage.ClientRectangle.Height < h)
                    {
                        Size = new Size(Size.Width, Size.Height + (h - TxtMessage.ClientRectangle.Height));
                    }
                }
                TxtMessage.Text = value;
            }
        }
        public JwAlertForm()
        {
            InitializeComponent();

            BtnOk.Theme = Theme;
        }

        protected override void ApplyTheme()
        {
            base.ApplyTheme();
            if (null == BtnOk) return;
            BtnOk.Theme = Theme;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
