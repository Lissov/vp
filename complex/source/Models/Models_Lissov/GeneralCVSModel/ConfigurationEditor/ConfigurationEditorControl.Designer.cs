namespace GeneralCVSModel.ConfigurationEditor
{
    partial class ConfigurationEditorControl
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
            this.splitContr = new System.Windows.Forms.SplitContainer();
            this.confEditor = new VisualControls.ConfigurationEditor.ConfigurationEditor();
            this.panGeneral = new System.Windows.Forms.Panel();
            this.splitContr.Panel1.SuspendLayout();
            this.splitContr.Panel2.SuspendLayout();
            this.splitContr.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContr
            // 
            this.splitContr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContr.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContr.Location = new System.Drawing.Point(0, 0);
            this.splitContr.Name = "splitContr";
            // 
            // splitContr.Panel1
            // 
            this.splitContr.Panel1.Controls.Add(this.confEditor);
            // 
            // splitContr.Panel2
            // 
            this.splitContr.Panel2.Controls.Add(this.panGeneral);
            this.splitContr.Size = new System.Drawing.Size(675, 463);
            this.splitContr.SplitterDistance = 269;
            this.splitContr.TabIndex = 1;
            // 
            // confEditor
            // 
            this.confEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.confEditor.Location = new System.Drawing.Point(0, 0);
            this.confEditor.Name = "confEditor";
            this.confEditor.Size = new System.Drawing.Size(269, 463);
            this.confEditor.TabIndex = 0;
            // 
            // panGeneral
            // 
            this.panGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panGeneral.Location = new System.Drawing.Point(0, 0);
            this.panGeneral.Name = "panGeneral";
            this.panGeneral.Size = new System.Drawing.Size(402, 463);
            this.panGeneral.TabIndex = 0;
            // 
            // ConfigurationEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContr);
            this.Name = "ConfigurationEditorControl";
            this.Size = new System.Drawing.Size(675, 463);
            this.splitContr.Panel1.ResumeLayout(false);
            this.splitContr.Panel2.ResumeLayout(false);
            this.splitContr.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public VisualControls.ConfigurationEditor.ConfigurationEditor confEditor;
        internal System.Windows.Forms.SplitContainer splitContr;
        internal System.Windows.Forms.Panel panGeneral;
    }
}
