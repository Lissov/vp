namespace VisualControls.ConfigurationEditor
{
    partial class ConfigurationEditor
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
            this.panTop = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.scContent = new System.Windows.Forms.SplitContainer();
            this.panGraph = new System.Windows.Forms.Panel();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.pgProperties = new System.Windows.Forms.PropertyGrid();
            this.panTop.SuspendLayout();
            this.scContent.Panel1.SuspendLayout();
            this.scContent.Panel2.SuspendLayout();
            this.scContent.SuspendLayout();
            this.panGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnApply);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(417, 36);
            this.panTop.TabIndex = 0;
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(12, 7);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // scContent
            // 
            this.scContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContent.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scContent.Location = new System.Drawing.Point(0, 36);
            this.scContent.Name = "scContent";
            // 
            // scContent.Panel1
            // 
            this.scContent.Panel1.Controls.Add(this.panGraph);
            this.scContent.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // scContent.Panel2
            // 
            this.scContent.Panel2.Controls.Add(this.pgProperties);
            this.scContent.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.scContent.Size = new System.Drawing.Size(417, 252);
            this.scContent.SplitterDistance = 253;
            this.scContent.TabIndex = 1;
            // 
            // panGraph
            // 
            this.panGraph.AutoScroll = true;
            this.panGraph.BackColor = System.Drawing.Color.White;
            this.panGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panGraph.Controls.Add(this.pbGraph);
            this.panGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panGraph.Location = new System.Drawing.Point(0, 0);
            this.panGraph.Name = "panGraph";
            this.panGraph.Size = new System.Drawing.Size(253, 252);
            this.panGraph.TabIndex = 0;
            // 
            // pbGraph
            // 
            this.pbGraph.Location = new System.Drawing.Point(-1, -1);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(100, 50);
            this.pbGraph.TabIndex = 0;
            this.pbGraph.TabStop = false;
            // 
            // pgProperties
            // 
            this.pgProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProperties.Location = new System.Drawing.Point(0, 0);
            this.pgProperties.Name = "pgProperties";
            this.pgProperties.Size = new System.Drawing.Size(160, 252);
            this.pgProperties.TabIndex = 0;
            // 
            // ConfigurationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scContent);
            this.Controls.Add(this.panTop);
            this.Name = "ConfigurationEditor";
            this.Size = new System.Drawing.Size(417, 288);
            this.panTop.ResumeLayout(false);
            this.scContent.Panel1.ResumeLayout(false);
            this.scContent.Panel2.ResumeLayout(false);
            this.scContent.ResumeLayout(false);
            this.panGraph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PropertyGrid pgProperties;
        private System.Windows.Forms.Panel panGraph;
        public System.Windows.Forms.PictureBox pbGraph;
        internal System.Windows.Forms.Button btnApply;
        internal System.Windows.Forms.SplitContainer scContent;
        public System.Windows.Forms.Panel panTop;
    }
}
