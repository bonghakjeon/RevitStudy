
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
            panelBtn = new DevExpress.XtraEditors.PanelControl();
            labelOrder = new DevExpress.XtraEditors.LabelControl();
            btnExit = new DevExpress.XtraEditors.SimpleButton();
            btnInsertImage = new DevExpress.XtraEditors.SimpleButton();
            btnCropImage = new DevExpress.XtraEditors.SimpleButton();
            btnOrgImage = new DevExpress.XtraEditors.SimpleButton();
            btnBlackConvert = new DevExpress.XtraEditors.SimpleButton();
            btnSelectElement = new DevExpress.XtraEditors.SimpleButton();
            btnSelectFile = new DevExpress.XtraEditors.SimpleButton();
            panelImage = new DevExpress.XtraEditors.PanelControl();
            splitContainerImage = new DevExpress.XtraEditors.SplitContainerControl();
            groupBoxOrgImage = new System.Windows.Forms.GroupBox();
            panelOrgImage = new System.Windows.Forms.Panel();
            pictureBoxOrgImage = new System.Windows.Forms.PictureBox();
            groupBoxEditImage = new System.Windows.Forms.GroupBox();
            panelEditImage = new System.Windows.Forms.Panel();
            pictureBoxEditImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)panelBtn).BeginInit();
            panelBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelImage).BeginInit();
            panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerImage.Panel1).BeginInit();
            splitContainerImage.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerImage.Panel2).BeginInit();
            splitContainerImage.Panel2.SuspendLayout();
            splitContainerImage.SuspendLayout();
            groupBoxOrgImage.SuspendLayout();
            panelOrgImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOrgImage).BeginInit();
            groupBoxEditImage.SuspendLayout();
            panelEditImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEditImage).BeginInit();
            SuspendLayout();
            // 
            // panelBtn
            // 
            panelBtn.Controls.Add(labelOrder);
            panelBtn.Controls.Add(btnExit);
            panelBtn.Controls.Add(btnInsertImage);
            panelBtn.Controls.Add(btnCropImage);
            panelBtn.Controls.Add(btnOrgImage);
            panelBtn.Controls.Add(btnBlackConvert);
            panelBtn.Controls.Add(btnSelectElement);
            panelBtn.Controls.Add(btnSelectFile);
            panelBtn.Dock = System.Windows.Forms.DockStyle.Left;
            panelBtn.Location = new System.Drawing.Point(0, 0);
            panelBtn.Name = "panelBtn";
            panelBtn.Size = new System.Drawing.Size(200, 608);
            panelBtn.TabIndex = 0;
            // 
            // labelOrder
            // 
            labelOrder.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            labelOrder.Appearance.Options.UseFont = true;
            labelOrder.Location = new System.Drawing.Point(40, 366);
            labelOrder.Name = "labelOrder";
            labelOrder.Size = new System.Drawing.Size(124, 224);
            labelOrder.TabIndex = 7;
            labelOrder.Text = "*   순서   *\r\n1. 파일 선택\r\n2. 이미지 자르기\r\n   (또는 흑백 전환)\r\n3. 이미지 삽입\r\n\r\n*   설명   *\r\n* 기능 - 원본 보기\r\n   흑백 전환된 이미지의  \r\n   원래 이미지를 읽어온다.\r\n* 기능 - 종료\r\n   이미지 삽입 화면 종료\r\n\r\n* 이미지가 큰 경우\r\n   윈도우 상자 크기를 \r\n   수정하여 줌인 \r\n";
            // 
            // btnExit
            // 
            btnExit.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnExit.Appearance.Options.UseFont = true;
            btnExit.Location = new System.Drawing.Point(70, 320);
            btnExit.Name = "btnExit";
            btnExit.Size = new System.Drawing.Size(60, 30);
            btnExit.TabIndex = 6;
            btnExit.Text = "종료";
            btnExit.Click += btnExit_Click;
            // 
            // btnInsertImage
            // 
            btnInsertImage.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnInsertImage.Appearance.Options.UseFont = true;
            btnInsertImage.Location = new System.Drawing.Point(50, 270);
            btnInsertImage.Name = "btnInsertImage";
            btnInsertImage.Size = new System.Drawing.Size(100, 30);
            btnInsertImage.TabIndex = 5;
            btnInsertImage.Text = "이미지 삽입";
            btnInsertImage.Click += btnInsertImage_Click;
            // 
            // btnCropImage
            // 
            btnCropImage.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnCropImage.Appearance.Options.UseFont = true;
            btnCropImage.Location = new System.Drawing.Point(40, 220);
            btnCropImage.Name = "btnCropImage";
            btnCropImage.Size = new System.Drawing.Size(120, 30);
            btnCropImage.TabIndex = 4;
            btnCropImage.Text = "이미지 자르기";
            btnCropImage.Click += btnCropImage_Click;
            // 
            // btnOrgImage
            // 
            btnOrgImage.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnOrgImage.Appearance.Options.UseFont = true;
            btnOrgImage.Location = new System.Drawing.Point(60, 170);
            btnOrgImage.Name = "btnOrgImage";
            btnOrgImage.Size = new System.Drawing.Size(80, 30);
            btnOrgImage.TabIndex = 3;
            btnOrgImage.Text = "원본 보기";
            btnOrgImage.Click += btnOrgImage_Click;
            // 
            // btnBlackConvert
            // 
            btnBlackConvert.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnBlackConvert.Appearance.Options.UseFont = true;
            btnBlackConvert.Location = new System.Drawing.Point(60, 120);
            btnBlackConvert.Name = "btnBlackConvert";
            btnBlackConvert.Size = new System.Drawing.Size(80, 30);
            btnBlackConvert.TabIndex = 2;
            btnBlackConvert.Text = "흑백 전환";
            btnBlackConvert.Click += btnBlackConvert_Click;
            // 
            // btnSelectElement
            // 
            btnSelectElement.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnSelectElement.Appearance.Options.UseFont = true;
            btnSelectElement.Location = new System.Drawing.Point(60, 70);
            btnSelectElement.Name = "btnSelectElement";
            btnSelectElement.Size = new System.Drawing.Size(80, 30);
            btnSelectElement.TabIndex = 1;
            btnSelectElement.Text = "객체 선택";
            btnSelectElement.Click += btnSelectElement_Click;
            // 
            // btnSelectFile
            // 
            btnSelectFile.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            btnSelectFile.Appearance.Options.UseFont = true;
            btnSelectFile.Location = new System.Drawing.Point(60, 20);
            btnSelectFile.Name = "btnSelectFile";
            btnSelectFile.Size = new System.Drawing.Size(80, 30);
            btnSelectFile.TabIndex = 0;
            btnSelectFile.Text = "파일 선택";
            btnSelectFile.Click += btnSelectFile_Click;
            // 
            // panelImage
            // 
            panelImage.Controls.Add(splitContainerImage);
            panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            panelImage.Location = new System.Drawing.Point(200, 0);
            panelImage.Name = "panelImage";
            panelImage.Size = new System.Drawing.Size(898, 608);
            panelImage.TabIndex = 1;
            // 
            // splitContainerImage
            // 
            splitContainerImage.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainerImage.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            splitContainerImage.Location = new System.Drawing.Point(2, 2);
            splitContainerImage.Name = "splitContainerImage";
            splitContainerImage.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainerImage.Panel1
            // 
            splitContainerImage.Panel1.Controls.Add(groupBoxOrgImage);
            splitContainerImage.Panel1.Text = "Panel1";
            // 
            // splitContainerImage.Panel2
            // 
            splitContainerImage.Panel2.Controls.Add(groupBoxEditImage);
            splitContainerImage.Panel2.Text = "Panel2";
            splitContainerImage.Size = new System.Drawing.Size(894, 604);
            splitContainerImage.SplitterPosition = 447;
            splitContainerImage.TabIndex = 0;
            // 
            // groupBoxOrgImage
            // 
            groupBoxOrgImage.Controls.Add(panelOrgImage);
            groupBoxOrgImage.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxOrgImage.Location = new System.Drawing.Point(0, 0);
            groupBoxOrgImage.Name = "groupBoxOrgImage";
            groupBoxOrgImage.Padding = new System.Windows.Forms.Padding(10);
            groupBoxOrgImage.Size = new System.Drawing.Size(447, 598);
            groupBoxOrgImage.TabIndex = 0;
            groupBoxOrgImage.TabStop = false;
            groupBoxOrgImage.Text = "원본 이미지";
            // 
            // panelOrgImage
            // 
            panelOrgImage.AutoScroll = true;
            panelOrgImage.Controls.Add(pictureBoxOrgImage);
            panelOrgImage.Dock = System.Windows.Forms.DockStyle.Fill;
            panelOrgImage.Location = new System.Drawing.Point(10, 25);
            panelOrgImage.Name = "panelOrgImage";
            panelOrgImage.Size = new System.Drawing.Size(427, 563);
            panelOrgImage.TabIndex = 0;
            // 
            // pictureBoxOrgImage
            // 
            pictureBoxOrgImage.Cursor = System.Windows.Forms.Cursors.Cross;
            pictureBoxOrgImage.Location = new System.Drawing.Point(14, 15);
            pictureBoxOrgImage.Name = "pictureBoxOrgImage";
            pictureBoxOrgImage.Size = new System.Drawing.Size(397, 530);
            pictureBoxOrgImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBoxOrgImage.TabIndex = 0;
            pictureBoxOrgImage.TabStop = false;
            pictureBoxOrgImage.Paint += pictureBoxOrgImage_Paint;
            pictureBoxOrgImage.MouseDown += pictureBoxOrgImage_MouseDown;
            pictureBoxOrgImage.MouseMove += pictureBoxOrgImage_MouseMove;
            pictureBoxOrgImage.MouseUp += pictureBoxOrgImage_MouseUp;
            // 
            // groupBoxEditImage
            // 
            groupBoxEditImage.Controls.Add(panelEditImage);
            groupBoxEditImage.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxEditImage.Location = new System.Drawing.Point(0, 0);
            groupBoxEditImage.Name = "groupBoxEditImage";
            groupBoxEditImage.Padding = new System.Windows.Forms.Padding(10);
            groupBoxEditImage.Size = new System.Drawing.Size(431, 598);
            groupBoxEditImage.TabIndex = 0;
            groupBoxEditImage.TabStop = false;
            groupBoxEditImage.Text = "편집 이미지";
            // 
            // panelEditImage
            // 
            panelEditImage.AutoScroll = true;
            panelEditImage.Controls.Add(pictureBoxEditImage);
            panelEditImage.Dock = System.Windows.Forms.DockStyle.Fill;
            panelEditImage.Location = new System.Drawing.Point(10, 25);
            panelEditImage.Name = "panelEditImage";
            panelEditImage.Size = new System.Drawing.Size(411, 563);
            panelEditImage.TabIndex = 0;
            // 
            // pictureBoxEditImage
            // 
            pictureBoxEditImage.Location = new System.Drawing.Point(16, 15);
            pictureBoxEditImage.Name = "pictureBoxEditImage";
            pictureBoxEditImage.Size = new System.Drawing.Size(380, 530);
            pictureBoxEditImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            pictureBoxEditImage.TabIndex = 0;
            pictureBoxEditImage.TabStop = false;
            // 
            // ImageForm
            // 
            Appearance.Options.UseFont = true;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSize = true;
            ClientSize = new System.Drawing.Size(1098, 608);
            Controls.Add(panelImage);
            Controls.Add(panelBtn);
            Font = new System.Drawing.Font("굴림", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            MinimumSize = new System.Drawing.Size(970, 530);
            Name = "ImageForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "이미지 삽입";
            FormClosed += ImageForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)panelBtn).EndInit();
            panelBtn.ResumeLayout(false);
            panelBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)panelImage).EndInit();
            panelImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerImage.Panel1).EndInit();
            splitContainerImage.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerImage.Panel2).EndInit();
            splitContainerImage.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerImage).EndInit();
            splitContainerImage.ResumeLayout(false);
            groupBoxOrgImage.ResumeLayout(false);
            panelOrgImage.ResumeLayout(false);
            panelOrgImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxOrgImage).EndInit();
            groupBoxEditImage.ResumeLayout(false);
            panelEditImage.ResumeLayout(false);
            panelEditImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEditImage).EndInit();
            ResumeLayout(false);
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