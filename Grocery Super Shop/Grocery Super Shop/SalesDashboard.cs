using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class SalesDashboard : Form
    {
        private DataGridView dgvSales;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public SalesDashboard()
        {
            this.Text = "Sales & Financial Analytics Dashboard";
            this.Size = new Size(800, 380);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Shop Sales & Financial Overview", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(25, 20), AutoSize = true });

            dgvSales = new DataGridView
            {
                Location = new Point(25, 70),
                Size = new Size(730, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvSales);

            Button btnClose = new Button { Text = "Close", Location = new Point(655, 285), Size = new Size(100, 32) };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            LoadSalesData();
        }

        private void LoadSalesData()
        {
            try
            {
                string query = @"SELECT 
                                    ShopName AS [Shop name], 
                                    WeeklySales AS [Weekly sales], 
                                    MonthlySales AS [Monthly Sales], 
                                    YearlySales AS [Yearly Sales], 
                                    CommissionEarned AS [Commission Earned], 
                                    TaxPayed AS [Tax Payed] 
                                 FROM SalesData";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvSales.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}