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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LabMonitoring.UserControls
{
    public partial class Report : UserControl
    {
        MySqlConnection con = new MySqlConnection("datasource=localhost;username=root;password=;database=labmonitoring;");
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataReader dtr;

        int items = 0;
        int lab = 0;

        public Report(string username)
        {
            InitializeComponent();
            txtUserNumber.Text = username;
            cmbItem.SelectedIndex = 0;
            cbComLab.SelectedIndex = 0;
            cbSearch.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        public void ClearAll()
        {
            txtSection.Clear();
            cmbItem.SelectedIndex = 0;
            txtRemarks.Clear();
            dtTimeIn.ResetText();
            dtTimeOut.ResetText();
            numQty.ResetText();
            cbComLab.SelectedIndex = 0;
            cbSearch.SelectedIndex = 0;
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to create this report?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    con.Open();

                    DateTime now = DateTime.Now;
                    string formattedDate = now.ToString("yyyy-MM-dd HH:mm:ss");

                    string sql = "INSERT INTO reports (itemid, timein, timeout, section, qty, remarks, todate, usernumber, labid) " +
                                "VALUES (@itemid, @timein, @timeout, @section, @qty, @remarks, @todate, @userid, @labid)";

                    MySqlCommand cmd2 = new MySqlCommand(sql, con);

                    // Assign parameter values
                    cmd2.Parameters.AddWithValue("@itemid", items);
                    cmd2.Parameters.AddWithValue("@labid", lab);
                    cmd2.Parameters.AddWithValue("@timein", dtTimeIn.Value.ToString("hh:mm:ss tt"));
                    cmd2.Parameters.AddWithValue("@timeout", dtTimeOut.Value.ToString("hh:mm:ss tt"));
                    cmd2.Parameters.AddWithValue("@section", txtSection.Text);
                    cmd2.Parameters.AddWithValue("@qty", numQty.Value);
                    cmd2.Parameters.AddWithValue("@remarks", txtRemarks.Text);
                    cmd2.Parameters.AddWithValue("@todate", formattedDate);
                    cmd2.Parameters.AddWithValue("@userid", txtUserNumber.Text);

                    cmd2.ExecuteNonQuery();

                    MessageBox.Show("Report created successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Report cancelled", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearAll();
        }

        public void LoadData()
        {
            try
            {
                con.Open();

                string sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) " +
                    "WHERE users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";

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

        private void Report_Load(object sender, EventArgs e)
        {
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
                    sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE " +
                    "LOWER(items.itemname) = LOWER('" + searchValue + "')" +
                    "OR LOWER(items.itemname) LIKE '%" + searchValue + "%' " +
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC;";
                }
                else if (cbSearch.Text == "Time In")
                {
                    sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE " +
                    "reports.timein LIKE '%" + txtSearch.Text + "%' " +
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Time Out")
                {
                    sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) WHERE " +
                    "reports.timeout LIKE '%" + txtSearch.Text + "%' " +
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Date Created")
                {
                    sql = "SELECT reports.reportid AS ReportID, " +
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
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Quantity")
                {
                    sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) " +
                    "WHERE LOWER(reports.qty) = LOWER('" + searchValue + "')" +
                    "OR LOWER(reports.qty) LIKE '%" + searchValue + "%' " +
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Remarks")
                {
                    sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) " +
                    "WHERE LOWER(reports.remarks) = LOWER('" + searchValue + "')" +
                    "OR LOWER(reports.remarks) LIKE '%" + searchValue + "%' " +
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Computer Laboratory")
                {
                    sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid) " +
                    "WHERE LOWER(comlab.comlab) = LOWER('" + searchValue + "')" +
                    "OR LOWER(comlab.comlab) LIKE '%" + searchValue + "%' " +
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";
                }
                else if (cbSearch.Text == "Section")
                {
                    sql = "SELECT reports.reportid AS ReportID, " +
                    "comlab.comlab AS ComLab, " +
                    "items.itemname AS ItemName, " +
                    "reports.timein AS TimeIn, reports.timeout AS TimeOut, reports.section AS Section, " +
                    "reports.qty as Quantity, reports.remarks AS Remarks, reports.todate AS " +
                    "DateCreated FROM (((reports INNER JOIN items ON reports.itemid = items.itemid) " +
                    "INNER JOIN users ON reports.usernumber = users.usernumber) " +
                    "INNER JOIN comlab ON reports.labid = comlab.labid)" +
                    " WHERE LOWER(reports.section) = LOWER('" + searchValue + "')" +
                    "OR LOWER(reports.section) LIKE '%" + searchValue + "%' " +
                    "AND users.usernumber = '" + txtUserNumber.Text + "' ORDER BY reportid DESC";
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
