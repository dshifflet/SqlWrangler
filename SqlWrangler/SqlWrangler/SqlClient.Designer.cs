namespace SqlWrangler
{
    partial class SqlClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "_task")]
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
            this.textBox1 = new ScintillaNET.Scintilla();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.executeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toDaveSqlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WizardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.compareRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(987, 519);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.HScrollBar = false;
            this.textBox1.Location = new System.Drawing.Point(0, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(987, 216);
            this.textBox1.TabIndex = 2;
            this.textBox1.UseTabs = false;
            this.textBox1.WrapMode = ScintillaNET.WrapMode.Word;
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp_1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.executeToolStripMenuItem,
            this.wizardToolStripMenuItem,
            this.toCSVToolStripMenuItem,
            this.toXMLToolStripMenuItem,
            this.toJSONToolStripMenuItem,
            this.cancelToolStripMenuItem,
            this.colorsToolStripMenuItem,
            this.toDaveSqlToolStripMenuItem,
            this.WizardToolStripMenuItem1,
            this.compareRowsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(987, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(987, 275);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.statusStrip1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(979, 249);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Results";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(973, 221);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 224);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(973, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(979, 249);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(973, 243);
            this.textBox2.TabIndex = 0;
            // 
            // executeToolStripMenuItem
            // 
            this.executeToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.wand_magic;
            this.executeToolStripMenuItem.Name = "executeToolStripMenuItem";
            this.executeToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.executeToolStripMenuItem.Text = "&Execute (F9)";
            this.executeToolStripMenuItem.Click += new System.EventHandler(this.executeToolStripMenuItem_Click);
            // 
            // wizardToolStripMenuItem
            // 
            this.wizardToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.clipboard_text;
            this.wizardToolStripMenuItem.Name = "wizardToolStripMenuItem";
            this.wizardToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.wizardToolStripMenuItem.Text = "&Snippets";
            // 
            // toCSVToolStripMenuItem
            // 
            this.toCSVToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.table_excel;
            this.toCSVToolStripMenuItem.Name = "toCSVToolStripMenuItem";
            this.toCSVToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.toCSVToolStripMenuItem.Text = "To &CSV";
            this.toCSVToolStripMenuItem.Click += new System.EventHandler(this.toCSVToolStripMenuItem_Click);
            // 
            // toXMLToolStripMenuItem
            // 
            this.toXMLToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.flask__arrow;
            this.toXMLToolStripMenuItem.Name = "toXMLToolStripMenuItem";
            this.toXMLToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.toXMLToolStripMenuItem.Text = "To &XML";
            this.toXMLToolStripMenuItem.Click += new System.EventHandler(this.toXMLToolStripMenuItem_Click);
            // 
            // toJSONToolStripMenuItem
            // 
            this.toJSONToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.flask__arrow;
            this.toJSONToolStripMenuItem.Name = "toJSONToolStripMenuItem";
            this.toJSONToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.toJSONToolStripMenuItem.Text = "To &JSON";
            this.toJSONToolStripMenuItem.Click += new System.EventHandler(this.toJSONToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Enabled = false;
            this.cancelToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.cross;
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // colorsToolStripMenuItem
            // 
            this.colorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.whiteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.blueToolStripMenuItem,
            this.toolStripMenuItem3,
            this.greenToolStripMenuItem});
            this.colorsToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.color;
            this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
            this.colorsToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.colorsToolStripMenuItem.Text = "Colors";
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Checked = true;
            this.defaultToolStripMenuItem.CheckOnClick = true;
            this.defaultToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.CheckOnClick = true;
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.whiteToolStripMenuItem.Text = "White";
            this.whiteToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem2.Text = "Yellow";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.CheckOnClick = true;
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem3.Text = "Red";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.CheckOnClick = true;
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.greenToolStripMenuItem.Text = "Green";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // toDaveSqlToolStripMenuItem
            // 
            this.toDaveSqlToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.database_export;
            this.toDaveSqlToolStripMenuItem.Name = "toDaveSqlToolStripMenuItem";
            this.toDaveSqlToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.toDaveSqlToolStripMenuItem.Text = "Export to SQLite";
            this.toDaveSqlToolStripMenuItem.Click += new System.EventHandler(this.toDaveSqlToolStripMenuItem_Click);
            // 
            // WizardToolStripMenuItem1
            // 
            this.WizardToolStripMenuItem1.Image = global::SqlWrangler.Properties.Resources.molecule;
            this.WizardToolStripMenuItem1.Name = "WizardToolStripMenuItem1";
            this.WizardToolStripMenuItem1.Size = new System.Drawing.Size(100, 20);
            this.WizardToolStripMenuItem1.Text = "Generate C#";
            this.WizardToolStripMenuItem1.Click += new System.EventHandler(this.WizardToolStripMenuItem1_Click);
            // 
            // compareRowsToolStripMenuItem
            // 
            this.compareRowsToolStripMenuItem.Image = global::SqlWrangler.Properties.Resources.contrast;
            this.compareRowsToolStripMenuItem.Name = "compareRowsToolStripMenuItem";
            this.compareRowsToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.compareRowsToolStripMenuItem.Text = "Compare Rows";
            this.compareRowsToolStripMenuItem.Click += new System.EventHandler(this.compareRowsToolStripMenuItem_Click);
            // 
            // SqlClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 519);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SqlClient";
            this.Text = "New Sql Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SqlClient_FormClosing);
            this.Load += new System.EventHandler(this.SqlClient_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem executeToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ToolStripMenuItem wizardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toCSVToolStripMenuItem;
        private ScintillaNET.Scintilla textBox1;
        private System.Windows.Forms.ToolStripMenuItem toXMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toDaveSqlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WizardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem compareRowsToolStripMenuItem;
    }
}