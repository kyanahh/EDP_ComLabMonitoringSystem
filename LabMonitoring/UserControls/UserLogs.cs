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

namespace LabMonitoring.UserControls
{
    public partial class UserLogs : UserControl
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=;database=labmonitoring;");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dtr;

        public UserLogs()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "";
                con.Open();

                if (cbSearch.Text == "User ID")
                {
                    sql = "SELECT userlogs.logid as LogTimeID, users.usernumber as UserID, log_time as Login_Time FROM users " +
                        "INNER JOIN userlogs on users.usernumber = userlogs.usernumber " +
                        "WHERE users.usernumber = " + txtSearch.Text + " ";
                }

                cmd = new MySqlCommand(sql, con);
                dtr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtr);
                dgvUserLogs.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void UserLogs_Load(object sender, EventArgs e)
        {
            loadData();
        }

        public void loadData()
        {
            try
            {
                con.Open();

                string sql = "SELECT * FROM userlogs ORDER BY logid DESC";
                cmd = new MySqlCommand(sql, con);
                dtr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtr);
                dgvUserLogs.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
