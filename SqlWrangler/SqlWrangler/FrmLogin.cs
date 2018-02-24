using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using SqlWrangler.Properties;
using Configuration = NHibernate.Cfg.Configuration;
using Settings = SqlWrangler.Properties.Settings;

namespace SqlWrangler
{
    public partial class FrmLogin : Form
    {
        private readonly List<string> _connectionNames = new List<string>();

        public FrmLogin()
        {
            InitializeComponent();
            var b = Resources.key;
            var pIcon = b.GetHicon();
            var i = Icon.FromHandle(pIcon);
            Icon = i;
            i.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var fm = new FrmMain();
                //Create connection
                var cns = comboBox1.Items[comboBox1.SelectedIndex].ToString();

                if (panel1.Visible)
                {
                    cns = cns.Replace("{username}", txtUserName.Text);
                    Settings.Default.LastUserName = txtUserName.Text;
                    cns = cns.Replace("{password}", txtPassword.Text);
                }
                
                var cn = CreateNhSessionFactory(cns).OpenStatelessSession().Connection;
                fm.Connection = cn;
                fm.Login = this;
                fm.Text = string.Format("Sql Wrangler connected to {0}",_connectionNames[comboBox1.SelectedIndex]);
                fm.Show();
                Hide();
                Settings.Default.LastConnection = comboBox1.SelectedIndex;
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            foreach (ConnectionStringSettings cns in ConfigurationManager.ConnectionStrings)
            {
                comboBox1.Items.Add(cns.ToString());
                _connectionNames.Add(cns.Name);
            }
            if (Settings.Default.LastConnection < comboBox1.Items.Count)
            {
                comboBox1.SelectedIndex = Settings.Default.LastConnection;    
            }
        }


        private ISessionFactory CreateNhSessionFactory(string connectionString)
        {
            var nhConfiguration = new Configuration();
            nhConfiguration.Cache(properties => properties.Provider<HashtableCacheProvider>());
            
            if (connectionString.ToUpper().Contains("SERVER="))
            {
                //is sql server
                nhConfiguration.DataBaseIntegration(dbi =>
                {
                    dbi.Dialect<MsSql2008Dialect>();
                    dbi.Driver<Sql2008ClientDriver>();
                    dbi.ConnectionProvider<DriverConnectionProvider>();
                    dbi.IsolationLevel = IsolationLevel.ReadCommitted;
                    dbi.ConnectionString = connectionString;
                    dbi.Timeout = 60;
                    dbi.LogFormattedSql = false;
                    dbi.LogSqlInConsole = false;
                });
            }
            else if (connectionString.ToUpper().Contains("OLEDB."))
            {
                //is sql lite
                nhConfiguration.DataBaseIntegration(dbi =>
                {
                    dbi.Dialect<GenericDialect>();
                    dbi.Driver<OleDbDriver>();
                    dbi.ConnectionProvider<DriverConnectionProvider>();
                    dbi.IsolationLevel = IsolationLevel.ReadCommitted;
                    dbi.ConnectionString = connectionString;
                    dbi.Timeout = 60;
                    dbi.LogFormattedSql = false;
                    dbi.LogSqlInConsole = false;
                });
            }
            else if (connectionString.ToUpper().EndsWith(".DB"))
            {
                //is sql lite
                nhConfiguration.DataBaseIntegration(dbi =>
                {
                    dbi.Dialect<SQLiteDialect>();
                    dbi.Driver<SQLite20Driver>();
                    dbi.ConnectionProvider<DriverConnectionProvider>();
                    dbi.IsolationLevel = IsolationLevel.ReadCommitted;
                    dbi.ConnectionString = connectionString;
                    dbi.Timeout = 60;
                    dbi.LogFormattedSql = false;
                    dbi.LogSqlInConsole = false;
                });
            }
            else
            {
                //must be oracle
                nhConfiguration.DataBaseIntegration(dbi =>
                {
                    dbi.Dialect<Oracle10gDialect>();
                    dbi.Driver<OracleManagedDataClientDriver>();
                    dbi.ConnectionProvider<DriverConnectionProvider>();
                    dbi.IsolationLevel = IsolationLevel.ReadCommitted;
                    dbi.ConnectionString = connectionString;
                    dbi.Timeout = 60;
                    dbi.LogFormattedSql = false;
                    dbi.LogSqlInConsole = false;
                });                
            }

            nhConfiguration.Properties["show_sql"] = "false";

            var sessionFactory = nhConfiguration.BuildSessionFactory();
            return sessionFactory;
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = comboBox1.SelectedItem.ToString();
            if (item.Contains("{password}"))
            {
                panel1.Visible = true;

                txtUserName.Text = Settings.Default.LastUserName;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var openFd = new OpenFileDialog()
            {
                CheckFileExists = true,
                Filter = "Excel Files |*.xls;*.xlsx;*.xlsm;"
            };
            ;
            if (openFd.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            
            try
            {
                var fm = new FrmMain();
                //Create connection

                var cn = CreateNhSessionFactory(GetExcelConnectionString(openFd.FileName)).OpenStatelessSession().Connection;
                fm.Connection = cn;
                fm.Login = this;
                fm.Text = string.Format("Sql Wrangler connected to {0}", openFd.FileName);
                fm.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string GetExcelConnectionString(string fileName)
        {
            var connectionTemplate = "";
            if (fileName.Trim().EndsWith(".xlsx") || fileName.Trim().EndsWith(".xlsm"))
            {
                connectionTemplate =
                    "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";";
            }
            else if (fileName.Trim().EndsWith(".xls"))
            {
                connectionTemplate =
                    "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";";
            }
            if (string.IsNullOrEmpty(connectionTemplate))
            {
                throw new FileLoadException("Not a valid excel file.");
            }

            return string.Format(connectionTemplate, fileName);
        }
    }
}
