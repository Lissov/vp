namespace Model_Load
{
    partial class ParameterLoadFunctionEditControl
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
            this.group = new System.Windows.Forms.GroupBox();
            this.tbInitial = new System.Windows.Forms.TextBox();
            this.labelInitial = new System.Windows.Forms.Label();
            this.btnChange = new System.Windows.Forms.Button();
            this.checkSimulate = new System.Windows.Forms.CheckBox();
            this.functionPanel = new VisualControls.FunctionEditPanel();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // group
            // 
            this.group.Controls.Add(this.tbInitial);
            this.group.Controls.Add(this.labelInitial);
            this.group.Controls.Add(this.btnChange);
            this.group.Controls.Add(this.checkSimulate);
            this.group.Controls.Add(this.functionPanel);
            this.group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group.Location = new System.Drawing.Point(0, 0);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(400, 196);
            this.group.TabIndex = 0;
            this.group.TabStop = false;
            this.group.Text = "groupBox1";
            // 
            // tbInitial
            // 
            this.tbInitial.Location = new System.Drawing.Point(171, 17);
            this.tbInitial.Name = "tbInitial";
            this.tbInitial.Size = new System.Drawing.Size(76, 20);
            this.tbInitial.TabIndex = 4;
            this.tbInitial.TextChanged += new System.EventHandler(this.tbInitial_TextChanged);
            // 
            // labelInitial
            // 
            this.labelInitial.Location = new System.Drawing.Point(89, 20);
            this.labelInitial.Name = "labelInitial";
            this.labelInitial.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelInitial.Size = new System.Drawing.Size(85, 18);
            this.labelInitial.TabIndex = 3;
            this.labelInitial.Text = ":Initial value";
            // 
            // btnChange
            // 
            this.btnChange.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChange.Location = new System.Drawing.Point(297, 15);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(97, 23);
            this.btnChange.TabIndex = 2;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // checkSimulate
            // 
            this.checkSimulate.AutoSize = true;
            this.checkSimulate.Location = new System.Drawing.Point(6, 19);
            this.checkSimulate.Name = "checkSimulate";
            this.checkSimulate.Size = new System.Drawing.Size(66, 17);
            this.checkSimulate.TabIndex = 1;
            this.checkSimulate.Text = "Simulate";
            this.checkSimulate.UseVisualStyleBackColor = true;
            this.checkSimulate.CheckedChanged += new System.EventHandler(this.checkSimulate_CheckedChanged);
            // 
            // functionPanel
            // 
            this.functionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.functionPanel.EditorsVisible = false;
            this.functionPanel.Location = new System.Drawing.Point(6, 42);
            this.functionPanel.Name = "functionPanel";
            this.functionPanel.Size = new System.Drawing.Size(388, 148);
            this.functionPanel.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ParameterLoadFunctionEditControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.group);
            this.Name = "ParameterLoadFunctionEditControl";
            this.Size = new System.Drawing.Size(400, 196);
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox group;
        private VisualControls.FunctionEditPanel functionPanel;
        private System.Windows.Forms.TextBox tbInitial;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        public System.Windows.Forms.Button btnChange;
        public System.Windows.Forms.CheckBox checkSimulate;
        public System.Windows.Forms.Label labelInitial;

    }
}
