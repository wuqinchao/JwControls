using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jw.Winform.Ctrls.JwIcons
{
    public partial class IconSelection : Form
    {
        private string FontKey;
        public IconSelection(string font)
        {
            FontKey = font;
            InitializeComponent();
        }

        private void IconSelection_Load(object sender, EventArgs e)
        {
            var dict = JwIconfontManager.GetFontInfo(FontKey).IconDict;
            foreach (var key in dict.Keys.OrderBy(x=>x))
            {
                var icon = new JwIcon()
                {
                    AdjustIconTop = 0,
                    AutoResize = false,
                    BackColor = System.Drawing.Color.Transparent,
                    BgActiveColor = System.Drawing.Color.Transparent,
                    BgColor = System.Drawing.Color.Transparent,
                    BgDisableColor = System.Drawing.Color.Transparent,
                    BorderActiveColor = System.Drawing.Color.Black,
                    BorderColor = System.Drawing.Color.Transparent,
                    BorderDisableColor = System.Drawing.Color.Transparent,
                    BorderWidth = 1,
                    CententCenter = true,
                    Font = new System.Drawing.Font("微软雅黑", 9F),
                    ForeActiveColor = System.Drawing.Color.Black,
                    ForeDisableColor = System.Drawing.Color.Black,
                    IconActiveColor = System.Drawing.Color.Black,
                    IconColor = System.Drawing.Color.Black,
                    IconDisableColor = System.Drawing.Color.Black,
                    IconSize = 20,
                    IconText = key,
                    Location = new System.Drawing.Point(3, 3),
                    Name = "jwIcon1",
                    Radius = 3,
                    Size = new System.Drawing.Size(28, 28),
                    TabIndex = 0,
                    TextMarginLeft = 4,
                    Theme = Jw.Winform.Ctrls.ThemeType.None,
                };

                icon.Click += Icon_Click;

                flowLayoutPanel1.Controls.Add(icon);
            }
        }

        private string _SelectKey;
        public string SelectKey
        {
            get => _SelectKey;
        }

        private void Icon_Click(object sender, EventArgs e)
        {
            _SelectKey = ((JwIcon)sender).IconText;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
