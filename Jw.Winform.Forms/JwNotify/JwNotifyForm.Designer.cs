using Jw.Winform.Ctrls;

namespace Jw.Winform.Forms
{
    partial class JwNotifyForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JwNotifyForm));
            this.TxtMessage = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.jwSilderBar1 = new Jw.Winform.Ctrls.JwSilderBar();
            this.BtnClose = new Jw.Winform.Ctrls.JwIcon();
            this.JwIIcon = new Jw.Winform.Ctrls.JwIcon();
            this.SuspendLayout();
            // 
            // TxtMessage
            // 
            this.TxtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtMessage.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.TxtMessage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TxtMessage.ForeColor = System.Drawing.Color.White;
            this.TxtMessage.Location = new System.Drawing.Point(49, 21);
            this.TxtMessage.Name = "TxtMessage";
            this.TxtMessage.Size = new System.Drawing.Size(250, 25);
            this.TxtMessage.TabIndex = 2;
            this.TxtMessage.Text = "------";
            this.TxtMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TxtMessage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JwNotifyForm_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // jwSilderBar1
            // 
            this.jwSilderBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jwSilderBar1.CustomTheme = ((Jw.Winform.Ctrls.JwSilderBarThemeStatus)(resources.GetObject("jwSilderBar1.CustomTheme")));
            this.jwSilderBar1.DecimalPlaces = 0;
            this.jwSilderBar1.LeftActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.LeftColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.LeftDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.LineActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.jwSilderBar1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.jwSilderBar1.LineDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.jwSilderBar1.LineHight = 5F;
            this.jwSilderBar1.Location = new System.Drawing.Point(-1, 58);
            this.jwSilderBar1.Margin = new System.Windows.Forms.Padding(0);
            this.jwSilderBar1.MaxValue = 100F;
            this.jwSilderBar1.MinValue = 0F;
            this.jwSilderBar1.Name = "jwSilderBar1";
            this.jwSilderBar1.Radius = 0;
            this.jwSilderBar1.ReadOnly = true;
            this.jwSilderBar1.Size = new System.Drawing.Size(310, 2);
            this.jwSilderBar1.TabIndex = 4;
            this.jwSilderBar1.Text = "jwSilderBar1";
            this.jwSilderBar1.TextLeftMargin = 0;
            this.jwSilderBar1.TextVisable = false;
            this.jwSilderBar1.Theme = Jw.Winform.Ctrls.ThemeType.Primary;
            this.jwSilderBar1.ThumbActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.ThumbDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.ThumbOutActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.ThumbOutColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.ThumbOutDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.jwSilderBar1.ThumbOutWidth = 0F;
            this.jwSilderBar1.ThumbVisble = false;
            this.jwSilderBar1.ThumbWidth = 0F;
            this.jwSilderBar1.Value = 50F;
            this.jwSilderBar1.ValueFormat = "";
            this.jwSilderBar1.ValueType = Jw.Winform.Ctrls.JwSilderBarValueType.Percent;
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
            this.BtnClose.ForeActiveColor = System.Drawing.Color.White;
            this.BtnClose.ForeColor = System.Drawing.Color.White;
            this.BtnClose.ForeDisableColor = System.Drawing.Color.White;
            this.BtnClose.IconActiveColor = System.Drawing.Color.White;
            this.BtnClose.IconColor = System.Drawing.Color.Black;
            this.BtnClose.IconDisableColor = System.Drawing.Color.White;
            this.BtnClose.IconFontName = "Iconfont";
            this.BtnClose.IconSize = 16;
            this.BtnClose.IconText = "form-close";
            this.BtnClose.Location = new System.Drawing.Point(286, 1);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Radius = 0;
            this.BtnClose.ShowFocused = false;
            this.BtnClose.Size = new System.Drawing.Size(20, 20);
            this.BtnClose.TabIndex = 3;
            this.BtnClose.TabStop = false;
            this.BtnClose.TextMarginLeft = 0;
            this.BtnClose.Theme = Jw.Winform.Ctrls.ThemeType.None;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // JwIIcon
            // 
            this.JwIIcon.AdjustIconTop = 0;
            this.JwIIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.JwIIcon.AutoResize = false;
            this.JwIIcon.BackColor = System.Drawing.Color.Transparent;
            this.JwIIcon.BgActiveColor = System.Drawing.Color.Transparent;
            this.JwIIcon.BgColor = System.Drawing.Color.Transparent;
            this.JwIIcon.BgDisableColor = System.Drawing.Color.Transparent;
            this.JwIIcon.BorderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(204)))));
            this.JwIIcon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.JwIIcon.BorderDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.JwIIcon.BorderWidth = 0;
            this.JwIIcon.CententCenter = true;
            this.JwIIcon.Enabled = false;
            this.JwIIcon.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.JwIIcon.ForeActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.JwIIcon.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.JwIIcon.IconActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.JwIIcon.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.JwIIcon.IconDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.JwIIcon.IconFontName = "Iconfont";
            this.JwIIcon.IconSize = 24;
            this.JwIIcon.IconText = "icon-warning-fill";
            this.JwIIcon.Location = new System.Drawing.Point(11, 18);
            this.JwIIcon.Name = "JwIIcon";
            this.JwIIcon.Radius = 3;
            this.JwIIcon.ShowFocused = true;
            this.JwIIcon.Size = new System.Drawing.Size(32, 29);
            this.JwIIcon.TabIndex = 0;
            this.JwIIcon.TextMarginLeft = 4;
            this.JwIIcon.Theme = Jw.Winform.Ctrls.ThemeType.None;
            // 
            // JwNotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(307, 60);
            this.ControlBox = false;
            this.Controls.Add(this.jwSilderBar1);
            this.Controls.Add(this.BtnClose);
            this.Controls.Add(this.TxtMessage);
            this.Controls.Add(this.JwIIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "JwNotifyForm";
            this.ShowInTaskbar = false;
            this.Text = "JwNotify";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.JwNotify_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.JwNotifyForm_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private JwIcon JwIIcon;
        private System.Windows.Forms.Label TxtMessage;
        private JwIcon BtnClose;
        private System.Windows.Forms.Timer timer1;
        private JwSilderBar jwSilderBar1;
    }
}