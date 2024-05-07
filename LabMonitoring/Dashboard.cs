using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using LabMonitoring.UserControls;
using MySqlX.XDevAPI.Common;



namespace LabMonitoring
{
    public partial class Dashboard : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=;database=labmonitoring;");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dtr;

        public Dashboard(string username)
        {
            InitializeComponent();
            lbUser.Text = username;
        }

        private void AddUserControl(UserControl uc)
        {
            panel2.Controls.Clear();
            panel2.Controls.Add(uc);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if(result == DialogResult.Yes)
            {
                Login login = new Login();
                login.Show();
                this.Visible = false;
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Report rep = new Report(lbUser.Text);
            AddUserControl(rep);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            AddUsers user = new AddUsers();
            AddUserControl(user);
        }

        private void btnUserLogs_Click(object sender, EventArgs e)
        {
            UserLogs logs = new UserLogs();
            AddUserControl(logs);
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            Records records = new Records();
            AddUserControl(records);
        }

        private void btnCL1_Click(object sender, EventArgs e)
        {
            ComLab1 cl1 = new ComLab1();
            AddUserControl(cl1);
        }

        private void btnCL2_Click(object sender, EventArgs e)
        {
            ComLab2 cl2 = new ComLab2();
            AddUserControl(cl2);
        }

        private void btnCL3_Click(object sender, EventArgs e)
        {
            ComLab3 cl3 = new ComLab3();
            AddUserControl(cl3);
        }

        private void btnCL4_Click(object sender, EventArgs e)
        {
            ComLab4 cl4 = new ComLab4();
            AddUserControl(cl4);
        }

        private void btnCL5_Click(object sender, EventArgs e)
        {
            ComLab5 cl5 = new ComLab5();
            AddUserControl(cl5);
        }

        private void btnCL6_Click(object sender, EventArgs e)
        {
            ComLab6 cl6 = new ComLab6();
            AddUserControl(cl6);
        }
    }
}
