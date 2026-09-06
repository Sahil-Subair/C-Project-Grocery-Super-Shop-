using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class InventoryDashboard : Form
    {
        private TextBox txtSearchProductID;
        private Button btnSearch;
        private Button btnOrderProduct;
        private DataGridView dgvInventory;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public InventoryDashboard()
        {
            this.Text = "Inventory Management";
            this.Size = new Size(700, 420);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Controls.Add(new Label { Text = "Search Product ID:", Location = new Point(25, 28), AutoSize = true });
            txtSearchProductID = new TextBox { Location = new Point(140, 25), Width = 90 };
            this.Controls.Add(txtSearchProductID);

            btnSearch = new Button { Text = "Search", Location = new Point(240, 23), Size = new Size(75, 28) };
            btnSearch.Click += BtnSearch_Click;
            this.Controls.Add(btnSearch);

            btnOrderProduct = new Button { Text = "Order Product", Location = new Point(535, 22), Size = new Size(115, 30) };
            btnOrderProduct.Click += BtnOrderProduct_Click;
            this.Controls.Add(btnOrderProduct);

            dgvInventory = new DataGridView
            {
                Location = new Point(25, 75),
                Size = new Size(625, 280),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            this.Controls.Add(dgvInventory);

            LoadInventoryData();
        }

        private void LoadInventoryData()
        {
            try
            {
                string query = @"SELECT 
                                    P.ProductID AS [Product ID], 
                                    P.ProductName AS [Product Name], 
                                    I.StockAmount AS [Stock Amount] 
                                 FROM Inventory I 
                                 JOIN Products P ON I.ProductID = P.ProductID";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvInventory.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading inventory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string id = txtSearchProductID.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                LoadInventoryData();
                return;
            }

            try
            {
                string query = @"SELECT 
                                    P.ProductID AS [Product ID], 
                                    P.ProductName AS [Product Name], 
                                    I.StockAmount AS [Stock Amount] 
                                 FROM Inventory I 
                                 JOIN Products P ON I.ProductID = P.ProductID 
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
                            dgvInventory.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOrderProduct_Click(object sender, EventArgs e)
        {
            Form orderForm = new Form { Text = "Order Product Stock", Size = new Size(350, 280), StartPosition = FormStartPosition.CenterParent };

            orderForm.Controls.Add(new Label { Text = "Product Name:", Location = new Point(30, 30), AutoSize = true });
            TextBox txtName = new TextBox { Location = new Point(130, 27), Width = 160 };
            orderForm.Controls.Add(txtName);

            orderForm.Controls.Add(new Label { Text = "Category:", Location = new Point(30, 80), AutoSize = true });
            TextBox txtCategory = new TextBox { Location = new Point(130, 77), Width = 160 };
            orderForm.Controls.Add(txtCategory);

            orderForm.Controls.Add(new Label { Text = "Order Quantity:", Location = new Point(30, 130), AutoSize = true });
            TextBox txtQty = new TextBox { Location = new Point(130, 127), Width = 160 };
            orderForm.Controls.Add(txtQty);

            Button btnSubmitOrder = new Button { Text = "Order Product", Location = new Point(130, 175), Size = new Size(160, 35) };
            btnSubmitOrder.Click += (s, args) =>
            {
                string pName = txtName.Text.Trim();
                string category = txtCategory.Text.Trim();
                if (string.IsNullOrEmpty(pName) || !int.TryParse(txtQty.Text.Trim(), out int qty))
                {
                    MessageBox.Show("Please enter valid product name and quantity.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        conn.Open();
                        string checkProd = "SELECT ProductID FROM Products WHERE ProductName = @Name";
                        int prodID = 0;
                        using (SqlCommand cmdCheck = new SqlCommand(checkProd, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@Name", pName);
                            object res = cmdCheck.ExecuteScalar();
                            if (res != null)
                            {
                                prodID = Convert.ToInt32(res);
                            }
                            else
                            {
                                string insertProd = "INSERT INTO Products (ProductName, Category, Price) VALUES (@Name, @Cat, 1.00); SELECT SCOPE_IDENTITY();";
                                using (SqlCommand cmdIns = new SqlCommand(insertProd, conn))
                                {
                                    cmdIns.Parameters.AddWithValue("@Name", pName);
                                    cmdIns.Parameters.AddWithValue("@Cat", string.IsNullOrEmpty(category) ? "General" : category);
                                    prodID = Convert.ToInt32(cmdIns.ExecuteScalar());
                                }
                            }
                        }

                        string checkInv = "SELECT InventoryID FROM Inventory WHERE ProductID = @PID";
                        using (SqlCommand cmdInvCheck = new SqlCommand(checkInv, conn))
                        {
                            cmdInvCheck.Parameters.AddWithValue("@PID", prodID);
                            object invRes = cmdInvCheck.ExecuteScalar();
                            if (invRes != null)
                            {
                                string updateInv = "UPDATE Inventory SET StockAmount = StockAmount + @Qty WHERE ProductID = @PID";
                                using (SqlCommand cmdUpd = new SqlCommand(updateInv, conn))
                                {
                                    cmdUpd.Parameters.AddWithValue("@Qty", qty);
                                    cmdUpd.Parameters.AddWithValue("@PID", prodID);
                                    cmdUpd.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                string insertInv = "INSERT INTO Inventory (ProductID, StockAmount) VALUES (@PID, @Qty)";
                                using (SqlCommand cmdInsInv = new SqlCommand(insertInv, conn))
                                {
                                    cmdInsInv.Parameters.AddWithValue("@PID", prodID);
                                    cmdInsInv.Parameters.AddWithValue("@Qty", qty);
                                    cmdInsInv.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    MessageBox.Show("Product ordered and added to inventory table successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    orderForm.Close();
                    LoadInventoryData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error ordering product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            orderForm.Controls.Add(btnSubmitOrder);
            orderForm.ShowDialog();
        }
    }
}