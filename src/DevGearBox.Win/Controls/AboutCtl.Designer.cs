namespace DevGearBox.Win.Controls
{
    partial class AboutCtl
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            SuspendLayout();

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "AboutCtl";
            Size = new Size(938, 1129);
            ResumeLayout(false);

            // Application Name Label
            Label lblAppName = new Label();
            lblAppName.Text = "DevGearBox";
            lblAppName.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblAppName.AutoSize = true;
            lblAppName.Location = new Point(20, 20);

            // Version Label
            Label lblVersion = new Label();
            lblVersion.Text = "Version: 1.0.0";
            lblVersion.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblVersion.AutoSize = true;
            lblVersion.Location = new Point(20, 60);

            // Author Label
            Label lblAuthor = new Label();
            lblAuthor.Text = "Author: Emanuele Bartolesi";
            lblAuthor.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblAuthor.AutoSize = true;
            lblAuthor.Location = new Point(20, 100);

            // Copyright Label
            Label lblCopyright = new Label();
            lblCopyright.Text = "© 2025 DevGearBox. All rights reserved.";
            lblCopyright.Font = new Font("Segoe UI", 10F, FontStyle.Italic, GraphicsUnit.Point);
            lblCopyright.AutoSize = true;
            lblCopyright.Location = new Point(20, 140);

            // Description Label
            Label lblDescription = new Label();
            lblDescription.Text = "DevGearBox is a powerful tool for developers to streamline their workflow.";
            lblDescription.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblDescription.AutoSize = true;
            lblDescription.MaximumSize = new Size(900, 0); // Wrap text
            lblDescription.Location = new Point(20, 180);

            // Add controls to the AboutCtl
            Controls.Add(lblAppName);
            Controls.Add(lblVersion);
            Controls.Add(lblAuthor);
            Controls.Add(lblCopyright);
            Controls.Add(lblDescription);

            // AboutCtl properties
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "AboutCtl";
            Size = new Size(938, 1129);

            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
    }
}
