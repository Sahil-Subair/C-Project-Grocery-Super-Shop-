using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public static class CartManager
    {
        public static DataTable CartTable = new DataTable();

        static CartManager()
        {
            if (CartTable.Columns.Count == 0)
            {
                CartTable.Columns.Add("Product ID", typeof(int));
                CartTable.Columns.Add("Product Name", typeof(string));
                CartTable.Columns.Add("Category", typeof(string));
                CartTable.Columns.Add("Price", typeof(decimal));
                CartTable.Columns.Add("Total Amount", typeof(int));
            }
        }

        public static void AddItem(int prodId, string name, string category, decimal price, int amount)
        {
            foreach (DataRow row in CartTable.Rows)
            {
                if (Convert.ToInt32(row["Product ID"]) == prodId)
                {
                    row["Total Amount"] = Convert.ToInt32(row["Total Amount"]) + amount;
                    return;
                }
            }
            CartTable.Rows.Add(prodId, name, category, price, amount);
        }

        public static void Clear()
        {
            CartTable.Rows.Clear();
        }
    }

    public class CustomerDashboard : Form
    {
        private TextBox txtSearchProductID;
        private Button btnSearch;
        private Button btnCart;
        private DataGridView dgvProducts;
        private Button btnSelectProduct;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public CustomerDashboard()
        {
            this.Text = "Customer Dashboard - Grocery Super Shop";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Search Product ID:", Location = new Point(25, 28), AutoSize = true });
            txtSearchProductID = new TextBox { Location = new Point(140, 25), Width = 90 };
            this.Controls.Add(txtSearchProductID);

            btnSearch = new Button { Text = "Search", Location = new Point(240, 23), Size = new Size(75, 28) };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            btnCart = new Button { Text = "Cart", Location = new Point(675, 22), Size = new Size(75, 30) };
            btnCart.Click += (s, e) => { new CartDashboard(this).ShowDialog(); };
            this.Controls.Add(btnCart);

            dgvProducts = new DataGridView
            {
                Location = new Point(25, 75),
                Size = new Size(725, 330),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            this.Controls.Add(dgvProducts);

            btnSelectProduct = new Button { Text = "Select Product", Location = new Point(25, 415), Size = new Size(120, 32) };
            btnSelectProduct.Click += BtnSelectProduct_Click;
            this.Controls.Add(btnSelectProduct);

            LoadProductData();
        }

        public void LoadProductData()
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

        private void BtnSelectProduct_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a product row first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int prodId = Convert.ToInt32(dgvProducts.SelectedRows[0].Cells["Product ID"].Value);
            new ProductDetailsForm(prodId, this).ShowDialog();
        }
    }

    public class ProductDetailsForm : Form
    {
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";
        private int productId;
        private CustomerDashboard parentDashboard;
        private string productName;
        private string category;
        private decimal price;
        private int stockAmount;

        private TextBox txtAmount;

        public ProductDetailsForm(int prodId, CustomerDashboard parent)
        {
            productId = prodId;
            parentDashboard = parent;

            this.Text = "Product Details";
            this.Size = new Size(380, 360);
            this.StartPosition = FormStartPosition.CenterParent;

            FetchDetails();

            Label lblHeader = new Label { Text = "PRODUCT DETAILS", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(30, 20), AutoSize = true };
            this.Controls.Add(lblHeader);

            this.Controls.Add(new Label { Text = "Product Name: " + productName, Location = new Point(30, 65), AutoSize = true });
            this.Controls.Add(new Label { Text = "Category: " + category, Location = new Point(30, 100), AutoSize = true });
            this.Controls.Add(new Label { Text = "Available in Shop: " + stockAmount, Location = new Point(30, 135), AutoSize = true });
            this.Controls.Add(new Label { Text = "Price: $" + price.ToString("0.00"), Location = new Point(30, 170), AutoSize = true });

            this.Controls.Add(new Label { Text = "Order Amount:", Location = new Point(30, 210), AutoSize = true });
            txtAmount = new TextBox { Location = new Point(150, 207), Width = 150, Text = "1" };
            this.Controls.Add(txtAmount);

            Button btnAddToCart = new Button { Text = "Add to Cart", Location = new Point(30, 260), Size = new Size(130, 32) };
            btnAddToCart.Click += BtnAddToCart_Click;
            this.Controls.Add(btnAddToCart);

            Button btnBack = new Button { Text = "Back to Dashboard", Location = new Point(175, 260), Size = new Size(130, 32) };
            btnBack.Click += (s, e) => this.Close();
            this.Controls.Add(btnBack);
        }

        private void FetchDetails()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = @"SELECT P.ProductName, P.Category, P.Price, ISNULL(I.StockAmount, 0) AS StockAmount 
                                     FROM Products P 
                                     LEFT JOIN Inventory I ON P.ProductID = I.ProductID 
                                     WHERE P.ProductID = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", productId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                productName = reader["ProductName"].ToString();
                                category = reader["Category"].ToString();
                                price = Convert.ToDecimal(reader["Price"]);
                                stockAmount = Convert.ToInt32(reader["StockAmount"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtAmount.Text.Trim(), out int orderQty) || orderQty <= 0)
            {
                MessageBox.Show("Please enter a valid amount.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (orderQty > stockAmount)
            {
                MessageBox.Show("Requested amount exceeds available stock in shop!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CartManager.AddItem(productId, productName, category, price, orderQty);
            MessageBox.Show("Product added to cart successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }

    public class CartDashboard : Form
    {
        private DataGridView dgvCart;
        private CustomerDashboard parentDashboard;

        public CartDashboard(CustomerDashboard parent)
        {
            parentDashboard = parent;

            this.Text = "Shopping Cart";
            this.Size = new Size(650, 420);
            this.StartPosition = FormStartPosition.CenterParent;

            dgvCart = new DataGridView
            {
                Location = new Point(25, 25),
                Size = new Size(585, 270),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = CartManager.CartTable
            };
            this.Controls.Add(dgvCart);

            Button btnRemove = new Button { Text = "Remove Item", Location = new Point(25, 315), Size = new Size(110, 32) };
            btnRemove.Click += BtnRemove_Click;
            this.Controls.Add(btnRemove);

            Button btnBack = new Button { Text = "Back to Dashboard", Location = new Point(145, 315), Size = new Size(135, 32) };
            btnBack.Click += (s, e) => this.Close();
            this.Controls.Add(btnBack);

            Button btnCheckout = new Button { Text = "Check out", Location = new Point(480, 315), Size = new Size(130, 32) };
            btnCheckout.Click += BtnCheckout_Click;
            this.Controls.Add(btnCheckout);
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = dgvCart.SelectedRows[0].Index;
            CartManager.CartTable.Rows.RemoveAt(rowIndex);
            MessageBox.Show("Item removed from cart.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (CartManager.CartTable.Rows.Count == 0)
            {
                MessageBox.Show("Your cart is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Close();
            new CheckoutForm().ShowDialog();
        }
    }

    public class CheckoutForm : Form
    {
        private TextBox txtFullName, txtContact, txtEmail, txtAddress;
        private ComboBox cmbPaymentMethod;
        private Label lblTotalPaid;
        private DataGridView dgvCheckoutItems;

        public CheckoutForm()
        {
            this.Text = "Checkout Process";
            this.Size = new Size(650, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            this.Controls.Add(new Label { Text = "Full Name:", Location = new Point(25, 20), AutoSize = true });
            txtFullName = new TextBox { Location = new Point(130, 17), Width = 180 };
            this.Controls.Add(txtFullName);

            this.Controls.Add(new Label { Text = "Contact:", Location = new Point(25, 55), AutoSize = true });
            txtContact = new TextBox { Location = new Point(130, 52), Width = 180 };
            this.Controls.Add(txtContact);

            this.Controls.Add(new Label { Text = "Email:", Location = new Point(25, 90), AutoSize = true });
            txtEmail = new TextBox { Location = new Point(130, 87), Width = 180 };
            this.Controls.Add(txtEmail);

            this.Controls.Add(new Label { Text = "Address:", Location = new Point(25, 125), AutoSize = true });
            txtAddress = new TextBox { Location = new Point(130, 122), Width = 180 };
            this.Controls.Add(txtAddress);

            // Payment method ComboBox inside a GroupBox to ensure single selection
            GroupBox gbPayment = new GroupBox { Text = "Payment Method", Location = new Point(330, 15), Size = new Size(280, 130) };

            cmbPaymentMethod = new ComboBox
            {
                Location = new Point(20, 45),
                Width = 235,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPaymentMethod.Items.Add("Cash on Delivery");
            cmbPaymentMethod.Items.Add("Bkash / Nagad");
            cmbPaymentMethod.Items.Add("Credit / Debit Card");
            cmbPaymentMethod.SelectedIndex = 0; // Default selection

            gbPayment.Controls.Add(cmbPaymentMethod);
            this.Controls.Add(gbPayment);

            dgvCheckoutItems = new DataGridView
            {
                Location = new Point(25, 180),
                Size = new Size(585, 160),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            DataTable checkoutView = new DataTable();
            checkoutView.Columns.Add("Product Name", typeof(string));
            checkoutView.Columns.Add("Amount", typeof(int));
            checkoutView.Columns.Add("Price", typeof(decimal));

            foreach (DataRow r in CartManager.CartTable.Rows)
            {
                checkoutView.Rows.Add(r["Product Name"], r["Total Amount"], r["Price"]);
            }
            dgvCheckoutItems.DataSource = checkoutView;
            this.Controls.Add(dgvCheckoutItems);

            decimal subTotal = 0;
            foreach (DataRow r in CartManager.CartTable.Rows)
            {
                subTotal += Convert.ToDecimal(r["Price"]) * Convert.ToInt32(r["Total Amount"]);
            }
            decimal finalTotal = subTotal + 5.00m;

            lblTotalPaid = new Label { Text = $"Total To be paid (incl. $5.00 delivery): ${finalTotal.ToString("0.00")}", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(25, 360), AutoSize = true };
            this.Controls.Add(lblTotalPaid);

            Button btnBackToCart = new Button { Text = "Back to Cart", Location = new Point(25, 410), Size = new Size(130, 35) };
            btnBackToCart.Click += (s, e) => { this.Close(); new CartDashboard(null).ShowDialog(); };
            this.Controls.Add(btnBackToCart);

            Button btnFinalCheckout = new Button { Text = "CHECKOUT", Location = new Point(480, 410), Size = new Size(130, 35) };
            btnFinalCheckout.Click += BtnFinalCheckout_Click;
            this.Controls.Add(btnFinalCheckout);
        }

        private void BtnFinalCheckout_Click(object sender, EventArgs e)
        {
            string name = txtFullName.Text.Trim();
            string contact = txtContact.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(contact) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please fill out all personal details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPaymentMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment method.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Order successfully placed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            new OrderHistoryDashboard().ShowDialog();
        }
    }

    public class OrderHistoryDashboard : Form
    {
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";
        private DataGridView dgvOrderHistory;

        public OrderHistoryDashboard()
        {
            this.Text = "Order History & Invoices";
            this.Size = new Size(650, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvOrderHistory = new DataGridView
            {
                Location = new Point(25, 25),
                Size = new Size(585, 280),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true
            };

            DataTable singleTable = new DataTable();
            singleTable.Columns.Add("Product Name", typeof(string));
            singleTable.Columns.Add("Amount", typeof(int));
            singleTable.Columns.Add("Price", typeof(decimal));
            singleTable.Columns.Add("Date of Order", typeof(string));

            foreach (DataRow r in CartManager.CartTable.Rows)
            {
                singleTable.Rows.Add(r["Product Name"], r["Total Amount"], r["Price"], DateTime.Now.ToString("yyyy-MM-dd"));
            }
            dgvOrderHistory.DataSource = singleTable;
            this.Controls.Add(dgvOrderHistory);

            Button btnPrint = new Button
            {
                Text = "PRINT INVOICE",
                Location = new Point(25, 325),
                Size = new Size(150, 35)
            };
            btnPrint.Click += (s, args) =>
            {
                if (dgvOrderHistory.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item row from the table first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var selectedRow = dgvOrderHistory.SelectedRows[0];
                MessageBox.Show($"Printing invoice", "Invoice Printed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };
            this.Controls.Add(btnPrint);

            Button btnReview = new Button
            {
                Text = "Add Review",
                Location = new Point(190, 325),
                Size = new Size(150, 35)
            };
            btnReview.Click += (s, args) =>
            {
                OpenAddReviewDialog();
            };
            this.Controls.Add(btnReview);

            Button btnClose = new Button
            {
                Text = "Close",
                Location = new Point(460, 325),
                Size = new Size(150, 35)
            };
            btnClose.Click += (s, args) => this.Close();
            this.Controls.Add(btnClose);

            CartManager.Clear();
        }

        private void OpenAddReviewDialog()
        {
            Form reviewForm = new Form { Text = "Add Review", Size = new Size(350, 260), StartPosition = FormStartPosition.CenterParent };

            reviewForm.Controls.Add(new Label { Text = "Shop Name:", Location = new Point(25, 20), AutoSize = true });
            TextBox txtShop = new TextBox { Location = new Point(120, 17), Width = 180, Text = "Grocery Super Shop" };
            reviewForm.Controls.Add(txtShop);

            reviewForm.Controls.Add(new Label { Text = "Review Comment:", Location = new Point(25, 60), AutoSize = true });
            TextBox txtComment = new TextBox { Location = new Point(120, 57), Width = 180, Height = 60, Multiline = true };
            reviewForm.Controls.Add(txtComment);

            Button btnSubmit = new Button { Text = "Submit Review", Location = new Point(120, 140), Size = new Size(180, 32) };
            btnSubmit.Click += (s, args) =>
            {
                string shop = txtShop.Text.Trim();
                string comment = txtComment.Text.Trim();

                if (string.IsNullOrEmpty(shop) || string.IsNullOrEmpty(comment))
                {
                    MessageBox.Show("Please fill out both fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string query = "INSERT INTO Reviews (ShopName, ReviewText) VALUES (@Shop, @Text)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Shop", shop);
                            cmd.Parameters.AddWithValue("@Text", comment);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Review added successfully! It will appear in the Super Admin dashboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reviewForm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving review: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            reviewForm.Controls.Add(btnSubmit);
            reviewForm.ShowDialog();
        }
    }
}