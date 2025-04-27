namespace DevGearBox.Win.Controls
{
    partial class Base64StringEncodeDecodeCtl
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
            btnDecode = new Button();
            txtSource = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtResult = new TextBox();
            btnEncode = new Button();
            btnCopyToClipboard = new Button();
            SuspendLayout();
            // 
            // btnDecode
            // 
            btnDecode.Location = new Point(626, 802);
            btnDecode.Margin = new Padding(3, 2, 3, 2);
            btnDecode.Name = "btnDecode";
            btnDecode.Size = new Size(192, 43);
            btnDecode.TabIndex = 0;
            btnDecode.Text = "Decode";
            btnDecode.UseVisualStyleBackColor = true;
            btnDecode.Click += btnDecode_Click;
            // 
            // txtSource
            // 
            txtSource.Location = new Point(3, 38);
            txtSource.Margin = new Padding(3, 2, 3, 2);
            txtSource.Multiline = true;
            txtSource.Name = "txtSource";
            txtSource.ScrollBars = ScrollBars.Vertical;
            txtSource.Size = new Size(816, 270);
            txtSource.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 14);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 2;
            label1.Text = "Source";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 317);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 4;
            label2.Text = "Result";
            // 
            // txtResult
            // 
            txtResult.Location = new Point(3, 341);
            txtResult.Margin = new Padding(3, 2, 3, 2);
            txtResult.Multiline = true;
            txtResult.Name = "txtResult";
            txtResult.ScrollBars = ScrollBars.Vertical;
            txtResult.Size = new Size(816, 457);
            txtResult.TabIndex = 3;
            // 
            // btnEncode
            // 
            btnEncode.Location = new Point(430, 802);
            btnEncode.Margin = new Padding(3, 2, 3, 2);
            btnEncode.Name = "btnEncode";
            btnEncode.Size = new Size(192, 43);
            btnEncode.TabIndex = 5;
            btnEncode.Text = "Encode";
            btnEncode.UseVisualStyleBackColor = true;
            btnEncode.Click += btnEncode_Click;
            // 
            // btnCopyToClipboard
            // 
            btnCopyToClipboard.Location = new Point(783, 314);
            btnCopyToClipboard.Margin = new Padding(3, 2, 3, 2);
            btnCopyToClipboard.Name = "btnCopyToClipboard";
            btnCopyToClipboard.Size = new Size(35, 22);
            btnCopyToClipboard.TabIndex = 6;
            btnCopyToClipboard.Text = "📋";
            btnCopyToClipboard.UseVisualStyleBackColor = true;
            btnCopyToClipboard.Click += btnCopyToClipboard_Click;
            // 
            // Base64StringEncodeDecodeCtl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnCopyToClipboard);
            Controls.Add(btnEncode);
            Controls.Add(label2);
            Controls.Add(txtResult);
            Controls.Add(label1);
            Controls.Add(txtSource);
            Controls.Add(btnDecode);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Base64StringEncodeDecodeCtl";
            Size = new Size(821, 847);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnDecode;
        private TextBox txtSource;
        private Label label1;
        private Label label2;
        private TextBox txtResult;
        private Button btnEncode;
        private Button btnCopyToClipboard;
    }
}
