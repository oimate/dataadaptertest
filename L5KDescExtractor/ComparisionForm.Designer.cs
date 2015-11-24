namespace L5KDescExtractor
{
    partial class ComparisionForm
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_rungs = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.acceptFromBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acceptFromCompareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_tags = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rungs)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tags)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(0, 527);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(792, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "parse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(792, 527);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_rungs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(784, 501);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Rungs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_rungs
            // 
            this.dgv_rungs.AllowUserToAddRows = false;
            this.dgv_rungs.AllowUserToDeleteRows = false;
            this.dgv_rungs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_rungs.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv_rungs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_rungs.Location = new System.Drawing.Point(3, 3);
            this.dgv_rungs.Name = "dgv_rungs";
            this.dgv_rungs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_rungs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_rungs.Size = new System.Drawing.Size(778, 495);
            this.dgv_rungs.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acceptFromBaseToolStripMenuItem,
            this.acceptFromCompareToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 48);
            // 
            // acceptFromBaseToolStripMenuItem
            // 
            this.acceptFromBaseToolStripMenuItem.Name = "acceptFromBaseToolStripMenuItem";
            this.acceptFromBaseToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.acceptFromBaseToolStripMenuItem.Text = "Accept from base";
            this.acceptFromBaseToolStripMenuItem.Click += new System.EventHandler(this.acceptFromBaseToolStripMenuItem_Click);
            // 
            // acceptFromCompareToolStripMenuItem
            // 
            this.acceptFromCompareToolStripMenuItem.Name = "acceptFromCompareToolStripMenuItem";
            this.acceptFromCompareToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.acceptFromCompareToolStripMenuItem.Text = "Accept from compare";
            this.acceptFromCompareToolStripMenuItem.Click += new System.EventHandler(this.acceptFromCompareToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_tags);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(784, 501);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tags";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_tags
            // 
            this.dgv_tags.AllowUserToAddRows = false;
            this.dgv_tags.AllowUserToDeleteRows = false;
            this.dgv_tags.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tags.ContextMenuStrip = this.contextMenuStrip1;
            this.dgv_tags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_tags.Location = new System.Drawing.Point(3, 3);
            this.dgv_tags.Name = "dgv_tags";
            this.dgv_tags.ReadOnly = true;
            this.dgv_tags.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_tags.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tags.Size = new System.Drawing.Size(778, 495);
            this.dgv_tags.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Location = new System.Drawing.Point(0, 550);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(792, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "save L5X";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ComparisionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Name = "ComparisionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ComparisionForm";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_rungs)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tags)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgv_rungs;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_tags;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem acceptFromBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acceptFromCompareToolStripMenuItem;
        private System.Windows.Forms.Button button2;
    }
}