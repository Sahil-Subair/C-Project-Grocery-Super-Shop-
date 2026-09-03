namespace WinFormsApp1
{
    partial class CustomerDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // CustomerDashboard
            // 
            ClientSize = new Size(284, 261);
            Name = "CustomerDashboard";
            Load += CustomerDashboard_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}