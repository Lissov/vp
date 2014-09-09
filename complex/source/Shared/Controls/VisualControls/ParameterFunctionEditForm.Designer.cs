namespace VisualControls
{
    partial class ParameterFunctionEditForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.parameterFunctionControl1 = new VisualControls.ParameterFunctionControl();
            this.SuspendLayout();
            // 
            // parameterFunctionControl1
            // 
            this.parameterFunctionControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.parameterFunctionControl1.Location = new System.Drawing.Point(0, 0);
            this.parameterFunctionControl1.Name = "parameterFunctionControl1";
            this.parameterFunctionControl1.Size = new System.Drawing.Size(696, 352);
            this.parameterFunctionControl1.TabIndex = 0;
            // 
            // ParameterFunctionEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 352);
            this.Controls.Add(this.parameterFunctionControl1);
            this.Name = "ParameterFunctionEditForm";
            this.Text = "Edit Function";
            this.ResumeLayout(false);

        }

        #endregion

        private ParameterFunctionControl parameterFunctionControl1;
    }
}