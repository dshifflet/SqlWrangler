using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using ScintillaNET;
using SqlWrangler.Properties;

namespace SqlWrangler
{
    public partial class SqlClient : Form
    {
        public IDbConnection Connection { get; set; }
        public List<TextSnippet> Snippets { get; set; }
        private IDbCommand _command;
        private Task _task;

        public SqlClient(List<TextSnippet> snippets)
        {
            Snippets = snippets;
            InitializeComponent();
            ScintallaStyle();
            BuildSnippetMenu();
            menuStrip1.BackColor = SystemColors.Control;

            var b = Resources.script;
            var pIcon = b.GetHicon();
            var i = Icon.FromHandle(pIcon);
            Icon = i;
            i.Dispose();

            
        }

        private void BuildSnippetMenu()
        {
            wizardToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in Snippets)
            {
                var theItem = item;
                var ts = new ToolStripMenuItem(item.Name, null, delegate(object sender, EventArgs args)
                {
                    if (ModifierKeys == Keys.Control)
                    {
                        var checkTs = (ToolStripMenuItem) sender;

                        var dr = MessageBox.Show(
                            string.Format("Remove item \"{0}\"?", checkTs.Text),
                            "Remove Item",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            wizardToolStripMenuItem.DropDownItems.Remove(checkTs);
                            Snippets.Remove((TextSnippet) checkTs.Tag);
                        }
                    }
                    else
                    {
                        textBox1.InsertText(textBox1.SelectionStart, theItem.Text);
                    }
                }) {Tag = item};
                wizardToolStripMenuItem.DropDownItems.Add(ts);
            }

            var insertFieldSnippet = new ToolStripMenuItem("Insert Field List", null, delegate
            {
                InsertFields();
            });

            var addSnippet = new ToolStripMenuItem("Add Snippet", null, delegate
            {
                if (textBox1.SelectedText.Length == 0)
                {
                    MessageBox.Show("You might want to try selecting some text.", "Select some Text",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var frmName = new FrmNameInput();
                frmName.ShowDialog();
                
                if (string.IsNullOrEmpty(frmName.UserName)) return;
                
                var snippet = new TextSnippet()
                {
                    Name = frmName.UserName,
                    Text = textBox1.SelectedText
                };
                Snippets.Add(snippet);
                BuildSnippetMenu();
            });
            wizardToolStripMenuItem.DropDownItems.Add(insertFieldSnippet);             
            wizardToolStripMenuItem.DropDownItems.Add(addSnippet);             
        }

        private void ScintallaStyle()
        {
            textBox1.StyleResetDefault();
            textBox1.Styles[Style.Default].Font = "Consolas";
            textBox1.Styles[Style.Default].Size = 12;       
            textBox1.StyleClearAll();
            
            textBox1.Styles[Style.Sql.Default].ForeColor = Color.Silver;
            textBox1.Styles[Style.Sql.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            textBox1.Styles[Style.Sql.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            textBox1.Styles[Style.Sql.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            textBox1.Styles[Style.Sql.Number].ForeColor = Color.Olive;
            textBox1.Styles[Style.Sql.Word].ForeColor = Color.Blue;
            textBox1.Styles[Style.Sql.Word2].ForeColor = Color.Red;
            textBox1.Styles[Style.Sql.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            textBox1.Styles[Style.Sql.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            //textBox1.Styles[Style.Sql.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            //textBox1.Styles[Style.Sql.StringEol].BackColor = Color.Pink;
            textBox1.Styles[Style.Sql.Operator].ForeColor = Color.Purple;
            //textBox1.Styles[Style.Sql.Preprocessor].ForeColor = Color.Maroon;            
            textBox1.Lexer = Lexer.Sql;
            textBox1.SetKeywords(0, "select from where inner join outer left right on order by group update insert delete truncate");
            textBox1.SetKeywords(1, "commit rollback");
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteSql();
        }

        private string GetCurrentlyHighlighted()
        {
            return textBox1.SelectedText.Length > 0 ? textBox1.SelectedText : textBox1.Text;
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private void ExecuteSql()
        {
            IsExecuting();
            var sql = GetCurrentlyHighlighted();

            _task = new Task(() =>
            {
                try
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    _command = Connection.CreateCommand();
                    _command.CommandText = sql;
                    using (var reader = _command.ExecuteReader())
                    {
                        var thatReader = reader;
                        var table = new DataTable();
                        //table.RowChanged += table_RowChanged;

                        table.BeginLoadData();
                        table.Load(reader); //this takes a while                        
                        table.EndLoadData();

                        Invoke(new Action(() => { UpdateGrid(table, thatReader); }));
                    }
                    _command = null;
                    Invoke(new Action(() => { DoneUpdatingGrid(stopwatch, sql); }));
                    stopwatch.Stop();
                    Invoke(new Action(DoneExecuting));
                }
                catch (OracleException ex)
                {
                    //TODO Get the line number somehow.  I have no idea if that is possible but would be nice.
                    Invoke(new Action(() => { ShowError(ex); }));
                }
                catch (Exception ex)
                {                    
                    Invoke(new Action(() => { ShowError(ex); }));                    
                }
            });
            _task.Start();
        }

        private void toCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null && dataGridView1.RowCount>0)
            {
                var dialog = new SaveFileDialog
                {
                    OverwritePrompt = true,
                    Filter = "CSV File|*.csv",
                    Title = "Save Data as CSV",
                    FileName = string.Concat(Guid.NewGuid().ToString(), ".csv")
                };
                var dr = dialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    ExportCsv(new FileInfo(dialog.FileName));
                    if (MessageBox.Show("Open in Excel?", "Open in Excel?", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        Process.Start("excel.exe", dialog.FileName);
                    }
                }                
            }
        }

        private void ExportCsv(FileInfo fi)
        {
            
            var dataTable = (DataTable) dataGridView1.DataSource;

            var lines = new List<string>();

            string[] columnNames = dataTable.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();

            var header = string.Join(",", columnNames);
            lines.Add(header);

            var valueLines = dataTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);

            File.WriteAllLines(fi.FullName, lines);            
        }

        private void textBox1_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                ExecuteSql();
            }
            if (e.KeyCode == Keys.Escape)
            {
                cancelToolStripMenuItem_Click(null, null);
            }
        }

        private void toXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveExternalFile(true);
        }


