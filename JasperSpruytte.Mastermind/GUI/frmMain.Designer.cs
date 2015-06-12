namespace JasperSpruytte.MastermindWindows.GUI
{
    partial class frmMain
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
            this.grbAvailableColors = new System.Windows.Forms.GroupBox();
            this.grbGameboard = new System.Windows.Forms.GroupBox();
            this.grbFeedback = new System.Windows.Forms.GroupBox();
            this.btnAdvance = new System.Windows.Forms.Button();
            this.grbSecretCode = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSavedGames = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbAvailableColors
            // 
            this.grbAvailableColors.Location = new System.Drawing.Point(12, 27);
            this.grbAvailableColors.Name = "grbAvailableColors";
            this.grbAvailableColors.Size = new System.Drawing.Size(257, 61);
            this.grbAvailableColors.TabIndex = 0;
            this.grbAvailableColors.TabStop = false;
            this.grbAvailableColors.Text = "Available Colors";
            // 
            // grbGameboard
            // 
            this.grbGameboard.Location = new System.Drawing.Point(12, 95);
            this.grbGameboard.Name = "grbGameboard";
            this.grbGameboard.Size = new System.Drawing.Size(200, 100);
            this.grbGameboard.TabIndex = 1;
            this.grbGameboard.TabStop = false;
            // 
            // grbFeedback
            // 
            this.grbFeedback.Location = new System.Drawing.Point(219, 95);
            this.grbFeedback.Name = "grbFeedback";
            this.grbFeedback.Size = new System.Drawing.Size(50, 100);
            this.grbFeedback.TabIndex = 2;
            this.grbFeedback.TabStop = false;
            // 
            // btnAdvance
            // 
            this.btnAdvance.Location = new System.Drawing.Point(275, 104);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(75, 23);
            this.btnAdvance.TabIndex = 3;
            this.btnAdvance.Text = "Guess";
            this.btnAdvance.UseVisualStyleBackColor = true;
            // 
            // grbSecretCode
            // 
            this.grbSecretCode.Location = new System.Drawing.Point(11, 201);
            this.grbSecretCode.Name = "grbSecretCode";
            this.grbSecretCode.Size = new System.Drawing.Size(200, 54);
            this.grbSecretCode.TabIndex = 4;
            this.grbSecretCode.TabStop = false;
            this.grbSecretCode.Text = "Secret code";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(358, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.tsmiSave,
            this.tsmiSavedGames});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.newToolStripMenuItem.Text = "&New game";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(175, 24);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // tsmiSave
            // 
            this.tsmiSave.Name = "tsmiSave";
            this.tsmiSave.Size = new System.Drawing.Size(175, 24);
            this.tsmiSave.Text = "&Save";
            this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
            // 
            // tsmiSavedGames
            // 
            this.tsmiSavedGames.Name = "tsmiSavedGames";
            this.tsmiSavedGames.Size = new System.Drawing.Size(175, 24);
            this.tsmiSavedGames.Text = "Saved &Games";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 265);
            this.Controls.Add(this.grbSecretCode);
            this.Controls.Add(this.btnAdvance);
            this.Controls.Add(this.grbFeedback);
            this.Controls.Add(this.grbGameboard);
            this.Controls.Add(this.grbAvailableColors);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Mastermind";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbAvailableColors;
        private System.Windows.Forms.GroupBox grbGameboard;
        private System.Windows.Forms.GroupBox grbFeedback;
        private System.Windows.Forms.Button btnAdvance;
        private System.Windows.Forms.GroupBox grbSecretCode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiSavedGames;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

    }
}

