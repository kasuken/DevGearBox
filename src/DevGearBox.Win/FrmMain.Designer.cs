namespace DevGearBox.Win
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            button1 = new Button();
            btnJSONFormatter = new Button();
            pnlMain = new Panel();
            btnAbout = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnAbout);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(btnJSONFormatter);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(214, 1129);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(12, 72);
            button1.Name = "button1";
            button1.Size = new Size(184, 47);
            button1.TabIndex = 1;
            button1.Text = "JSON to YAML";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnJSONFormatter
            // 
            btnJSONFormatter.Location = new Point(12, 19);
            btnJSONFormatter.Name = "btnJSONFormatter";
            btnJSONFormatter.Size = new Size(184, 47);
            btnJSONFormatter.TabIndex = 0;
            btnJSONFormatter.Text = "JSON Formatter";
            btnJSONFormatter.UseVisualStyleBackColor = true;
            btnJSONFormatter.Click += btnJSONFormatter_Click;
            // 
            // pnlMain
            // 
            pnlMain.Location = new Point(232, 12);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(938, 1129);
            pnlMain.TabIndex = 1;
            // 
            // btnAbout
            // 
            btnAbout.Location = new Point(12, 1079);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(184, 47);
            btnAbout.TabIndex = 2;
            btnAbout.Text = "About";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 1153);
            Controls.Add(pnlMain);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DevGearBox";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel pnlMain;
        private Button btnJSONFormatter;
        private Button button1;
        private Button btnAbout;
    }
}
