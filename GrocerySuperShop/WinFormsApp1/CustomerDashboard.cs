using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class CustomerDashboard : Form
    {
        private TextBox txtSearchName;
        private Button btnSearch;
        private Button btnCart;
        private DataGridView dgvItems;

        public CustomerDashboard()
        {
            InitializeComponent();
            SetupDashboardUI();
            LoadCatalogData();
        }

        private void SetupDashboardUI()
        {
            this.Text = "Customer Dashboard - Product Catalog";
            this.Width = 800;
            this.Height = 550;
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblSearch = new Label() { Text = "Search Item Name:", Left = 20, Top = 25, Width = 110 };
            txtSearchName = new TextBox() { Left = 135, Top = 23, Width = 180 };

            btnSearch = new Button() { Text = "Search", Left = 325, Top = 21, Width = 80 };
            btnSearch.Click += BtnSearch_Click;

            btnCart = new Button() { Text = "CART", Left = 660, Top = 21, Width = 90, Height = 30, BackColor = Color.LightYellow };
            btnCart.Click += (s, e) => {
                CartForm cart = new CartForm();
                cart.ShowDialog();
            };

            dgvItems = new DataGridView()
            {
                Left = 20,
                Top = 70,
                Width = 730,
                Height = 400,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvItems.CellContentClick += DgvItems_CellContentClick;

            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearchName);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnCart);
            this.Controls.Add(dgvItems);
        }

        private void LoadCatalogData(string nameSearch = "")
        {
            try
            {
                string query = "SELECT ItemID AS [Item ID], ItemName AS [Item Name], Category, Price, ShopReviews AS [Reviews], Status FROM Items";
                SqlParameter[] parameters = null;

                if (!string.IsNullOrEmpty(nameSearch))
                {
                    query += " WHERE ItemName LIKE @SearchName";
                    parameters = new SqlParameter[] { new SqlParameter("@SearchName", $"%{nameSearch.Trim()}%") };
                }

                DataTable dt = DatabaseHelper.ExecuteSelectQuery(query, parameters);

                if (!dt.Columns.Contains("Action"))
                {
                    dgvItems.DataSource = null;
                    dgvItems.Columns.Clear();
                    dgvItems.DataSource = dt;

                    DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn()
                    {
                        Name = "Action",
                        HeaderText = "Actions",
                        Text = "SELECT",
                        UseColumnTextForButtonValue = true
                    };
                    dgvItems.Columns.Add(btnCol);
                }
                else
                {
                    dgvItems.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading catalog: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadCatalogData(txtSearchName.Text.Trim());
        }

        private void DgvItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvItems.Columns["Action"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvItems.Rows[e.RowIndex];
                int itemId = Convert.ToInt32(row.Cells["Item ID"].Value);
                string itemName = row.Cells["Item Name"].Value.ToString();
                string category = row.Cells["Category"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                string reviews = row.Cells["Reviews"].Value.ToString();
                string status = row.Cells["Status"].Value.ToString();

                ItemDetailsForm detailsForm = new ItemDetailsForm(itemId, itemName, category, price, reviews, status);
                detailsForm.ShowDialog();
            }
        }

        private void CustomerDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}