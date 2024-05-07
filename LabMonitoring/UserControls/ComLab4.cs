using MySql.Data.MySqlClient;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace LabMonitoring.UserControls
{
    public partial class ComLab4 : UserControl
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=;database=labmonitoring;");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dtr;

        int items = 0;
        int lab = 0;

        public ComLab4()
        {
            InitializeComponent();
            cmbItem.SelectedIndex = 0;
            cbSearch.SelectedIndex = 0;
            cbComLab.SelectedIndex = 0;
        }

        public void ClearAll()
        {
            txtSection.Clear();
            cmbItem.SelectedIndex = 0;
            txtRemarks.Clear();
            dtTimeIn.ResetText();
            dtTimeOut.ResetText();
            numQty.ResetText();
            txtUserNumber.Clear();
            txtReportID.Clear();
            txtSearch.Clear();
            cbSearch.SelectedIndex = 0;
            cbComLab.SelectedIndex = 0;
        }

        public void LoadData()
        {
            try
            {
                con.Open();

                string sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE reports.labid = 4 " +
                    "ORDER BY reportid DESC";

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
            LoadData();
        }

        private void ComLab4_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void cmbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem = cmbItem.SelectedItem.ToString();

            if (selectedItem == "Aircon")
                items = 1;
            else if (selectedItem == "Projector")
                items = 2;
            else if (selectedItem == "Carpet")
                items = 3;
            else if (selectedItem == "Windows")
                items = 4;
            else if (selectedItem == "Light Switch")
                items = 5;
            else if (selectedItem == "Power Socket")
                items = 6;
            else if (selectedItem == "Light")
                items = 7;
            else if (selectedItem == "Table")
                items = 8;
            else if (selectedItem == "Cabinets")
                items = 9;
            else if (selectedItem == "White Board")
                items = 10;
            else if (selectedItem == "Chairs")
                items = 11;
            else if (selectedItem == "Door")
                items = 12;
            else
                items = 0;
        }

        private void cbComLab_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedItem1 = cbComLab.SelectedItem.ToString();

            if (selectedItem1 == "Computer Laboratory 1")
                lab = 1;
            else if (selectedItem1 == "Computer Laboratory 2")
                lab = 2;
            else if (selectedItem1 == "Computer Laboratory 3")
                lab = 3;
            else if (selectedItem1 == "Computer Laboratory 4")
                lab = 4;
            else if (selectedItem1 == "Computer Laboratory 5")
                lab = 5;
            else if (selectedItem1 == "Computer Laboratory 6")
                lab = 6;
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dgvData.Rows[index];
            txtReportID.Text = row.Cells[0].Value.ToString();
            txtUserNumber.Text = row.Cells[1].Value.ToString();
            txtSection.Text = row.Cells[8].Value.ToString();
            numQty.Value = Convert.ToDecimal(row.Cells[9].Value);
            dtTimeIn.Value = Convert.ToDateTime(row.Cells[6].Value.ToString());
            dtTimeOut.Value = Convert.ToDateTime(row.Cells[7].Value.ToString());
            txtRemarks.Text = row.Cells[10].Value.ToString();

            string lab1 = row.Cells[4].Value.ToString();

            if (lab1 == "Computer Laboratory 1")
            {
                cbComLab.SelectedIndex = 1;
            }
            else if (lab1 == "Computer Laboratory 2")
            {
                cbComLab.SelectedIndex = 2;
            }
            else if (lab1 == "Computer Laboratory 3")
            {
                cbComLab.SelectedIndex = 3;
            }
            else if (lab1 == "Computer Laboratory 4")
            {
                cbComLab.SelectedIndex = 4;
            }
            else if (lab1 == "Computer Laboratory 5")
            {
                cbComLab.SelectedIndex = 5;
            }
            else if (lab1 == "Computer Laboratory 6")
            {
                cbComLab.SelectedIndex = 6;
            }

            string item = row.Cells[5].Value.ToString();

            if (item == "Aircon")
            {
                cmbItem.SelectedIndex = 1;
            }
            else if (item == "Projector")
            {
                cmbItem.SelectedIndex = 2;
            }
            else if (item == "Carpet")
            {
                cmbItem.SelectedIndex = 3;
            }
            else if (item == "Windows")
            {
                cmbItem.SelectedIndex = 4;
            }
            else if (item == "Light Switch")
            {
                cmbItem.SelectedIndex = 5;
            }
            else if (item == "Power Socket")
            {
                cmbItem.SelectedIndex = 6;
            }
            else if (item == "Light")
            {
                cmbItem.SelectedIndex = 7;
            }
            else if (item == "Table")
            {
                cmbItem.SelectedIndex = 8;
            }
            else if (item == "Cabinets")
            {
                cmbItem.SelectedIndex = 9;
            }
            else if (item == "White Board")
            {
                cmbItem.SelectedIndex = 10;
            }
            else if (item == "Chairs")
            {
                cmbItem.SelectedIndex = 11;
            }
            else if (item == "Door")
            {
                cmbItem.SelectedIndex = 12;
            }

            cbComLab.Refresh();
            cmbItem.Refresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to permanently delete this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    con.Open();
                    string sql = "DELETE FROM reports WHERE reportid = '" + txtReportID.Text + "' ";
                    cmd = new MySqlCommand(sql, con);
                    dtr = cmd.ExecuteReader();

                    MessageBox.Show("Record deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this record?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    con.Open();
                    string sql = "UPDATE reports SET section = @section, remarks = @remarks, " +
                        "qty = @qty, timein = @timein, timeout = @timeout, itemid = @itemid, " +
                        "labid = @labid WHERE reportid = " + txtReportID.Text + ";";
                    cmd = new MySqlCommand(sql, con);

                    // Assign parameter values
                    cmd.Parameters.AddWithValue("@itemid", items);
                    cmd.Parameters.AddWithValue("@labid", lab);
                    cmd.Parameters.AddWithValue("@timein", dtTimeIn.Value.ToString("hh:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@timeout", dtTimeOut.Value.ToString("hh:mm:ss tt"));
                    cmd.Parameters.AddWithValue("@section", txtSection.Text);
                    cmd.Parameters.AddWithValue("@qty", numQty.Value);
                    cmd.Parameters.AddWithValue("@remarks", txtRemarks.Text);

                    dtr = cmd.ExecuteReader();

                    MessageBox.Show("Record updated successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                };


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "";
                string searchValue = txtSearch.Text.ToLower();  // Convert search term to lowercase
                con.Open();

                if (cbSearch.Text == "Item")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE " +
                    "LOWER(items.itemname) = LOWER('" + searchValue + "')" +
                    "OR LOWER(items.itemname) LIKE '%" + searchValue + "%' AND " +
                    "reports.labid = 4 ORDER BY reportid DESC;";
                }
                else if (cbSearch.Text == "Time In")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE " +
                    "reports.timein LIKE '%" + txtSearch.Text + "%' AND reports.labid = 4" +
                    " ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Time Out")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE " +
                    "reports.timeout LIKE '%" + txtSearch.Text + "%' AND reports.labid = 4 " +
                    "ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "First Name")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid)" +
                    " WHERE LOWER(users.firstname) = LOWER('" + searchValue + "')" +
                    "OR LOWER(users.firstname) LIKE '%" + searchValue + "%' AND reports.labid = 4 " +
                    "ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Last Name")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE " +
                    "LOWER(users.lastname) = '" + searchValue + "'" +
                    "OR LOWER(users.lastname) LIKE '%" + searchValue + "%' AND reports.labid = 4 " +
                    "ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Date Created")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) " +
                    "WHERE LOWER(reports.todate) = STR_TO_DATE('" + searchValue + "', '%Y-%m-%d') " +
                    "OR LOWER(reports.todate) LIKE '%" + searchValue + "%' " +
                    "OR LOWER(reports.todate) = '" + searchValue + "' " +
                    "OR STR_TO_DATE(reports.todate, '%M %d, %Y') = STR_TO_DATE('" + searchValue + "', '%M %d, %Y')" +
                    "OR LOWER(date_format(reports.todate, '%M %d, %Y')) = '" + searchValue + "'" +
                    "OR LOWER(date_format(reports.todate, '%M %d, %Y')) LIKE '%" + searchValue + "%' " +
                    "AND reports.labid = 4 ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Section")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid)" +
                    " WHERE LOWER(reports.section) = LOWER('" + searchValue + "')" +
                    "OR LOWER(reports.section) LIKE '%" + searchValue + "%' AND reports.labid = 4 " +
                    "ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Quantity")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) " +
                    "WHERE LOWER(reports.qty) = LOWER('" + searchValue + "')" +
                    "OR LOWER(reports.qty) LIKE '%" + searchValue + "%' AND reports.labid = 4 " +
                    "ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Remarks")
                {
                    sql = "SELECT reports.reportid AS ReportID, reports.usernumber as UserID, " +
                    "users.firstname AS FirstName, users.lastname AS LastName, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) " +
                    "WHERE LOWER(reports.remarks) = LOWER('" + searchValue + "')" +
                    "OR LOWER(reports.remarks) LIKE '%" + searchValue + "%' AND reports.labid = 4 " +
                    "ORDER BY reportid DESC";
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
    }
}
