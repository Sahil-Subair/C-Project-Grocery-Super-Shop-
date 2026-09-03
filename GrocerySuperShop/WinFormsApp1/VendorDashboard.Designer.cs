namespace WinFormsApp1
{
    partial class VendorDashboard
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
            txtSearchItemID = new TextBox();
            btnSearchItem = new Button();
            btnAddItem = new Button();
            btnRemoveItem = new Button();
            btnViewOffers = new Button();
            dgvItems = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(75, 61);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 0;
            label1.Text = "Search Item ID:";
            // 
            // txtSearchItemID
            // 
            txtSearchItemID.Location = new Point(167, 58);
            txtSearchItemID.Name = "txtSearchItemID";
            txtSearchItemID.Size = new Size(100, 23);
            txtSearchItemID.TabIndex = 1;
            // 
            // btnSearchItem
            // 
            btnSearchItem.Location = new Point(289, 58);
            btnSearchItem.Name = "btnSearchItem";
            btnSearchItem.Size = new Size(75, 23);
            btnSearchItem.TabIndex = 2;
            btnSearchItem.Text = "Search";
            btnSearchItem.UseVisualStyleBackColor = true;
            // 
            // btnAddItem
            // 
            btnAddItem.Location = new Point(524, 61);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(75, 23);
            btnAddItem.TabIndex = 3;
            btnAddItem.Text = "+ Add Item";
            btnAddItem.UseVisualStyleBackColor = true;
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.Location = new Point(605, 61);
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(86, 23);
            btnRemoveItem.TabIndex = 4;
            btnRemoveItem.Text = "Remove Item";
            btnRemoveItem.UseVisualStyleBackColor = true;
            // 
            // btnViewOffers
            // 
            btnViewOffers.Location = new Point(697, 61);
            btnViewOffers.Name = "btnViewOffers";
            btnViewOffers.Size = new Size(86, 23);
            btnViewOffers.TabIndex = 5;
            btnViewOffers.Text = "View Offers";
            btnViewOffers.UseVisualStyleBackColor = true;
            // 
            // dgvItems
            // 
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Location = new Point(12, 106);
            dgvItems.Name = "dgvItems";
            dgvItems.Size = new Size(776, 332);
            dgvItems.TabIndex = 6;
            // 
            // VendorDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvItems);
            Controls.Add(btnViewOffers);
            Controls.Add(btnRemoveItem);
            Controls.Add(btnAddItem);
            Controls.Add(btnSearchItem);
            Controls.Add(txtSearchItemID);
            Controls.Add(label1);
            Name = "VendorDashboard";
            Text = "VendorDashboard";
            Load += VendorDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtSearchItemID;
        private Button btnSearchItem;
        private Button btnAddItem;
        private Button btnRemoveItem;
        private Button btnViewOffers;
        private DataGridView dgvItems;
    }
}