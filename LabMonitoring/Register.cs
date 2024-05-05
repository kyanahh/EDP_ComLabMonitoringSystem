using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabMonitoring
{
    public partial class Register : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=;database=labmonitoring;");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dtr;

        public Register()
        {
            InitializeComponent();
        }

        private void lbRegister_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Visible = false;
        }

        private void btnRegister_Click(object sender, EventArgs e)
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

                string sql = "INSERT INTO users (firstname, lastname, usernumber, pin, typeid) VALUES(@firstname, @lastname, @usernumber, @pin, 2)";
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@firstname", txtFname.Text);
                cmd.Parameters.AddWithValue("@lastname", txtLname.Text);
                cmd.Parameters.AddWithValue("@usernumber", txtUserNumber.Text);
                cmd.Parameters.AddWithValue("@pin", txtPin.Text);

                dtr = cmd.ExecuteReader();

                ClearAll();

                MessageBox.Show("Account created successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Login login = new Login();
                login.Show();
                this.Visible = false;

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
            txtFname.Clear();
            txtLname.Clear();
            txtPin.Clear();
            txtUserNumber.Clear();
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtUserNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtFname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtLname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
