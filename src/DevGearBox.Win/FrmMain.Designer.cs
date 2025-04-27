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
            btnAbout = new Button();
            btnJSONtoYAML = new Button();
            btnJSONFormatter = new Button();
            pnlMain = new Panel();
            btnBase64String = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnBase64String);
            panel1.Controls.Add(btnAbout);
            panel1.Controls.Add(btnJSONtoYAML);
            panel1.Controls.Add(btnJSONFormatter);
            panel1.Location = new Point(10, 9);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(187, 847);
            panel1.TabIndex = 0;
            // 
            // btnAbout
            // 
            btnAbout.Location = new Point(10, 809);
            btnAbout.Margin = new Padding(3, 2, 3, 2);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(161, 35);
            btnAbout.TabIndex = 2;
            btnAbout.Text = "About";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // btnJSONtoYAML
            // 
            btnJSONtoYAML.Location = new Point(10, 54);
            btnJSONtoYAML.Margin = new Padding(3, 2, 3, 2);
            btnJSONtoYAML.Name = "btnJSONtoYAML";
            btnJSONtoYAML.Size = new Size(161, 35);
            btnJSONtoYAML.TabIndex = 1;
            btnJSONtoYAML.Text = "JSON to YAML";
            btnJSONtoYAML.UseVisualStyleBackColor = true;
            btnJSONtoYAML.Click += btnJSONtoYAML_Click;
            // 
            // btnJSONFormatter
            // 
            btnJSONFormatter.Location = new Point(10, 14);
            btnJSONFormatter.Margin = new Padding(3, 2, 3, 2);
            btnJSONFormatter.Name = "btnJSONFormatter";
            btnJSONFormatter.Size = new Size(161, 35);
            btnJSONFormatter.TabIndex = 0;
            btnJSONFormatter.Text = "JSON Formatter";
            btnJSONFormatter.UseVisualStyleBackColor = true;
            btnJSONFormatter.Click += btnJSONFormatter_Click;
            // 
            // pnlMain
            // 
            pnlMain.Location = new Point(203, 9);
            pnlMain.Margin = new Padding(3, 2, 3, 2);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(821, 847);
            pnlMain.TabIndex = 1;
            // 
            // btnBase64String
            // 
            btnBase64String.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBase64String.Location = new Point(10, 93);
            btnBase64String.Margin = new Padding(3, 2, 3, 2);
            btnBase64String.Name = "btnBase64String";
            btnBase64String.Size = new Size(161, 35);
            btnBase64String.TabIndex = 3;
            btnBase64String.Text = "Base64 String Encode/Decode";
            btnBase64String.UseVisualStyleBackColor = true;
            btnBase64String.Click += btnBase64String_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 865);
            Controls.Add(pnlMain);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DevGearBox";
            Load += FrmMain_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel pnlMain;
        private Button btnJSONFormatter;
        private Button btnJSONtoYAML;
        private Button btnAbout;
        private Button btnBase64String;
    }
}
