using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class VendorDashboard : Form
    {
        public VendorDashboard()
        {
            InitializeComponent();
            LoadItemsData();

            // Wire up event handlers
            btnSearchItem.Click += BtnSearchItem_Click;
            btnAddItem.Click += BtnAddItem_Click;
            btnRemoveItem.Click += BtnRemoveItem_Click;
            btnViewOffers.Click += BtnViewOffers_Click;
        }

        private void LoadItemsData(string itemIdSearch = "")
        {
            try
            {
                string query = "SELECT ItemID AS [Item ID], ItemName AS [Item Name], Category, Price, " +
                               "StockAmount AS [Stock Amount], TotalSales AS [Total Sales], " +
                               "ShopReviews AS [Shop Reviews], Status FROM Items";

                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(itemIdSearch))
                {
                    query += " WHERE ItemID = @ItemID";
                    parameters = new SqlParameter[] { new SqlParameter("@ItemID", itemIdSearch.Trim()) };
                }

                DataTable dt = DatabaseHelper.ExecuteSelectQuery(query, parameters);
                dgvItems.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearchItem_Click(object sender, EventArgs e)
        {
            string searchId = txtSearchItemID.Text.Trim();
            if (string.IsNullOrEmpty(searchId))
            {
                LoadItemsData();
            }
            else if (int.TryParse(searchId, out _))
            {
                LoadItemsData(searchId);
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric Item ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 360,
                Height = 280,
                Text = "Add New Item",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lbl1 = new Label() { Text = "Item Name:", Left = 20, Top = 20, Width = 100 };
            TextBox txtItemName = new TextBox() { Left = 130, Top = 20, Width = 180 };

            Label lbl2 = new Label() { Text = "Category:", Left = 20, Top = 60, Width = 100 };
            TextBox txtCategory = new TextBox() { Left = 130, Top = 60, Width = 180 };

            Label lbl3 = new Label() { Text = "Price:", Left = 20, Top = 100, Width = 100 };
            TextBox txtPrice = new TextBox() { Left = 130, Top = 100, Width = 180 };

            Label lbl4 = new Label() { Text = "Stock Amount:", Left = 20, Top = 140, Width = 100 };
            TextBox txtStock = new TextBox() { Left = 130, Top = 140, Width = 180 };

            Button confirmation = new Button() { Text = "Save", Left = 130, Top = 190, Width = 80, DialogResult = DialogResult.OK };
            confirmation.Click += (s, ev) => { prompt.Close(); };

            prompt.Controls.Add(lbl1);
            prompt.Controls.Add(txtItemName);
            prompt.Controls.Add(lbl2);
            prompt.Controls.Add(txtCategory);
            prompt.Controls.Add(lbl3);
            prompt.Controls.Add(txtPrice);
            prompt.Controls.Add(lbl4);
            prompt.Controls.Add(txtStock);
            prompt.Controls.Add(confirmation);

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                string itemName = txtItemName.Text.Trim();
                string category = txtCategory.Text.Trim();

                if (!string.IsNullOrEmpty(itemName) && !string.IsNullOrEmpty(category) &&
                    decimal.TryParse(txtPrice.Text.Trim(), out decimal price) &&
                    int.TryParse(txtStock.Text.Trim(), out int stock))
                {
                    try
                    {
                        string query = "INSERT INTO Items (ItemName, Category, Price, StockAmount, TotalSales, ShopReviews, Status) VALUES (@ItemName, @Category, @Price, @Stock, 0, 0, 'Available')";
                        SqlParameter[] parameters = {
                            new SqlParameter("@ItemName", itemName),
                            new SqlParameter("@Category", category),
                            new SqlParameter("@Price", price),
                            new SqlParameter("@Stock", stock)
                        };

                        DatabaseHelper.ExecuteQuery(query, parameters);
                        MessageBox.Show("Item added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItemsData();
                        txtSearchItemID.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please check your inputs. Ensure Price and Stock are valid numbers.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
            {
                int itemId = Convert.ToInt32(dgvItems.SelectedRows[0].Cells["Item ID"].Value);

                DialogResult result = MessageBox.Show($"Are you sure you want to remove Item ID {itemId}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string query = "DELETE FROM Items WHERE ItemID = @ItemID";
                        SqlParameter[] parameters = { new SqlParameter("@ItemID", itemId) };

                        DatabaseHelper.ExecuteQuery(query, parameters);
                        MessageBox.Show("Item removed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadItemsData();
                        txtSearchItemID.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error removing item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item row from the table to remove.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnViewOffers_Click(object sender, EventArgs e)
        {
            OffersDashboard offersForm = new OffersDashboard();
            offersForm.ShowDialog();
        }

        private void VendorDashboard_Load(object sender, EventArgs e)
        {
            // Initialization logic if needed
        }
    }
}