        private void SaveExternalFile(bool isXml)
        {
            if (dataGridView1.DataSource != null && dataGridView1.RowCount > 0)
            {
                var dialog = new SaveFileDialog {OverwritePrompt = true};
                var defaultExtension = ".json";
                dialog.Filter = "JSON File|*.json";
                dialog.Title = "Save Data as JSON";                    
                if (isXml)
                {
                    dialog.Filter = "XML File|*.xml";
                    dialog.Title = "Save Data as XML";
                    defaultExtension = ".xml";
                }

                var dataTable = (DataTable)dataGridView1.DataSource;
                var nm = Guid.NewGuid().ToString();
                if (!string.IsNullOrEmpty(dataTable.TableName))
                {
                    nm = dataTable.TableName;
                }
                else
                {
                    dataTable.TableName = Guid.NewGuid().ToString();
                }

                dialog.FileName = string.Concat(nm, defaultExtension);
                
                var dr = dialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (isXml)
                    {
                        dataTable.WriteXml(dialog.FileName, XmlWriteMode.WriteSchema);
                    }
                    else
                    {
                        var output = JsonConvert.SerializeObject(dataTable);
                        using (var sw = new StreamWriter(new FileInfo(dialog.FileName).OpenWrite()))
                        {
                            sw.Write(output);
                        }
                    }
                    
                    if (MessageBox.Show("Open in Notepad?", "Open in Notepad?", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question)
                        == DialogResult.Yes)
                    {
                        Process.Start("notepad", dialog.FileName);
                    }
                }
            }            
        }

        private void IsExecuting()
        {
            tabControl1.SelectTab(1);
            toolStripStatusLabel1.Text = "Executing";
            textBox2.Text = "Executing\r\n";

            foreach (ToolStripItem item in menuStrip1.Items)
            {                
                item.Enabled = item.Text.Equals("cancel", StringComparison.OrdinalIgnoreCase);
            }            
        }

        private void DoneExecuting()
        {
            foreach (ToolStripItem item in menuStrip1.Items)
            {
                item.Enabled = !item.Text.Equals("cancel", StringComparison.OrdinalIgnoreCase);
            }            
        }

        private void ShowError(Exception ex)
        {
            textBox2.Text += ex.Message + "\r\n\r\n" + ex;
            tabControl1.SelectTab(1);
            toolStripStatusLabel1.Text = "";
            textBox1.Focus();            
        }


        private void UpdateGrid(DataTable table, IDataReader reader)
        {            
            dataGridView1.DataSource = table;
            foreach (DataGridViewColumn dc in dataGridView1.Columns)
            {
                if (dc.ValueType == typeof (DateTime))
                {
                    dc.DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
                }
            }

            toolStripStatusLabel1.Text = string.Format("Rows found {0}", dataGridView1.RowCount);
            if (reader.RecordsAffected >= 0)
            {
                textBox2.Text += string.Format("Rows affected {0}\r\n", reader.RecordsAffected);
            }            
        }


