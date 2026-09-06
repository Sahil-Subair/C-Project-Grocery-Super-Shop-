using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class AdminDashboard : Form
    {
        private TextBox txtSearchProductID;
        private Button btnSearch;
        private Button btnAddProduct;
        private Button btnInventory;
        private Button btnSales;
        private Button btnOffers;
        private DataGridView dgvProducts;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public AdminDashboard()
        {
            this.Text = "Admin Dashboard - Grocery Super Shop";
            this.Size = new Size(850, 520);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Search Product ID:", Location = new Point(25, 28), AutoSize = true });
            txtSearchProductID = new TextBox { Location = new Point(140, 25), Width = 90 };
            this.Controls.Add(txtSearchProductID);

            btnSearch = new Button { Text = "Search", Location = new Point(240, 23), Size = new Size(75, 28) };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            btnAddProduct = new Button { Text = "Add Product", Location = new Point(325, 23), Size = new Size(95, 28) };
            btnAddProduct.Click += BtnAddProduct_Click;
            this.Controls.Add(btnAddProduct);

            btnInventory = new Button { Text = "Inventory", Location = new Point(520, 22), Size = new Size(85, 30) };
            btnInventory.Click += (s, e) => new InventoryDashboard().ShowDialog();
            this.Controls.Add(btnInventory);

            btnSales = new Button { Text = "Sales", Location = new Point(615, 22), Size = new Size(75, 30) };
            btnSales.Click += (s, e) => new AdminSalesDashboard().ShowDialog();
            this.Controls.Add(btnSales);

            btnOffers = new Button { Text = "Offers", Location = new Point(700, 22), Size = new Size(85, 30) };
            btnOffers.Click += (s, e) => new OfferDashboard().ShowDialog();
            this.Controls.Add(btnOffers);

            dgvProducts = new DataGridView
            {
                Location = new Point(25, 75),
                Size = new Size(780, 380),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvProducts);

            LoadProductData();
        }

        private void LoadProductData()
        {
            try
            {
                string query = "SELECT ProductID AS [Product ID], ProductName AS [Product Name], Category AS [Category], Price AS [Price] FROM Products";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvProducts.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string id = txtSearchProductID.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                LoadProductData();
                return;
            }

            try
            {
                string query = "SELECT ProductID AS [Product ID], ProductName AS [Product Name], Category AS [Category], Price AS [Price] FROM Products WHERE ProductID = @ID";
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvProducts.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            Form addForm = new Form { Text = "Add New Product", Size = new Size(350, 320), StartPosition = FormStartPosition.CenterParent };

            addForm.Controls.Add(new Label { Text = "Product Name:", Location = new Point(30, 30), AutoSize = true });
            TextBox txtName = new TextBox { Location = new Point(130, 27), Width = 160 };
            addForm.Controls.Add(txtName);

            addForm.Controls.Add(new Label { Text = "Category:", Location = new Point(30, 80), AutoSize = true });
            TextBox txtCat = new TextBox { Location = new Point(130, 77), Width = 160 };
            addForm.Controls.Add(txtCat);

            addForm.Controls.Add(new Label { Text = "Price:", Location = new Point(30, 130), AutoSize = true });
            TextBox txtPrice = new TextBox { Location = new Point(130, 127), Width = 160 };
            addForm.Controls.Add(txtPrice);

            Button btnSubmit = new Button { Text = "Add to Table", Location = new Point(130, 185), Size = new Size(160, 35) };
            btnSubmit.Click += (s, args) =>
            {
                string name = txtName.Text.Trim();
                string category = txtCat.Text.Trim();
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(category) || !decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
                {
                    MessageBox.Show("Please enter valid product details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = @"INSERT INTO Products (ProductName, Category, Price) VALUES (@Name, @Cat, @Price);
                                         INSERT INTO Inventory (ProductID, StockAmount) VALUES (SCOPE_IDENTITY(), 50);";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@Cat", category);
                            cmd.Parameters.AddWithValue("@Price", price);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    addForm.Close();
                    LoadProductData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            addForm.Controls.Add(btnSubmit);
            addForm.ShowDialog();
        }
    }
}