namespace VisualControls.ConfigurationEditor
{
    partial class ConfigurationEditorForm
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
            this.configEditor = new VisualControls.ConfigurationEditor.ConfigurationEditor();
            this.SuspendLayout();
            // 
            // configEditor
            // 
            this.configEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configEditor.Location = new System.Drawing.Point(0, 0);
            this.configEditor.Name = "configEditor";
            this.configEditor.Size = new System.Drawing.Size(729, 355);
            this.configEditor.TabIndex = 0;
            // 
            // ConfigurationEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 355);
            this.Controls.Add(this.configEditor);
            this.Name = "ConfigurationEditorForm";
            this.Text = "Configuration Editor";
            this.ResumeLayout(false);

        }

        #endregion

        internal ConfigurationEditor configEditor;

    }
}