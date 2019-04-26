using FastReport;
using FastReport . Export . Xml;
using SiftingDataBll;
using System;
using System . Data;
using System . Windows . Forms;

namespace SiftingData
{
    public partial class FormChild :DevExpress . XtraEditors . XtraForm
    {

        public FormChild ( )
        {
            InitializeComponent ( );  
            

                   
        }
        
        private void FormChild_Load ( object sender ,EventArgs e )
        {
            toolState ( );


        }

        protected void toolState ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }

        protected virtual int Query ( )
        {
           
            return 0;
        }
        protected void QueryTool ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
        }
        private void toolQuery_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "查询";
            Query ( );
        }

        protected virtual int Add (  )
        {
           
            return 0;
        }
        private void toolAdd_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "新增";
            Add ( );
        }
        protected void addTool ( )
        {
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }


        protected virtual int Edit ( )
        {
            
            return 0;
        }
        private void toolEdit_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "编辑";
            Edit ( );
        }
        protected void editTool ( )
        {
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }

        protected virtual int Delete ( )
        {
            
            return 0;
        }
        protected void deleteTool ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }
        private void toolDelete_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "删除";
            Delete ( );
        }


        protected virtual int Save ( )
        {
           
            return 0;
        }
        private void toolSave_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "保存";
            Save ( );
        }
        protected void saveTool ( )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
        }

        protected virtual int Cancel ( )
        {
           
            return 0;
        }
        private void toolCanecl_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "取消";
            Cancel ( );
        }
        protected void cancelTool ( string  state)
        {
            if ( state . Equals ( "add" ) )
            {
                toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
            else if ( state . Equals ( "edit" ) )
            {
                toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
        }

        protected virtual int Print ( )
        {
           
            return 0;
        }
        private void toolPrint_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "打印";
            Print ( );
        }

        protected virtual int Export ( )
        {
            
            return 0;
        }
        private void toolExport_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "导出";
            Export ( );
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="printFile"></param>
        protected void Print ( DataTable [ ] dt ,string printFile )
        {
            DataSet ds = new DataSet ( );
            if ( dt . Length > 0 )
            {
                for ( int i = 0 ; i < dt . Length ; i++ )
                {
                    ds . Tables . Add ( dt [ i ] );
                }
            }

            if ( ds != null && ds . Tables . Count > 0 )
            {
                string file = Application . StartupPath + "\\" + printFile + "";
                Report report = new Report ( );
                report . Load ( file );
                report . RegisterData ( ds );
                report . Show ( );
            }

        }

        /// <summary>
        /// 导出Execl
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="printFile"></param>
        protected void Export ( DataTable [ ] dt ,string printFile )
        {
            DataSet ds = new DataSet ( );
            if ( dt . Length > 0 )
            {
                for ( int i = 0 ; i < dt . Length ; i++ )
                {
                    ds . Tables . Add ( dt [ i ] );
                }
            }

            if ( ds != null && ds . Tables . Count > 0 )
            {
                string file = Application . StartupPath + "\\" + printFile + "";
                Report report = new Report ( );
                report . Load ( file );
                report . RegisterData ( ds );
                report . Prepare ( );
                XMLExport exprots = new XMLExport ( );
                exprots . Export ( report );
            }
        }

        protected virtual int QueryAll ( )
        {
            
            return 0;
        }
        private void toolQueryAll_ItemClick ( object sender ,DevExpress . XtraBars . ItemClickEventArgs e )
        {
            UserInfoMation . TypeOfOper = "全查";
            QueryAll ( );
        }
    }
}
