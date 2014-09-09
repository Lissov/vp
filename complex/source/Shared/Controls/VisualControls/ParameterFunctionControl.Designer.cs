namespace VisualControls
{
    partial class ParameterFunctionControl
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
            this.fePanel = new VisualControls.FunctionEditPanel();
            this.cbFunctionType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fePanel
            // 
            this.fePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fePanel.Location = new System.Drawing.Point(0, 27);
            this.fePanel.Name = "fePanel";
            this.fePanel.Size = new System.Drawing.Size(569, 319);
            this.fePanel.TabIndex = 5;
            // 
            // cbFunctionType
            // 
            this.cbFunctionType.FormattingEnabled = true;
            this.cbFunctionType.Location = new System.Drawing.Point(83, 0);
            this.cbFunctionType.Name = "cbFunctionType";
            this.cbFunctionType.Size = new System.Drawing.Size(209, 21);
            this.cbFunctionType.TabIndex = 4;
            this.cbFunctionType.SelectedIndexChanged += new System.EventHandler(this.cbFunctionType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Function type:";
            // 
            // ParameterFunctionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fePanel);
            this.Controls.Add(this.cbFunctionType);
            this.Controls.Add(this.label1);
            this.Name = "ParameterFunctionControl";
            this.Size = new System.Drawing.Size(569, 346);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FunctionEditPanel fePanel;
        private System.Windows.Forms.ComboBox cbFunctionType;
        private System.Windows.Forms.Label label1;
    }
}
