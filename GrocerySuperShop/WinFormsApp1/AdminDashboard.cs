using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
            LoadShopsData();

            // Wire up button click events safely
            btnSearch.Click += BtnSearch_Click;
            btnAddShop.Click += BtnAddShop_Click;
            btnRemoveShop.Click += BtnRemoveShop_Click;
        }

        private void LoadShopsData(string shopIdSearch = "")
        {
            try
            {
                string query = "SELECT ShopID AS [Vendor ID], ShopName AS [Shop Name], OwnerName AS [Owner Name], " +
                               "TotalSales AS [Total Sales], CommissionEarned AS [Commission Earned], " +
                               "ShopReviews AS [Shop Reviews], Status FROM Shops";

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(shopIdSearch))
                {
                    query += " WHERE ShopID = @ShopID";
                    parameters = new SqlParameter[] { new SqlParameter("@ShopID", shopIdSearch.Trim()) };
                }

                DataTable dt = DatabaseHelper.ExecuteSelectQuery(query, parameters);
                dgvShops.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shops: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchId = txtSearchID.Text.Trim();
            if (string.IsNullOrEmpty(searchId))
            {
                LoadShopsData();
            }
            else if (int.TryParse(searchId, out _))
            {
                LoadShopsData(searchId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Shop ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddShop_Click(object sender, EventArgs e)
        {
            // Simple input dialog for Shop Name and Owner Name
            Form prompt = new Form()
            {
                Width = 350,
                Height = 220,
                Text = "Add New Shop",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lbl1 = new Label() { Text = "Shop Name:", Left = 20, Top = 20, Width = 100 };
            TextBox txtShopName = new TextBox() { Left = 130, Top = 20, Width = 180 };

            Label lbl2 = new Label() { Text = "Owner Name:", Left = 20, Top = 60, Width = 100 };
            TextBox txtOwnerName = new TextBox() { Left = 130, Top = 60, Width = 180 };

            Button confirmation = new Button() { Text = "Save", Left = 130, Top = 110, Width = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (s, ev) => { prompt.Close(); };

            prompt.Controls.Add(lbl1);
            prompt.Controls.Add(txtShopName);
            prompt.Controls.Add(lbl2);
            prompt.Controls.Add(txtOwnerName);
            prompt.Controls.Add(confirmation);

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string shopName = txtShopName.Text.Trim();
                string ownerName = txtOwnerName.Text.Trim();

                if (!string.IsNullOrEmpty(shopName) && !string.IsNullOrEmpty(ownerName))
                {
                    try
                    {
                        // Auto-generates ID, initializes sales, commission, and reviews to 0/null equivalents
                        string query = "INSERT INTO Shops (ShopName, OwnerName, TotalSales, CommissionEarned, ShopReviews, Status) VALUES (@ShopName, @OwnerName, 0, 0, 0, 'Active')";
                        SqlParameter[] parameters = {
                            new SqlParameter("@ShopName", shopName),
                            new SqlParameter("@OwnerName", ownerName)
                        };

                        DatabaseHelper.ExecuteQuery(query, parameters);
                        MessageBox.Show("Shop added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadShopsData();
                        txtSearchID.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding shop: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Shop Name and Owner Name cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnRemoveShop_Click(object sender, EventArgs e)
        {
            if (dgvShops.SelectedRows.Count > 0)
            {
                int shopId = Convert.ToInt32(dgvShops.SelectedRows[0].Cells["Vendor ID"].Value);

                DialogResult result = MessageBox.Show($"Are you sure you want to remove Shop ID {shopId}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Shops WHERE ShopID = @ShopID";
                        SqlParameter[] parameters = { new SqlParameter("@ShopID", shopId) };

                        DatabaseHelper.ExecuteQuery(query, parameters);
                        MessageBox.Show("Shop removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadShopsData();
                        txtSearchID.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error removing shop: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a shop row from the table to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void AdminDashboard_Load(object sender, EventArgs e)
        {
      
        }
    }
}