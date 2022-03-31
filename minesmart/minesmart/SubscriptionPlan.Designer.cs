namespace minesmart
{
    partial class SubscriptionPlan
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.btnplan = new System.Windows.Forms.Button();
            this.lblkey = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(70, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "SUBSCRIPTION PLAN";
            // 
            // txtUserId
            // 
            this.txtUserId.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtUserId.Location = new System.Drawing.Point(61, 72);
            this.txtUserId.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(285, 26);
            this.txtUserId.TabIndex = 4;
            // 
            // btnplan
            // 
            this.btnplan.BackColor = System.Drawing.Color.PapayaWhip;
            this.btnplan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnplan.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnplan.Location = new System.Drawing.Point(61, 132);
            this.btnplan.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnplan.Name = "btnplan";
            this.btnplan.Size = new System.Drawing.Size(285, 36);
            this.btnplan.TabIndex = 6;
            this.btnplan.Text = "Subscribe";
            this.btnplan.UseVisualStyleBackColor = false;
            this.btnplan.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblkey
            // 
            this.lblkey.AutoSize = true;
            this.lblkey.Location = new System.Drawing.Point(61, 50);
            this.lblkey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblkey.Name = "lblkey";
            this.lblkey.Size = new System.Drawing.Size(26, 15);
            this.lblkey.TabIndex = 7;
            this.lblkey.Text = "Key";
            // 
            // SubscriptionPlan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 201);
            this.Controls.Add(this.lblkey);
            this.Controls.Add(this.btnplan);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "SubscriptionPlan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubscriptionPlan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Button btnplan;
        private System.Windows.Forms.Label lblkey;
    }
}