namespace WinFormsApp1
{
    partial class OffersDashboard
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
            txtSearchOfferID = new TextBox();
            btnSearchOffer = new Button();
            btnCreateOffer = new Button();
            btnRemoveOffer = new Button();
            dgvOffers = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvOffers).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 68);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 0;
            label1.Text = "Search Offer ID:";
            // 
            // txtSearchOfferID
            // 
            txtSearchOfferID.Location = new Point(145, 65);
            txtSearchOfferID.Name = "txtSearchOfferID";
            txtSearchOfferID.Size = new Size(100, 23);
            txtSearchOfferID.TabIndex = 1;
            // 
            // btnSearchOffer
            // 
            btnSearchOffer.Location = new Point(276, 65);
            btnSearchOffer.Name = "btnSearchOffer";
            btnSearchOffer.Size = new Size(75, 23);
            btnSearchOffer.TabIndex = 2;
            btnSearchOffer.Text = "Search";
            btnSearchOffer.UseVisualStyleBackColor = true;
            // 
            // btnCreateOffer
            // 
            btnCreateOffer.Location = new Point(577, 68);
            btnCreateOffer.Name = "btnCreateOffer";
            btnCreateOffer.Size = new Size(98, 23);
            btnCreateOffer.TabIndex = 3;
            btnCreateOffer.Text = "+ Create Offer";
            btnCreateOffer.UseVisualStyleBackColor = true;
            // 
            // btnRemoveOffer
            // 
            btnRemoveOffer.Location = new Point(681, 68);
            btnRemoveOffer.Name = "btnRemoveOffer";
            btnRemoveOffer.Size = new Size(98, 23);
            btnRemoveOffer.TabIndex = 4;
            btnRemoveOffer.Text = "Remove Offer";
            btnRemoveOffer.UseVisualStyleBackColor = true;
            // 
            // dgvOffers
            // 
            dgvOffers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOffers.Location = new Point(12, 111);
            dgvOffers.Name = "dgvOffers";
            dgvOffers.Size = new Size(776, 327);
            dgvOffers.TabIndex = 5;
            // 
            // OffersDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvOffers);
            Controls.Add(btnRemoveOffer);
            Controls.Add(btnCreateOffer);
            Controls.Add(btnSearchOffer);
            Controls.Add(txtSearchOfferID);
            Controls.Add(label1);
            Name = "OffersDashboard";
            Text = "OffersDashboard";
            Load += OffersDashboard_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOffers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtSearchOfferID;
        private Button btnSearchOffer;
        private Button btnCreateOffer;
        private Button btnRemoveOffer;
        private DataGridView dgvOffers;
    }
}