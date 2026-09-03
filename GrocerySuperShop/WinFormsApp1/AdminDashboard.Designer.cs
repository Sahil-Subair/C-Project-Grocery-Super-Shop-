namespace WinFormsApp1
{
    partial class AdminDashboard
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
            txtSearchID = new TextBox();
            btnSearch = new Button();
            btnAddShop = new Button();
            btnRemoveShop = new Button();
            dgvShops = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvShops).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(31, 45);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Search Shop ID:";
            // 
            // txtSearchID
            // 
            txtSearchID.Location = new Point(126, 42);
            txtSearchID.Name = "txtSearchID";
            txtSearchID.Size = new Size(100, 23);
            txtSearchID.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(244, 42);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 23);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnAddShop
            // 
            btnAddShop.Location = new Point(569, 42);
            btnAddShop.Name = "btnAddShop";
            btnAddShop.Size = new Size(75, 23);
            btnAddShop.TabIndex = 3;
            btnAddShop.Text = "Add Shop";
            btnAddShop.UseVisualStyleBackColor = true;
            // 
            // btnRemoveShop
            // 
            btnRemoveShop.Location = new Point(684, 42);
            btnRemoveShop.Name = "btnRemoveShop";
            btnRemoveShop.Size = new Size(89, 23);
            btnRemoveShop.TabIndex = 4;
            btnRemoveShop.Text = "Remove Shop";
            btnRemoveShop.UseVisualStyleBackColor = true;
            // 
            // dgvShops
            // 
            dgvShops.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvShops.Location = new Point(31, 87);
            dgvShops.Name = "dgvShops";
            dgvShops.Size = new Size(742, 351);
            dgvShops.TabIndex = 5;
            // 
            // AdminDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvShops);
            Controls.Add(btnRemoveShop);
            Controls.Add(btnAddShop);
            Controls.Add(btnSearch);
            Controls.Add(txtSearchID);
            Controls.Add(label1);
            Name = "AdminDashboard";
            Text = "AdminDashboard";
            Load += AdminDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dgvShops).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtSearchID;
        private Button btnSearch;
        private Button btnAddShop;
        private Button btnRemoveShop;
        private DataGridView dgvShops;
    }
}