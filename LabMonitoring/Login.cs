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


namespace LabMonitoring
{
    public partial class Login : Form
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=;database=labmonitoring;");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dtr;

        public Login()
        {
            InitializeComponent();
        }

        private void lbRegister_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Visible = false;
        }

        private void txtUserNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "SELECT * FROM users where usernumber = '" + txtUserNumber.Text + "' AND pin = '" + txtPin.Text + "'";
                cmd = new MySqlCommand(sql, con);
                dtr = cmd.ExecuteReader();

                DateTime now = DateTime.Now;
                string formattedDate = now.ToString("yyyy-MM-dd HH:mm:ss");


                if (dtr.Read())
                {
                    int userId = Convert.ToInt32(dtr.GetValue(1));
                    string type = dtr.GetValue(5).ToString();

                    dtr.Close();

                    string sql2 = "INSERT INTO userlogs(log_time, usernumber) VALUES('" + formattedDate + "', '" + userId + "')";
                    MySqlCommand cmd2 = new MySqlCommand(sql2, con);
                    MySqlDataReader dtr2 = cmd2.ExecuteReader();

                    if (type == "1")
                    {
                        Dashboard db = new Dashboard(txtUserNumber.Text);
                        db.Show();
                        this.Visible = false;

                        db.btnUsers.Enabled = true;
                        db.btnRecords.Enabled = true;
                        db.btnUserLogs.Enabled = true;
                        db.btnCL1.Enabled = true;
                        db.btnCL2.Enabled = true;
                        db.btnCL3.Enabled = true;
                        db.btnCL4.Enabled = true;
                        db.btnCL5.Enabled = true;
                        db.btnCL6.Enabled = true;
                    }
                    else if (type == "2")
                    {
                        Dashboard db = new Dashboard(txtUserNumber.Text);
                        db.Show();
                        this.Visible = false;
                    }

                    
                }
                else
                {
                    lbError.Text = "The user number or pin you entered is incorrect";
                }
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
    }
}
