namespace DevGearBox.Win.Controls
{
    partial class JsonFormatterCtl
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

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnFormatJSON = new Button();
            SuspendLayout();
            // 
            // btnFormatJSON
            // 
            btnFormatJSON.Location = new Point(624, 978);
            btnFormatJSON.Name = "btnFormatJSON";
            btnFormatJSON.Size = new Size(311, 148);
            btnFormatJSON.TabIndex = 0;
            btnFormatJSON.Text = "Format JSON";
            btnFormatJSON.UseVisualStyleBackColor = true;
            // 
            // JsonFormatterCtl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnFormatJSON);
            Name = "JsonFormatterCtl";
            Size = new Size(938, 1129);
            Load += JsonFormatterCtl_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnFormatJSON;
    }
}
