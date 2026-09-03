using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class CartForm : Form
    {
        private DataGridView dgvCart;
        private Label lblTotalPrice;
        private Button btnRemoveItem;
        private Button btnBack;
        private Button btnCheckout;

        // A DataTable to hold cart items (Product Name, Category, Amount, Price)
        private DataTable cartTable;

        public CartForm()
        {
            InitializeComponent();
            SetupCartUI();
            LoadSampleCartData(); // Temporary sample data to test layout & total calculation
        }

        private void SetupCartUI()
        {
            this.Text = "Shopping Cart";
            this.Width = 650;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            // Header Label
            Label lblCartTitle = new Label()
            {
                Text = "CART",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Left = 25,
                Top = 20,
                Width = 200
            };

            // DataGridView for Cart Items
            dgvCart = new DataGridView()
            {
                Left = 25,
                Top = 70,
                Width = 580,
                Height = 270,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            // Total Price Label
            lblTotalPrice = new Label()
            {
                Text = "Total Price: $0.00",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Left = 400,
                Top = 355,
                Width = 205,
                TextAlign = ContentAlignment.MiddleRight
            };

            // Action Buttons
            btnRemoveItem = new Button()
            {
                Text = "Remove Selected",
                Left = 25,
                Top = 400,
                Width = 140,
                Height = 35,
                BackColor = Color.LightCoral
            };
            btnRemoveItem.Click += BtnRemoveItem_Click;

            btnBack = new Button()
            {
                Text = "Back to Dashboard",
                Left = 180,
                Top = 400,
                Width = 140,
                Height = 35
            };
            btnBack.Click += (s, e) => { this.Close(); };

            btnCheckout = new Button()
            {
                Text = "Checkout",
                Left = 465,
                Top = 400,
                Width = 140,
                Height = 35,
                BackColor = Color.LightGreen,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            btnCheckout.Click += BtnCheckout_Click;

            // Add controls to form
            this.Controls.Add(lblCartTitle);
            this.Controls.Add(dgvCart);
            this.Controls.Add(lblTotalPrice);
            this.Controls.Add(btnRemoveItem);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnCheckout);
        }

        private void LoadSampleCartData()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("Product Name", typeof(string));
            cartTable.Columns.Add("Category", typeof(string));
            cartTable.Columns.Add("Amount", typeof(int));
            cartTable.Columns.Add("Price", typeof(decimal));

            // Adding a couple of test rows to verify layout & totals
            cartTable.Rows.Add("Milk", "Dairy", 2, 3.50m);
            cartTable.Rows.Add("Bread", "Bakery", 1, 2.80m);

            dgvCart.DataSource = cartTable;
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                int amount = Convert.ToInt32(row["Amount"]);
                decimal price = Convert.ToDecimal(row["Price"]);
                total += (amount * price);
            }
            lblTotalPrice.Text = $"Total Price: ${total:F2}";
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCart.SelectedRows.Count > 0)
            {
                int selectedIndex = dgvCart.SelectedRows[0].Index;
                cartTable.Rows[selectedIndex].Delete();
                cartTable.AcceptChanges();
                CalculateTotal();
                MessageBox.Show("Item removed from cart.", "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an item to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Add some items before checking out!", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate subtotal
            decimal subtotal = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                int amount = Convert.ToInt32(row["Amount"]);
                decimal price = Convert.ToDecimal(row["Price"]);
                subtotal += (amount * price);
            }

            // Open CheckoutForm and pass the cart data and subtotal
            CheckoutForm checkout = new CheckoutForm(cartTable, subtotal);
            checkout.ShowDialog();

            // After checkout window closes, clear cart and close cart form to return to dashboard
            cartTable.Rows.Clear();
            this.Close();
        }
    }
}