using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using SqlWrangler.Properties;

namespace SqlWrangler
{
    public partial class FrmMain : Form
    {

        public IDbConnection Connection { get; set; }
        public FrmLogin Login { get; set; }
        public List<TextSnippet> Snippets = new List<TextSnippet>();

        public FrmMain()
        {
            InitializeComponent();
            GetSnippets();


            var b = Resources.toolbox;
            var pIcon = b.GetHicon();
            var i = Icon.FromHandle(pIcon);
            Icon = i;
            i.Dispose();

        }


        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            NewSqlForm();
        }

        private void NewSqlForm()
        {
            var newForm = new SqlClient(Snippets)
            {
                Connection = Connection,
                MdiParent = this
            };
            newForm.Show();            
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Connection.Close();
            Connection.Dispose();
            Login.Close();            
            PersistSnippets();
        }

        private void PersistSnippets()
        {
            var fi = new FileInfo("snippets.xml");
            if (fi.Exists) fi.Delete();
            var serializer = new XmlSerializer(typeof (List<TextSnippet>));
            using (var tw = XmlWriter.Create(fi.OpenWrite()))
            {
                serializer.Serialize(tw, Snippets);
            }
        }

        protected override bool ProcessCmdKey(ref Message message, Keys keys)
        {
            switch (keys)
            {
                case Keys.F1:
                {
                    NewSqlForm();
                    break;
                }
            }
            return base.ProcessCmdKey(ref message, keys);
        }

        private void GetSnippets()
        {
            var xmlfi = new FileInfo("snippets.xml");
            if (xmlfi.Exists)
            {
                var serializer = new XmlSerializer(typeof (List<TextSnippet>));
                if (xmlfi.Length > 0)
                {
                    using (var sr = new StreamReader(xmlfi.OpenRead()))
                    {
                        Snippets = (List<TextSnippet>) serializer.Deserialize(sr);
                    }
                }
                return;
            }

            //Old snippets text file...
            var fi = new FileInfo("snippets.txt");

            if (!fi.Exists) return;
            using (var sr = new StreamReader(fi.OpenRead()))
            {
                var s = sr.ReadLine();
                while (s != null)
                {
                    try
                    {
                        var idx = s.IndexOf(",", StringComparison.Ordinal);
                        if (idx > -1)
                        {
                            var splits = new string[2];
                            splits[0] = s.Substring(0, idx);
                            splits[1] = s.Substring(idx + 1);
                            var snip = new TextSnippet()
                            {
                                Name = splits[0],
                                Text = splits[1].Replace(@"\r", "\r").Replace(@"\n", "\n").Replace(@"\t", "\t")
                            };
                            Snippets.Add(snip);
                        }
                        s = sr.ReadLine();
                    }
                    catch
                    {
                        //
                    }
                }
            }
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format(@"
                CURRENT CONNECTION
                {0}
                ---------------------------                
                SQL WRANGLER


            ", Connection.ConnectionString), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
