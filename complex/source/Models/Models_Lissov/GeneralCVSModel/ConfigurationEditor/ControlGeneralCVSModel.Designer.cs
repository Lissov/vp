namespace GeneralCVSModel.ConfigurationEditor
{
    partial class ControlGeneralCVSModel
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbStep = new System.Windows.Forms.TextBox();
            this.btnStepDecrease = new System.Windows.Forms.Button();
            this.btnStepIncrease = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Step:";
            // 
            // tbStep
            // 
            this.tbStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStep.Location = new System.Drawing.Point(41, 10);
            this.tbStep.Name = "tbStep";
            this.tbStep.Size = new System.Drawing.Size(399, 20);
            this.tbStep.TabIndex = 1;
            // 
            // btnStepDecrease
            // 
            this.btnStepDecrease.Location = new System.Drawing.Point(41, 36);
            this.btnStepDecrease.Name = "btnStepDecrease";
            this.btnStepDecrease.Size = new System.Drawing.Size(33, 23);
            this.btnStepDecrease.TabIndex = 2;
            this.btnStepDecrease.Text = "/ 2";
            this.btnStepDecrease.UseVisualStyleBackColor = true;
            this.btnStepDecrease.Click += new System.EventHandler(this.btnStepDecrease_Click);
            // 
            // btnStepIncrease
            // 
            this.btnStepIncrease.Location = new System.Drawing.Point(80, 36);
            this.btnStepIncrease.Name = "btnStepIncrease";
            this.btnStepIncrease.Size = new System.Drawing.Size(33, 23);
            this.btnStepIncrease.TabIndex = 3;
            this.btnStepIncrease.Text = "* 2";
            this.btnStepIncrease.UseVisualStyleBackColor = true;
            this.btnStepIncrease.Click += new System.EventHandler(this.btnStepIncrease_Click);
            // 
            // ControlGeneralCVSModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnStepIncrease);
            this.Controls.Add(this.btnStepDecrease);
            this.Controls.Add(this.tbStep);
            this.Controls.Add(this.label1);
            this.Name = "ControlGeneralCVSModel";
            this.Size = new System.Drawing.Size(443, 213);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStep;
        private System.Windows.Forms.Button btnStepDecrease;
        private System.Windows.Forms.Button btnStepIncrease;
    }
}
