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
    public partial class AddUsers : UserControl
    {

        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=;database=labmonitoring;");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dtr;

        public AddUsers()
        {
            InitializeComponent();
            cmbType.SelectedIndex = 0;

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if username already exists
                con.Open();
                string checkSql = "SELECT COUNT(*) FROM users WHERE usernumber = @usernumber";
                cmd = new MySqlCommand(checkSql, con);
                cmd.Parameters.AddWithValue("@usernumber", txtUserNumber.Text);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (count > 0)
                {
                    ClearAll();
                    lbError.Text = "Account already exists!";
                    return;
                }

                con.Open();

                string sql = "INSERT INTO users (firstname, lastname, usernumber, pin, typeid) VALUES(@firstname, @lastname, @usernumber, @pin, @typeid)";
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@firstname", txtFirst.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLast.Text);
                cmd.Parameters.AddWithValue("@usernumber", txtUserNumber.Text);
                cmd.Parameters.AddWithValue("@pin", txtPin.Text);
                cmd.Parameters.AddWithValue("@typeid", cmbType.SelectedIndex);

                dtr = cmd.ExecuteReader();

                ClearAll();

                lbError.Text = "Account created successfully";

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

        public void ClearAll()
        {
            txtFirst.Clear();
            txtLast.Clear();
            txtPin.Clear();
            txtUserNumber.Clear();
            cmbType.SelectedIndex = 0;
        }

    }
}
