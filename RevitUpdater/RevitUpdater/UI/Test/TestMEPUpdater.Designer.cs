
namespace RevitUpdater.UI.Test
{
    partial class TestMEPUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestMEPUpdater));
            this.cboState = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboState2 = new System.Windows.Forms.ComboBox();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbNames = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnAddNames = new DevExpress.XtraEditors.SimpleButton();
            this.txtAddName = new DevExpress.XtraEditors.TextEdit();
            this.btnAddName = new DevExpress.XtraEditors.SimpleButton();
            this.txtSearchName = new DevExpress.XtraEditors.TextEdit();
            this.btnSearchName = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNames.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // cboState
            // 
            this.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cboState.FormattingEnabled = true;
            this.cboState.Items.AddRange(new object[] {
            "Alabama",
            "Alaska"});
            this.cboState.Location = new System.Drawing.Point(209, 40);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(121, 20);
            this.cboState.TabIndex = 0;
            this.cboState.SelectedIndexChanged += new System.EventHandler(this.cboState_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "State:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Selected:";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(100, 76);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(13, 14);
            this.lblValue.TabIndex = 3;
            this.lblValue.Text = "?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "State:";
            // 
            // cboState2
            // 
            this.cboState2.FormattingEnabled = true;
            this.cboState2.Items.AddRange(new object[] {
            "Alabama",
            "Alaska"});
            this.cboState2.Location = new System.Drawing.Point(78, 130);
            this.cboState2.Name = "cboState2";
            this.cboState2.Size = new System.Drawing.Size(121, 22);
            this.cboState2.TabIndex = 4;
            this.cboState2.SelectionChangeCommitted += new System.EventHandler(this.cboState2_SelectionChangeCommitted);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(209, 120);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "상태:";
            // 
            // cmbNames
            // 
            this.cmbNames.Location = new System.Drawing.Point(371, 60);
            this.cmbNames.Name = "cmbNames";
            this.cmbNames.Properties.AppearanceFocused.BackColor = System.Drawing.Color.LightCyan;
            this.cmbNames.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cmbNames.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNames.Properties.Items.AddRange(new object[] {
            "Ahmet",
            "Cihan",
            "David",
            "Jack",
            "Jamie",
            "Murat"});
            this.cmbNames.Properties.Sorted = true;
            this.cmbNames.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbNames.Size = new System.Drawing.Size(149, 20);
            this.cmbNames.TabIndex = 7;
            // 
            // btnAddNames
            // 
            this.btnAddNames.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnAddNames.ImageOptions.SvgImage")));
            this.btnAddNames.Location = new System.Drawing.Point(392, 97);
            this.btnAddNames.Name = "btnAddNames";
            this.btnAddNames.Size = new System.Drawing.Size(111, 33);
            this.btnAddNames.TabIndex = 8;
            this.btnAddNames.Text = "Add Names";
            this.btnAddNames.Click += new System.EventHandler(this.btnAddNames_Click);
            // 
            // txtAddName
            // 
            this.txtAddName.Location = new System.Drawing.Point(548, 60);
            this.txtAddName.Name = "txtAddName";
            this.txtAddName.Size = new System.Drawing.Size(143, 20);
            this.txtAddName.TabIndex = 9;
            // 
            // btnAddName
            // 
            this.btnAddName.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddName.ImageOptions.Image")));
            this.btnAddName.Location = new System.Drawing.Point(568, 97);
            this.btnAddName.Name = "btnAddName";
            this.btnAddName.Size = new System.Drawing.Size(110, 33);
            this.btnAddName.TabIndex = 10;
            this.btnAddName.Text = "Add Name";
            this.btnAddName.Click += new System.EventHandler(this.btnAddName_Click);
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(711, 60);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(132, 20);
            this.txtSearchName.TabIndex = 11;
            // 
            // btnSearchName
            // 
            this.btnSearchName.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSearchName.ImageOptions.SvgImage")));
            this.btnSearchName.Location = new System.Drawing.Point(724, 97);
            this.btnSearchName.Name = "btnSearchName";
            this.btnSearchName.Size = new System.Drawing.Size(119, 33);
            this.btnSearchName.TabIndex = 12;
            this.btnSearchName.Text = "Search Name";
            this.btnSearchName.Click += new System.EventHandler(this.btnSearchName_Click);
            // 
            // TestMEPUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 268);
            this.Controls.Add(this.btnSearchName);
            this.Controls.Add(this.txtSearchName);
            this.Controls.Add(this.btnAddName);
            this.Controls.Add(this.txtAddName);
            this.Controls.Add(this.btnAddNames);
            this.Controls.Add(this.cmbNames);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboState2);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboState);
            this.Name = "TestMEPUpdater";
            this.Text = "TestMEPUpdater";
            this.Load += new System.EventHandler(this.TestMEPUpdater_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbNames.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchName.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboState2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbNames;
        private DevExpress.XtraEditors.SimpleButton btnAddNames;
        private DevExpress.XtraEditors.TextEdit txtAddName;
        private DevExpress.XtraEditors.SimpleButton btnAddName;
        private DevExpress.XtraEditors.TextEdit txtSearchName;
        private DevExpress.XtraEditors.SimpleButton btnSearchName;
    }
}