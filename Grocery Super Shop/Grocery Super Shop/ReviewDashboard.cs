using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class ReviewDashboard : Form
    {
        private TextBox txtSearchReviewID;
        private Button btnSearch;
        private Button btnDeleteReview;
        private DataGridView dgvReviews;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public ReviewDashboard()
        {
            this.Text = "Shop Reviews Management";
            this.Size = new Size(700, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Search Review ID:", Location = new Point(25, 28), AutoSize = true });
            txtSearchReviewID = new TextBox { Location = new Point(140, 25), Width = 90 };
            this.Controls.Add(txtSearchReviewID);

            btnSearch = new Button { Text = "Search", Location = new Point(240, 23), Size = new Size(75, 28) };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            btnDeleteReview = new Button { Text = "Delete Review", Location = new Point(330, 23), Size = new Size(110, 28) };
            btnDeleteReview.Click += BtnDeleteReview_Click;
            this.Controls.Add(btnDeleteReview);

            dgvReviews = new DataGridView
            {
                Location = new Point(25, 75),
                Size = new Size(625, 280),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvReviews);

            LoadReviewData();
        }

        private void LoadReviewData()
        {
            try
            {
                string query = "SELECT ReviewID AS [Review ID], ShopName AS [Shop Name], ReviewText AS [Reviews] FROM Reviews";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvReviews.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading reviews: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string id = txtSearchReviewID.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                LoadReviewData();
                return;
            }

            try
            {
                string query = "SELECT ReviewID AS [Review ID], ShopName AS [Shop Name], ReviewText AS [Reviews] FROM Reviews WHERE ReviewID = @ID";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvReviews.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteReview_Click(object sender, EventArgs e)
        {
            if (dgvReviews.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a review to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int reviewID = Convert.ToInt32(dgvReviews.SelectedRows[0].Cells["Review ID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this review?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Reviews WHERE ReviewID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", reviewID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Review deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadReviewData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting review: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}