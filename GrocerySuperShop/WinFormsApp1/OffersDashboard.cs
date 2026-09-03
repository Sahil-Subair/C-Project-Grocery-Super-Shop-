using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class OffersDashboard : Form
    {
        public OffersDashboard()
        {
            InitializeComponent();
            LoadOffersData();

            // Wire up event handlers
            btnSearchOffer.Click += BtnSearchOffer_Click;
            btnCreateOffer.Click += BtnCreateOffer_Click;
            btnRemoveOffer.Click += BtnRemoveOffer_Click;
        }

        private void LoadOffersData(string offerIdSearch = "")
        {
            try
            {
                string query = "SELECT OfferID AS [OFFER ID], ItemName AS [Item Name], " +
                               "DiscountDescription AS [Discount Description], " +
                               "DiscountedPrice AS [Discounted Price] FROM Offers";

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(offerIdSearch))
                {
                    query += " WHERE OfferID = @OfferID";
                    parameters = new SqlParameter[] { new SqlParameter("@OfferID", offerIdSearch.Trim()) };
                }

                DataTable dt = DatabaseHelper.ExecuteSelectQuery(query, parameters);
                dgvOffers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading offers: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearchOffer_Click(object sender, EventArgs e)
        {
            string searchId = txtSearchOfferID.Text.Trim();
            if (string.IsNullOrEmpty(searchId))
            {
                LoadOffersData();
            }
            else if (int.TryParse(searchId, out _))
            {
                LoadOffersData(searchId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Offer ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCreateOffer_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 360,
                Height = 240,
                Text = "Create Offer",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lbl1 = new Label() { Text = "Item Name:", Left = 20, Top = 20, Width = 120 };
            TextBox txtItemName = new TextBox() { Left = 140, Top = 20, Width = 170 };

            Label lbl2 = new Label() { Text = "Description:", Left = 20, Top = 60, Width = 120 };
            TextBox txtDescription = new TextBox() { Left = 140, Top = 60, Width = 170 }; // e.g., Durga Puja Offer / Eid Offer

            Label lbl3 = new Label() { Text = "Discounted Price:", Left = 20, Top = 100, Width = 120 };
            TextBox txtDiscPrice = new TextBox() { Left = 140, Top = 100, Width = 170 };

            Button confirmation = new Button() { Text = "Save", Left = 140, Top = 150, Width = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (s, ev) => { prompt.Close(); };

            prompt.Controls.Add(lbl1);
            prompt.Controls.Add(txtItemName);
            prompt.Controls.Add(lbl2);
            prompt.Controls.Add(txtDescription);
            prompt.Controls.Add(lbl3);
            prompt.Controls.Add(txtDiscPrice);
            prompt.Controls.Add(confirmation);

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string itemName = txtItemName.Text.Trim();
                string description = txtDescription.Text.Trim();

                if (!string.IsNullOrEmpty(itemName) && !string.IsNullOrEmpty(description) &&
                    decimal.TryParse(txtDiscPrice.Text.Trim(), out decimal discPrice))
                {
                    try
                    {
                        string query = "INSERT INTO Offers (ItemName, DiscountDescription, DiscountedPrice) VALUES (@ItemName, @Description, @DiscPrice)";
                        SqlParameter[] parameters = {
                            new SqlParameter("@ItemName", itemName),
                            new SqlParameter("@Description", description),
                            new SqlParameter("@DiscPrice", discPrice)
                        };

                        DatabaseHelper.ExecuteQuery(query, parameters);
                        MessageBox.Show("Offer created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOffersData();
                        txtSearchOfferID.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error creating offer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please ensure all fields are filled correctly and Discounted Price is a valid number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnRemoveOffer_Click(object sender, EventArgs e)
        {
            if (dgvOffers.SelectedRows.Count > 0)
            {
                int offerId = Convert.ToInt32(dgvOffers.SelectedRows[0].Cells["OFFER ID"].Value);

                DialogResult result = MessageBox.Show($"Are you sure you want to remove Offer ID {offerId}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Offers WHERE OfferID = @OfferID";
                        SqlParameter[] parameters = { new SqlParameter("@OfferID", offerId) };

                        DatabaseHelper.ExecuteQuery(query, parameters);
                        MessageBox.Show("Offer removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOffersData();
                        txtSearchOfferID.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error removing offer: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an offer row from the table to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void OffersDashboard_Load(object sender, EventArgs e)
        {
            // Initialization logic if needed
        }
    }
}