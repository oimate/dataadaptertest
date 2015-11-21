namespace L5KDescExtractor
{
    partial class LogForm
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
            this.logfield = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // logfield
            // 
            this.logfield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logfield.Location = new System.Drawing.Point(0, 0);
            this.logfield.Multiline = true;
            this.logfield.Name = "logfield";
            this.logfield.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logfield.Size = new System.Drawing.Size(792, 573);
            this.logfield.TabIndex = 0;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.logfield);
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LogForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logfield;
    }
}