namespace L5KDescExtractor
{
    partial class MainWindow
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
            this.bStart = new System.Windows.Forms.Button();
            this.bL5kFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bStart
            // 
            this.bStart.Enabled = false;
            this.bStart.Location = new System.Drawing.Point(12, 41);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bL5kFile
            // 
            this.bL5kFile.Location = new System.Drawing.Point(12, 12);
            this.bL5kFile.Name = "bL5kFile";
            this.bL5kFile.Size = new System.Drawing.Size(75, 23);
            this.bL5kFile.TabIndex = 1;
            this.bL5kFile.Text = "L5K file";
            this.bL5kFile.UseVisualStyleBackColor = true;
            this.bL5kFile.Click += new System.EventHandler(this.bL5kFile_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 270);
            this.Controls.Add(this.bL5kFile);
            this.Controls.Add(this.bStart);
            this.Name = "MainWindow";
            this.Text = "oiajf";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bL5kFile;
    }
}

