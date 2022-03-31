
namespace minesmart
{
    partial class UCCamersetting
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblConRcamera = new System.Windows.Forms.Label();
            this.streamPlayerControl2 = new WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl();
            this.streamPlayerControl1 = new WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl();
            this.lblConfcamera1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblConRcamera
            // 
            this.lblConRcamera.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConRcamera.AutoSize = true;
            this.lblConRcamera.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConRcamera.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConRcamera.Location = new System.Drawing.Point(10, 332);
            this.lblConRcamera.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConRcamera.Name = "lblConRcamera";
            this.lblConRcamera.Size = new System.Drawing.Size(108, 19);
            this.lblConRcamera.TabIndex = 165;
            this.lblConRcamera.Text = "REAR CAMERA";
            // 
            // streamPlayerControl2
            // 
            this.streamPlayerControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.streamPlayerControl2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.streamPlayerControl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.streamPlayerControl2.Location = new System.Drawing.Point(10, 354);
            this.streamPlayerControl2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.streamPlayerControl2.Name = "streamPlayerControl2";
            this.streamPlayerControl2.Size = new System.Drawing.Size(262, 190);
            this.streamPlayerControl2.TabIndex = 163;
            // 
            // streamPlayerControl1
            // 
            this.streamPlayerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.streamPlayerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.streamPlayerControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.streamPlayerControl1.Location = new System.Drawing.Point(10, 134);
            this.streamPlayerControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.streamPlayerControl1.Name = "streamPlayerControl1";
            this.streamPlayerControl1.Size = new System.Drawing.Size(262, 190);
            this.streamPlayerControl1.TabIndex = 161;
            // 
            // lblConfcamera1
            // 
            this.lblConfcamera1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConfcamera1.AutoSize = true;
            this.lblConfcamera1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConfcamera1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblConfcamera1.Location = new System.Drawing.Point(10, 112);
            this.lblConfcamera1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfcamera1.Name = "lblConfcamera1";
            this.lblConfcamera1.Size = new System.Drawing.Size(118, 19);
            this.lblConfcamera1.TabIndex = 160;
            this.lblConfcamera1.Text = "FRONT CAMERA";
            // 
            // richTextBox1
            // 
            this.richTextBox1.AutoWordSelection = true;
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.No;
            this.richTextBox1.Font = new System.Drawing.Font("Verdana", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.ForeColor = System.Drawing.Color.Red;
            this.richTextBox1.Location = new System.Drawing.Point(10, 27);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(23);
            this.richTextBox1.Multiline = false;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(246, 85);
            this.richTextBox1.TabIndex = 159;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UCCamersetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblConRcamera);
            this.Controls.Add(this.streamPlayerControl2);
            this.Controls.Add(this.streamPlayerControl1);
            this.Controls.Add(this.lblConfcamera1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "UCCamersetting";
            this.Size = new System.Drawing.Size(310, 558);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConRcamera;
        public WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl streamPlayerControl2;
        public WebEye.Controls.WinForms.StreamPlayerControl.StreamPlayerControl streamPlayerControl1;
        private System.Windows.Forms.Label lblConfcamera1;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer1;
    }
}
