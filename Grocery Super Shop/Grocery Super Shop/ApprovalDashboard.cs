using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class ApprovalDashboard : Form
    {
        private DataGridView dgvApprovals;
        private Button btnApprove;
        private Button btnDelete;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public ApprovalDashboard()
        {
            this.Text = "Admin Approvals Dashboard";
            this.Size = new Size(650, 380);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Pending Admin Registrations", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true });

            dgvApprovals = new DataGridView
            {
                Location = new Point(25, 65),
                Size = new Size(585, 220),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvApprovals);

            btnApprove = new Button { Text = "Approve", Location = new Point(25, 300), Size = new Size(100, 32) };
            btnApprove.Click += BtnApprove_Click;
            this.Controls.Add(btnApprove);

            btnDelete = new Button { Text = "Delete", Location = new Point(135, 300), Size = new Size(100, 32) };
            btnDelete.Click += BtnDelete_Click;
            this.Controls.Add(btnDelete);

            LoadPendingApprovals();
        }

        private void LoadPendingApprovals()
        {
            try
            {
                string query = "SELECT UserID, Username AS [Username], FullName AS [Shop Owner Name] FROM Users WHERE Role = 'Admin' AND IsApproved = 0";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvApprovals.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading approvals: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvApprovals.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvApprovals.SelectedRows[0].Cells["UserID"].Value);
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "UPDATE Users SET IsApproved = 1 WHERE UserID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", userId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Admin approved successfully! They can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPendingApprovals();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error approving: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvApprovals.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvApprovals.SelectedRows[0].Cells["UserID"].Value);
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Users WHERE UserID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", userId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Admin registration deleted. They cannot log into the system.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPendingApprovals();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an item from the table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}