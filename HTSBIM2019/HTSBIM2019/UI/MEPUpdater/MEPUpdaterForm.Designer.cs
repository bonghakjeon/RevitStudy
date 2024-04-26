

namespace HTSBIM2019.UI.MEPUpdater
{
    partial class MEPUpdaterForm
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
            this.panelTitle = new DevExpress.XtraEditors.PanelControl();
            this.labelTitle = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnON = new DevExpress.XtraEditors.SimpleButton();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelCategory = new DevExpress.XtraEditors.LabelControl();
            this.treeViewCategory = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).BeginInit();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitle
            // 
            this.panelTitle.Controls.Add(this.labelTitle);
            this.panelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitle.Location = new System.Drawing.Point(0, 0);
            this.panelTitle.Name = "panelTitle";
            this.panelTitle.Size = new System.Drawing.Size(398, 30);
            this.panelTitle.TabIndex = 0;
            // 
            // labelTitle
            // 
            this.labelTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelTitle.Appearance.Options.UseFont = true;
            this.labelTitle.Location = new System.Drawing.Point(5, 5);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(148, 19);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "1. MEP 사용 기록 관리";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnON);
            this.panelControl2.Controls.Add(this.btnTest);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 218);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(398, 50);
            this.panelControl2.TabIndex = 1;
            // 
            // btnON
            // 
            this.btnON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnON.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.btnON.Appearance.Options.UseFont = true;
            this.btnON.Location = new System.Drawing.Point(318, 5);
            this.btnON.Name = "btnON";
            this.btnON.Size = new System.Drawing.Size(75, 40);
            this.btnON.TabIndex = 1;
            this.btnON.Text = "ON";
            this.btnON.Click += new System.EventHandler(this.btnON_Click);
            // 
            // btnTest
            // 
            this.btnTest.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.btnTest.Appearance.Options.UseFont = true;
            this.btnTest.Location = new System.Drawing.Point(6, 6);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 40);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "TEST";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.treeViewCategory);
            this.panelControl3.Controls.Add(this.labelCategory);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(0, 30);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(398, 188);
            this.panelControl3.TabIndex = 2;
            // 
            // labelCategory
            // 
            this.labelCategory.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.labelCategory.Appearance.Options.UseFont = true;
            this.labelCategory.Location = new System.Drawing.Point(89, 6);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(64, 24);
            this.labelCategory.TabIndex = 0;
            this.labelCategory.Text = "카테고리";
            // 
            // treeViewCategory
            // 
            this.treeViewCategory.Location = new System.Drawing.Point(89, 36);
            this.treeViewCategory.Name = "treeViewCategory";
            this.treeViewCategory.Size = new System.Drawing.Size(220, 134);
            this.treeViewCategory.TabIndex = 2;
            this.treeViewCategory.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewCategory_AfterCheck);
            // 
            // MEPUpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 268);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MEPUpdaterForm";
            this.Text = "MEPUpdaterForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MEPUpdater_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).EndInit();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelTitle;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.SimpleButton btnON;
        private DevExpress.XtraEditors.LabelControl labelCategory;
        private System.Windows.Forms.TreeView treeViewCategory;
        // private NewTreeViewControl treeViewCategory;
    }
}