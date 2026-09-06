using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class SuperAdminDashboard : Form
    {
        private TextBox txtSearchShopID;
        private Button btnSearch;
        private Button btnApprovals;
        private Button btnSales;
        private Button btnSuspend;
        private Button btnReviews; // Added Reviews button
        private DataGridView dgvShops;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public SuperAdminDashboard()
        {
            this.Text = "Super Admin Dashboard";
            this.Size = new Size(880, 500); // Slightly widened to accommodate the new button
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblSearch = new Label { Text = "Search Shop ID:", Location = new Point(25, 28), AutoSize = true };
            this.Controls.Add(lblSearch);

            txtSearchShopID = new TextBox { Location = new Point(130, 25), Width = 90 };
            this.Controls.Add(txtSearchShopID);

            btnSearch = new Button { Text = "Search", Location = new Point(230, 23), Size = new Size(70, 28) };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            // Action Buttons
            btnApprovals = new Button { Text = "Approvals", Location = new Point(380, 22), Size = new Size(85, 30) };
            btnApprovals.Click += (s, e) => { new ApprovalDashboard().ShowDialog(); LoadShopData(); };
            this.Controls.Add(btnApprovals);

            btnSales = new Button { Text = "Sales", Location = new Point(475, 22), Size = new Size(75, 30) };
            btnSales.Click += (s, e) => new SalesDashboard().ShowDialog();
            this.Controls.Add(btnSales);

            btnReviews = new Button { Text = "Reviews", Location = new Point(560, 22), Size = new Size(85, 30) };
            btnReviews.Click += (s, e) => new ReviewDashboard().ShowDialog(); // Opens the Reviews Dashboard
            this.Controls.Add(btnReviews);

            btnSuspend = new Button { Text = "Suspend", Location = new Point(655, 22), Size = new Size(85, 30) };
            btnSuspend.Click += BtnSuspend_Click;
            this.Controls.Add(btnSuspend);

            // Main Data Grid View
            dgvShops = new DataGridView
            {
                Location = new Point(25, 75),
                Size = new Size(815, 360),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvShops);

            LoadShopData();
        }

        private void LoadShopData()
        {
            try
            {
                string query = @"SELECT 
                                    UserID AS [Shop ID], 
                                    Username AS [Shop name], 
                                    FullName AS [Owner Name], 
                                    CASE WHEN IsApproved = 1 THEN 'Active' ELSE 'Pending' END AS [Status] 
                                  FROM Users 
                                  WHERE Role = 'Admin'";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvShops.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string id = txtSearchShopID.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                LoadShopData();
                return;
            }

            try
            {
                string query = @"SELECT 
                                    UserID AS [Shop ID], 
                                    Username AS [Shop name], 
                                    FullName AS [Owner Name], 
                                    CASE WHEN IsApproved = 1 THEN 'Active' ELSE 'Pending' END AS [Status] 
                                  FROM Users 
                                  WHERE Role = 'Admin' AND UserID = @ID";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvShops.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSuspend_Click(object sender, EventArgs e)
        {
            if (dgvShops.SelectedRows.Count > 0)
            {
                int shopId = Convert.ToInt32(dgvShops.SelectedRows[0].Cells["Shop ID"].Value);
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "UPDATE Users SET IsApproved = 0 WHERE UserID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", shopId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Shop / Admin account suspended successfully.", "Suspended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadShopData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error suspending account: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a shop row from the table to suspend.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}