using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";

            SqlParameter[] parameters = {
                new SqlParameter("@Username", txtUsername.Text.Trim()),
                new SqlParameter("@Password", txtPassword.Text.Trim())
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);

            if (result != null)
            {
                string role = result.ToString();
                MessageBox.Show($"Login Successful! Welcome, {role}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (role == "Admin")
                {
                    AdminDashboard adminDashboard = new AdminDashboard();
                    this.Hide();
                    adminDashboard.ShowDialog();
                    this.Close();
                }
                else if (role == "Vendor")
                {
                    VendorDashboard vendorDashboard = new VendorDashboard();
                    this.Hide();
                    vendorDashboard.ShowDialog();
                    this.Close();
                }
                else if (role == "Customer")
                {
                    CustomerDashboard customerDashboard = new CustomerDashboard();
                    this.Hide();
                    customerDashboard.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Welcome {role}! Dashboard coming soon.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Invalid Username or Password.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenRegister_Click(object sender, EventArgs e)
        {
            RegisterForm regForm = new RegisterForm();
            regForm.ShowDialog();
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}