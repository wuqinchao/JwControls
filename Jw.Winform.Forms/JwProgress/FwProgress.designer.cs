using Jw.Winform.Ctrls;

namespace Jw.Winform.Forms
{
    partial class FwProgress
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
            this.t_value = new System.Windows.Forms.ProgressBar();
            this.t_title = new System.Windows.Forms.Label();
            this.t_task = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.t_right = new System.Windows.Forms.Label();
            this.t_left = new System.Windows.Forms.Label();
            this.btn_cancel = new Jw.Winform.Ctrls.JwButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // t_value
            // 
            this.t_value.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.t_value.Location = new System.Drawing.Point(60, 94);
            this.t_value.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.t_value.Name = "t_value";
            this.t_value.Size = new System.Drawing.Size(373, 23);
            this.t_value.TabIndex = 0;
            // 
            // t_title
            // 
            this.t_title.AutoSize = true;
            this.t_title.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.t_title.ForeColor = System.Drawing.Color.White;
            this.t_title.Location = new System.Drawing.Point(8, 8);
            this.t_title.Name = "t_title";
            this.t_title.Size = new System.Drawing.Size(37, 20);
            this.t_title.TabIndex = 1;
            this.t_title.Text = "任务";
            // 
            // t_task
            // 
            this.t_task.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.t_task.ForeColor = System.Drawing.Color.White;
            this.t_task.Location = new System.Drawing.Point(58, 70);
            this.t_task.Name = "t_task";
            this.t_task.Size = new System.Drawing.Size(373, 20);
            this.t_task.TabIndex = 2;
            this.t_task.Text = "正在启动...";
            this.t_task.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.GrayText;
            this.panel1.Controls.Add(this.btn_cancel);
            this.panel1.Controls.Add(this.t_right);
            this.panel1.Controls.Add(this.t_left);
            this.panel1.Controls.Add(this.t_title);
            this.panel1.Controls.Add(this.t_task);
            this.panel1.Controls.Add(this.t_value);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 196);
            this.panel1.TabIndex = 3;
            // 
            // t_right
            // 
            this.t_right.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.t_right.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.t_right.ForeColor = System.Drawing.Color.White;
            this.t_right.Location = new System.Drawing.Point(244, 121);
            this.t_right.Name = "t_right";
            this.t_right.Size = new System.Drawing.Size(193, 17);
            this.t_right.TabIndex = 4;
            this.t_right.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // t_left
            // 
            this.t_left.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.t_left.ForeColor = System.Drawing.Color.White;
            this.t_left.Location = new System.Drawing.Point(58, 121);
            this.t_left.Name = "t_left";
            this.t_left.Size = new System.Drawing.Size(187, 17);
            this.t_left.TabIndex = 3;
            // 
            // btn_cancel
            // 
            this.btn_cancel.AdjustIconTop = 0;
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.AutoResize = false;
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.BgActiveColor = System.Drawing.Color.Transparent;
            this.btn_cancel.BgColor = System.Drawing.Color.Transparent;
            this.btn_cancel.BgDisableColor = System.Drawing.Color.Transparent;
            this.btn_cancel.BorderActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(204)))));
            this.btn_cancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btn_cancel.BorderDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btn_cancel.BorderWidth = 0;
            this.btn_cancel.CententCenter = true;
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_cancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.btn_cancel.ForeActiveColor = System.Drawing.Color.Red;
            this.btn_cancel.ForeDisableColor = System.Drawing.Color.Silver;
            this.btn_cancel.IconActiveColor = System.Drawing.Color.Red;
            this.btn_cancel.IconColor = System.Drawing.Color.Red;
            this.btn_cancel.IconDisableColor = System.Drawing.Color.Silver;
            this.btn_cancel.IconFontName = "Iconfont";
            this.btn_cancel.IconSize = 18;
            this.btn_cancel.IconText = "form-close";
            this.btn_cancel.Location = new System.Drawing.Point(440, 94);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Radius = 0;
            this.btn_cancel.ShowFocused = true;
            this.btn_cancel.Size = new System.Drawing.Size(24, 24);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.TextMarginLeft = 4;
            this.btn_cancel.Theme = Jw.Winform.Ctrls.ThemeType.None;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // FwProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FwProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FwProgress";
            this.Load += new System.EventHandler(this.FwProgress_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar t_value;
        private System.Windows.Forms.Label t_title;
        private System.Windows.Forms.Label t_task;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label t_right;
        private System.Windows.Forms.Label t_left;
        private JwButton btn_cancel;
    }
}