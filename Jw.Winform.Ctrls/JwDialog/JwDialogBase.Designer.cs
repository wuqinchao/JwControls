namespace Jw.Winform.Ctrls
{
    partial class JwDialogBase
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
            this.PanHeadBox = new System.Windows.Forms.Panel();
            this.JwHead = new Jw.Winform.Ctrls.JwIcon();
            this.BtnClose = new Jw.Winform.Ctrls.JwIcon();
            this.PanHeadBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanHeadBox
            // 
            this.PanHeadBox.Controls.Add(this.JwHead);
            this.PanHeadBox.Controls.Add(this.BtnClose);
            this.PanHeadBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanHeadBox.Location = new System.Drawing.Point(0, 0);
            this.PanHeadBox.Name = "PanHeadBox";
            this.PanHeadBox.Size = new System.Drawing.Size(400, 40);
            this.PanHeadBox.TabIndex = 0;
            // 
            // JwHead
            // 
            this.JwHead.AdjustIconTop = 2;
            this.JwHead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.JwHead.AutoResize = false;
            this.JwHead.BackColor = System.Drawing.Color.Transparent;
            this.JwHead.BgActiveColor = System.Drawing.Color.Transparent;
            this.JwHead.BgColor = System.Drawing.Color.Transparent;
            this.JwHead.BgDisableColor = System.Drawing.Color.Transparent;
            this.JwHead.BorderActiveColor = System.Drawing.Color.Black;
            this.JwHead.BorderColor = System.Drawing.Color.Black;
            this.JwHead.BorderDisableColor = System.Drawing.Color.Black;
            this.JwHead.BorderWidth = 0;
            this.JwHead.CententCenter = false;
            this.JwHead.Enabled = false;
            this.JwHead.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.JwHead.ForeActiveColor = System.Drawing.Color.Black;
            this.JwHead.ForeDisableColor = System.Drawing.Color.Black;
            this.JwHead.IconActiveColor = System.Drawing.Color.Black;
            this.JwHead.IconColor = System.Drawing.Color.Black;
            this.JwHead.IconDisableColor = System.Drawing.Color.Black;
            this.JwHead.IconSize = 28;
            this.JwHead.IconText = "icon-info-fill";
            this.JwHead.Location = new System.Drawing.Point(3, 3);
            this.JwHead.Margin = new System.Windows.Forms.Padding(0);
            this.JwHead.Name = "JwHead";
            this.JwHead.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.JwHead.Radius = 3;
            this.JwHead.ShowFocused = true;
            this.JwHead.Size = new System.Drawing.Size(333, 33);
            this.JwHead.TabIndex = 2;
            this.JwHead.Text = "提示信息";
            this.JwHead.TextMarginLeft = 3;
            this.JwHead.Theme = Jw.Winform.Ctrls.ThemeType.none;
            // 
            // BtnClose
            // 
            this.BtnClose.AdjustIconTop = 0;
            this.BtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnClose.AutoResize = false;
            this.BtnClose.BackColor = System.Drawing.Color.Transparent;
            this.BtnClose.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnClose.BgColor = System.Drawing.Color.Transparent;
            this.BtnClose.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnClose.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnClose.BorderColor = System.Drawing.Color.Black;
            this.BtnClose.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnClose.BorderWidth = 0;
            this.BtnClose.CententCenter = true;
            this.BtnClose.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnClose.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnClose.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnClose.IconActiveColor = System.Drawing.Color.Black;
            this.BtnClose.IconColor = System.Drawing.Color.Black;
            this.BtnClose.IconDisableColor = System.Drawing.Color.Black;
            this.BtnClose.IconSize = 20;
            this.BtnClose.IconText = "form-close";
            this.BtnClose.Location = new System.Drawing.Point(373, 4);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Radius = 0;
            this.BtnClose.ShowFocused = false;
            this.BtnClose.Size = new System.Drawing.Size(21, 20);
            this.BtnClose.TabIndex = 1;
            this.BtnClose.TextMarginLeft = 4;
            this.BtnClose.Theme = Jw.Winform.Ctrls.ThemeType.none;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // JwDialogBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 260);
            this.ControlBox = false;
            this.Controls.Add(this.PanHeadBox);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "JwDialogBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JwDialogBase";
            this.PanHeadBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanHeadBox;
        private JwIcon BtnClose;
        protected JwIcon JwHead;
    }
}