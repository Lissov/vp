namespace GeneralCVSModel.ConfigurationEditor
{
    partial class ControlKidneyModel
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
            this.cbKidney = new System.Windows.Forms.CheckBox();
            this.cbRegulation = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(297, 290);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "Kidney model describes excretion of fluids. Filtration and reabsorstion in kidney" +
                "s are described, reabsorbtion rate depends on osmotic pressure in kidney capilla" +
                "res.";
            // 
            // cbKidney
            // 
            this.cbKidney.AutoSize = true;
            this.cbKidney.Location = new System.Drawing.Point(3, 321);
            this.cbKidney.Name = "cbKidney";
            this.cbKidney.Size = new System.Drawing.Size(136, 17);
            this.cbKidney.TabIndex = 2;
            this.cbKidney.Text = "Simulate kidney activity";
            this.cbKidney.UseVisualStyleBackColor = true;
            this.cbKidney.CheckedChanged += new System.EventHandler(this.cbKidney_CheckedChanged);
            // 
            // cbRegulation
            // 
            this.cbRegulation.AutoSize = true;
            this.cbRegulation.Location = new System.Drawing.Point(3, 344);
            this.cbRegulation.Name = "cbRegulation";
            this.cbRegulation.Size = new System.Drawing.Size(175, 17);
            this.cbRegulation.TabIndex = 3;
            this.cbRegulation.Text = "Simulate filtration rate regulation";
            this.cbRegulation.UseVisualStyleBackColor = true;
            this.cbRegulation.CheckedChanged += new System.EventHandler(this.cbRegulation_CheckedChanged);
            // 
            // ControlKidneyModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbRegulation);
            this.Controls.Add(this.cbKidney);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ControlKidneyModel";
            this.Size = new System.Drawing.Size(297, 492);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox cbKidney;
        private System.Windows.Forms.CheckBox cbRegulation;
    }
}
