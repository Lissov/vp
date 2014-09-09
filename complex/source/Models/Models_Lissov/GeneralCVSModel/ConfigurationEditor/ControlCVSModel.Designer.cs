namespace GeneralCVSModel.ConfigurationEditor
{
    partial class ControlCVSModel
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
            this.configurationEditor1 = new VisualControls.ConfigurationEditor.ConfigurationEditor();
            this.SuspendLayout();
            // 
            // configurationEditor1
            // 
            this.configurationEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationEditor1.Location = new System.Drawing.Point(0, 0);
            this.configurationEditor1.Name = "configurationEditor1";
            this.configurationEditor1.Size = new System.Drawing.Size(362, 304);
            this.configurationEditor1.TabIndex = 0;
            // 
            // ControlCVSModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.configurationEditor1);
            this.Name = "ControlCVSModel";
            this.Size = new System.Drawing.Size(362, 304);
            this.ResumeLayout(false);

        }

        #endregion

        private VisualControls.ConfigurationEditor.ConfigurationEditor configurationEditor1;

    }
}
