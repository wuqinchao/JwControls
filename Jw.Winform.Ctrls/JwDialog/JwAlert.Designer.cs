namespace Jw.Winform.Ctrls
{
    partial class JwAlert
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BtnOk = new Jw.Winform.Ctrls.JwIcon();
            this.TxtMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // JwHead
            // 
            this.JwHead.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.JwHead.Size = new System.Drawing.Size(269, 33);
            this.JwHead.Text = "提示";
            // 
            // BtnOk
            // 
            this.BtnOk.AdjustIconTop = 0;
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnOk.AutoResize = false;
            this.BtnOk.BackColor = System.Drawing.Color.Transparent;
            this.BtnOk.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnOk.BgColor = System.Drawing.Color.Transparent;
            this.BtnOk.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnOk.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnOk.BorderColor = System.Drawing.Color.Black;
            this.BtnOk.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnOk.BorderWidth = 1;
            this.BtnOk.CententCenter = true;
            this.BtnOk.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnOk.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnOk.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnOk.IconActiveColor = System.Drawing.Color.Black;
            this.BtnOk.IconColor = System.Drawing.Color.Black;
            this.BtnOk.IconDisableColor = System.Drawing.Color.Black;
            this.BtnOk.IconSize = 20;
            this.BtnOk.IconText = "";
            this.BtnOk.Location = new System.Drawing.Point(152, 145);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Radius = 4;
            this.BtnOk.ShowFocused = true;
            this.BtnOk.Size = new System.Drawing.Size(80, 32);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "确定";
            this.BtnOk.TextMarginLeft = 4;
            this.BtnOk.Theme = Jw.Winform.Ctrls.ThemeType.primary;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // TxtMessage
            // 
            this.TxtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMessage.Location = new System.Drawing.Point(32, 70);
            this.TxtMessage.Name = "TxtMessage";
            this.TxtMessage.Size = new System.Drawing.Size(320, 50);
            this.TxtMessage.TabIndex = 2;
            this.TxtMessage.Text = "-----------";
            this.TxtMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JwAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderWidth = 1;
            this.ClientSize = new System.Drawing.Size(384, 189);
            this.Controls.Add(this.TxtMessage);
            this.Controls.Add(this.BtnOk);
            this.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.Name = "JwAlert";
            this.Radius = 1;
            this.Text = "JwAlert";
            this.Title = "提示";
            this.Controls.SetChildIndex(this.BtnOk, 0);
            this.Controls.SetChildIndex(this.TxtMessage, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private JwIcon BtnOk;
        private System.Windows.Forms.Label TxtMessage;
    }
}