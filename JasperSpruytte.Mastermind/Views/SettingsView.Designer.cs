namespace JasperSpruytte.MastermindWindows.Views
{
    partial class SettingsView
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
            this.nudNumberOfTurns = new System.Windows.Forms.NumericUpDown();
            this.nudNumberOfColors = new System.Windows.Forms.NumericUpDown();
            this.nudLengthOfSecretCode = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.rbUser = new System.Windows.Forms.RadioButton();
            this.rbComputer = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfTurns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfColors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLengthOfSecretCode)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudNumberOfTurns
            // 
            this.nudNumberOfTurns.Location = new System.Drawing.Point(190, 12);
            this.nudNumberOfTurns.Name = "nudNumberOfTurns";
            this.nudNumberOfTurns.Size = new System.Drawing.Size(48, 22);
            this.nudNumberOfTurns.TabIndex = 0;
            // 
            // nudNumberOfColors
            // 
            this.nudNumberOfColors.Location = new System.Drawing.Point(190, 40);
            this.nudNumberOfColors.Name = "nudNumberOfColors";
            this.nudNumberOfColors.Size = new System.Drawing.Size(48, 22);
            this.nudNumberOfColors.TabIndex = 1;
            // 
            // nudLengthOfSecretCode
            // 
            this.nudLengthOfSecretCode.Location = new System.Drawing.Point(190, 68);
            this.nudLengthOfSecretCode.Name = "nudLengthOfSecretCode";
            this.nudLengthOfSecretCode.Size = new System.Drawing.Size(48, 22);
            this.nudLengthOfSecretCode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Number of turns";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of possible colors";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Length of secret code";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 158);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 37);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(128, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 37);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Who is guessing?";
            // 
            // rbUser
            // 
            this.rbUser.AutoSize = true;
            this.rbUser.Location = new System.Drawing.Point(3, 3);
            this.rbUser.Name = "rbUser";
            this.rbUser.Size = new System.Drawing.Size(59, 21);
            this.rbUser.TabIndex = 9;
            this.rbUser.TabStop = true;
            this.rbUser.Text = "User";
            this.rbUser.UseVisualStyleBackColor = true;
            // 
            // rbComputer
            // 
            this.rbComputer.AutoSize = true;
            this.rbComputer.Location = new System.Drawing.Point(3, 30);
            this.rbComputer.Name = "rbComputer";
            this.rbComputer.Size = new System.Drawing.Size(90, 21);
            this.rbComputer.TabIndex = 10;
            this.rbComputer.TabStop = true;
            this.rbComputer.Text = "Computer";
            this.rbComputer.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbUser);
            this.panel1.Controls.Add(this.rbComputer);
            this.panel1.Location = new System.Drawing.Point(190, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(95, 57);
            this.panel1.TabIndex = 11;
            // 
            // frmSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(292, 204);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudLengthOfSecretCode);
            this.Controls.Add(this.nudNumberOfColors);
            this.Controls.Add(this.nudNumberOfTurns);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfTurns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberOfColors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLengthOfSecretCode)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudNumberOfTurns;
        private System.Windows.Forms.NumericUpDown nudNumberOfColors;
        private System.Windows.Forms.NumericUpDown nudLengthOfSecretCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbUser;
        private System.Windows.Forms.RadioButton rbComputer;
        private System.Windows.Forms.Panel panel1;
    }
}