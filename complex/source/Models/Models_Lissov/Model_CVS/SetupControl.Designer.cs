namespace Model_CVS
{
    partial class SetupControl
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
            this.tpProperties = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnOldLoad = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAddVirtCompartment = new System.Windows.Forms.Button();
            this.btnMergeCompartments = new System.Windows.Forms.Button();
            this.tbMaximumStep = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnVisualEditor = new System.Windows.Forms.Button();
            this.btnConfigLoadOldFormat = new System.Windows.Forms.Button();
            this.btnConfigSave = new System.Windows.Forms.Button();
            this.btnConfigLoad = new System.Windows.Forms.Button();
            this.tbConfigComments = new System.Windows.Forms.TextBox();
            this.tbConfigDepCount = new System.Windows.Forms.TextBox();
            this.tbConfigName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.logControl1 = new LissovLog.LogControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.paramsTable = new Model_CVS.ParamsGridControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tbBaroreceptionSensitivity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnEditBrainFlowRegulationFunction = new System.Windows.Forms.Button();
            this.checkBoxParameterBool1 = new VisualControls.CheckBoxParameterBool();
            this.tpAddValues = new System.Windows.Forms.TabPage();
            this.gbPerifRes = new System.Windows.Forms.GroupBox();
            this.btnAddValue = new System.Windows.Forms.Button();
            this.cbResTo = new System.Windows.Forms.ComboBox();
            this.cbResFrom = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cbThrough = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tpProperties.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tpAddValues.SuspendLayout();
            this.gbPerifRes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // tpProperties
            // 
            this.tpProperties.Controls.Add(this.tabPage1);
            this.tpProperties.Controls.Add(this.tabPage2);
            this.tpProperties.Controls.Add(this.tabPage3);
            this.tpProperties.Controls.Add(this.tabPage4);
            this.tpProperties.Controls.Add(this.tabPage5);
            this.tpProperties.Controls.Add(this.tpAddValues);
            this.tpProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpProperties.Location = new System.Drawing.Point(0, 0);
            this.tpProperties.Name = "tpProperties";
            this.tpProperties.SelectedIndex = 0;
            this.tpProperties.Size = new System.Drawing.Size(478, 291);
            this.tpProperties.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnOldLoad);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(470, 265);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnOldLoad
            // 
            this.btnOldLoad.Location = new System.Drawing.Point(10, 82);
            this.btnOldLoad.Name = "btnOldLoad";
            this.btnOldLoad.Size = new System.Drawing.Size(195, 23);
            this.btnOldLoad.TabIndex = 2;
            this.btnOldLoad.Text = "Load from VP_OLE file";
            this.btnOldLoad.UseVisualStyleBackColor = true;
            this.btnOldLoad.Click += new System.EventHandler(this.btnOldLoad_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Author: Pavel Lissov";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Model of Vascular Net";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnAddVirtCompartment);
            this.tabPage2.Controls.Add(this.btnMergeCompartments);
            this.tabPage2.Controls.Add(this.tbMaximumStep);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.btnVisualEditor);
            this.tabPage2.Controls.Add(this.btnConfigLoadOldFormat);
            this.tabPage2.Controls.Add(this.btnConfigSave);
            this.tabPage2.Controls.Add(this.btnConfigLoad);
            this.tabPage2.Controls.Add(this.tbConfigComments);
            this.tabPage2.Controls.Add(this.tbConfigDepCount);
            this.tabPage2.Controls.Add(this.tbConfigName);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(470, 265);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Configuration";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnAddVirtCompartment
            // 
            this.btnAddVirtCompartment.Location = new System.Drawing.Point(125, 221);
            this.btnAddVirtCompartment.Name = "btnAddVirtCompartment";
            this.btnAddVirtCompartment.Size = new System.Drawing.Size(146, 23);
            this.btnAddVirtCompartment.TabIndex = 13;
            this.btnAddVirtCompartment.Text = "Add virtual compartment";
            this.btnAddVirtCompartment.UseVisualStyleBackColor = true;
            this.btnAddVirtCompartment.Click += new System.EventHandler(this.btnAddVirtCompartment_Click);
            // 
            // btnMergeCompartments
            // 
            this.btnMergeCompartments.Location = new System.Drawing.Point(125, 192);
            this.btnMergeCompartments.Name = "btnMergeCompartments";
            this.btnMergeCompartments.Size = new System.Drawing.Size(146, 23);
            this.btnMergeCompartments.TabIndex = 12;
            this.btnMergeCompartments.Text = "Merge compartments";
            this.btnMergeCompartments.UseVisualStyleBackColor = true;
            this.btnMergeCompartments.Click += new System.EventHandler(this.btnMergeCompartments_Click);
            // 
            // tbMaximumStep
            // 
            this.tbMaximumStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMaximumStep.Location = new System.Drawing.Point(125, 165);
            this.tbMaximumStep.Name = "tbMaximumStep";
            this.tbMaximumStep.ReadOnly = true;
            this.tbMaximumStep.Size = new System.Drawing.Size(339, 20);
            this.tbMaximumStep.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 168);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Maximum Step";
            // 
            // btnVisualEditor
            // 
            this.btnVisualEditor.Location = new System.Drawing.Point(271, 3);
            this.btnVisualEditor.Name = "btnVisualEditor";
            this.btnVisualEditor.Size = new System.Drawing.Size(94, 23);
            this.btnVisualEditor.TabIndex = 9;
            this.btnVisualEditor.Text = "Visual editor";
            this.btnVisualEditor.UseVisualStyleBackColor = true;
            this.btnVisualEditor.Click += new System.EventHandler(this.btnVisualEditor_Click);
            // 
            // btnConfigLoadOldFormat
            // 
            this.btnConfigLoadOldFormat.Location = new System.Drawing.Point(171, 3);
            this.btnConfigLoadOldFormat.Name = "btnConfigLoadOldFormat";
            this.btnConfigLoadOldFormat.Size = new System.Drawing.Size(94, 23);
            this.btnConfigLoadOldFormat.TabIndex = 8;
            this.btnConfigLoadOldFormat.Text = "Load VP_OLE";
            this.btnConfigLoadOldFormat.UseVisualStyleBackColor = true;
            this.btnConfigLoadOldFormat.Click += new System.EventHandler(this.btnConfigLoadOldFormat_Click);
            // 
            // btnConfigSave
            // 
            this.btnConfigSave.Location = new System.Drawing.Point(90, 3);
            this.btnConfigSave.Name = "btnConfigSave";
            this.btnConfigSave.Size = new System.Drawing.Size(75, 23);
            this.btnConfigSave.TabIndex = 7;
            this.btnConfigSave.Text = "Save";
            this.btnConfigSave.UseVisualStyleBackColor = true;
            this.btnConfigSave.Click += new System.EventHandler(this.btnConfigSave_Click);
            // 
            // btnConfigLoad
            // 
            this.btnConfigLoad.Location = new System.Drawing.Point(9, 3);
            this.btnConfigLoad.Name = "btnConfigLoad";
            this.btnConfigLoad.Size = new System.Drawing.Size(75, 23);
            this.btnConfigLoad.TabIndex = 6;
            this.btnConfigLoad.Text = "Load";
            this.btnConfigLoad.UseVisualStyleBackColor = true;
            this.btnConfigLoad.Click += new System.EventHandler(this.btnConfigLoad_Click);
            // 
            // tbConfigComments
            // 
            this.tbConfigComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConfigComments.Location = new System.Drawing.Point(125, 74);
            this.tbConfigComments.Multiline = true;
            this.tbConfigComments.Name = "tbConfigComments";
            this.tbConfigComments.Size = new System.Drawing.Size(339, 83);
            this.tbConfigComments.TabIndex = 5;
            this.tbConfigComments.TextChanged += new System.EventHandler(this.ConfigChanged);
            // 
            // tbConfigDepCount
            // 
            this.tbConfigDepCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConfigDepCount.Location = new System.Drawing.Point(125, 52);
            this.tbConfigDepCount.Name = "tbConfigDepCount";
            this.tbConfigDepCount.ReadOnly = true;
            this.tbConfigDepCount.Size = new System.Drawing.Size(339, 20);
            this.tbConfigDepCount.TabIndex = 4;
            // 
            // tbConfigName
            // 
            this.tbConfigName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConfigName.Location = new System.Drawing.Point(125, 30);
            this.tbConfigName.Name = "tbConfigName";
            this.tbConfigName.Size = new System.Drawing.Size(339, 20);
            this.tbConfigName.TabIndex = 3;
            this.tbConfigName.TextChanged += new System.EventHandler(this.ConfigChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Comments:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Compartments count: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Name:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.logControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(470, 265);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Log";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // logControl1
            // 
            this.logControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logControl1.Location = new System.Drawing.Point(3, 3);
            this.logControl1.Name = "logControl1";
            this.logControl1.Size = new System.Drawing.Size(464, 259);
            this.logControl1.TabIndex = 0;
            this.logControl1.UseCompatibleStateImageBehavior = false;
            this.logControl1.View = System.Windows.Forms.View.Details;
            this.logControl1.VirtualMode = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.paramsTable);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(470, 265);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Parameters";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // paramsTable
            // 
            this.paramsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paramsTable.Location = new System.Drawing.Point(3, 3);
            this.paramsTable.Name = "paramsTable";
            this.paramsTable.Size = new System.Drawing.Size(464, 259);
            this.paramsTable.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tbBaroreceptionSensitivity);
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.btnEditBrainFlowRegulationFunction);
            this.tabPage5.Controls.Add(this.checkBoxParameterBool1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(470, 265);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Properties";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tbBaroreceptionSensitivity
            // 
            this.tbBaroreceptionSensitivity.Location = new System.Drawing.Point(148, 47);
            this.tbBaroreceptionSensitivity.Name = "tbBaroreceptionSensitivity";
            this.tbBaroreceptionSensitivity.Size = new System.Drawing.Size(81, 20);
            this.tbBaroreceptionSensitivity.TabIndex = 3;
            this.tbBaroreceptionSensitivity.TextChanged += new System.EventHandler(this.tbBaroreceptionSensitivity_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Baroreception sensitivity:";
            // 
            // btnEditBrainFlowRegulationFunction
            // 
            this.btnEditBrainFlowRegulationFunction.Location = new System.Drawing.Point(183, 5);
            this.btnEditBrainFlowRegulationFunction.Name = "btnEditBrainFlowRegulationFunction";
            this.btnEditBrainFlowRegulationFunction.Size = new System.Drawing.Size(160, 23);
            this.btnEditBrainFlowRegulationFunction.TabIndex = 1;
            this.btnEditBrainFlowRegulationFunction.Text = "Edit resistance dependency";
            this.btnEditBrainFlowRegulationFunction.UseVisualStyleBackColor = true;
            this.btnEditBrainFlowRegulationFunction.Click += new System.EventHandler(this.btnEditBrainFlowRegulationFunction_Click);
            // 
            // checkBoxParameterBool1
            // 
            this.checkBoxParameterBool1.AutoSize = true;
            this.checkBoxParameterBool1.Location = new System.Drawing.Point(7, 9);
            this.checkBoxParameterBool1.Name = "checkBoxParameterBool1";
            this.checkBoxParameterBool1.ParameterName = "RegulateBrainFlow";
            this.checkBoxParameterBool1.Size = new System.Drawing.Size(170, 17);
            this.checkBoxParameterBool1.TabIndex = 0;
            this.checkBoxParameterBool1.Text = "Brain BloodFlow self-regulation";
            this.checkBoxParameterBool1.UseVisualStyleBackColor = true;
            // 
            // tpAddValues
            // 
            this.tpAddValues.Controls.Add(this.gbPerifRes);
            this.tpAddValues.Location = new System.Drawing.Point(4, 22);
            this.tpAddValues.Name = "tpAddValues";
            this.tpAddValues.Padding = new System.Windows.Forms.Padding(3);
            this.tpAddValues.Size = new System.Drawing.Size(470, 265);
            this.tpAddValues.TabIndex = 5;
            this.tpAddValues.Text = "Additional Values";
            this.tpAddValues.UseVisualStyleBackColor = true;
            // 
            // gbPerifRes
            // 
            this.gbPerifRes.Controls.Add(this.label10);
            this.gbPerifRes.Controls.Add(this.cbThrough);
            this.gbPerifRes.Controls.Add(this.btnAddValue);
            this.gbPerifRes.Controls.Add(this.cbResTo);
            this.gbPerifRes.Controls.Add(this.cbResFrom);
            this.gbPerifRes.Controls.Add(this.label9);
            this.gbPerifRes.Controls.Add(this.label8);
            this.gbPerifRes.Location = new System.Drawing.Point(6, 6);
            this.gbPerifRes.Name = "gbPerifRes";
            this.gbPerifRes.Size = new System.Drawing.Size(337, 135);
            this.gbPerifRes.TabIndex = 0;
            this.gbPerifRes.TabStop = false;
            this.gbPerifRes.Text = "Partial resistanse";
            // 
            // btnAddValue
            // 
            this.btnAddValue.Location = new System.Drawing.Point(227, 104);
            this.btnAddValue.Name = "btnAddValue";
            this.btnAddValue.Size = new System.Drawing.Size(104, 23);
            this.btnAddValue.TabIndex = 4;
            this.btnAddValue.Text = "Add value";
            this.btnAddValue.UseVisualStyleBackColor = true;
            this.btnAddValue.Click += new System.EventHandler(this.btnAddValue_Click);
            // 
            // cbResTo
            // 
            this.cbResTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbResTo.FormattingEnabled = true;
            this.cbResTo.Location = new System.Drawing.Point(65, 50);
            this.cbResTo.Name = "cbResTo";
            this.cbResTo.Size = new System.Drawing.Size(266, 21);
            this.cbResTo.TabIndex = 3;
            this.cbResTo.DropDown += new System.EventHandler(this.fillCompartments);
            // 
            // cbResFrom
            // 
            this.cbResFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbResFrom.FormattingEnabled = true;
            this.cbResFrom.Location = new System.Drawing.Point(65, 23);
            this.cbResFrom.Name = "cbResFrom";
            this.cbResFrom.Size = new System.Drawing.Size(266, 21);
            this.cbResFrom.TabIndex = 2;
            this.cbResFrom.DropDown += new System.EventHandler(this.fillCompartments);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "To";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "From";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cbThrough
            // 
            this.cbThrough.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbThrough.FormattingEnabled = true;
            this.cbThrough.Location = new System.Drawing.Point(65, 77);
            this.cbThrough.Name = "cbThrough";
            this.cbThrough.Size = new System.Drawing.Size(266, 21);
            this.cbThrough.TabIndex = 5;
            this.cbThrough.DropDown += new System.EventHandler(this.fillCompartments);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Through";
            // 
            // SetupControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tpProperties);
            this.Name = "SetupControl";
            this.Size = new System.Drawing.Size(478, 291);
            this.tpProperties.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tpAddValues.ResumeLayout(false);
            this.gbPerifRes.ResumeLayout(false);
            this.gbPerifRes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tpProperties;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbConfigComments;
        private System.Windows.Forms.TextBox tbConfigDepCount;
        private System.Windows.Forms.TextBox tbConfigName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfigSave;
        private System.Windows.Forms.Button btnConfigLoad;
        private System.Windows.Forms.Button btnConfigLoadOldFormat;
        private System.Windows.Forms.Button btnOldLoad;
        private LissovLog.LogControl logControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private ParamsGridControl paramsTable;
        private System.Windows.Forms.Button btnVisualEditor;
        private System.Windows.Forms.TextBox tbMaximumStep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnMergeCompartments;
        private System.Windows.Forms.Button btnAddVirtCompartment;
        private System.Windows.Forms.TabPage tabPage5;
        private VisualControls.CheckBoxParameterBool checkBoxParameterBool1;
        private System.Windows.Forms.Button btnEditBrainFlowRegulationFunction;
        private System.Windows.Forms.TextBox tbBaroreceptionSensitivity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TabPage tpAddValues;
        private System.Windows.Forms.GroupBox gbPerifRes;
        private System.Windows.Forms.Button btnAddValue;
        private System.Windows.Forms.ComboBox cbResTo;
        private System.Windows.Forms.ComboBox cbResFrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbThrough;
        private System.Windows.Forms.Label label10;
    }
}
