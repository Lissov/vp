namespace VisualControls.Editors
{
    partial class BaseParameterEditor
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
            this.lTitle = new System.Windows.Forms.Label();
            this.panEditors = new System.Windows.Forms.Panel();
            this.lUnit = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Location = new System.Drawing.Point(3, 8);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(35, 13);
            this.lTitle.TabIndex = 0;
            this.lTitle.Text = "label1";
            // 
            // panEditors
            // 
            this.panEditors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panEditors.Location = new System.Drawing.Point(134, 3);
            this.panEditors.Name = "panEditors";
            this.panEditors.Size = new System.Drawing.Size(193, 24);
            this.panEditors.TabIndex = 1;
            // 
            // lUnit
            // 
            this.lUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lUnit.AutoSize = true;
            this.lUnit.Location = new System.Drawing.Point(333, 8);
            this.lUnit.Name = "lUnit";
            this.lUnit.Size = new System.Drawing.Size(28, 13);
            this.lUnit.TabIndex = 2;
            this.lUnit.Text = "lUnit";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // BaseParameterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lUnit);
            this.Controls.Add(this.panEditors);
            this.Controls.Add(this.lTitle);
            this.Name = "BaseParameterEditor";
            this.Size = new System.Drawing.Size(400, 30);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lTitle;
        protected System.Windows.Forms.Label lUnit;
        protected System.Windows.Forms.Panel panEditors;
        protected System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
