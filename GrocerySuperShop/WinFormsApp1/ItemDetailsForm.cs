using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ItemDetailsForm : Form
    {
        private int itemId;
        private string itemName;
        private decimal itemPrice;

        private Label lblName;
        private Label lblCategory;
        private Label lblPrice;
        private Label lblReviews;
        private Label lblStatus;
        private NumericUpDown numQuantity; // The control for choosing the amount

        public ItemDetailsForm(int id, string name, string category, decimal price, string reviews, string status)
        {
            InitializeComponent();
            itemId = id;
            itemName = name;
            itemPrice = price;

            SetupDetailsUI(category, reviews, status);
        }

        private void SetupDetailsUI(string category, string reviews, string status)
        {
            this.Text = "Item Details";
            this.Width = 420;
            this.Height = 400;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblTitle = new Label() { Text = "Product Details", Font = new Font("Segoe UI", 14, FontStyle.Bold), Left = 30, Top = 20, Width = 300 };

            lblName = new Label() { Text = $"Name: {itemName}", Left = 30, Top = 70, Width = 350 };
            lblCategory = new Label() { Text = $"Category: {category}", Left = 30, Top = 110, Width = 350 };
            lblPrice = new Label() { Text = $"Price: ${itemPrice:F2}", Left = 30, Top = 150, Width = 350 };
            lblReviews = new Label() { Text = $"Rating / Reviews: {reviews} / 5.0", Left = 30, Top = 190, Width = 350 };
            lblStatus = new Label() { Text = $"Status: {status}", Left = 30, Top = 230, Width = 350 };

            // Quantity label and the NumericUpDown control named 'numQuantity'
            Label lblQty = new Label() { Text = "Quantity:", Left = 30, Top = 273, Width = 70 };
            numQuantity = new NumericUpDown()
            {
                Left = 105,
                Top = 270,
                Width = 80,
                Minimum = 1,   // Can't pick less than 1
                Maximum = 100, // Max limit 100
                Value = 1      // Starts at 1 by default
            };

            // Back button
            Button btnBack = new Button() { Text = "Back", Left = 30, Top = 315, Width = 100, Height = 30 };
            btnBack.Click += (s, e) => { this.Close(); };

            // Add to Cart button - reads what number is currently in numQuantity
            Button btnAddToCart = new Button() { Text = "Add to Cart", Left = 145, Top = 315, Width = 130, Height = 30, BackColor = Color.LightGreen };
            btnAddToCart.Click += (s, e) => {
                int selectedQty = (int)numQuantity.Value; // Gets the number chosen
                MessageBox.Show($"{selectedQty} x {itemName} selected to add! (Cart functionality coming soon)", "Add to Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            // Add everything to the form window
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblName);
            this.Controls.Add(lblCategory);
            this.Controls.Add(lblPrice);
            this.Controls.Add(lblReviews);
            this.Controls.Add(lblStatus);
            this.Controls.Add(lblQty);
            this.Controls.Add(numQuantity);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnAddToCart);
        }
    }
}