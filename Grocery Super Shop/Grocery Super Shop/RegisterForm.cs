using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public class RegisterForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtFullName;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private Button btnRegister;
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GrocerySuperShopDB;Integrated Security=True";

        public RegisterForm()
        {
            this.Text = "Register - Grocery Super Shop";
            this.Size = new Size(380, 360);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label { Text = "Create Account", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(40, 20), AutoSize = true };
            this.Controls.Add(lblTitle);

            this.Controls.Add(new Label { Text = "Username:", Location = new Point(40, 75), AutoSize = true });
            txtUsername = new TextBox { Location = new Point(130, 72), Width = 180 };
            this.Controls.Add(txtUsername);

            this.Controls.Add(new Label { Text = "Full Name:", Location = new Point(40, 120), AutoSize = true });
            txtFullName = new TextBox { Location = new Point(130, 117), Width = 180 };
            this.Controls.Add(txtFullName);

            this.Controls.Add(new Label { Text = "Password:", Location = new Point(40, 165), AutoSize = true });
            txtPassword = new TextBox { Location = new Point(130, 162), Width = 180, PasswordChar = '*' };
            this.Controls.Add(txtPassword);

            this.Controls.Add(new Label { Text = "Role:", Location = new Point(40, 210), AutoSize = true });
            cmbRole = new ComboBox { Location = new Point(130, 207), Width = 180, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new string[] { "Admin", "Customer" });
            cmbRole.SelectedIndex = 0; // Default to Admin
            this.Controls.Add(cmbRole);

            btnRegister = new Button { Text = "Register", Location = new Point(130, 260), Size = new Size(180, 32) };
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("All fields are required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Admins require approval (IsApproved = 0), Customers are auto-approved (IsApproved = 1)
            int isApproved = (role == "Admin") ? 0 : 1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();
                    string query = "INSERT INTO Users (Username, FullName, Password, Role, IsApproved) VALUES (@User, @Name, @Pass, @Role, @Approved)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@User", username);
                        cmd.Parameters.AddWithValue("@Name", fullName);
                        cmd.Parameters.AddWithValue("@Pass", password);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@Approved", isApproved);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (role == "Admin")
                {
                    MessageBox.Show("Registration submitted successfully! You must wait for Super Admin approval before logging in.", "Pending Approval", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during registration (Username may already exist): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}