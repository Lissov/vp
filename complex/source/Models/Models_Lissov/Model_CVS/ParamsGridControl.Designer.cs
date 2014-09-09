namespace Model_CVS
{
    partial class ParamsGridControl
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
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.oneCompartmentDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDepartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnstressed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRigidity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResistanceInput = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResistanceOutput = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRig_Adr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnstressed_Adr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnstressed_PExt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKElast = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPVButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colFlowbalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnChangeAll = new System.Windows.Forms.Button();
            this.colMuscularTone = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCompartmentDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.DataSource = this.oneCompartmentDataBindingSource;
            this.grid.EmbeddedNavigator.Name = "";
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.gridView1;
            this.grid.Name = "grid";
            this.grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemButtonEdit1});
            this.grid.Size = new System.Drawing.Size(544, 299);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // oneCompartmentDataBindingSource
            // 
            this.oneCompartmentDataBindingSource.DataSource = typeof(Model_CVS.OneCompartmentData);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDepartmentName,
            this.colCompartmentName,
            this.colVolume,
            this.colUnstressed,
            this.colRigidity,
            this.colResistanceInput,
            this.colResistanceOutput,
            this.colHeight,
            this.colRig_Adr,
            this.colUnstressed_Adr,
            this.colUnstressed_PExt,
            this.colKElast,
            this.colMuscularTone,
            this.colPVButton,
            this.colFlowbalance});
            this.gridView1.GridControl = this.grid;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colDepartmentName
            // 
            this.colDepartmentName.Caption = "DepartmentName";
            this.colDepartmentName.FieldName = "DepartmentName";
            this.colDepartmentName.Name = "colDepartmentName";
            this.colDepartmentName.OptionsColumn.AllowEdit = false;
            this.colDepartmentName.Visible = true;
            this.colDepartmentName.VisibleIndex = 0;
            // 
            // colCompartmentName
            // 
            this.colCompartmentName.Caption = "CompartmentName";
            this.colCompartmentName.FieldName = "CompartmentName";
            this.colCompartmentName.Name = "colCompartmentName";
            this.colCompartmentName.OptionsColumn.AllowEdit = false;
            this.colCompartmentName.Visible = true;
            this.colCompartmentName.VisibleIndex = 1;
            // 
            // colVolume
            // 
            this.colVolume.Caption = "Volume";
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 2;
            // 
            // colUnstressed
            // 
            this.colUnstressed.Caption = "Unstressed";
            this.colUnstressed.FieldName = "Unstressed";
            this.colUnstressed.Name = "colUnstressed";
            this.colUnstressed.Visible = true;
            this.colUnstressed.VisibleIndex = 3;
            // 
            // colRigidity
            // 
            this.colRigidity.Caption = "Rigidity";
            this.colRigidity.FieldName = "Rigidity";
            this.colRigidity.Name = "colRigidity";
            this.colRigidity.Visible = true;
            this.colRigidity.VisibleIndex = 4;
            // 
            // colResistanceInput
            // 
            this.colResistanceInput.Caption = "Resistanse Input";
            this.colResistanceInput.FieldName = "ResistanceInputKoeff";
            this.colResistanceInput.Name = "colResistanceInput";
            this.colResistanceInput.Visible = true;
            this.colResistanceInput.VisibleIndex = 5;
            // 
            // colResistanceOutput
            // 
            this.colResistanceOutput.Caption = "ResistanceOutput";
            this.colResistanceOutput.FieldName = "ResistanceOutput";
            this.colResistanceOutput.Name = "colResistanceOutput";
            this.colResistanceOutput.Visible = true;
            this.colResistanceOutput.VisibleIndex = 6;
            // 
            // colHeight
            // 
            this.colHeight.Caption = "Height";
            this.colHeight.FieldName = "Height";
            this.colHeight.Name = "colHeight";
            this.colHeight.Visible = true;
            this.colHeight.VisibleIndex = 7;
            // 
            // colRig_Adr
            // 
            this.colRig_Adr.Caption = "Rig_Adr";
            this.colRig_Adr.FieldName = "Rig_Adr";
            this.colRig_Adr.Name = "colRig_Adr";
            this.colRig_Adr.Visible = true;
            this.colRig_Adr.VisibleIndex = 8;
            // 
            // colUnstressed_Adr
            // 
            this.colUnstressed_Adr.Caption = "Unstressed_Adr";
            this.colUnstressed_Adr.FieldName = "Unstressed_Adr";
            this.colUnstressed_Adr.Name = "colUnstressed_Adr";
            this.colUnstressed_Adr.Visible = true;
            this.colUnstressed_Adr.VisibleIndex = 9;
            // 
            // colUnstressed_PExt
            // 
            this.colUnstressed_PExt.Caption = "Unstressed_PExt";
            this.colUnstressed_PExt.FieldName = "Unstressed_PExt";
            this.colUnstressed_PExt.Name = "colUnstressed_PExt";
            this.colUnstressed_PExt.Visible = true;
            this.colUnstressed_PExt.VisibleIndex = 10;
            // 
            // colKElast
            // 
            this.colKElast.Caption = "KElast";
            this.colKElast.FieldName = "KElast";
            this.colKElast.Name = "colKElast";
            this.colKElast.Visible = true;
            this.colKElast.VisibleIndex = 11;
            // 
            // colPVButton
            // 
            this.colPVButton.Caption = "PV";
            this.colPVButton.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colPVButton.FieldName = "PVFunctionText";
            this.colPVButton.Name = "colPVButton";
            this.colPVButton.Visible = true;
            this.colPVButton.VisibleIndex = 13;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // colFlowbalance
            // 
            this.colFlowbalance.Caption = "Flow Balance";
            this.colFlowbalance.FieldName = "FlowBalance";
            this.colFlowbalance.Name = "colFlowbalance";
            this.colFlowbalance.OptionsColumn.AllowEdit = false;
            this.colFlowbalance.OptionsColumn.ReadOnly = true;
            this.colFlowbalance.Visible = true;
            this.colFlowbalance.VisibleIndex = 14;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(466, 305);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnChangeAll
            // 
            this.btnChangeAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeAll.Location = new System.Drawing.Point(3, 305);
            this.btnChangeAll.Name = "btnChangeAll";
            this.btnChangeAll.Size = new System.Drawing.Size(75, 23);
            this.btnChangeAll.TabIndex = 2;
            this.btnChangeAll.Text = "Change all";
            this.btnChangeAll.UseVisualStyleBackColor = true;
            this.btnChangeAll.Click += new System.EventHandler(this.btnChangeAll_Click);
            // 
            // colMuscularTone
            // 
            this.colMuscularTone.Caption = "Muscular tone";
            this.colMuscularTone.FieldName = "MuscularTone";
            this.colMuscularTone.Name = "colMuscularTone";
            this.colMuscularTone.Visible = true;
            this.colMuscularTone.VisibleIndex = 12;
            // 
            // ParamsGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnChangeAll);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.grid);
            this.Name = "ParamsGridControl";
            this.Size = new System.Drawing.Size(544, 331);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCompartmentDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource oneCompartmentDataBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.Button btnApply;
        private DevExpress.XtraGrid.Columns.GridColumn colDepartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colCompartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colUnstressed;
        private DevExpress.XtraGrid.Columns.GridColumn colRigidity;
        private DevExpress.XtraGrid.Columns.GridColumn colHeight;
        private DevExpress.XtraGrid.Columns.GridColumn colRig_Adr;
        private DevExpress.XtraGrid.Columns.GridColumn colUnstressed_Adr;
        private DevExpress.XtraGrid.Columns.GridColumn colUnstressed_PExt;
        private DevExpress.XtraGrid.Columns.GridColumn colKElast;
        private DevExpress.XtraGrid.Columns.GridColumn colPVButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.Button btnChangeAll;
        private DevExpress.XtraGrid.Columns.GridColumn colResistanceInput;
        private DevExpress.XtraGrid.Columns.GridColumn colResistanceOutput;
        private DevExpress.XtraGrid.Columns.GridColumn colFlowbalance;
        private DevExpress.XtraGrid.Columns.GridColumn colMuscularTone;
    }
}
