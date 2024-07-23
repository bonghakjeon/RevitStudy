
namespace HTSBIM2019.UI.ImageEditor
{
    partial class ImageEditorForm
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
            this.panelBtn = new DevExpress.XtraEditors.PanelControl();
            this.labelOrder = new DevExpress.XtraEditors.LabelControl();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            this.btnInsertImage = new DevExpress.XtraEditors.SimpleButton();
            this.btnCropImage = new DevExpress.XtraEditors.SimpleButton();
            this.btnOrgImage = new DevExpress.XtraEditors.SimpleButton();
            this.btnBlackConvert = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectElement = new DevExpress.XtraEditors.SimpleButton();
            this.btnSelectFile = new DevExpress.XtraEditors.SimpleButton();
            this.panelImage = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerImage = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupBoxOrgImage = new System.Windows.Forms.GroupBox();
            this.panelOrgImage = new System.Windows.Forms.Panel();
            this.pictureBoxOrgImage = new System.Windows.Forms.PictureBox();
            this.groupBoxEditImage = new System.Windows.Forms.GroupBox();
            this.panelEditImage = new System.Windows.Forms.Panel();
            this.pictureBoxEditImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelBtn)).BeginInit();
            this.panelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelImage)).BeginInit();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerImage)).BeginInit();
            this.splitContainerImage.SuspendLayout();
            this.groupBoxOrgImage.SuspendLayout();
            this.panelOrgImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrgImage)).BeginInit();
            this.groupBoxEditImage.SuspendLayout();
            this.panelEditImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEditImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBtn
            // 
            this.panelBtn.Controls.Add(this.labelOrder);
            this.panelBtn.Controls.Add(this.btnExit);
            this.panelBtn.Controls.Add(this.btnInsertImage);
            this.panelBtn.Controls.Add(this.btnCropImage);
            this.panelBtn.Controls.Add(this.btnOrgImage);
            this.panelBtn.Controls.Add(this.btnBlackConvert);
            this.panelBtn.Controls.Add(this.btnSelectElement);
            this.panelBtn.Controls.Add(this.btnSelectFile);
            this.panelBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBtn.Location = new System.Drawing.Point(0, 0);
            this.panelBtn.Name = "panelBtn";
            this.panelBtn.Size = new System.Drawing.Size(200, 608);
            this.panelBtn.TabIndex = 0;
            // 
            // labelOrder
            // 
            this.labelOrder.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrder.Appearance.Options.UseFont = true;
            this.labelOrder.Location = new System.Drawing.Point(40, 366);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(124, 224);
            this.labelOrder.TabIndex = 7;
            this.labelOrder.Text = "*   순서   *\r\n1. 파일 선택\r\n2. 이미지 자르기\r\n   (또는 흑백 전환)\r\n3. 이미지 삽입\r\n\r\n*   설명   *\r\n* 기능 - " +
    "원본 보기\r\n   흑백 전환된 이미지의  \r\n   원래 이미지를 읽어온다.\r\n* 기능 - 종료\r\n   이미지 삽입 화면 종료\r\n\r\n* 이미지가 " +
    "큰 경우\r\n   윈도우 상자 크기를 \r\n   수정하여 줌인 \r\n";
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Location = new System.Drawing.Point(40, 320);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(120, 30);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "종료";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnInsertImage
            // 
            this.btnInsertImage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertImage.Appearance.Options.UseFont = true;
            this.btnInsertImage.Location = new System.Drawing.Point(40, 270);
            this.btnInsertImage.Name = "btnInsertImage";
            this.btnInsertImage.Size = new System.Drawing.Size(120, 30);
            this.btnInsertImage.TabIndex = 5;
            this.btnInsertImage.Text = "이미지 삽입";
            this.btnInsertImage.Click += new System.EventHandler(this.btnInsertImage_Click);
            // 
            // btnCropImage
            // 
            this.btnCropImage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCropImage.Appearance.Options.UseFont = true;
            this.btnCropImage.Location = new System.Drawing.Point(40, 220);
            this.btnCropImage.Name = "btnCropImage";
            this.btnCropImage.Size = new System.Drawing.Size(120, 30);
            this.btnCropImage.TabIndex = 4;
            this.btnCropImage.Text = "이미지 자르기";
            this.btnCropImage.Click += new System.EventHandler(this.btnCropImage_Click);
            // 
            // btnOrgImage
            // 
            this.btnOrgImage.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOrgImage.Appearance.Options.UseFont = true;
            this.btnOrgImage.Location = new System.Drawing.Point(40, 170);
            this.btnOrgImage.Name = "btnOrgImage";
            this.btnOrgImage.Size = new System.Drawing.Size(120, 30);
            this.btnOrgImage.TabIndex = 3;
            this.btnOrgImage.Text = "원본 보기";
            this.btnOrgImage.Click += new System.EventHandler(this.btnOrgImage_Click);
            // 
            // btnBlackConvert
            // 
            this.btnBlackConvert.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBlackConvert.Appearance.Options.UseFont = true;
            this.btnBlackConvert.Location = new System.Drawing.Point(40, 120);
            this.btnBlackConvert.Name = "btnBlackConvert";
            this.btnBlackConvert.Size = new System.Drawing.Size(120, 30);
            this.btnBlackConvert.TabIndex = 2;
            this.btnBlackConvert.Text = "흑백 전환";
            this.btnBlackConvert.Click += new System.EventHandler(this.btnBlackConvert_Click);
            // 
            // btnSelectElement
            // 
            this.btnSelectElement.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectElement.Appearance.Options.UseFont = true;
            this.btnSelectElement.Location = new System.Drawing.Point(40, 70);
            this.btnSelectElement.Name = "btnSelectElement";
            this.btnSelectElement.Size = new System.Drawing.Size(120, 30);
            this.btnSelectElement.TabIndex = 1;
            this.btnSelectElement.Text = "객체 선택";
            this.btnSelectElement.Click += new System.EventHandler(this.btnSelectElement_Click);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectFile.Appearance.Options.UseFont = true;
            this.btnSelectFile.Location = new System.Drawing.Point(40, 20);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(120, 30);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "파일 선택";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // panelImage
            // 
            this.panelImage.Controls.Add(this.splitContainerImage);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImage.Location = new System.Drawing.Point(200, 0);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(898, 608);
            this.panelImage.TabIndex = 1;
            // 
            // splitContainerImage
            // 
            this.splitContainerImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerImage.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerImage.Location = new System.Drawing.Point(2, 2);
            this.splitContainerImage.Name = "splitContainerImage";
            this.splitContainerImage.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainerImage.Panel1.Controls.Add(this.groupBoxOrgImage);
            this.splitContainerImage.Panel1.Text = "Panel1";
            this.splitContainerImage.Panel2.Controls.Add(this.groupBoxEditImage);
            this.splitContainerImage.Panel2.Text = "Panel2";
            this.splitContainerImage.Size = new System.Drawing.Size(894, 604);
            this.splitContainerImage.SplitterPosition = 447;
            this.splitContainerImage.TabIndex = 0;
            // 
            // groupBoxOrgImage
            // 
            this.groupBoxOrgImage.Controls.Add(this.panelOrgImage);
            this.groupBoxOrgImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxOrgImage.Location = new System.Drawing.Point(0, 0);
            this.groupBoxOrgImage.Name = "groupBoxOrgImage";
            this.groupBoxOrgImage.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxOrgImage.Size = new System.Drawing.Size(447, 598);
            this.groupBoxOrgImage.TabIndex = 0;
            this.groupBoxOrgImage.TabStop = false;
            this.groupBoxOrgImage.Text = "원본 이미지";
            // 
            // panelOrgImage
            // 
            this.panelOrgImage.AutoScroll = true;
            this.panelOrgImage.Controls.Add(this.pictureBoxOrgImage);
            this.panelOrgImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrgImage.Location = new System.Drawing.Point(10, 25);
            this.panelOrgImage.Name = "panelOrgImage";
            this.panelOrgImage.Size = new System.Drawing.Size(427, 563);
            this.panelOrgImage.TabIndex = 0;
            // 
            // pictureBoxOrgImage
            // 
            this.pictureBoxOrgImage.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBoxOrgImage.Location = new System.Drawing.Point(15, 15);
            this.pictureBoxOrgImage.Name = "pictureBoxOrgImage";
            this.pictureBoxOrgImage.Size = new System.Drawing.Size(397, 530);
            this.pictureBoxOrgImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxOrgImage.TabIndex = 0;
            this.pictureBoxOrgImage.TabStop = false;
            this.pictureBoxOrgImage.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxOrgImage_Paint);
            this.pictureBoxOrgImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOrgImage_MouseDown);
            this.pictureBoxOrgImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOrgImage_MouseMove);
            this.pictureBoxOrgImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOrgImage_MouseUp);
            this.pictureBoxOrgImage.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBoxOrgImage_MouseWheel);
            // 
            // groupBoxEditImage
            // 
            this.groupBoxEditImage.Controls.Add(this.panelEditImage);
            this.groupBoxEditImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEditImage.Location = new System.Drawing.Point(0, 0);
            this.groupBoxEditImage.Name = "groupBoxEditImage";
            this.groupBoxEditImage.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxEditImage.Size = new System.Drawing.Size(431, 598);
            this.groupBoxEditImage.TabIndex = 0;
            this.groupBoxEditImage.TabStop = false;
            this.groupBoxEditImage.Text = "편집 이미지";
            // 
            // panelEditImage
            // 
            this.panelEditImage.AutoScroll = true;
            this.panelEditImage.Controls.Add(this.pictureBoxEditImage);
            this.panelEditImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditImage.Location = new System.Drawing.Point(10, 25);
            this.panelEditImage.Name = "panelEditImage";
            this.panelEditImage.Size = new System.Drawing.Size(411, 563);
            this.panelEditImage.TabIndex = 0;
            // 
            // pictureBoxEditImage
            // 
            this.pictureBoxEditImage.Location = new System.Drawing.Point(15, 15);
            this.pictureBoxEditImage.Name = "pictureBoxEditImage";
            this.pictureBoxEditImage.Size = new System.Drawing.Size(380, 530);
            this.pictureBoxEditImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxEditImage.TabIndex = 0;
            this.pictureBoxEditImage.TabStop = false;
            // 
            // ImageEditorForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1098, 608);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.panelBtn);
            this.Font = new System.Drawing.Font("굴림", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(970, 530);
            this.Name = "ImageEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "이미지 편집";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ImageForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelBtn)).EndInit();
            this.panelBtn.ResumeLayout(false);
            this.panelBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelImage)).EndInit();
            this.panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerImage)).EndInit();
            this.splitContainerImage.ResumeLayout(false);
            this.groupBoxOrgImage.ResumeLayout(false);
            this.panelOrgImage.ResumeLayout(false);
            this.panelOrgImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOrgImage)).EndInit();
            this.groupBoxEditImage.ResumeLayout(false);
            this.panelEditImage.ResumeLayout(false);
            this.panelEditImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEditImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelBtn;
        private DevExpress.XtraEditors.PanelControl panelImage;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerImage;
        private System.Windows.Forms.GroupBox groupBoxOrgImage;
        private System.Windows.Forms.GroupBox groupBoxEditImage;
        private System.Windows.Forms.PictureBox pictureBoxOrgImage;
        private System.Windows.Forms.PictureBox pictureBoxEditImage;
        private DevExpress.XtraEditors.SimpleButton btnSelectFile;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private DevExpress.XtraEditors.SimpleButton btnInsertImage;
        private DevExpress.XtraEditors.SimpleButton btnCropImage;
        private DevExpress.XtraEditors.SimpleButton btnOrgImage;
        private DevExpress.XtraEditors.SimpleButton btnBlackConvert;
        private DevExpress.XtraEditors.SimpleButton btnSelectElement;
        private DevExpress.XtraEditors.LabelControl labelOrder;
        private System.Windows.Forms.Panel panelOrgImage;
        private System.Windows.Forms.Panel panelEditImage;
    }
}