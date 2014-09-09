namespace Model_Baroreception
{
    partial class ReceptorZoneControl
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
            this.funcPanel = new VisualControls.FunctionEditPanel();
            this.lZone = new System.Windows.Forms.Label();
            this.cbUseZone = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbTreshold = new System.Windows.Forms.TextBox();
            this.tbElastic = new System.Windows.Forms.TextBox();
            this.tbWeight = new System.Windows.Forms.TextBox();
            this.tbDelay = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // funcPanel
            // 
            this.funcPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.funcPanel.EditorsVisible = true;
            this.funcPanel.Location = new System.Drawing.Point(3, 79);
            this.funcPanel.Name = "funcPanel";
            this.funcPanel.Size = new System.Drawing.Size(527, 223);
            this.funcPanel.TabIndex = 0;
            // 
            // lZone
            // 
            this.lZone.AutoSize = true;
            this.lZone.Location = new System.Drawing.Point(44, 9);
            this.lZone.Name = "lZone";
            this.lZone.Size = new System.Drawing.Size(35, 13);
            this.lZone.TabIndex = 2;
            this.lZone.Text = "label2";
            // 
            // cbUseZone
            // 
            this.cbUseZone.AutoSize = true;
            this.cbUseZone.Location = new System.Drawing.Point(3, 8);
            this.cbUseZone.Name = "cbUseZone";
            this.cbUseZone.Size = new System.Drawing.Size(45, 17);
            this.cbUseZone.TabIndex = 3;
            this.cbUseZone.Text = "Use";
            this.cbUseZone.UseVisualStyleBackColor = true;
            this.cbUseZone.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Treshold";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(262, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Weight";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Elastic coefficient";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(262, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Delay";
            // 
            // tbTreshold
            // 
            this.tbTreshold.Location = new System.Drawing.Point(104, 29);
            this.tbTreshold.Name = "tbTreshold";
            this.tbTreshold.Size = new System.Drawing.Size(88, 20);
            this.tbTreshold.TabIndex = 8;
            this.tbTreshold.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // tbElastic
            // 
            this.tbElastic.Location = new System.Drawing.Point(104, 51);
            this.tbElastic.Name = "tbElastic";
            this.tbElastic.Size = new System.Drawing.Size(88, 20);
            this.tbElastic.TabIndex = 9;
            this.tbElastic.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // tbWeight
            // 
            this.tbWeight.Location = new System.Drawing.Point(309, 29);
            this.tbWeight.Name = "tbWeight";
            this.tbWeight.Size = new System.Drawing.Size(88, 20);
            this.tbWeight.TabIndex = 10;
            this.tbWeight.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // tbDelay
            // 
            this.tbDelay.Location = new System.Drawing.Point(309, 51);
            this.tbDelay.Name = "tbDelay";
            this.tbDelay.Size = new System.Drawing.Size(88, 20);
            this.tbDelay.TabIndex = 11;
            this.tbDelay.TextChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "mmHg";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "of 1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(403, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "of 1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(403, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "s";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ReceptorZoneControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbDelay);
            this.Controls.Add(this.tbWeight);
            this.Controls.Add(this.tbElastic);
            this.Controls.Add(this.tbTreshold);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lZone);
            this.Controls.Add(this.cbUseZone);
            this.Controls.Add(this.funcPanel);
            this.Name = "ReceptorZoneControl";
            this.Size = new System.Drawing.Size(533, 305);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualControls.FunctionEditPanel funcPanel;
        private System.Windows.Forms.Label lZone;
        private System.Windows.Forms.CheckBox cbUseZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbTreshold;
        private System.Windows.Forms.TextBox tbElastic;
        private System.Windows.Forms.TextBox tbWeight;
        private System.Windows.Forms.TextBox tbDelay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
