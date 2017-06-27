using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SqlWrangler.Properties;

namespace SqlWrangler
{
    public partial class DataRowComparerViewer : Form
    {
        private readonly DataTable _table;
        private readonly string _title;
        private string _mode = "";
        private int _keyIndex;

        private readonly List<int> _lookAtColumns;
        private List<RowComparerResult> _comparerResults;       

        public DataRowComparerViewer(DataTable table, string title)
        {
            var b = Resources.contrast;
            var pIcon = b.GetHicon();
            var i = Icon.FromHandle(pIcon);
            Icon = i;
            i.Dispose();


            if (table == null) throw new ArgumentNullException("table");
            
            InitializeComponent();
            _title = title;
            _table = table;
            _lookAtColumns = new List<int>();
            //dataGridView1.EnableHeadersVisualStyles = false;
            FillGrid();
            ToggleMenuItems();
        }

        private void FillGrid()
        {
            dataGridView2.DataSource = null;
            if (!string.IsNullOrEmpty(_title))
            {
                Text = _title;
            }
            _comparerResults = new List<RowComparerResult>();

            foreach (DataRow row in _table.Rows)
            {
                _comparerResults.Add(Compare(row, _table, _table.Columns[_keyIndex].ColumnName));
            }
                        
            dataGridView1.CellFormatting+=dataGridView1_CellFormatting;
            dataGridView1.DataSource = _table;

            foreach (DataGridViewColumn dc in dataGridView1.Columns)
            {
                if (dc.ValueType == typeof(DateTime))
                {
                    dc.DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
                }
            }                   

         
        }

        void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= _table.Rows.Count) return;

            var id = _table.Rows[e.RowIndex][_keyIndex].ToString();
            e.CellStyle.BackColor =  _comparerResults.Any(o => o.Key.Equals(id) && o.Duplicates.Any()) 
                ? Color.AliceBlue : Color.White;
            
        }

        private RowComparerResult Compare(DataRow reference, DataTable table, string keyField)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                var result = new RowComparerResult {Key = reference[keyField].ToString()};
                var matches = new List<DataRow>();
                foreach (DataRow row in table.Rows)
                {
                    var id = row[keyField].ToString();

                    //don't look at yourself
                    if (result.Key == id) continue;

                    var match = _lookAtColumns.Any();
                    var idx = 0;
                    foreach (DataColumn column in table.Columns)
                    {
                        if (_lookAtColumns.Contains(idx))
                        {
                            var colName = column.ColumnName;
                            if (!column.ColumnName.Equals(keyField, StringComparison.OrdinalIgnoreCase))
                            {
                                //check if they don't match
                                if (!reference[colName].ToString().Equals(row[colName].ToString(),
                                    StringComparison.OrdinalIgnoreCase))
                                {
                                    match = false;
                                }
                            }
                        }
                        idx++;
                    }
                    if (match)
                    {
                        matches.Add(row);
                    }
                }
                result.Duplicates = matches.ToArray();
                return result;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DisplayDetailForRow(e.RowIndex);
        }

        private void DisplayDetailForRow(int rowIndex)
        {
            var id = _table.Rows[rowIndex][_keyIndex].ToString();
            var check = _comparerResults.FirstOrDefault(o => o.Key.Equals(id));
            var list = new List<DataRow>();
            list.Add(_table.Rows[rowIndex]);
            if (check != null)
            {
                if (check.Duplicates.Any())
                {
                    list.AddRange(check.Duplicates);
                }

                dataGridView2.DataSource = list.CopyToDataTable();
                foreach (DataGridViewColumn dc in dataGridView2.Columns)
                {
                    if (dc.ValueType == typeof(DateTime))
                    {
                        dc.DefaultCellStyle.Format = "MM/dd/yyyy HH:mm:ss";
                    }
                }                   
            }                
        }

        private void setKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mode.Equals("setkey"))
            {
                _mode = "";
            }
            else
            {
                _mode = "setkey";    
            }
            ToggleMenuItems();
        }

        private void ToggleMenuItems()
        {
            setKeyToolStripMenuItem.BackColor = DefaultBackColor;
            setCheckFieldsToolStripMenuItem.BackColor = DefaultBackColor;
            switch (_mode)
            {
                case "setkey":
                    setKeyToolStripMenuItem.BackColor = Color.LightBlue;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }                    
                    break;
                case "setcheck":
                    setCheckFieldsToolStripMenuItem.BackColor = Color.LightBlue;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }                    
                    break;
                default:
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        column.SortMode = DataGridViewColumnSortMode.Automatic;
                    }     
                    break;
            }
            var idx = 0;
            foreach (DataGridViewColumn dgc in dataGridView1.Columns)
            {
                if (idx == _keyIndex)
                {
                    dgc.HeaderCell.Style.Font = new Font(dgc.HeaderCell.DataGridView.DefaultCellStyle.Font,
                        FontStyle.Bold);
                }
                else
                {
                    dgc.HeaderCell.Style.Font = new Font(dgc.HeaderCell.DataGridView.DefaultCellStyle.Font,
                        FontStyle.Regular);                    
                }
                if (_lookAtColumns.Contains(idx))
                {
                    dgc.HeaderCell.Style.Font = new Font(dgc.HeaderCell.DataGridView.DefaultCellStyle.Font,
                        FontStyle.Italic | FontStyle.Underline);                    
                }
                else if (idx != _keyIndex)
                {                   
                    dgc.HeaderCell.Style.Font = new Font(dgc.HeaderCell.DataGridView.DefaultCellStyle.Font,
                        FontStyle.Regular);                                            
                }
                idx++;
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (string.IsNullOrEmpty(_mode)) return;

            if (_mode == "setkey")
            {
                _keyIndex = e.ColumnIndex;
                _mode = "";
                _lookAtColumns.Remove(_keyIndex);
            }

            if (_mode == "setcheck")
            {
                if (e.ColumnIndex == _keyIndex) return;

                if (_lookAtColumns.Contains(e.ColumnIndex))
                {
                    _lookAtColumns.Remove(e.ColumnIndex);
                }
                else
                {
                    _lookAtColumns.Add(e.ColumnIndex);
                }
            }

            FillGrid();
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DisplayDetailForRow(dataGridView1.SelectedRows[0].Index);
            }
            ToggleMenuItems();
        }

        private void setCheckFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mode.Equals("setcheck"))
            {
                _mode = "";
            }
            else
            {
                _mode = "setcheck";
            }
            ToggleMenuItems();
        }

        private void clearAllCheckFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _lookAtColumns.Clear();
            FillGrid();
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DisplayDetailForRow(dataGridView1.SelectedRows[0].Index);
            }
            ToggleMenuItems();
        }

        private void DataRowComparerViewer_Load(object sender, EventArgs e)
        {

        }
    }

    public class RowComparerResult
    {
        public string Key { get; set; }
        public DataRow[] Duplicates { get; set; }
    }
}
