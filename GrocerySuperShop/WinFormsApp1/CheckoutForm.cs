using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class CheckoutForm : Form
    {
        private TextBox txtName;
        private TextBox txtContact;
        private TextBox txtAddress;
        private TextBox txtEmail;
        private ComboBox cmbPaymentMethod;

        private DataGridView dgvSummary;
        private Label lblSubtotal;
        private Label lblDelivery;
        private Label lblFinalTotal;

        private Button btnBack;
        private Button btnCheckOut;

        private DataTable cartItemsData;
        private decimal subtotalAmount;
        private const decimal DeliveryCharge = 5.00m;

        public CheckoutForm(DataTable cartItems, decimal subtotal)
        {
            InitializeComponent();
            cartItemsData = cartItems;
            subtotalAmount = subtotal;

            SetupCheckoutUI();
        }

        private void SetupCheckoutUI()
        {
            this.Text = "Checkout";
            this.Width = 550;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;

            // Title
            Label lblTitle = new Label() { Text = "Checkout Details", Font = new Font("Segoe UI", 14, FontStyle.Bold), Left = 25, Top = 20, Width = 250 };

            // Customer Info Fields
            Label lblN = new Label() { Text = "Name:", Left = 25, Top = 65, Width = 100 };
            txtName = new TextBox() { Left = 130, Top = 63, Width = 370 };

            Label lblC = new Label() { Text = "Contact:", Left = 25, Top = 105, Width = 100 };
            txtContact = new TextBox() { Left = 130, Top = 103, Width = 370 };

            Label lblA = new Label() { Text = "Address:", Left = 25, Top = 145, Width = 100 };
            txtAddress = new TextBox() { Left = 130, Top = 143, Width = 370 };

            Label lblE = new Label() { Text = "E-mail:", Left = 25, Top = 185, Width = 100 };
            txtEmail = new TextBox() { Left = 130, Top = 183, Width = 370 };

            // Payment Method Selection
            Label lblPay = new Label() { Text = "Payment Method:", Left = 25, Top = 225, Width = 100 };
            cmbPaymentMethod = new ComboBox() { Left = 130, Top = 223, Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbPaymentMethod.Items.AddRange(new string[] { "Cash On Delivery", "Bkash/Nagad", "Credit/Debit Card" });
            cmbPaymentMethod.SelectedIndex = 0;

            // Small Table for Selected Items Summary
            Label lblTableTitle = new Label() { Text = "Order Summary:", Font = new Font("Segoe UI", 10, FontStyle.Bold), Left = 25, Top = 265, Width = 200 };
            dgvSummary = new DataGridView()
            {
                Left = 25,
                Top = 290,
                Width = 475,
                Height = 150,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = cartItemsData
            };

            // Pricing Labels
            decimal finalTotal = subtotalAmount + DeliveryCharge;
            lblSubtotal = new Label() { Text = $"Subtotal: ${subtotalAmount:F2}", Left = 330, Top = 450, Width = 170, TextAlign = ContentAlignment.MiddleRight };
            lblDelivery = new Label() { Text = $"Delivery Charge: ${DeliveryCharge:F2}", Left = 330, Top = 475, Width = 170, TextAlign = ContentAlignment.MiddleRight };
            lblFinalTotal = new Label() { Text = $"Total: ${finalTotal:F2}", Font = new Font("Segoe UI", 11, FontStyle.Bold), Left = 300, Top = 505, Width = 200, TextAlign = ContentAlignment.MiddleRight };

            // Action Buttons
            btnBack = new Button() { Text = "Back", Left = 25, Top = 560, Width = 120, Height = 35 };
            btnBack.Click += (s, e) => { this.Close(); };

            btnCheckOut = new Button() { Text = "CheckOut", Left = 380, Top = 560, Width = 120, Height = 35, BackColor = Color.LightGreen, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            btnCheckOut.Click += BtnCheckOut_Click;

            // Add Controls to Form
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblN); this.Controls.Add(txtName);
            this.Controls.Add(lblC); this.Controls.Add(txtContact);
            this.Controls.Add(lblA); this.Controls.Add(txtAddress);
            this.Controls.Add(lblE); this.Controls.Add(txtEmail);
            this.Controls.Add(lblPay); this.Controls.Add(cmbPaymentMethod);
            this.Controls.Add(lblTableTitle);
            this.Controls.Add(dgvSummary);
            this.Controls.Add(lblSubtotal);
            this.Controls.Add(lblDelivery);
            this.Controls.Add(lblFinalTotal);
            this.Controls.Add(btnBack);
            this.Controls.Add(btnCheckOut);
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please fill in all customer information fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Show success message
            MessageBox.Show("ORDER PLACED", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Close current and cart windows, returning/opening CustomerDashboard
            this.Close();
        }
    }
}