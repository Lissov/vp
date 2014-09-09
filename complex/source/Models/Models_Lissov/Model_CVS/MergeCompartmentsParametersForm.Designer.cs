namespace Model_CVS
{
    partial class MergeCompartmentsParametersForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbMerger = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDestination = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.gridData = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.compDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colParameterName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValueMerger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValueDestination = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNewValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.compDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Merge compartment";
            // 
            // cbMerger
            // 
            this.cbMerger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMerger.FormattingEnabled = true;
            this.cbMerger.Location = new System.Drawing.Point(142, 9);
            this.cbMerger.Name = "cbMerger";
            this.cbMerger.Size = new System.Drawing.Size(349, 21);
            this.cbMerger.TabIndex = 1;
            this.cbMerger.SelectedIndexChanged += new System.EventHandler(this.CompartmentSelected);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Destination compartment";
            // 
            // cbDestination
            // 
            this.cbDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDestination.FormattingEnabled = true;
            this.cbDestination.Location = new System.Drawing.Point(142, 37);
            this.cbDestination.Name = "cbDestination";
            this.cbDestination.Size = new System.Drawing.Size(349, 21);
            this.cbDestination.TabIndex = 3;
            this.cbDestination.SelectedIndexChanged += new System.EventHandler(this.CompartmentSelected);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(416, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnMerge
            // 
            this.btnMerge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMerge.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnMerge.Location = new System.Drawing.Point(335, 280);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 9;
            this.btnMerge.Text = "Merge";
            this.btnMerge.UseVisualStyleBackColor = true;
            // 
            // gridData
            // 
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.DataSource = this.compDataBindingSource;
            this.gridData.EmbeddedNavigator.Name = "";
            this.gridData.Location = new System.Drawing.Point(12, 64);
            this.gridData.MainView = this.gridView1;
            this.gridData.Name = "gridData";
            this.gridData.Size = new System.Drawing.Size(479, 210);
            this.gridData.TabIndex = 10;
            this.gridData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colParameterName,
            this.colValueMerger,
            this.colValueDestination,
            this.colNewValue});
            this.gridView1.GridControl = this.gridData;
            this.gridView1.Name = "gridView1";
            // 
            // compDataBindingSource
            // 
            this.compDataBindingSource.DataSource = typeof(Model_CVS.CompData);
            // 
            // colParameterName
            // 
            this.colParameterName.Caption = "ParameterName";
            this.colParameterName.FieldName = "ParameterName";
            this.colParameterName.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.colParameterName.Name = "colParameterName";
            this.colParameterName.OptionsColumn.AllowEdit = false;
            this.colParameterName.OptionsColumn.ReadOnly = true;
            this.colParameterName.Visible = true;
            this.colParameterName.VisibleIndex = 0;
            // 
            // colValueMerger
            // 
            this.colValueMerger.Caption = "ValueMerger";
            this.colValueMerger.FieldName = "ValueMerger";
            this.colValueMerger.Name = "colValueMerger";
            this.colValueMerger.OptionsColumn.AllowEdit = false;
            this.colValueMerger.OptionsColumn.ReadOnly = true;
            this.colValueMerger.Visible = true;
            this.colValueMerger.VisibleIndex = 1;
            // 
            // colValueDestination
            // 
            this.colValueDestination.Caption = "ValueDestination";
            this.colValueDestination.FieldName = "ValueDestination";
            this.colValueDestination.Name = "colValueDestination";
            this.colValueDestination.OptionsColumn.AllowEdit = false;
            this.colValueDestination.OptionsColumn.ReadOnly = true;
            this.colValueDestination.Visible = true;
            this.colValueDestination.VisibleIndex = 2;
            // 
            // colNewValue
            // 
            this.colNewValue.Caption = "NewValue";
            this.colNewValue.FieldName = "NewValue";
            this.colNewValue.Name = "colNewValue";
            this.colNewValue.Visible = true;
            this.colNewValue.VisibleIndex = 3;
            // 
            // MergeCompartmentsParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 315);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbDestination);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMerger);
            this.Controls.Add(this.label1);
            this.Name = "MergeCompartmentsParametersForm";
            this.Text = "MergeCompartmentsParameters";
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.compDataBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbMerger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbDestination;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMerge;
        private DevExpress.XtraGrid.GridControl gridData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource compDataBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colParameterName;
        private DevExpress.XtraGrid.Columns.GridColumn colValueMerger;
        private DevExpress.XtraGrid.Columns.GridColumn colValueDestination;
        private DevExpress.XtraGrid.Columns.GridColumn colNewValue;
    }
}