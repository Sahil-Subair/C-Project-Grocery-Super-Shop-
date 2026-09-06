using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class OfferDashboard : Form
    {
        private DataGridView dgvOffers;
        private Button btnAddOffer;
        private Button btnDeleteOffer;
        private Button btnClose;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public OfferDashboard()
        {
            this.Text = "Offers & Discounts Management";
            this.Size = new Size(700, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Active Promotional Offers", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true });

            dgvOffers = new DataGridView
            {
                Location = new Point(25, 65),
                Size = new Size(625, 250),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvOffers);

            btnAddOffer = new Button { Text = "Add Offer", Location = new Point(25, 330), Size = new Size(100, 32) };
            btnAddOffer.Click += BtnAddOffer_Click;
            this.Controls.Add(btnAddOffer);

            btnDeleteOffer = new Button { Text = "Delete Offer", Location = new Point(135, 330), Size = new Size(100, 32) };
            btnDeleteOffer.Click += BtnDeleteOffer_Click;
            this.Controls.Add(btnDeleteOffer);

            btnClose = new Button { Text = "Close", Location = new Point(550, 330), Size = new Size(100, 32) };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            LoadOffersData();
        }

        private void LoadOffersData()
        {
            try
            {
                string query = "SELECT OfferID AS [Offer ID], OfferTitle AS [Offer Title], DiscountPercentage AS [Discount %], ValidUntil AS [Valid Until] FROM Offers";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvOffers.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading offers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddOffer_Click(object sender, EventArgs e)
        {
            Form addForm = new Form { Text = "Add New Offer", Size = new Size(350, 300), StartPosition = FormStartPosition.CenterParent };

            addForm.Controls.Add(new Label { Text = "Offer Title:", Location = new Point(30, 30), AutoSize = true });
            TextBox txtTitle = new TextBox { Location = new Point(140, 27), Width = 150 };
            addForm.Controls.Add(txtTitle);

            addForm.Controls.Add(new Label { Text = "Discount %:", Location = new Point(30, 80), AutoSize = true });
            TextBox txtDiscount = new TextBox { Location = new Point(140, 77), Width = 150 };
            addForm.Controls.Add(txtDiscount);

            addForm.Controls.Add(new Label { Text = "Valid Until:", Location = new Point(30, 130), AutoSize = true });
            TextBox txtDate = new TextBox { Location = new Point(140, 127), Width = 150 };
            addForm.Controls.Add(txtDate);

            Button btnSubmit = new Button { Text = "Add Offer", Location = new Point(140, 180), Size = new Size(150, 35) };
            btnSubmit.Click += (s, args) =>
            {
                string title = txtTitle.Text.Trim();
                if (string.IsNullOrEmpty(title) || !int.TryParse(txtDiscount.Text.Trim(), out int discount) || !DateTime.TryParse(txtDate.Text.Trim(), out DateTime validUntil))
                {
                    MessageBox.Show("Please enter valid details (ensure date format is YYYY-MM-DD).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "INSERT INTO Offers (OfferTitle, DiscountPercentage, ValidUntil) VALUES (@Title, @Discount, @ValidUntil)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Title", title);
                            cmd.Parameters.AddWithValue("@Discount", discount);
                            cmd.Parameters.AddWithValue("@ValidUntil", validUntil);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Offer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    addForm.Close();
                    LoadOffersData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding offer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            addForm.Controls.Add(btnSubmit);
            addForm.ShowDialog();
        }

        private void BtnDeleteOffer_Click(object sender, EventArgs e)
        {
            if (dgvOffers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an offer to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int offerID = Convert.ToInt32(dgvOffers.SelectedRows[0].Cells["Offer ID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this offer?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Offers WHERE OfferID = @ID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID", offerID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Offer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOffersData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting offer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}