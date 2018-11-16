namespace SiftingData
{
    partial class FormMainQuery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components . Dispose ( );
            }
            base . Dispose ( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ( )
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.LOA011 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LOA002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LOA003 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(446, 264);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridControl1_KeyPress);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.LOA011,
            this.LOA002,
            this.LOA003});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.CheckBoxSelectorColumnWidth = 35;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // LOA011
            // 
            this.LOA011.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LOA011.AppearanceCell.Options.UseFont = true;
            this.LOA011.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.LOA011.AppearanceHeader.Options.UseFont = true;
            this.LOA011.Caption = "批号";
            this.LOA011.FieldName = "LOA011";
            this.LOA011.Name = "LOA011";
            this.LOA011.OptionsColumn.AllowEdit = false;
            this.LOA011.Visible = true;
            this.LOA011.VisibleIndex = 1;
            // 
            // LOA002
            // 
            this.LOA002.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.LOA002.AppearanceCell.Options.UseFont = true;
            this.LOA002.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.LOA002.AppearanceHeader.Options.UseFont = true;
            this.LOA002.Caption = "仓库";
            this.LOA002.FieldName = "LOA002";
            this.LOA002.Name = "LOA002";
            this.LOA002.OptionsColumn.AllowEdit = false;
            this.LOA002.Visible = true;
            this.LOA002.VisibleIndex = 2;
            // 
            // LOA003
            // 
            this.LOA003.AppearanceCell.Font = new System.Drawing.Font("宋体", 10.5F);
            this.LOA003.AppearanceCell.Options.UseFont = true;
            this.LOA003.AppearanceHeader.Font = new System.Drawing.Font("宋体", 10.5F);
            this.LOA003.AppearanceHeader.Options.UseFont = true;
            this.LOA003.Caption = "数量";
            this.LOA003.FieldName = "LOA003";
            this.LOA003.Name = "LOA003";
            this.LOA003.OptionsColumn.AllowEdit = false;
            this.LOA003.Visible = true;
            this.LOA003.VisibleIndex = 3;
            // 
            // FormMainQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 264);
            this.Controls.Add(this.gridControl1);
            this.Name = "FormMainQuery";
            this.Text = "FormMainQuery";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress . XtraGrid . GridControl gridControl1;
        private DevExpress . XtraGrid . Views . Grid . GridView gridView1;
        private DevExpress . XtraGrid . Columns . GridColumn LOA011;
        private DevExpress . XtraGrid . Columns . GridColumn LOA002;
        private DevExpress . XtraGrid . Columns . GridColumn LOA003;
    }
}