namespace minesmart
{
    partial class Product
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Product));
            this.txtproductname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnproduct = new System.Windows.Forms.Button();
            this.lblproduct = new System.Windows.Forms.Label();
            this.txtproductnme = new System.Windows.Forms.TextBox();
            this.btnproductsave = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtproductname
            // 
            this.txtproductname.AcceptsTab = true;
            resources.ApplyResources(this.txtproductname, "txtproductname");
            this.txtproductname.Name = "txtproductname";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Name = "label2";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            // 
            // btnproduct
            // 
            resources.ApplyResources(this.btnproduct, "btnproduct");
            this.btnproduct.Name = "btnproduct";
            // 
            // lblproduct
            // 
            resources.ApplyResources(this.lblproduct, "lblproduct");
            this.lblproduct.Name = "lblproduct";
            // 
            // txtproductnme
            // 
            resources.ApplyResources(this.txtproductnme, "txtproductnme");
            this.txtproductnme.Name = "txtproductnme";
            this.txtproductnme.Validated += new System.EventHandler(this.txtproductnme_Validated);
            // 
            // btnproductsave
            // 
            resources.ApplyResources(this.btnproductsave, "btnproductsave");
            this.btnproductsave.Name = "btnproductsave";
            this.btnproductsave.UseVisualStyleBackColor = true;
            this.btnproductsave.Click += new System.EventHandler(this.btnproductsave_Click);
            // 
            // btnclose
            // 
            resources.ApplyResources(this.btnclose, "btnclose");
            this.btnclose.Name = "btnclose";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // Product
            // 
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnproductsave);
            this.Controls.Add(this.txtproductnme);
            this.Controls.Add(this.lblproduct);
            this.Name = "Product";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtproductname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnproduct;
        private System.Windows.Forms.Label lblproduct;
        private System.Windows.Forms.TextBox txtproductnme;
        private System.Windows.Forms.Button btnproductsave;
        private System.Windows.Forms.Button btnclose;
    }
}

