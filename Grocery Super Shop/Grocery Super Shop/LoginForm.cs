using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public LoginForm()
        {
            this.Text = "Login - Grocery Super Shop";
            this.Size = new Size(380, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label { Text = "System Login", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(40, 20), AutoSize = true };
            this.Controls.Add(lblTitle);

            this.Controls.Add(new Label { Text = "Username:", Location = new Point(40, 75), AutoSize = true });
            txtUsername = new TextBox { Location = new Point(130, 72), Width = 180 };
            this.Controls.Add(txtUsername);

            this.Controls.Add(new Label { Text = "Password:", Location = new Point(40, 120), AutoSize = true });
            txtPassword = new TextBox { Location = new Point(130, 117), Width = 180, PasswordChar = '*' };
            this.Controls.Add(txtPassword);

            btnLogin = new Button { Text = "Login", Location = new Point(130, 170), Size = new Size(85, 32) };
            btnLogin.Click += BtnLogin_Click;
            this.Controls.Add(btnLogin);

            btnRegister = new Button { Text = "Register", Location = new Point(225, 170), Size = new Size(85, 32) };
            btnRegister.Click += (s, e) => { new RegisterForm().ShowDialog(); };
            this.Controls.Add(btnRegister);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Please enter username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT Role, IsApproved FROM Users WHERE Username = @User AND Password = @Pass";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@User", user);
                        cmd.Parameters.AddWithValue("@Pass", pass);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string role = reader["Role"].ToString();
                                int isApproved = Convert.ToInt32(reader["IsApproved"]);

                                if (isApproved == 0 && role != "Customer")
                                {
                                    MessageBox.Show("Your account is pending Super Admin approval. You cannot log in yet.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                MessageBox.Show($"Login Successful as {role}!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();

                                if (role == "SuperAdmin")
                                {
                                    new SuperAdminDashboard().ShowDialog();
                                }
                                else if (role == "Admin")
                                {
                                    new AdminDashboard().ShowDialog();
                                }
                                else if (role == "Customer")
                                {
                                    new CustomerDashboard().ShowDialog();
                                }

                                this.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}