
namespace RevitUpdater.UI.MEPUpdater
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
            if(disposing && (components != null))
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
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCategory = new DevExpress.XtraEditors.LabelControl();
            this.textParamName = new RevitUpdater.Controls.Text.WaterMarkTextControl();
            this.labelParam = new DevExpress.XtraEditors.LabelControl();
            this.panelBtn = new DevExpress.XtraEditors.PanelControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
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
            this.panelContent.Controls.Add(this.comboBoxCategory);
            this.panelContent.Controls.Add(this.labelCategory);
            this.panelContent.Controls.Add(this.textParamName);
            this.panelContent.Controls.Add(this.labelParam);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 30);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(398, 238);
            this.panelContent.TabIndex = 1;
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Font = new System.Drawing.Font("Tahoma", 10F);
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(124, 45);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(220, 24);
            this.comboBoxCategory.TabIndex = 3;
            // 
            // labelCategory
            // 
            this.labelCategory.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.labelCategory.Appearance.Options.UseFont = true;
            this.labelCategory.Location = new System.Drawing.Point(41, 41);
            this.labelCategory.Name = "labelCategory";
            this.labelCategory.Size = new System.Drawing.Size(77, 24);
            this.labelCategory.TabIndex = 2;
            this.labelCategory.Text = "카테고리 :";
            // 
            // textParamName
            // 
            this.textParamName.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textParamName.Location = new System.Drawing.Point(124, 109);
            this.textParamName.Name = "textParamName";
            this.textParamName.Size = new System.Drawing.Size(220, 25);
            this.textParamName.TabIndex = 1;
            this.textParamName.WaterMarkColor = System.Drawing.Color.Gray;
            this.textParamName.WaterMarkText = "매개변수 이름을 입력하세요.";
            // 
            // labelParam
            // 
            this.labelParam.Appearance.Font = new System.Drawing.Font("Tahoma", 15F);
            this.labelParam.Appearance.Options.UseFont = true;
            this.labelParam.Location = new System.Drawing.Point(41, 107);
            this.labelParam.Name = "labelParam";
            this.labelParam.Size = new System.Drawing.Size(77, 24);
            this.labelParam.TabIndex = 0;
            this.labelParam.Text = "매개변수 :";
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.btnTest);
            this.panelBtn.Controls.Add(this.btnON);
            this.panelBtn.Controls.Add(this.btnOFF);
            this.panelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBtn.Location = new System.Drawing.Point(0, 218);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Size = new System.Drawing.Size(398, 50);
            this.panelBtn.TabIndex = 2;
            // 
            // btnTest
            // 
            this.btnTest.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.btnTest.Appearance.Options.UseFont = true;
            this.btnTest.Location = new System.Drawing.Point(6, 6);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 40);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "TEST";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
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
            // MEPUpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 268);
            this.Controls.Add(this.panelBtn);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MEPUpdaterForm";
            this.Tag = "매개변수 이름을 입력하세요.";
            this.Text = "MEPUpdater";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MEPUpdater_FormClosed);
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
        private Controls.Text.WaterMarkTextControl textParamName;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.LabelControl labelCategory;
        private System.Windows.Forms.ComboBox comboBoxCategory;
    }
}