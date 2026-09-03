namespace WinFormsApp1
{
    partial class RegisterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtRegUsername = new TextBox();
            txtRegPassword = new TextBox();
            cmbRole = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            btnBackToLogin = new Button();
            btnRegister = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(80, 74);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 0;
            label1.Text = "Username";
            // 
            // txtRegUsername
            // 
            txtRegUsername.Location = new Point(160, 71);
            txtRegUsername.Name = "txtRegUsername";
            txtRegUsername.Size = new Size(100, 23);
            txtRegUsername.TabIndex = 2;
            // 
            // txtRegPassword
            // 
            txtRegPassword.Location = new Point(160, 116);
            txtRegPassword.Name = "txtRegPassword";
            txtRegPassword.Size = new Size(100, 23);
            txtRegPassword.TabIndex = 3;
            txtRegPassword.UseSystemPasswordChar = true;
            // 
            // cmbRole
            // 
            cmbRole.FormattingEnabled = true;
            cmbRole.Items.AddRange(new object[] { "Customer", "Vendor" });
            cmbRole.Location = new Point(160, 168);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(100, 23);
            cmbRole.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(80, 119);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 1;
            label2.Text = "Password";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(107, 171);
            label3.Name = "label3";
            label3.Size = new Size(30, 15);
            label3.TabIndex = 5;
            label3.Text = "Role";
            // 
            // btnBackToLogin
            // 
            btnBackToLogin.Location = new Point(62, 227);
            btnBackToLogin.Name = "btnBackToLogin";
            btnBackToLogin.Size = new Size(91, 23);
            btnBackToLogin.TabIndex = 6;
            btnBackToLogin.Text = "Back to Login";
            btnBackToLogin.UseVisualStyleBackColor = true;
            btnBackToLogin.Click += btnBackToLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(221, 227);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(75, 23);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(407, 344);
            Controls.Add(btnRegister);
            Controls.Add(btnBackToLogin);
            Controls.Add(label3);
            Controls.Add(cmbRole);
            Controls.Add(txtRegPassword);
            Controls.Add(txtRegUsername);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RegisterForm";
            Text = "RegisterForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtRegUsername;
        private TextBox txtRegPassword;
        private ComboBox cmbRole;
        private Label label2;
        private Label label3;
        private Button btnBackToLogin;
        private Button btnRegister;
    }
}