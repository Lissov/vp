namespace GeneralCVSModel.ConfigurationEditor
{
    partial class ControlBaroreceptionModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlBaroreceptionModel));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cbRegulation = new System.Windows.Forms.CheckBox();
            this.cbWillis = new System.Windows.Forms.CheckBox();
            this.cbUseThermoHRDelta = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(290, 236);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // cbRegulation
            // 
            this.cbRegulation.AutoSize = true;
            this.cbRegulation.Location = new System.Drawing.Point(0, 242);
            this.cbRegulation.Name = "cbRegulation";
            this.cbRegulation.Size = new System.Drawing.Size(136, 17);
            this.cbRegulation.TabIndex = 2;
            this.cbRegulation.Text = "Simulate baroregulation";
            this.cbRegulation.UseVisualStyleBackColor = true;
            this.cbRegulation.CheckedChanged += new System.EventHandler(this.cbRegulation_CheckedChanged);
            // 
            // cbWillis
            // 
            this.cbWillis.AutoSize = true;
            this.cbWillis.Location = new System.Drawing.Point(0, 265);
            this.cbWillis.Name = "cbWillis";
            this.cbWillis.Size = new System.Drawing.Size(118, 17);
            this.cbWillis.TabIndex = 3;
            this.cbWillis.Text = "Simulate willis circle";
            this.cbWillis.UseVisualStyleBackColor = true;
            this.cbWillis.CheckedChanged += new System.EventHandler(this.cbWillis_CheckedChanged);
            // 
            // cbUseThermoHRDelta
            // 
            this.cbUseThermoHRDelta.AutoSize = true;
            this.cbUseThermoHRDelta.Location = new System.Drawing.Point(0, 288);
            this.cbUseThermoHRDelta.Name = "cbUseThermoHRDelta";
            this.cbUseThermoHRDelta.Size = new System.Drawing.Size(214, 17);
            this.cbUseThermoHRDelta.TabIndex = 4;
            this.cbUseThermoHRDelta.Text = "Simulate thermoregulation of Heart Rate";
            this.cbUseThermoHRDelta.UseVisualStyleBackColor = true;
            this.cbUseThermoHRDelta.CheckedChanged += new System.EventHandler(this.cbUseThermoHRDelta_CheckedChanged);
            // 
            // ControlBaroreceptionModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbUseThermoHRDelta);
            this.Controls.Add(this.cbWillis);
            this.Controls.Add(this.cbRegulation);
            this.Controls.Add(this.richTextBox1);
            this.Name = "ControlBaroreceptionModel";
            this.Size = new System.Drawing.Size(290, 496);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox cbRegulation;
        private System.Windows.Forms.CheckBox cbWillis;
        private System.Windows.Forms.CheckBox cbUseThermoHRDelta;
    }
}
