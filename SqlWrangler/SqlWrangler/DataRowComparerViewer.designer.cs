namespace SqlWrangler
{
    partial class DataRowComparerViewer
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCheckFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllCheckFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView2);
            this.splitContainer1.Size = new System.Drawing.Size(799, 409);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(0, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(799, 184);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setKeyToolStripMenuItem,
            this.setCheckFieldsToolStripMenuItem,
            this.clearAllCheckFieldsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setKeyToolStripMenuItem
            // 
            this.setKeyToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.key;
            this.setKeyToolStripMenuItem.Name = "setKeyToolStripMenuItem";
            this.setKeyToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.setKeyToolStripMenuItem.Text = "Set Key";
            this.setKeyToolStripMenuItem.Click += new System.EventHandler(this.setKeyToolStripMenuItem_Click);
            // 
            // setCheckFieldsToolStripMenuItem
            // 
            this.setCheckFieldsToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.pipette;
            this.setCheckFieldsToolStripMenuItem.Name = "setCheckFieldsToolStripMenuItem";
            this.setCheckFieldsToolStripMenuItem.Size = new System.Drawing.Size(120, 20);
            this.setCheckFieldsToolStripMenuItem.Text = "Set Check Fields";
            this.setCheckFieldsToolStripMenuItem.Click += new System.EventHandler(this.setCheckFieldsToolStripMenuItem_Click);
            // 
            // clearAllCheckFieldsToolStripMenuItem
            // 
            this.clearAllCheckFieldsToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.cross;
            this.clearAllCheckFieldsToolStripMenuItem.Name = "clearAllCheckFieldsToolStripMenuItem";
            this.clearAllCheckFieldsToolStripMenuItem.Size = new System.Drawing.Size(148, 20);
            this.clearAllCheckFieldsToolStripMenuItem.Text = "Clear All Check Fields";
            this.clearAllCheckFieldsToolStripMenuItem.Click += new System.EventHandler(this.clearAllCheckFieldsToolStripMenuItem_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(799, 197);
            this.dataGridView2.TabIndex = 0;
            // 
            // DataRowComparerViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 409);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DataRowComparerViewer";
            this.Text = "DataComparer Window";
            this.Load += new System.EventHandler(this.DataRowComparerViewer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCheckFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllCheckFieldsToolStripMenuItem;


    }
}

