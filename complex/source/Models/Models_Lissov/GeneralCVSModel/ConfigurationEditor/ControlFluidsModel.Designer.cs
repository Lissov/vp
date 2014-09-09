namespace GeneralCVSModel.ConfigurationEditor
{
    partial class ControlFluidsModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlFluidsModel));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cbFiltrationRegulation = new System.Windows.Forms.CheckBox();
            this.cbSimulate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(284, 316);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // cbFiltrationRegulation
            // 
            this.cbFiltrationRegulation.AutoSize = true;
            this.cbFiltrationRegulation.Location = new System.Drawing.Point(3, 345);
            this.cbFiltrationRegulation.Name = "cbFiltrationRegulation";
            this.cbFiltrationRegulation.Size = new System.Drawing.Size(418, 17);
            this.cbFiltrationRegulation.TabIndex = 2;
            this.cbFiltrationRegulation.Text = "Simulate dependence of capillary filtration coefficient from central venous press" +
                "ure. ";
            this.cbFiltrationRegulation.UseVisualStyleBackColor = true;
            this.cbFiltrationRegulation.CheckedChanged += new System.EventHandler(this.cbFiltrationRegulation_CheckedChanged);
            // 
            // cbSimulate
            // 
            this.cbSimulate.AutoSize = true;
            this.cbSimulate.Location = new System.Drawing.Point(3, 322);
            this.cbSimulate.Name = "cbSimulate";
            this.cbSimulate.Size = new System.Drawing.Size(321, 17);
            this.cbSimulate.TabIndex = 3;
            this.cbSimulate.Text = "Simulate flids exchange between cellular and interstitial spaces";
            this.cbSimulate.UseVisualStyleBackColor = true;
            this.cbSimulate.CheckedChanged += new System.EventHandler(this.cbSimulate_CheckedChanged);
            // 
            // ControlFluidsModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbSimulate);
            this.Controls.Add(this.cbFiltrationRegulation);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ControlFluidsModel";
            this.Size = new System.Drawing.Size(284, 413);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox cbFiltrationRegulation;
        private System.Windows.Forms.CheckBox cbSimulate;
    }
}
