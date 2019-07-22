using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace SqlWrangler.Services
{
    internal class SnippetMenuBuilder
    {
        public void BuildSnippetMenu(List<TextSnippet> snippets, 
            ToolStripMenuItem wizardToolStripMenuItem, Keys modifierKeys,
            Scintilla element, DataGridView dataGridView)
        {
            wizardToolStripMenuItem.DropDownItems.Clear();
            foreach (var item in snippets)
            {
                var theItem = item;
                var ts = new ToolStripMenuItem(item.Name, null, delegate (object sender, EventArgs args)
                {
                    if (modifierKeys == Keys.Control)
                    {
                        var checkTs = (ToolStripMenuItem)sender;

                        var dr = MessageBox.Show(
                            $"Remove item \"{checkTs.Text}\"?",
                            "Remove Item",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            wizardToolStripMenuItem.DropDownItems.Remove(checkTs);
                            snippets.Remove((TextSnippet)checkTs.Tag);
                        }
                    }
                    else
                    {
                        element.InsertText(element.SelectionStart, theItem.Text);
                    }
                })
                { Tag = item };
                wizardToolStripMenuItem.DropDownItems.Add(ts);
            }

            var insertFieldSnippet = new ToolStripMenuItem("Insert Field List", null, delegate
            {
                InsertFields(element, dataGridView);
            });

            var addSnippet = new ToolStripMenuItem("Add Snippet", null, delegate
            {
                if (element.SelectedText.Length == 0)
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
                    Text = element.SelectedText
                };
                snippets.Add(snippet);
                BuildSnippetMenu(snippets, wizardToolStripMenuItem, 
                    modifierKeys, element, dataGridView);
            });
            wizardToolStripMenuItem.DropDownItems.Add(insertFieldSnippet);
            wizardToolStripMenuItem.DropDownItems.Add(addSnippet);
        }

        private void InsertFields(Scintilla element, DataGridView dataGridView)
        {
            if (dataGridView.DataSource != null && dataGridView.RowCount > 0)
            {
                var dataTable = (DataTable)dataGridView.DataSource;
                var schema = dataTable.CreateDataReader().GetSchemaTable();
                var sb = new StringBuilder();

                foreach (DataRow row in schema.Rows)
                {
                    sb.Append($"\t{row["ColumnName"]},\r\n");
                }
                var txt = sb.ToString();
                if (txt.EndsWith(",\r\n"))
                {
                    txt = txt.Substring(0, txt.Length - 3).ToLower();
                }
                element.InsertText(element.SelectionStart, txt);
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
    }
}
