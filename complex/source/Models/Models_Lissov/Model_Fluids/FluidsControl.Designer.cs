namespace Model_Fluids
{
    partial class FluidsControl
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnLoadConfiguation = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.modelSetupControlSimple1 = new LissovBase.ModelSetupControlSimple();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.paramsGridControl1 = new Model_Fluids.ParamsGridControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(335, 239);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnLoadConfiguation);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(327, 213);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnLoadConfiguation
            // 
            this.btnLoadConfiguation.Location = new System.Drawing.Point(6, 6);
            this.btnLoadConfiguation.Name = "btnLoadConfiguation";
            this.btnLoadConfiguation.Size = new System.Drawing.Size(144, 23);
            this.btnLoadConfiguation.TabIndex = 0;
            this.btnLoadConfiguation.Text = "Load Configuration";
            this.btnLoadConfiguation.UseVisualStyleBackColor = true;
            this.btnLoadConfiguation.Click += new System.EventHandler(this.btnLoadConfiguation_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.modelSetupControlSimple1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(327, 213);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // modelSetupControlSimple1
            // 
            this.modelSetupControlSimple1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelSetupControlSimple1.Location = new System.Drawing.Point(3, 3);
            this.modelSetupControlSimple1.Name = "modelSetupControlSimple1";
            this.modelSetupControlSimple1.Size = new System.Drawing.Size(321, 207);
            this.modelSetupControlSimple1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.paramsGridControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(327, 213);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Parameters";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // paramsGridControl1
            // 
            this.paramsGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paramsGridControl1.Location = new System.Drawing.Point(3, 3);
            this.paramsGridControl1.Name = "paramsGridControl1";
            this.paramsGridControl1.Size = new System.Drawing.Size(321, 207);
            this.paramsGridControl1.TabIndex = 0;
            // 
            // FluidsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "FluidsControl";
            this.Size = new System.Drawing.Size(335, 239);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private LissovBase.ModelSetupControlSimple modelSetupControlSimple1;
        private System.Windows.Forms.Button btnLoadConfiguation;
        private System.Windows.Forms.TabPage tabPage3;
        private ParamsGridControl paramsGridControl1;
    }
}
