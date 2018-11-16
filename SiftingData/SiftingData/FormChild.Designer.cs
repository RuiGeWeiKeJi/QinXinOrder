namespace SiftingData
{
    partial class FormChild
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormChild));
            this.barMenu = new DevExpress.XtraBars.Bar();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.barTool = new DevExpress.XtraBars.Bar();
            this.toolQuery = new DevExpress.XtraBars.BarButtonItem();
            this.toolAdd = new DevExpress.XtraBars.BarButtonItem();
            this.toolEdit = new DevExpress.XtraBars.BarButtonItem();
            this.toolDelete = new DevExpress.XtraBars.BarButtonItem();
            this.toolPrint = new DevExpress.XtraBars.BarButtonItem();
            this.toolExport = new DevExpress.XtraBars.BarButtonItem();
            this.toolSave = new DevExpress.XtraBars.BarButtonItem();
            this.toolCanecl = new DevExpress.XtraBars.BarButtonItem();
            this.toolQueryAll = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // barMenu
            // 
            this.barMenu.BarAppearance.Disabled.BackColor = System.Drawing.Color.Gainsboro;
            this.barMenu.BarAppearance.Disabled.Font = new System.Drawing.Font("宋体", 10.5F);
            this.barMenu.BarAppearance.Disabled.Options.UseFont = true;
            this.barMenu.BarAppearance.Hovered.BackColor = System.Drawing.Color.Gainsboro;
            this.barMenu.BarAppearance.Hovered.Font = new System.Drawing.Font("宋体", 10.5F);
            this.barMenu.BarAppearance.Hovered.Options.UseFont = true;
            this.barMenu.BarAppearance.Normal.BackColor = System.Drawing.Color.Gainsboro;
            this.barMenu.BarAppearance.Normal.Font = new System.Drawing.Font("宋体", 10.5F);
            this.barMenu.BarAppearance.Normal.Options.UseFont = true;
            this.barMenu.BarAppearance.Pressed.BackColor = System.Drawing.Color.Gainsboro;
            this.barMenu.BarAppearance.Pressed.Font = new System.Drawing.Font("宋体", 10.5F);
            this.barMenu.BarAppearance.Pressed.Options.UseFont = true;
            this.barMenu.BarName = "Main menu";
            this.barMenu.DockCol = 0;
            this.barMenu.DockRow = 0;
            this.barMenu.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barMenu.FloatLocation = new System.Drawing.Point(93, 157);
            this.barMenu.OptionsBar.DrawBorder = false;
            this.barMenu.OptionsBar.DrawDragBorder = false;
            this.barMenu.OptionsBar.RotateWhenVertical = false;
            this.barMenu.Text = "Main menu";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barTool});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolQuery,
            this.toolAdd,
            this.toolEdit,
            this.toolDelete,
            this.toolPrint,
            this.toolExport,
            this.toolSave,
            this.toolCanecl,
            this.barButtonItem1,
            this.toolQueryAll});
            this.barManager1.MainMenu = this.barTool;
            this.barManager1.MaxItemId = 15;
            // 
            // barTool
            // 
            this.barTool.BarAppearance.Disabled.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barTool.BarAppearance.Disabled.Options.UseFont = true;
            this.barTool.BarAppearance.Hovered.Font = new System.Drawing.Font("宋体", 10.5F);
            this.barTool.BarAppearance.Hovered.Options.UseFont = true;
            this.barTool.BarAppearance.Normal.BackColor = System.Drawing.Color.Transparent;
            this.barTool.BarAppearance.Normal.Font = new System.Drawing.Font("宋体", 10.5F);
            this.barTool.BarAppearance.Normal.Options.UseBackColor = true;
            this.barTool.BarAppearance.Normal.Options.UseFont = true;
            this.barTool.BarAppearance.Pressed.Font = new System.Drawing.Font("宋体", 10.5F);
            this.barTool.BarAppearance.Pressed.Options.UseFont = true;
            this.barTool.BarName = "Main menu";
            this.barTool.DockCol = 0;
            this.barTool.DockRow = 0;
            this.barTool.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barTool.FloatLocation = new System.Drawing.Point(141, 151);
            this.barTool.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolQuery, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolAdd, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolPrint, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolSave, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolCanecl, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolQueryAll, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.barTool.OptionsBar.AllowQuickCustomization = false;
            this.barTool.OptionsBar.DrawBorder = false;
            this.barTool.OptionsBar.DrawDragBorder = false;
            this.barTool.OptionsBar.Hidden = true;
            this.barTool.OptionsBar.RotateWhenVertical = false;
            this.barTool.Text = "Main menu";
            // 
            // toolQuery
            // 
            this.toolQuery.Caption = "查询";
            this.toolQuery.Id = 0;
            this.toolQuery.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolQuery.ImageOptions.Image")));
            this.toolQuery.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolQuery.ImageOptions.LargeImage")));
            this.toolQuery.Name = "toolQuery";
            this.toolQuery.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolQuery_ItemClick);
            // 
            // toolAdd
            // 
            this.toolAdd.Caption = "新增";
            this.toolAdd.Id = 1;
            this.toolAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolAdd.ImageOptions.Image")));
            this.toolAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolAdd.ImageOptions.LargeImage")));
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolAdd_ItemClick);
            // 
            // toolEdit
            // 
            this.toolEdit.Caption = "编辑";
            this.toolEdit.Id = 2;
            this.toolEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolEdit.ImageOptions.Image")));
            this.toolEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolEdit.ImageOptions.LargeImage")));
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolEdit_ItemClick);
            // 
            // toolDelete
            // 
            this.toolDelete.Caption = "删除";
            this.toolDelete.Id = 3;
            this.toolDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolDelete.ImageOptions.Image")));
            this.toolDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolDelete.ImageOptions.LargeImage")));
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolDelete_ItemClick);
            // 
            // toolPrint
            // 
            this.toolPrint.Caption = "打印";
            this.toolPrint.Id = 7;
            this.toolPrint.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolPrint.ImageOptions.Image")));
            this.toolPrint.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolPrint.ImageOptions.LargeImage")));
            this.toolPrint.Name = "toolPrint";
            this.toolPrint.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolPrint_ItemClick);
            // 
            // toolExport
            // 
            this.toolExport.Caption = "导出";
            this.toolExport.Id = 8;
            this.toolExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolExport.ImageOptions.Image")));
            this.toolExport.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolExport.ImageOptions.LargeImage")));
            this.toolExport.Name = "toolExport";
            this.toolExport.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolExport_ItemClick);
            // 
            // toolSave
            // 
            this.toolSave.Caption = "保存";
            this.toolSave.Id = 9;
            this.toolSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolSave.ImageOptions.Image")));
            this.toolSave.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolSave.ImageOptions.LargeImage")));
            this.toolSave.Name = "toolSave";
            this.toolSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolSave_ItemClick);
            // 
            // toolCanecl
            // 
            this.toolCanecl.Caption = "取消";
            this.toolCanecl.Id = 10;
            this.toolCanecl.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolCanecl.ImageOptions.Image")));
            this.toolCanecl.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolCanecl.ImageOptions.LargeImage")));
            this.toolCanecl.Name = "toolCanecl";
            this.toolCanecl.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolCanecl_ItemClick);
            // 
            // toolQueryAll
            // 
            this.toolQueryAll.Caption = "全查";
            this.toolQueryAll.Id = 14;
            this.toolQueryAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolQueryAll.ImageOptions.Image")));
            this.toolQueryAll.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("toolQueryAll.ImageOptions.LargeImage")));
            this.toolQueryAll.Name = "toolQueryAll";
            this.toolQueryAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolQueryAll_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.barDockControlTop.Appearance.Options.UseBackColor = true;
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1237, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 438);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1237, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 414);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1237, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 414);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Id = 12;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // FormChild
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 438);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "FormChild";
            this.Text = "配方作业";
            this.Load += new System.EventHandler(this.FormChild_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public DevExpress . XtraBars . Bar barMenu;
        private DevExpress . XtraBars . BarDockControl barDockControlTop;
        private DevExpress . XtraBars . BarDockControl barDockControlBottom;
        private DevExpress . XtraBars . BarDockControl barDockControlLeft;
        private DevExpress . XtraBars . BarDockControl barDockControlRight;
        public DevExpress . XtraBars . Bar barTool;
        public DevExpress . XtraBars . BarButtonItem toolQuery;
        public DevExpress . XtraBars . BarButtonItem toolAdd;
        public DevExpress . XtraBars . BarButtonItem toolEdit;
        public DevExpress . XtraBars . BarButtonItem toolDelete;
        public DevExpress . XtraBars . BarButtonItem toolPrint;
        public DevExpress . XtraBars . BarButtonItem toolExport;
        public DevExpress . XtraBars . BarButtonItem toolSave;
        public DevExpress . XtraBars . BarButtonItem toolCanecl;
        public DevExpress . XtraBars . BarManager barManager1;
        private DevExpress . XtraBars . BarButtonItem barButtonItem1;
        public DevExpress . XtraBars . BarButtonItem toolQueryAll;
    }
}