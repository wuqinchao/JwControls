namespace Jw.Winform.Ctrls
{
    partial class TreeNodeEditor
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
            this.property = new System.Windows.Forms.PropertyGrid();
            this.tree = new System.Windows.Forms.TreeView();
            this.BtnOk = new Jw.Winform.Ctrls.JwButton();
            this.BtnCancel = new Jw.Winform.Ctrls.JwButton();
            this.BtnAddRoot = new Jw.Winform.Ctrls.JwButton();
            this.BtnAddChild = new Jw.Winform.Ctrls.JwButton();
            this.BtnUp = new Jw.Winform.Ctrls.JwButton();
            this.BtnDown = new Jw.Winform.Ctrls.JwButton();
            this.BtnDelete = new Jw.Winform.Ctrls.JwButton();
            this.SuspendLayout();
            // 
            // property
            // 
            this.property.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.property.Location = new System.Drawing.Point(344, 22);
            this.property.Name = "property";
            this.property.Size = new System.Drawing.Size(285, 405);
            this.property.TabIndex = 0;
            // 
            // tree
            // 
            this.tree.AllowDrop = true;
            this.tree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tree.HideSelection = false;
            this.tree.Location = new System.Drawing.Point(25, 22);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(260, 367);
            this.tree.TabIndex = 1;
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
            // 
            // BtnOk
            // 
            this.BtnOk.AdjustIconTop = 2;
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.BtnOk.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnOk.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnOk.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnOk.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnOk.IconActiveColor = System.Drawing.Color.Black;
            this.BtnOk.IconColor = System.Drawing.Color.Black;
            this.BtnOk.IconDisableColor = System.Drawing.Color.Black;
            this.BtnOk.IconFontName = "Iconfont";
            this.BtnOk.IconSize = 20;
            this.BtnOk.IconText = "op2-succ";
            this.BtnOk.Location = new System.Drawing.Point(554, 442);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Radius = 3;
            this.BtnOk.ShowFocused = true;
            this.BtnOk.Size = new System.Drawing.Size(76, 32);
            this.BtnOk.TabIndex = 2;
            this.BtnOk.Text = "确定";
            this.BtnOk.TextMarginLeft = 4;
            this.BtnOk.Theme = Jw.Winform.Ctrls.ThemeType.Primary;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.AdjustIconTop = 2;
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.AutoResize = false;
            this.BtnCancel.BackColor = System.Drawing.Color.Transparent;
            this.BtnCancel.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnCancel.BgColor = System.Drawing.Color.Transparent;
            this.BtnCancel.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnCancel.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnCancel.BorderColor = System.Drawing.Color.Black;
            this.BtnCancel.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnCancel.BorderWidth = 1;
            this.BtnCancel.CententCenter = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnCancel.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnCancel.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnCancel.IconActiveColor = System.Drawing.Color.Black;
            this.BtnCancel.IconColor = System.Drawing.Color.Black;
            this.BtnCancel.IconDisableColor = System.Drawing.Color.Black;
            this.BtnCancel.IconFontName = "Iconfont";
            this.BtnCancel.IconSize = 20;
            this.BtnCancel.IconText = "op2-reeor";
            this.BtnCancel.Location = new System.Drawing.Point(472, 442);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Radius = 3;
            this.BtnCancel.ShowFocused = true;
            this.BtnCancel.Size = new System.Drawing.Size(76, 32);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.TextMarginLeft = 4;
            this.BtnCancel.Theme = Jw.Winform.Ctrls.ThemeType.Danger;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnAddRoot
            // 
            this.BtnAddRoot.AdjustIconTop = 0;
            this.BtnAddRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAddRoot.AutoResize = false;
            this.BtnAddRoot.BackColor = System.Drawing.Color.Transparent;
            this.BtnAddRoot.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnAddRoot.BgColor = System.Drawing.Color.Transparent;
            this.BtnAddRoot.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnAddRoot.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnAddRoot.BorderColor = System.Drawing.Color.Black;
            this.BtnAddRoot.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnAddRoot.BorderWidth = 1;
            this.BtnAddRoot.CententCenter = true;
            this.BtnAddRoot.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnAddRoot.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnAddRoot.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnAddRoot.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnAddRoot.IconActiveColor = System.Drawing.Color.Black;
            this.BtnAddRoot.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnAddRoot.IconDisableColor = System.Drawing.Color.Gray;
            this.BtnAddRoot.IconFontName = "Iconfont";
            this.BtnAddRoot.IconSize = 18;
            this.BtnAddRoot.IconText = "home-fill";
            this.BtnAddRoot.Location = new System.Drawing.Point(25, 395);
            this.BtnAddRoot.Name = "BtnAddRoot";
            this.BtnAddRoot.Radius = 3;
            this.BtnAddRoot.ShowFocused = true;
            this.BtnAddRoot.Size = new System.Drawing.Size(113, 32);
            this.BtnAddRoot.TabIndex = 4;
            this.BtnAddRoot.Text = "添加根节点";
            this.BtnAddRoot.TextMarginLeft = 4;
            this.BtnAddRoot.Theme = Jw.Winform.Ctrls.ThemeType.Dark;
            this.BtnAddRoot.Click += new System.EventHandler(this.BtnAddRoot_Click);
            // 
            // BtnAddChild
            // 
            this.BtnAddChild.AdjustIconTop = 0;
            this.BtnAddChild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnAddChild.AutoResize = false;
            this.BtnAddChild.BackColor = System.Drawing.Color.Transparent;
            this.BtnAddChild.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnAddChild.BgColor = System.Drawing.Color.Transparent;
            this.BtnAddChild.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnAddChild.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnAddChild.BorderColor = System.Drawing.Color.Black;
            this.BtnAddChild.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnAddChild.BorderWidth = 1;
            this.BtnAddChild.CententCenter = true;
            this.BtnAddChild.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnAddChild.Enabled = false;
            this.BtnAddChild.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnAddChild.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnAddChild.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnAddChild.IconActiveColor = System.Drawing.Color.Black;
            this.BtnAddChild.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnAddChild.IconDisableColor = System.Drawing.Color.Gray;
            this.BtnAddChild.IconFontName = "Iconfont";
            this.BtnAddChild.IconSize = 16;
            this.BtnAddChild.IconText = "device-map-fill";
            this.BtnAddChild.Location = new System.Drawing.Point(172, 395);
            this.BtnAddChild.Name = "BtnAddChild";
            this.BtnAddChild.Radius = 3;
            this.BtnAddChild.ShowFocused = true;
            this.BtnAddChild.Size = new System.Drawing.Size(113, 32);
            this.BtnAddChild.TabIndex = 5;
            this.BtnAddChild.Text = "添加子节点";
            this.BtnAddChild.TextMarginLeft = 4;
            this.BtnAddChild.Theme = Jw.Winform.Ctrls.ThemeType.Dark;
            this.BtnAddChild.Click += new System.EventHandler(this.BtnAddChild_Click);
            // 
            // BtnUp
            // 
            this.BtnUp.AdjustIconTop = 0;
            this.BtnUp.AutoResize = true;
            this.BtnUp.BackColor = System.Drawing.Color.Transparent;
            this.BtnUp.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnUp.BgColor = System.Drawing.Color.Transparent;
            this.BtnUp.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnUp.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnUp.BorderColor = System.Drawing.Color.Black;
            this.BtnUp.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnUp.BorderWidth = 0;
            this.BtnUp.CententCenter = true;
            this.BtnUp.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnUp.Enabled = false;
            this.BtnUp.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnUp.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnUp.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnUp.IconActiveColor = System.Drawing.Color.Black;
            this.BtnUp.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnUp.IconDisableColor = System.Drawing.Color.Gray;
            this.BtnUp.IconFontName = "Iconfont";
            this.BtnUp.IconSize = 24;
            this.BtnUp.IconText = "arrow1-up";
            this.BtnUp.Location = new System.Drawing.Point(293, 22);
            this.BtnUp.Name = "BtnUp";
            this.BtnUp.Radius = 0;
            this.BtnUp.ShowFocused = true;
            this.BtnUp.Size = new System.Drawing.Size(24, 28);
            this.BtnUp.TabIndex = 6;
            this.BtnUp.TextMarginLeft = 4;
            this.BtnUp.Theme = Jw.Winform.Ctrls.ThemeType.None;
            this.BtnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // BtnDown
            // 
            this.BtnDown.AdjustIconTop = 0;
            this.BtnDown.AutoResize = true;
            this.BtnDown.BackColor = System.Drawing.Color.Transparent;
            this.BtnDown.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnDown.BgColor = System.Drawing.Color.Transparent;
            this.BtnDown.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnDown.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnDown.BorderColor = System.Drawing.Color.Black;
            this.BtnDown.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnDown.BorderWidth = 0;
            this.BtnDown.CententCenter = true;
            this.BtnDown.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnDown.Enabled = false;
            this.BtnDown.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnDown.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnDown.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnDown.IconActiveColor = System.Drawing.Color.Black;
            this.BtnDown.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnDown.IconDisableColor = System.Drawing.Color.Gray;
            this.BtnDown.IconFontName = "Iconfont";
            this.BtnDown.IconSize = 24;
            this.BtnDown.IconText = "arrow1-down";
            this.BtnDown.Location = new System.Drawing.Point(293, 66);
            this.BtnDown.Name = "BtnDown";
            this.BtnDown.Radius = 0;
            this.BtnDown.ShowFocused = true;
            this.BtnDown.Size = new System.Drawing.Size(24, 28);
            this.BtnDown.TabIndex = 6;
            this.BtnDown.TextMarginLeft = 4;
            this.BtnDown.Theme = Jw.Winform.Ctrls.ThemeType.None;
            this.BtnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.AdjustIconTop = 0;
            this.BtnDelete.AutoResize = true;
            this.BtnDelete.BackColor = System.Drawing.Color.Transparent;
            this.BtnDelete.BgActiveColor = System.Drawing.Color.Transparent;
            this.BtnDelete.BgColor = System.Drawing.Color.Transparent;
            this.BtnDelete.BgDisableColor = System.Drawing.Color.Transparent;
            this.BtnDelete.BorderActiveColor = System.Drawing.Color.Black;
            this.BtnDelete.BorderColor = System.Drawing.Color.Black;
            this.BtnDelete.BorderDisableColor = System.Drawing.Color.Black;
            this.BtnDelete.BorderWidth = 0;
            this.BtnDelete.CententCenter = true;
            this.BtnDelete.DialogResult = System.Windows.Forms.DialogResult.None;
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.BtnDelete.ForeActiveColor = System.Drawing.Color.Black;
            this.BtnDelete.ForeDisableColor = System.Drawing.Color.Black;
            this.BtnDelete.IconActiveColor = System.Drawing.Color.Black;
            this.BtnDelete.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.BtnDelete.IconDisableColor = System.Drawing.Color.Gray;
            this.BtnDelete.IconFontName = "Iconfont";
            this.BtnDelete.IconSize = 30;
            this.BtnDelete.IconText = "op2-reeor";
            this.BtnDelete.Location = new System.Drawing.Point(290, 108);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Radius = 0;
            this.BtnDelete.ShowFocused = true;
            this.BtnDelete.Size = new System.Drawing.Size(30, 35);
            this.BtnDelete.TabIndex = 6;
            this.BtnDelete.TextMarginLeft = 4;
            this.BtnDelete.Theme = Jw.Winform.Ctrls.ThemeType.None;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // TreeNodeEditor
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(657, 486);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnDown);
            this.Controls.Add(this.BtnUp);
            this.Controls.Add(this.BtnAddChild);
            this.Controls.Add(this.BtnAddRoot);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.tree);
            this.Controls.Add(this.property);
            this.Name = "TreeNodeEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "节点编辑器";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid property;
        private System.Windows.Forms.TreeView tree;
        private JwButton BtnOk;
        private JwButton BtnCancel;
        private JwButton BtnAddRoot;
        private JwButton BtnAddChild;
        private JwButton BtnUp;
        private JwButton BtnDown;
        private JwButton BtnDelete;
    }
}