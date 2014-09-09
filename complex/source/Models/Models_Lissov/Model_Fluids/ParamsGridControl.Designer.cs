namespace Model_Fluids
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
            this.colCompartmentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolumeI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnstressedI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRigidityI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolumeC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnstressedC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRigidityC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResistanceVI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResistanceIC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colResistanceIL = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHeight = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKElastI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKElastC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFRPressure0 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCFRPressureGain = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPVFunctionTextI = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPVFunctionTextC = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnChangeAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.oneCompartmentDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
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
            this.repositoryItemButtonEdit1,
            this.repositoryItemButtonEdit2});
            this.grid.Size = new System.Drawing.Size(544, 299);
            this.grid.TabIndex = 0;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // oneCompartmentDataBindingSource
            // 
            this.oneCompartmentDataBindingSource.DataSource = typeof(Model_Fluids.OneCompartmentData);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCompartmentName,
            this.colVolumeI,
            this.colUnstressedI,
            this.colRigidityI,
            this.colVolumeC,
            this.colUnstressedC,
            this.colRigidityC,
            this.colResistanceVI,
            this.colResistanceIC,
            this.colResistanceIL,
            this.colHeight,
            this.colKElastI,
            this.colKElastC,
            this.colCFRPressure0,
            this.colCFRPressureGain,
            this.colPVFunctionTextI,
            this.colPVFunctionTextC});
            this.gridView1.GridControl = this.grid;
            this.gridView1.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            // 
            // colCompartmentName
            // 
            this.colCompartmentName.Caption = "CompartmentName";
            this.colCompartmentName.FieldName = "CompartmentName";
            this.colCompartmentName.Name = "colCompartmentName";
            this.colCompartmentName.Visible = true;
            this.colCompartmentName.VisibleIndex = 0;
            // 
            // colVolumeI
            // 
            this.colVolumeI.Caption = "VolumeI";
            this.colVolumeI.FieldName = "VolumeI";
            this.colVolumeI.Name = "colVolumeI";
            this.colVolumeI.Visible = true;
            this.colVolumeI.VisibleIndex = 1;
            // 
            // colUnstressedI
            // 
            this.colUnstressedI.Caption = "UnstressedI";
            this.colUnstressedI.FieldName = "UnstressedI";
            this.colUnstressedI.Name = "colUnstressedI";
            this.colUnstressedI.Visible = true;
            this.colUnstressedI.VisibleIndex = 2;
            // 
            // colRigidityI
            // 
            this.colRigidityI.Caption = "RigidityI";
            this.colRigidityI.FieldName = "RigidityI";
            this.colRigidityI.Name = "colRigidityI";
            this.colRigidityI.Visible = true;
            this.colRigidityI.VisibleIndex = 3;
            // 
            // colVolumeC
            // 
            this.colVolumeC.Caption = "VolumeC";
            this.colVolumeC.FieldName = "VolumeC";
            this.colVolumeC.Name = "colVolumeC";
            this.colVolumeC.Visible = true;
            this.colVolumeC.VisibleIndex = 4;
            // 
            // colUnstressedC
            // 
            this.colUnstressedC.Caption = "UnstressedC";
            this.colUnstressedC.FieldName = "UnstressedC";
            this.colUnstressedC.Name = "colUnstressedC";
            this.colUnstressedC.Visible = true;
            this.colUnstressedC.VisibleIndex = 5;
            // 
            // colRigidityC
            // 
            this.colRigidityC.Caption = "RigidityC";
            this.colRigidityC.FieldName = "RigidityC";
            this.colRigidityC.Name = "colRigidityC";
            this.colRigidityC.Visible = true;
            this.colRigidityC.VisibleIndex = 6;
            // 
            // colResistanceVI
            // 
            this.colResistanceVI.Caption = "ResistanceVI";
            this.colResistanceVI.FieldName = "ResistanceVI";
            this.colResistanceVI.Name = "colResistanceVI";
            this.colResistanceVI.Visible = true;
            this.colResistanceVI.VisibleIndex = 7;
            // 
            // colResistanceIC
            // 
            this.colResistanceIC.Caption = "ResistanceIC";
            this.colResistanceIC.FieldName = "ResistanceIC";
            this.colResistanceIC.Name = "colResistanceIC";
            this.colResistanceIC.Visible = true;
            this.colResistanceIC.VisibleIndex = 8;
            // 
            // colResistanceIL
            // 
            this.colResistanceIL.Caption = "ResistanceIL";
            this.colResistanceIL.FieldName = "ResistanceIL";
            this.colResistanceIL.Name = "colResistanceIL";
            this.colResistanceIL.Visible = true;
            this.colResistanceIL.VisibleIndex = 9;
            // 
            // colHeight
            // 
            this.colHeight.Caption = "Height";
            this.colHeight.FieldName = "Height";
            this.colHeight.Name = "colHeight";
            this.colHeight.Visible = true;
            this.colHeight.VisibleIndex = 10;
            // 
            // colKElastI
            // 
            this.colKElastI.Caption = "KElastI";
            this.colKElastI.FieldName = "KElastI";
            this.colKElastI.Name = "colKElastI";
            this.colKElastI.Visible = true;
            this.colKElastI.VisibleIndex = 11;
            // 
            // colKElastC
            // 
            this.colKElastC.Caption = "KElastC";
            this.colKElastC.FieldName = "KElastC";
            this.colKElastC.Name = "colKElastC";
            this.colKElastC.Visible = true;
            this.colKElastC.VisibleIndex = 12;
            // 
            // colCFRPressure0
            // 
            this.colCFRPressure0.Caption = "CFRPressure0";
            this.colCFRPressure0.FieldName = "CFRPressure0";
            this.colCFRPressure0.Name = "colCFRPressure0";
            this.colCFRPressure0.Visible = true;
            this.colCFRPressure0.VisibleIndex = 13;
            // 
            // colCFRPressureGain
            // 
            this.colCFRPressureGain.Caption = "CFRPressureGain";
            this.colCFRPressureGain.FieldName = "CFRPressureGain";
            this.colCFRPressureGain.Name = "colCFRPressureGain";
            this.colCFRPressureGain.Visible = true;
            this.colCFRPressureGain.VisibleIndex = 14;
            // 
            // colPVFunctionTextI
            // 
            this.colPVFunctionTextI.Caption = "PVFunctionTextI";
            this.colPVFunctionTextI.ColumnEdit = this.repositoryItemButtonEdit1;
            this.colPVFunctionTextI.FieldName = "PVFunctionTextI";
            this.colPVFunctionTextI.Name = "colPVFunctionTextI";
            this.colPVFunctionTextI.Visible = true;
            this.colPVFunctionTextI.VisibleIndex = 15;
            // 
            // colPVFunctionTextC
            // 
            this.colPVFunctionTextC.Caption = "PVFunctionTextC";
            this.colPVFunctionTextC.ColumnEdit = this.repositoryItemButtonEdit2;
            this.colPVFunctionTextC.FieldName = "PVFunctionTextC";
            this.colPVFunctionTextC.Name = "colPVFunctionTextC";
            this.colPVFunctionTextC.Visible = true;
            this.colPVFunctionTextC.VisibleIndex = 16;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit1_ButtonClick);
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.repositoryItemButtonEdit2_ButtonClick);
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
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource oneCompartmentDataBindingSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private System.Windows.Forms.Button btnApply;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private System.Windows.Forms.Button btnChangeAll;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn colCompartmentName;
        private DevExpress.XtraGrid.Columns.GridColumn colVolumeI;
        private DevExpress.XtraGrid.Columns.GridColumn colUnstressedI;
        private DevExpress.XtraGrid.Columns.GridColumn colRigidityI;
        private DevExpress.XtraGrid.Columns.GridColumn colVolumeC;
        private DevExpress.XtraGrid.Columns.GridColumn colUnstressedC;
        private DevExpress.XtraGrid.Columns.GridColumn colRigidityC;
        private DevExpress.XtraGrid.Columns.GridColumn colResistanceVI;
        private DevExpress.XtraGrid.Columns.GridColumn colResistanceIC;
        private DevExpress.XtraGrid.Columns.GridColumn colResistanceIL;
        private DevExpress.XtraGrid.Columns.GridColumn colHeight;
        private DevExpress.XtraGrid.Columns.GridColumn colKElastI;
        private DevExpress.XtraGrid.Columns.GridColumn colKElastC;
        private DevExpress.XtraGrid.Columns.GridColumn colCFRPressure0;
        private DevExpress.XtraGrid.Columns.GridColumn colCFRPressureGain;
        private DevExpress.XtraGrid.Columns.GridColumn colPVFunctionTextI;
        private DevExpress.XtraGrid.Columns.GridColumn colPVFunctionTextC;
    }
}
