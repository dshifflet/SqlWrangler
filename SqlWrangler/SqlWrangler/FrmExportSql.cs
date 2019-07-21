using System;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SqliteTools;
using SqlWrangler.Properties;

namespace SqlWrangler
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
    public partial class FrmExportSql : Form
    {

        private DataTable Table { get; }

        public FrmExportSql(DataTable table)
        {
            if(table==null) throw new ArgumentNullException(nameof(table));
            InitializeComponent();

            chkInsert.Checked = Settings.Default.ExportInsert;
            chkCreate.Checked = Settings.Default.ExportCreate;
            Table = table;

            var b = Resources.database_export;
            var pIcon = b.GetHicon();
            var i = Icon.FromHandle(pIcon);
            Icon = i;
            i.Dispose();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please fill name fields", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!chkCreate.Checked && !chkInsert.Checked)
            {
                MessageBox.Show("Check either create or Insert", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                
            }
          
            var dialog = new SaveFileDialog
            {
                OverwritePrompt = true,
                Filter = "SQL File|*.sql",
                Title = "Save Data as SQL for SQLite",
                FileName =
                    $"{(chkCreate.Checked ? "create_" : "")}{(chkInsert.Checked ? "insert_" : "")}{txtSchema.Text.ToUpper()}{(string.IsNullOrEmpty(txtSchema.Text) ? "" : "_")}{txtName.Text.ToUpper()}.sql"
            };
            var dr = dialog.ShowDialog();

            if (Table != null && Table.Rows.Count > 0)
            {
                if (dr == DialogResult.OK)
                {
                    //var dataTable = (DataTable)dataGridView1.DataSource;
                    ExportSql(new FileInfo(dialog.FileName), Table, txtSchema.Text, txtName.Text,
                        chkCreate.Checked, chkInsert.Checked);
                    if (MessageBox.Show("Open in Notepad?", "Open in Notepad?", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        Process.Start("notepad.exe", dialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Data in Results.  Unable to export", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Settings.Default.LastSchema = txtSchema.Text;
            Settings.Default.ExportCreate = chkCreate.Checked;
            Settings.Default.ExportInsert = chkInsert.Checked;
            
            Close();
        }
        
        private void ExportSql(FileInfo fi, DataTable dataTable, string schema, string name, bool create, bool insert)
        {            
            var tbl = new Table(name, schema);
            if (fi.Exists)
            {
                fi.Delete();
            }
            using (var sw = new StreamWriter(fi.OpenWrite()))
            {
                if (create)
                {
                    Console.WriteLine("");
                    sw.WriteLine("/* CREATE TABLE {0} */", tbl.ActualName);
                    Console.WriteLine("");
                    sw.Write(tbl.CreateSql(dataTable));
                    sw.WriteLine("");
                }

                if (!insert) return;

                Console.WriteLine("");
                sw.WriteLine("/* INSERT TABLE {0} */", tbl.ActualName);
                Console.WriteLine("");
                sw.Write(tbl.GenerateImportDataSql(dataTable));
                sw.WriteLine("");
            }
        }

        private void FrmExportSql_Load(object sender, EventArgs e)
        {
            txtSchema.Text = Settings.Default.LastSchema;
        }

    }
}
