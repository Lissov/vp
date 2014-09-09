namespace LissovBase
{
    partial class ModelSetupControlSimple
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
            this.btnLoadVPOle = new System.Windows.Forms.Button();
            this.logControl1 = new LissovLog.LogControl();
            this.SuspendLayout();
            // 
            // btnLoadVPOle
            // 
            this.btnLoadVPOle.Location = new System.Drawing.Point(3, 3);
            this.btnLoadVPOle.Name = "btnLoadVPOle";
            this.btnLoadVPOle.Size = new System.Drawing.Size(104, 23);
            this.btnLoadVPOle.TabIndex = 1;
            this.btnLoadVPOle.Text = "Load VP_OLE";
            this.btnLoadVPOle.UseVisualStyleBackColor = true;
            this.btnLoadVPOle.Click += new System.EventHandler(this.btnLoadVPOle_Click);
            // 
            // logControl1
            // 
            this.logControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logControl1.Location = new System.Drawing.Point(3, 32);
            this.logControl1.Name = "logControl1";
            this.logControl1.Size = new System.Drawing.Size(144, 115);
            this.logControl1.TabIndex = 2;
            this.logControl1.UseCompatibleStateImageBehavior = false;
            this.logControl1.View = System.Windows.Forms.View.Details;
            this.logControl1.VirtualMode = true;
            // 
            // ModelSetupControlSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logControl1);
            this.Controls.Add(this.btnLoadVPOle);
            this.Name = "ModelSetupControlSimple";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadVPOle;
        private LissovLog.LogControl logControl1;
    }
}