        private void DoneUpdatingGrid(Stopwatch stopwatch, string sql)
        {
            textBox2.Text += string.Format("Executed in {0} ms\r\n", stopwatch.ElapsedMilliseconds);

            if (dataGridView1.RowCount > 0)
            {
                tabControl1.SelectTab(0);
            }

            Text = sql.Length > 50 ? sql.Substring(0, 50) : sql;
            textBox1.Focus();            
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_command != null)
            {
                _command.Cancel();
                _command = null;
            }
            DoneExecuting();
        }

        private void toDaveSqlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                return;
            }
            var frm = new FrmExportSql((DataTable) dataGridView1.DataSource);
            frm.ShowDialog();
        }
        
        private void SqlClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Connection.Close();
            //Connection.Dispose();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void toJSONToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveExternalFile(false);
        }

        private void SqlClient_Load(object sender, EventArgs e)
        {

        }

        private void Uncheck(object sender)
        {
            foreach (var control in colorsToolStripMenuItem.DropDownItems)
            {
                if (control is ToolStripMenuItem && sender is ToolStripMenuItem)
                {
                    var tsmi = (ToolStripMenuItem)control;
                    if (tsmi != (ToolStripMenuItem)sender)
                    {
                        if (tsmi.CheckOnClick)
                        {
                            tsmi.Checked = false;
                        }
                    }
                }
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uncheck(sender);
            var tsi = (ToolStripMenuItem) sender;
            switch (tsi.Text)
            {
                case "Default":
                    menuStrip1.BackColor = SystemColors.Control;
                    break;
                case "White":
                    menuStrip1.BackColor = Color.White;
                    break;
                case "Yellow":
                    menuStrip1.BackColor = Color.LightYellow;
                    break;
                case "Blue":
                    menuStrip1.BackColor = Color.LightBlue;
                    break;
                case "Red":
                    menuStrip1.BackColor = Color.LightCoral;
                    break;
                case "Green":
                    menuStrip1.BackColor = Color.LightGreen;
                    break;
            }
        }

        //todo refactor this...
        private void WizardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null && dataGridView1.RowCount > 0)
            {
                var dialog = new SaveFileDialog {OverwritePrompt = true};
                var defaultExtension = ".cs";
                dialog.Filter = "CS File|*.cs";
                dialog.Title = "Save Data as CS";

                var dataTable = (DataTable) dataGridView1.DataSource;
                var nm = Guid.NewGuid().ToString();
                if (!string.IsNullOrEmpty(dataTable.TableName))
                {
                    nm = dataTable.TableName;
                }
                else
                {
                    dataTable.TableName = Guid.NewGuid().ToString();
                }

                dialog.FileName = string.Concat(nm, defaultExtension);

                var dr = dialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    var fi = new FileInfo(dialog.FileName);
                    if (fi.Exists)
                    {
                        fi.Delete();
                    }

                    using (var sw = new StreamWriter(fi.OpenWrite()))
                    {
                        var wiz = new Wizard();
                        wiz.WriteCsWizard(dataTable, sw, "TODO", GetCurrentlyHighlighted());
                    }

                    if (MessageBox.Show("Open in Notepad?", "Open in Notepad?", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Process.Start("notepad", dialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                        "Please execute some SQL that results in rows to use this feature",
                        "Need Data for Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
            }
        }

        private void InsertFields()
        {
            if (dataGridView1.DataSource != null && dataGridView1.RowCount > 0)
            {
                var dataTable = (DataTable)dataGridView1.DataSource;
                var schema = dataTable.CreateDataReader().GetSchemaTable();
                var sb = new StringBuilder();
                
                foreach (DataRow row in schema.Rows)                   
                {
                    sb.Append(string.Format("\t{0},\r\n", row["ColumnName"]));
                }
                var txt = sb.ToString();
                if (txt.EndsWith(",\r\n"))
                {
                    txt = txt.Substring(0, txt.Length - 3).ToLower();
                }
                textBox1.InsertText(textBox1.SelectionStart, txt);
            }
            else
            {
                MessageBox.Show(
                        "Please execute some SQL that results in rows to use this feature",
                        "Need Data for Wizard",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);                
            }
        }

        private void compareRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataTable = (DataTable)dataGridView1.DataSource;
            if (dataTable == null)
            {
                MessageBox.Show(
                        "Please execute some SQL that results in rows to use this feature",
                        "Need Data for DataComparer",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                return;
            }
            var wnd = new DataRowComparerViewer(dataTable, "Data Comparer");
            wnd.MdiParent = MdiParent;
            wnd.Show();
        }
    }

    public class TextSnippet
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }

}
