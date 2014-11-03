namespace GeneralCVSModel.ConfigurationEditor
{
    partial class ControlEnergyModel
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbTimeKoeff = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(284, 57);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Energy model represents the energetic functions as a dynamics of ATD.\n";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Time koeff.";
            // 
            // tbTimeKoeff
            // 
            this.tbTimeKoeff.Location = new System.Drawing.Point(75, 63);
            this.tbTimeKoeff.Name = "tbTimeKoeff";
            this.tbTimeKoeff.Size = new System.Drawing.Size(100, 20);
            this.tbTimeKoeff.TabIndex = 3;
            this.tbTimeKoeff.TextChanged += new System.EventHandler(this.tbTimeKoeff_TextChanged);
            // 
            // ControlEnergyModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTimeKoeff);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ControlEnergyModel";
            this.Size = new System.Drawing.Size(284, 413);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTimeKoeff;
    }
}
