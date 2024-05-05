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
            MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            Login login = new Login();
            login.Show();
            this.Visible = false;
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
    }
}
