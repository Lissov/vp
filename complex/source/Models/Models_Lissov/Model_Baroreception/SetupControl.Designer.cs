namespace Model_Baroreception
{
    partial class SetupControl
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
            this.btnLoadVPOle = new System.Windows.Forms.Button();
            this.cbUseBaroreflex = new System.Windows.Forms.CheckBox();
            this.tabZones = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // btnLoadVPOle
            // 
            this.btnLoadVPOle.Location = new System.Drawing.Point(3, 3);
            this.btnLoadVPOle.Name = "btnLoadVPOle";
            this.btnLoadVPOle.Size = new System.Drawing.Size(144, 23);
            this.btnLoadVPOle.TabIndex = 0;
            this.btnLoadVPOle.Text = "Load VP_Ole parameters";
            this.btnLoadVPOle.UseVisualStyleBackColor = true;
            this.btnLoadVPOle.Click += new System.EventHandler(this.btnLoadVPOle_Click);
            // 
            // cbUseBaroreflex
            // 
            this.cbUseBaroreflex.AutoSize = true;
            this.cbUseBaroreflex.Location = new System.Drawing.Point(3, 45);
            this.cbUseBaroreflex.Name = "cbUseBaroreflex";
            this.cbUseBaroreflex.Size = new System.Drawing.Size(167, 17);
            this.cbUseBaroreflex.TabIndex = 1;
            this.cbUseBaroreflex.Text = "Baroreception as at start point";
            this.cbUseBaroreflex.UseVisualStyleBackColor = true;
            this.cbUseBaroreflex.CheckedChanged += new System.EventHandler(this.cbUseBaroreflex_CheckedChanged);
            // 
            // tabZones
            // 
            this.tabZones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabZones.Location = new System.Drawing.Point(3, 68);
            this.tabZones.Name = "tabZones";
            this.tabZones.SelectedIndex = 0;
            this.tabZones.Size = new System.Drawing.Size(460, 159);
            this.tabZones.TabIndex = 2;
            // 
            // SetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabZones);
            this.Controls.Add(this.cbUseBaroreflex);
            this.Controls.Add(this.btnLoadVPOle);
            this.Name = "SetupControl";
            this.Size = new System.Drawing.Size(466, 258);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadVPOle;
        private System.Windows.Forms.CheckBox cbUseBaroreflex;
        private System.Windows.Forms.TabControl tabZones;
    }
}
