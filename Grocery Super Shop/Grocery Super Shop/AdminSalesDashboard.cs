using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class AdminSalesDashboard : Form
    {
        private TextBox txtSearchProductID;
        private Button btnSearch;
        private DataGridView dgvSales;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public AdminSalesDashboard()
        {
            this.Text = "Product Sales Analytics";
            this.Size = new Size(700, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Search Product ID:", Location = new Point(25, 28), AutoSize = true });
            txtSearchProductID = new TextBox { Location = new Point(140, 25), Width = 90 };
            this.Controls.Add(txtSearchProductID);

            btnSearch = new Button { Text = "Search", Location = new Point(240, 23), Size = new Size(75, 28) };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            dgvSales = new DataGridView
            {
                Location = new Point(25, 75),
                Size = new Size(625, 260),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvSales);

            LoadSalesData();
        }

        private void LoadSalesData()
        {
            try
            {
                string query = @"SELECT 
                                    P.ProductID AS [Product ID], 
                                    P.ProductName AS [Product Name], 
                                    S.TotalSold AS [Total Sold], 
                                    S.TotalEarned AS [Total Earned] 
                                 FROM ProductSales S 
                                 JOIN Products P ON S.ProductID = P.ProductID";

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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string id = txtSearchProductID.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                LoadSalesData();
                return;
            }

            try
            {
                string query = @"SELECT 
                                    P.ProductID AS [Product ID], 
                                    P.ProductName AS [Product Name], 
                                    S.TotalSold AS [Total Sold], 
                                    S.TotalEarned AS [Total Earned] 
                                 FROM ProductSales S 
                                 JOIN Products P ON S.ProductID = P.ProductID 
                                 WHERE P.ProductID = @ID";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvSales.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}