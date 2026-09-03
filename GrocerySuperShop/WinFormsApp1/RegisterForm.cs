using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtRegUsername.Text.Trim();
            string password = txtRegPassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all fields and select a role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";

            System.Data.SqlClient.SqlParameter[] parameters = {
        new System.Data.SqlClient.SqlParameter("@Username", username),
        new System.Data.SqlClient.SqlParameter("@Password", password),
        new System.Data.SqlClient.SqlParameter("@Role", role)
    };

            try
            {
                DatabaseHelper.ExecuteQuery(query, parameters);
                MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Close register form back to login
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during registration (Username may already exist): " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBackToLogin_Click(object sender, EventArgs e)
        {
            this.Close(); // Closes the register form and returns to the login form
        }
    }
}
