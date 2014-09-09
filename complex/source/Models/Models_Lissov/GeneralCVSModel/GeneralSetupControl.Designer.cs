namespace GeneralCVSModel
{
    partial class GeneralSetupControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbWillisBaroreception = new System.Windows.Forms.CheckBox();
            this.cbKidneyRegulation = new System.Windows.Forms.CheckBox();
            this.cbCapillaryFiltration = new System.Windows.Forms.CheckBox();
            this.cbKidney = new System.Windows.Forms.CheckBox();
            this.cbBaroreception = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpConfiguration = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.logControl = new LissovLog.LogControl();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.resultsTable = new GeneralCVSModel.ResultsTable.ResultsTable();
            this.btnSaveGraphs = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(686, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Modelling complex for research of hemodynamics in human body under external Press" +
                "ure and Gravity loads";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(7, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 131);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "External loads";
            this.groupBox1.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbWillisBaroreception);
            this.groupBox2.Controls.Add(this.cbKidneyRegulation);
            this.groupBox2.Controls.Add(this.cbCapillaryFiltration);
            this.groupBox2.Controls.Add(this.cbKidney);
            this.groupBox2.Controls.Add(this.cbBaroreception);
            this.groupBox2.Location = new System.Drawing.Point(7, 172);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 161);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Regulation";
            // 
            // cbWillisBaroreception
            // 
            this.cbWillisBaroreception.AutoSize = true;
            this.cbWillisBaroreception.Location = new System.Drawing.Point(41, 51);
            this.cbWillisBaroreception.Name = "cbWillisBaroreception";
            this.cbWillisBaroreception.Size = new System.Drawing.Size(147, 17);
            this.cbWillisBaroreception.TabIndex = 4;
            this.cbWillisBaroreception.Text = "Willis Circle baroreception";
            this.cbWillisBaroreception.ThreeState = true;
            this.cbWillisBaroreception.UseVisualStyleBackColor = true;
            this.cbWillisBaroreception.CheckStateChanged += new System.EventHandler(this.cbWillisBaroreception_CheckStateChanged);
            // 
            // cbKidneyRegulation
            // 
            this.cbKidneyRegulation.AutoSize = true;
            this.cbKidneyRegulation.Location = new System.Drawing.Point(41, 101);
            this.cbKidneyRegulation.Name = "cbKidneyRegulation";
            this.cbKidneyRegulation.Size = new System.Drawing.Size(107, 17);
            this.cbKidneyRegulation.TabIndex = 3;
            this.cbKidneyRegulation.Text = "Kidney regulation";
            this.cbKidneyRegulation.UseVisualStyleBackColor = true;
            this.cbKidneyRegulation.CheckedChanged += new System.EventHandler(this.cKidneyRegulation_CheckedChanged);
            // 
            // cbCapillaryFiltration
            // 
            this.cbCapillaryFiltration.AutoSize = true;
            this.cbCapillaryFiltration.Location = new System.Drawing.Point(16, 124);
            this.cbCapillaryFiltration.Name = "cbCapillaryFiltration";
            this.cbCapillaryFiltration.Size = new System.Drawing.Size(238, 17);
            this.cbCapillaryFiltration.TabIndex = 2;
            this.cbCapillaryFiltration.Text = "Capillary filtration depending venous pressure";
            this.cbCapillaryFiltration.UseVisualStyleBackColor = true;
            this.cbCapillaryFiltration.CheckedChanged += new System.EventHandler(this.cbCapillaryFiltration_CheckedChanged);
            // 
            // cbKidney
            // 
            this.cbKidney.AutoSize = true;
            this.cbKidney.Location = new System.Drawing.Point(16, 78);
            this.cbKidney.Name = "cbKidney";
            this.cbKidney.Size = new System.Drawing.Size(58, 17);
            this.cbKidney.TabIndex = 1;
            this.cbKidney.Text = "Kidney";
            this.cbKidney.UseVisualStyleBackColor = true;
            this.cbKidney.CheckedChanged += new System.EventHandler(this.cbKidney_CheckedChanged);
            // 
            // cbBaroreception
            // 
            this.cbBaroreception.AutoSize = true;
            this.cbBaroreception.Location = new System.Drawing.Point(16, 28);
            this.cbBaroreception.Name = "cbBaroreception";
            this.cbBaroreception.Size = new System.Drawing.Size(92, 17);
            this.cbBaroreception.TabIndex = 0;
            this.cbBaroreception.Text = "Baroreception";
            this.cbBaroreception.UseVisualStyleBackColor = true;
            this.cbBaroreception.CheckedChanged += new System.EventHandler(this.cbBaroreception_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpConfiguration);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(7, 35);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(548, 298);
            this.tabControl1.TabIndex = 4;
            // 
            // tpConfiguration
            // 
            this.tpConfiguration.Location = new System.Drawing.Point(4, 22);
            this.tpConfiguration.Name = "tpConfiguration";
            this.tpConfiguration.Padding = new System.Windows.Forms.Padding(3);
            this.tpConfiguration.Size = new System.Drawing.Size(540, 272);
            this.tpConfiguration.TabIndex = 0;
            this.tpConfiguration.Text = "Configuration";
            this.tpConfiguration.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnSaveGraphs);
            this.tabPage2.Controls.Add(this.logControl);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(540, 272);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Technical info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // logControl
            // 
            this.logControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logControl.Location = new System.Drawing.Point(6, 35);
            this.logControl.Name = "logControl";
            this.logControl.Size = new System.Drawing.Size(531, 235);
            this.logControl.TabIndex = 1;
            this.logControl.UseCompatibleStateImageBehavior = false;
            this.logControl.View = System.Windows.Forms.View.Details;
            this.logControl.VirtualMode = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Validate configuration";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.resultsTable);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(540, 272);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Results table";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // resultsTable
            // 
            this.resultsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsTable.Location = new System.Drawing.Point(3, 3);
            this.resultsTable.Name = "resultsTable";
            this.resultsTable.Size = new System.Drawing.Size(534, 266);
            this.resultsTable.TabIndex = 0;
            // 
            // btnSaveGraphs
            // 
            this.btnSaveGraphs.Location = new System.Drawing.Point(340, 6);
            this.btnSaveGraphs.Name = "btnSaveGraphs";
            this.btnSaveGraphs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveGraphs.TabIndex = 2;
            this.btnSaveGraphs.Text = "Save graphs";
            this.btnSaveGraphs.UseVisualStyleBackColor = true;
            this.btnSaveGraphs.Click += new System.EventHandler(this.btnSaveGraphs_Click);
            // 
            // GeneralSetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Name = "GeneralSetupControl";
            this.Size = new System.Drawing.Size(555, 337);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.CheckBox cbCapillaryFiltration;
        internal System.Windows.Forms.CheckBox cbKidney;
        internal System.Windows.Forms.CheckBox cbBaroreception;
        internal System.Windows.Forms.CheckBox cbKidneyRegulation;
        internal System.Windows.Forms.CheckBox cbWillisBaroreception;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpConfiguration;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private LissovLog.LogControl logControl;
        private System.Windows.Forms.TabPage tabPage1;
        public GeneralCVSModel.ResultsTable.ResultsTable resultsTable;
        private System.Windows.Forms.Button btnSaveGraphs;
    }
}
