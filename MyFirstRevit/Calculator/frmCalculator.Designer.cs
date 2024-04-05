
namespace Calculator
{
    partial class frmCalculator
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
            this.tbDisplay = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tbResult = new DevExpress.XtraEditors.TextEdit();
            this.btn7 = new DevExpress.XtraEditors.SimpleButton();
            this.btn9 = new DevExpress.XtraEditors.SimpleButton();
            this.btn8 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.btn6 = new DevExpress.XtraEditors.SimpleButton();
            this.btn5 = new DevExpress.XtraEditors.SimpleButton();
            this.btn4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.btn3 = new DevExpress.XtraEditors.SimpleButton();
            this.btn2 = new DevExpress.XtraEditors.SimpleButton();
            this.btn1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton13 = new DevExpress.XtraEditors.SimpleButton();
            this.btnEqual = new DevExpress.XtraEditors.SimpleButton();
            this.btn0 = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tbDisplay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbResult.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tbDisplay
            // 
            this.tbDisplay.Location = new System.Drawing.Point(12, 15);
            this.tbDisplay.Name = "tbDisplay";
            this.tbDisplay.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.tbDisplay.Properties.Appearance.Options.UseFont = true;
            this.tbDisplay.Size = new System.Drawing.Size(318, 40);
            this.tbDisplay.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(146, 84);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(57, 25);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Result";
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(209, 73);
            this.tbResult.Name = "tbResult";
            this.tbResult.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.tbResult.Properties.Appearance.Options.UseFont = true;
            this.tbResult.Size = new System.Drawing.Size(121, 40);
            this.tbResult.TabIndex = 2;
            // 
            // btn7
            // 
            this.btn7.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn7.Appearance.Options.UseFont = true;
            this.btn7.Location = new System.Drawing.Point(12, 156);
            this.btn7.Name = "btn7";
            this.btn7.Size = new System.Drawing.Size(75, 47);
            this.btn7.TabIndex = 3;
            this.btn7.Text = "7";
            this.btn7.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn9
            // 
            this.btn9.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn9.Appearance.Options.UseFont = true;
            this.btn9.Location = new System.Drawing.Point(174, 156);
            this.btn9.Name = "btn9";
            this.btn9.Size = new System.Drawing.Size(75, 47);
            this.btn9.TabIndex = 4;
            this.btn9.Text = "9";
            this.btn9.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn8
            // 
            this.btn8.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn8.Appearance.Options.UseFont = true;
            this.btn8.Location = new System.Drawing.Point(93, 156);
            this.btn8.Name = "btn8";
            this.btn8.Size = new System.Drawing.Size(75, 47);
            this.btn8.TabIndex = 5;
            this.btn8.Text = "8";
            this.btn8.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.simpleButton4.Appearance.Options.UseFont = true;
            this.simpleButton4.Location = new System.Drawing.Point(255, 156);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(75, 47);
            this.simpleButton4.TabIndex = 6;
            this.simpleButton4.Text = "+";
            this.simpleButton4.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.simpleButton5.Appearance.Options.UseFont = true;
            this.simpleButton5.Location = new System.Drawing.Point(255, 215);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(75, 47);
            this.simpleButton5.TabIndex = 7;
            this.simpleButton5.Text = "-";
            this.simpleButton5.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn6
            // 
            this.btn6.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn6.Appearance.Options.UseFont = true;
            this.btn6.Location = new System.Drawing.Point(174, 215);
            this.btn6.Name = "btn6";
            this.btn6.Size = new System.Drawing.Size(75, 47);
            this.btn6.TabIndex = 8;
            this.btn6.Text = "6";
            this.btn6.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn5
            // 
            this.btn5.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn5.Appearance.Options.UseFont = true;
            this.btn5.Location = new System.Drawing.Point(93, 215);
            this.btn5.Name = "btn5";
            this.btn5.Size = new System.Drawing.Size(75, 47);
            this.btn5.TabIndex = 9;
            this.btn5.Text = "5";
            this.btn5.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn4
            // 
            this.btn4.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn4.Appearance.Options.UseFont = true;
            this.btn4.Location = new System.Drawing.Point(12, 215);
            this.btn4.Name = "btn4";
            this.btn4.Size = new System.Drawing.Size(75, 47);
            this.btn4.TabIndex = 10;
            this.btn4.Text = "4";
            this.btn4.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // simpleButton9
            // 
            this.simpleButton9.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.simpleButton9.Appearance.Options.UseFont = true;
            this.simpleButton9.Location = new System.Drawing.Point(255, 268);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(75, 47);
            this.simpleButton9.TabIndex = 11;
            this.simpleButton9.Text = "X";
            this.simpleButton9.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn3
            // 
            this.btn3.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn3.Appearance.Options.UseFont = true;
            this.btn3.Location = new System.Drawing.Point(174, 268);
            this.btn3.Name = "btn3";
            this.btn3.Size = new System.Drawing.Size(75, 47);
            this.btn3.TabIndex = 12;
            this.btn3.Text = "3";
            this.btn3.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn2
            // 
            this.btn2.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn2.Appearance.Options.UseFont = true;
            this.btn2.Location = new System.Drawing.Point(93, 268);
            this.btn2.Name = "btn2";
            this.btn2.Size = new System.Drawing.Size(75, 47);
            this.btn2.TabIndex = 13;
            this.btn2.Text = "2";
            this.btn2.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btn1
            // 
            this.btn1.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn1.Appearance.Options.UseFont = true;
            this.btn1.Location = new System.Drawing.Point(12, 268);
            this.btn1.Name = "btn1";
            this.btn1.Size = new System.Drawing.Size(75, 47);
            this.btn1.TabIndex = 14;
            this.btn1.Text = "1";
            this.btn1.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // simpleButton13
            // 
            this.simpleButton13.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.simpleButton13.Appearance.Options.UseFont = true;
            this.simpleButton13.Location = new System.Drawing.Point(255, 322);
            this.simpleButton13.Name = "simpleButton13";
            this.simpleButton13.Size = new System.Drawing.Size(75, 47);
            this.simpleButton13.TabIndex = 15;
            this.simpleButton13.Text = "/";
            this.simpleButton13.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btnEqual
            // 
            this.btnEqual.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btnEqual.Appearance.Options.UseFont = true;
            this.btnEqual.Location = new System.Drawing.Point(174, 322);
            this.btnEqual.Name = "btnEqual";
            this.btnEqual.Size = new System.Drawing.Size(75, 47);
            this.btnEqual.TabIndex = 16;
            this.btnEqual.Text = "=";
            this.btnEqual.Click += new System.EventHandler(this.btnEqual_Click);
            // 
            // btn0
            // 
            this.btn0.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btn0.Appearance.Options.UseFont = true;
            this.btn0.Location = new System.Drawing.Point(93, 322);
            this.btn0.Name = "btn0";
            this.btn0.Size = new System.Drawing.Size(75, 47);
            this.btn0.TabIndex = 17;
            this.btn0.Text = "0";
            this.btn0.Click += new System.EventHandler(this.btnNum_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Location = new System.Drawing.Point(12, 322);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 47);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "C";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 394);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btn0);
            this.Controls.Add(this.btnEqual);
            this.Controls.Add(this.simpleButton13);
            this.Controls.Add(this.btn1);
            this.Controls.Add(this.btn2);
            this.Controls.Add(this.btn3);
            this.Controls.Add(this.simpleButton9);
            this.Controls.Add(this.btn4);
            this.Controls.Add(this.btn5);
            this.Controls.Add(this.btn6);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.btn8);
            this.Controls.Add(this.btn9);
            this.Controls.Add(this.btn7);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tbDisplay);
            this.Name = "frmCalculator";
            this.Text = "frmCalculator";
            ((System.ComponentModel.ISupportInitialize)(this.tbDisplay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbResult.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit tbDisplay;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tbResult;
        private DevExpress.XtraEditors.SimpleButton btn7;
        private DevExpress.XtraEditors.SimpleButton btn9;
        private DevExpress.XtraEditors.SimpleButton btn8;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton btn6;
        private DevExpress.XtraEditors.SimpleButton btn5;
        private DevExpress.XtraEditors.SimpleButton btn4;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.SimpleButton btn3;
        private DevExpress.XtraEditors.SimpleButton btn2;
        private DevExpress.XtraEditors.SimpleButton btn1;
        private DevExpress.XtraEditors.SimpleButton simpleButton13;
        private DevExpress.XtraEditors.SimpleButton btnEqual;
        private DevExpress.XtraEditors.SimpleButton btn0;
        private DevExpress.XtraEditors.SimpleButton btnClear;
    }
}