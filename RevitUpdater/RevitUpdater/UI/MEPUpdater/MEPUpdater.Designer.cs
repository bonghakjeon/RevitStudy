
namespace RevitUpdater.UI.MEPUpdater
{
    partial class MEPUpdater
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
            this.panelContent = new DevExpress.XtraEditors.PanelControl();
            this.labelParam = new DevExpress.XtraEditors.LabelControl();
            this.panelBtn = new DevExpress.XtraEditors.PanelControl();
            this.btnON = new DevExpress.XtraEditors.SimpleButton();
            this.btnOFF = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).BeginInit();
            this.panelTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).BeginInit();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelBtn)).BeginInit();
            this.panelBtn.SuspendLayout();
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
            this.labelTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.labelTitle.Appearance.Options.UseFont = true;
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTitle.Location = new System.Drawing.Point(2, 2);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(160, 24);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "TEST MEPUpdater";
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.labelParam);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 30);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(398, 238);
            this.panelContent.TabIndex = 1;
            // 
            // labelParam
            // 
            this.labelParam.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.labelParam.Appearance.Options.UseFont = true;
            this.labelParam.Location = new System.Drawing.Point(12, 75);
            this.labelParam.Name = "labelParam";
            this.labelParam.Size = new System.Drawing.Size(153, 24);
            this.labelParam.TabIndex = 0;
            this.labelParam.Text = "매개변수 이름 입력 :";
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnON);
            this.panelBtn.Controls.Add(this.btnOFF);
            this.panelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBtn.Location = new System.Drawing.Point(0, 218);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Size = new System.Drawing.Size(398, 50);
            this.panelBtn.TabIndex = 2;
            // 
            // btnON
            // 
            this.btnON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnON.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.btnON.Appearance.Options.UseFont = true;
            this.btnON.Location = new System.Drawing.Point(237, 5);
            this.btnON.Name = "btnON";
            this.btnON.Size = new System.Drawing.Size(75, 40);
            this.btnON.TabIndex = 1;
            this.btnON.Text = "ON";
            this.btnON.Click += new System.EventHandler(this.btnON_Click);
            // 
            // btnOFF
            // 
            this.btnOFF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOFF.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.btnOFF.Appearance.Options.UseFont = true;
            this.btnOFF.Location = new System.Drawing.Point(318, 5);
            this.btnOFF.Name = "btnOFF";
            this.btnOFF.Size = new System.Drawing.Size(75, 40);
            this.btnOFF.TabIndex = 0;
            this.btnOFF.Text = "OFF";
            this.btnOFF.Click += new System.EventHandler(this.btnOFF_Click);
            // 
            // MEPUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 268);
            this.Controls.Add(this.panelBtn);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MEPUpdater";
            this.Text = "MEPUpdater";
            ((System.ComponentModel.ISupportInitialize)(this.panelTitle)).EndInit();
            this.panelTitle.ResumeLayout(false);
            this.panelTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelContent)).EndInit();
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelBtn)).EndInit();
            this.panelBtn.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelTitle;
        private DevExpress.XtraEditors.LabelControl labelTitle;
        private DevExpress.XtraEditors.PanelControl panelContent;
        private DevExpress.XtraEditors.PanelControl panelBtn;
        private DevExpress.XtraEditors.SimpleButton btnOFF;
        private DevExpress.XtraEditors.SimpleButton btnON;
        private DevExpress.XtraEditors.LabelControl labelParam;
    }
}