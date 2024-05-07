using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


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

            loadData();
        }

        public void ClearAll()
        {
            txtFirst.Clear();
            txtLast.Clear();
            txtPin.Clear();
            txtUserNumber.Clear();
            cmbType.SelectedIndex = 0;
        }

        private void AddUsers_Load(object sender, EventArgs e)
        {
            loadData();
        }

        public void loadData()
        {
            try
            {
                con.Open();

                string sql = "SELECT users.usernumber AS UserID, users.firstname AS FirstNane, users.lastname AS " +
                    "LastName, usertype.usertype AS UserType FROM users INNER JOIN usertype ON users.typeid = " +
                    "usertype.typeid ORDER BY users.userid DESC";

                cmd = new MySqlCommand(sql, con);
                dtr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtr);
                dgvData.DataSource = dt;

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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dgvData.Rows[index];
            txtUserNumber.Text = row.Cells[0].Value.ToString();
            txtFirst.Text = row.Cells[1].Value.ToString();
            txtLast.Text = row.Cells[2].Value.ToString();

            string usertype = row.Cells[3].Value.ToString();

            if (usertype == "Admin")
            {
                cmbType.SelectedIndex = 1;
            }
            else if (usertype == "User")
            {
                cmbType.SelectedIndex = 2;
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to update this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    con.Open();

                    string sql = "UPDATE users SET usernumber = @usernumber, firstname = @firstname, lastname = @lastname, typeid = @typeid";

                    // If the PIN is provided, include it in the query
                    if (!string.IsNullOrEmpty(txtPin.Text))
                    {
                        sql += ", pin = @pin";
                    }

                    sql += " WHERE usernumber = @existingUsernumber;";

                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@usernumber", txtUserNumber.Text);
                        cmd.Parameters.AddWithValue("@firstname", txtFirst.Text);
                        cmd.Parameters.AddWithValue("@lastname", txtLast.Text);
                        cmd.Parameters.AddWithValue("@typeid", cmbType.SelectedIndex);
                        cmd.Parameters.AddWithValue("@existingUsernumber", txtUserNumber.Text);

                        if (!string.IsNullOrEmpty(txtPin.Text))
                        {
                            cmd.Parameters.AddWithValue("@pin", txtPin.Text);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No matching record found to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                }

                ClearAll(); // Reset input fields
                loadData(); // Reload data
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to permanently delete this user?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    con.Open();
                    string sql = "DELETE FROM users WHERE usernumber = '" + txtUserNumber.Text + "' ";
                    cmd = new MySqlCommand(sql, con);
                    dtr = cmd.ExecuteReader();

                    MessageBox.Show("User deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                };

                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            ClearAll();
            loadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "";
                string searchValue = txtSearch.Text.ToLower();  // Convert search term to lowercase
                con.Open();

                if (cbSearch.Text == "User Number")
                {
                    sql = "SELECT users.usernumber AS UserID, users.firstname AS FirstNane, users.lastname AS " +
                    "LastName, usertype.usertype AS UserType FROM users INNER JOIN usertype ON users.typeid = " +
                    "usertype.typeid WHERE " +
                    "LOWER(users.usernumber) = LOWER('" + searchValue + "')" +
                    "OR LOWER(users.usernumber) LIKE '%" + searchValue + "%' ORDER BY users.userid DESC;";
                }
                else if (cbSearch.Text == "First Name")
                {
                    sql = "SELECT users.usernumber AS UserID, users.firstname AS FirstNane, users.lastname AS " +
                    "LastName, usertype.usertype AS UserType FROM users INNER JOIN usertype ON users.typeid = " +
                    "usertype.typeid WHERE " +
                    "LOWER(users.firstname) = LOWER('" + searchValue + "')" +
                    "OR LOWER(users.firstname) LIKE '%" + searchValue + "%' ORDER BY users.userid DESC;";
                }
                else if (cbSearch.Text == "Last Name")
                {
                    sql = "SELECT users.usernumber AS UserID, users.firstname AS FirstNane, users.lastname AS " +
                    "LastName, usertype.usertype AS UserType FROM users INNER JOIN usertype ON users.typeid = " +
                    "usertype.typeid WHERE " +
                    "LOWER(users.lastname) = LOWER('" + searchValue + "')" +
                    "OR LOWER(users.lastname) LIKE '%" + searchValue + "%' ORDER BY users.userid DESC;";
                }
                else if (cbSearch.Text == "User Type")
                {
                    sql = "SELECT users.usernumber AS UserID, users.firstname AS FirstNane, users.lastname AS " +
                    "LastName, usertype.usertype AS UserType FROM users INNER JOIN usertype ON users.typeid = " +
                    "usertype.typeid WHERE " +
                    "LOWER(usertype.usertype) = LOWER('" + searchValue + "')" +
                    "OR LOWER(usertype.usertype) LIKE '%" + searchValue + "%' ORDER BY users.userid DESC;";
                }

                cmd = new MySqlCommand(sql, con);
                dtr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtr);
                dgvData.DataSource = dt;

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
            ClearAll();
            loadData();
        }
    }
}
