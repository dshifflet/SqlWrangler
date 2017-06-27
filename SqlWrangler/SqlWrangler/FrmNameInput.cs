using System;
using System.Drawing;
using System.Windows.Forms;
using SqlWrangler.Properties;

namespace SqlWrangler
{
    public partial class FrmNameInput : Form
    {
        public string UserName { get; set; }

        public FrmNameInput()
        {
            InitializeComponent();
            var b = Resources.pencil;
            var pIcon = b.GetHicon();
            var i = Icon.FromHandle(pIcon);
            Icon = i;
            i.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserName = txtName.Text;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserName = null;
            Close();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char) Keys.Enter)
            {
                button1.PerformClick();
            }
        }

    }
}
