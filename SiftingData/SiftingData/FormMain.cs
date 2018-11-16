using System;
using System . Data;
using System . Linq;
using System . Collections . Generic;
using System . Windows . Forms;
using DevExpress . XtraEditors;

namespace SiftingData
{
    public partial class FormMain :FormChild
    {
        SiftingDataBll.Bll.MainBll _bll=null;
        SiftingDataEntity . MainEntity model =null;
        DataTable tableView, tablePNum,tablePrintOne,tablePrintTwo,tableQuery,tableOrder;
        string state=string.Empty,resultCal = string . Empty,strWhere=string.Empty,strW=string.Empty,cancelWhere=string.Empty;
        bool result=false;

        decimal sum = 0M;

        List<int> idxList=new List<int>();

        public FormMain ( )
        {
            InitializeComponent ( );

            _bll = new SiftingDataBll . Bll . MainBll ( );
            model = new SiftingDataEntity . MainEntity ( );
            getQueryForColumn ( );
            Utility . GridViewMoHuSelect . SetFilter ( gridLookUpEdit1View );
            Utility . GridViewMoHuSelect . SetFilter ( sunOfNumA );
            Utility . GridViewMoHuSelect . SetFilter ( gridView2 );
            Utility . GridViewMoHuSelect . SetFilter ( gridView4 );
            Utility . GridViewMoHuSelect . SetFilter ( View4 );
            controlUnEnable ( );
        }

        #region Main
        protected override int Query ( )
        {
            if ( string . IsNullOrEmpty ( txtDEL015 . Text ) && string . IsNullOrEmpty ( txtRAA001 . Text ) && string . IsNullOrEmpty ( txtRAA015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择配方单号或订单号和主件品号" );
                return 0;
            }

            if ( !string . IsNullOrEmpty ( txtDEL015 . Text ) )
            {
                txtDEL015 . Tag = txtDEL015 . Text;
                strWhere = "1=1";
                strWhere += " AND DEL015='" + txtDEL015 . Text + "'";
            }
            else
            {
                if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
                {
                    XtraMessageBox . Show ( "请选择订单号" );
                    return 0;
                }
                if ( string . IsNullOrEmpty ( txtRAA015 . Text ) )
                {
                    XtraMessageBox . Show ( "请选择主件品号" );
                    return 0;
                }
                strWhere = "1=1";
                strWhere += " AND DEL016='" + txtDEA002 . Text + "' AND DEL017='" + txtRAA001 . Text + "' AND DEL018='" + txtDEL018 . Text + "'";
            }
            cancelWhere = strWhere;
            tableView = _bll . getTableView ( strWhere );
            gridControl1 . DataSource = tableView;
           
            if ( tableView == null || tableView . Rows . Count < 1 )
                return 0;
            //editState = tableView . Rows [ 0 ] [ "LIA961" ] . ToString ( );
            txtDEL015 . Tag = txtDEL015 . Text =  tableView . Rows [ 0 ] [ "DEL015" ] . ToString ( );
            txtDEL013 . Text = tableView . Rows [ 0 ] [ "DEL013" ] . ToString ( );
            btn . Text = string . IsNullOrEmpty ( tableView . Rows [ 0 ] [ "DEL014" ] . ToString ( ) ) == true ? "审核" : ( Convert . ToBoolean ( tableView . Rows [ 0 ] [ "DEL014" ] . ToString ( ) ) == true ? "反审核" : "审核" );
           
            txtRAA001 . EditValue = txtRAA001 . Text = tableView . Rows [ 0 ] [ "DEL017" ] . ToString ( );
            //txtRAA015 . EditValue = txtRAA015 . Text = tableView . Rows [ 0 ] [ "DEL011" ] . ToString ( );
            txtDEL018 . EditValue = txtDEL018 . Text = string . Empty;
            txtDEL018 . EditValue = txtDEL018 . Text = tableView . Rows [ 0 ] [ "DEL018" ] . ToString ( );
            QueryTool ( );
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            txtDEL015 . Enabled = true;
            btn . Enabled = true;
            if ( btn . Text . Equals ( "反审核" ) )
                toolEdit . Visibility = toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            else
                toolEdit . Visibility = toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            //getQueryForColumn ( );

            return base . Query ( );
        }
        protected override int Add ( )
        {
            controlClear ( );
            controlEnable ( );
            gridControl1 . DataSource = null;
            txtDEL015 . Enabled = false;
            btn . Enabled = false;

            state = "add";
            addTool ( );

            txtDEL015 . Tag = _bll . getOdd ( );
            //txtDEL015 . Tag = txtDEL015 . Text;

            tableView = _bll . getTableView ( " DEL015='" + txtDEL015 . Text + "'" );
            gridControl1 . DataSource = tableView;
            model . DEL015 = txtDEL015 . Text;
            btn . Text = "审核";

            strW = "1=1";
            strW = " AND IBB015='N' AND IBB023='T' AND IBB965=''";
            tableQuery = _bll . getTableQuery ( strW );
            txtRAA001 . Properties . DataSource = tableQuery;
            txtRAA001 . Properties . DisplayMember = "IBB001";
            txtRAA001 . Properties . ValueMember = "IBB001";

            return base . Add ( );
        }
        protected override int Edit ( )
        {
            //if ( editState . Equals ( "T" ) )
            //{
            //    XtraMessageBox . Show ( "此配方已全部入库完成,不允许编辑" );
            //    return 0;
            //}
            controlEnable ( );
            editTool ( );
            txtDEL015 . Enabled = false;
            btn . Enabled = false;
            state = "edit";
            //txtDEL015 . Text = txtDEL015 . Tag . ToString ( );

            return base . Edit ( );
        }
        protected override int Delete ( )
        {
            if ( string . IsNullOrEmpty ( txtDEL015 . Text ) )
            {
                XtraMessageBox . Show ( "配方单号不可为空" );
                return 0;
            }
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "订单单号不可为空" );
                return 0;
            }
            if ( string . IsNullOrEmpty ( txtDEA002 . Text ) )
            {
                XtraMessageBox . Show ( "品名不可为空" );
                return 0;
            }
            
            if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
            {
                if ( _bll . ExistsJSK ( txtRAA001 . Text ,txtDEA002 . Text ) )
                {
                    XtraMessageBox . Show ( "系统已经生成分筛领料,不允许删除" );
                    return 0;
                }

                result = _bll . Delete ( txtDEL015 . Text ,txtRAA001 . Text ,txtDEL018 . Text );
                if ( result )
                {
                    XtraMessageBox . Show ( "删除成功" );
                    controlClear ( );
                    controlUnEnable ( );
                    deleteTool ( );
                    txtDEL015 . Text = string . Empty;
                    txtDEL015 . Enabled = true;
                    btn . Enabled = true;

                    strW = "1=1";
                    strW = " AND IBB015='N' AND IBB023='T' AND IBB965!=''";
                    tableQuery = _bll . getTableQuery ( strW );
                    txtRAA001 . Properties . DataSource = tableQuery;
                    txtRAA001 . Properties . DisplayMember = "IBB001";
                    txtRAA001 . Properties . ValueMember = "IBB001";

                }
                else
                    XtraMessageBox . Show ( "删除失败" );
            }

            return base . Delete ( );
        }
        protected override int Print ( )
        {
            if ( string . IsNullOrEmpty ( txtDEL015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择配方单号" );
                return 0;
            }
            getPrintTable ( );

            Print ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"加工通知单.frx" );

            return base . Print ( );
        }
        protected override int Export ( )
        {
            if ( string . IsNullOrEmpty ( txtDEL015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择配方单号" );
                return 0;
            }
            getPrintTable ( );

            Export ( new DataTable [ ] { tablePrintOne ,tablePrintTwo } ,"加工通知单.frx" );

            return base . Export ( );
        }
        protected override int Save ( )
        {
            if ( checkTable ( ) == false )
                return 0;

            model . DEL017 = txtRAA001 . Text;
           

            if ( state . Equals ( "add" ) )
                result = _bll . Save ( tableView ,txtDEL013 . Text ,txtRAA001 . Text ,txtDEA002 . Text ,txtRAA015 . Text ,txtDEL018 . Text );
            else if ( state . Equals ( "edit" ) )
                result = _bll . Update ( tableView ,txtDEL015 . Text ,idxList ,txtDEL013 . Text ,txtRAA001 . Text ,txtDEA002 . Text ,txtRAA015 . Text ,txtDEL018 . Text );

            if ( result )
            {
                XtraMessageBox . Show ( "成功保存" );
                saveTool ( );
                controlUnEnable ( );
                idxList . Clear ( );
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                btn . Text = "审核";
                btn . Enabled = true;

                strWhere = "1=1";
                strWhere += " AND DEL015='" + txtDEL015 . Tag + "'";
                tableView = _bll . getTableView ( strWhere );
                gridControl1 . DataSource = tableView;

                DataTable tableOdd = _bll . getOddTable ( );
                txtDEL015 . Properties . DataSource = tableOdd;
                txtDEL015 . Properties . DisplayMember = "DEL015";
                txtDEL015 . Properties . ValueMember = "DEL015";
                txtDEL015 . Text = txtDEL015 . Tag == null ? string . Empty : txtDEL015 . Tag . ToString ( );

                strW = "1=1";
                strW = " AND IBB015='N' AND IBB023='T' ";//AND IBB965=''
                tableQuery = _bll . getTableQuery ( strW );
                txtRAA001 . Properties . DataSource = tableQuery;
                txtRAA001 . Properties . DisplayMember = "IBB001";
                txtRAA001 . Properties . ValueMember = "IBB001";

                txtRAA001 . EditValue = model . DEL017;

                //Query ( );
            }
            else
                XtraMessageBox . Show ( "保存失败" );

            return base . Save ( );
        }
        protected override int Cancel ( )
        {
            if ( state . Equals ( "add" ) )
            {
                controlClear ( );
            }
            controlUnEnable ( );
            cancelTool ( state );
            toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            txtDEL015 . Enabled = true;
            btn . Enabled = true;

            strW = "1=1";
            strW = " AND IBB015='N' AND IBB023='T' AND IBB965!=''";
            tableQuery = _bll . getTableQuery ( strW );
            txtRAA001 . Properties . DataSource = tableQuery;
            txtRAA001 . Properties . DisplayMember = "IBB001";
            txtRAA001 . Properties . ValueMember = "IBB001";

            if ( state . Equals ( "edit" ) )
            {
                tableView = _bll . getTableView ( cancelWhere );
                gridControl1 . DataSource = tableView;
            }

            return base . Cancel ( );
        }
        protected override int QueryAll ( )
        {
            if ( string . IsNullOrEmpty ( strW ) )
                strW = " AND IBB015='N' AND IBB023='T' AND IBB965!=''";
            else
                strW = " AND IBB023='T' AND IBB965!=''";
            tableQuery = _bll . getTableQuery ( strW );
            txtRAA001 . Properties . DataSource = tableQuery;
            txtRAA001 . Properties . DisplayMember = "IBB001";
            txtRAA001 . Properties . ValueMember = "IBB001";
            txtDEL015 . Enabled = true;
            btn . Enabled = true;

            tablePNum = _bll . getTablePNum ( );
            sonOfNum . DataSource = tablePNum;
            sonOfNum . DisplayMember = "DEA001";
            sonOfNum . ValueMember = "DEA001";

            if ( toolSave . Visibility == DevExpress . XtraBars . BarItemVisibility . Always )
            {
                txtDEL015 . Enabled = false;
                btn . Enabled = false;
            }
            else
            {
                txtDEL015 . Enabled = true;
                btn . Enabled = true;
            }

            return base . QueryAll ( );
        }
        private void btn_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "来源单号不可为空" );
                return;
            }
            SiftingDataEntity . MainEntity model = new SiftingDataEntity . MainEntity ( );
            state = btn . Text;
            if ( state . Equals ( "审核" ) )
                model . DEL014 = true;
            else
                model . DEL014 = false;

            model . DEL011 = txtRAA001 . Text;
            model . DEL015 = txtDEL015 . Text;
            if ( state . Equals ( "反审核" ) )
            {
                if ( _bll . Exists ( txtRAA001.Text ,txtDEA002.Text ) == false )
                {
                    XtraMessageBox . Show ( "已经入库,不允许反审核" );
                    return ;
                }
            }
            result = _bll . Examine ( model );
            if ( result )
            {
                XtraMessageBox . Show ( state + "成功" );
                //if ( state . Equals ( "审核" ) )
                //    btn . Text = "反审核";
                //else if ( state . Equals ( "反审核" ) )
                //    btn . Text = "审核";
                examine ( state );
                if ( state . Equals ( "反审核" ) )
                    toolEdit . Visibility = toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                else
                    toolEdit . Visibility = toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
            else
                XtraMessageBox . Show ( state + "失败" );
        }
        #endregion

        #region Event
        private void txtRAA001_EditValueChanged ( object sender ,EventArgs e )
        {
            txtRAA003 . Text = txtRAA015 . Text = txtDEA002 . Text = txtDEA057 . Text = txtRAA018 . Text = txtIBB981 . Text = txtIBB980 . Text = txtIBB961 . Text = txtIBB005 . Text =txtDFA002.Text= string . Empty;
            if ( txtRAA001 . EditValue == null )
                return;
            if ( string . IsNullOrEmpty ( txtRAA001 . EditValue . ToString ( ) ) )
                return;
            if ( tableQuery . Select ( "IBB001='" + txtRAA001 . EditValue + "'" ) . Length < 1 )
                return;
            DataRow row = tableQuery . Select ( "IBB001='" + txtRAA001 . EditValue + "'" ) [ 0 ];
            if ( row == null )
                return;

            txtRAA003 . Text = row [ "IBA003" ] . ToString ( );
            txtDFA002 . Text = row [ "DFA002" ] . ToString ( );

            tableOrder = _bll . getTableQueryPin ( txtRAA001 . EditValue . ToString ( ) ,strW );
            txtDEL018 . Properties . DataSource = tableOrder;
            txtDEL018 . Properties . DisplayMember = "IBB002";
            txtDEL018 . Properties . ValueMember = "IBB002";
        }
        private void txtRAA015_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtRAA015 . EditValue == null || txtRAA015 . EditValue . ToString ( ) == string . Empty )
                return;
            //DataRow row = gridView3 . GetFocusedDataRow ( );
            //if ( row == null )
            //{
            if ( tableOrder . Select ( "IBB003='" + txtRAA015 . EditValue + "'" ) . Length < 1 )
                return;
            DataRow row = tableOrder . Select ( "IBB003='" + txtRAA015 . EditValue + "'" ) [ 0 ];
            if ( row == null )
                return;
            //}
            txtDEA002 . Text = row [ "IBB004" ] . ToString ( );
            txtDEA057 . Text = row [ "IBB041" ] . ToString ( );
            txtIBB961 . Text = row [ "IBB961" ] . ToString ( );
            txtRAA018 . Text = row [ "IBB006" ] . ToString ( );
            txtIBB981 . Text = row [ "IBB981" ] . ToString ( );
            txtIBB980 . Text = row [ "IBB980" ] . ToString ( );
            txtIBB005 . Text = row [ "IBB005" ] . ToString ( );
            txtDEL018 . Text = row [ "IBB002" ] . ToString ( );
        }
        private void txtDEL018_EditValueChanged ( object sender ,EventArgs e )
        {
            if ( txtDEL018 . EditValue == null || txtDEL018 . EditValue . ToString ( ) == string . Empty )
                return;
            if ( tableOrder == null || tableOrder . Rows . Count < 1 )
                return;
            if ( tableOrder . Select ( "IBB002='" + txtDEL018 . EditValue + "'" ) . Length < 1 )
                return;
            DataRow row = tableOrder . Select ( "IBB002='" + txtDEL018 . EditValue + "'" ) [ 0 ];
            if ( row == null )
                return;
            txtDEA002 . Text = row [ "IBB004" ] . ToString ( );
            txtDEA057 . Text = row [ "IBB041" ] . ToString ( );
            txtIBB961 . Text = row [ "IBB961" ] . ToString ( );
            txtRAA018 . Text = row [ "IBB006" ] . ToString ( );
            txtIBB981 . Text = row [ "IBB981" ] . ToString ( );
            txtIBB980 . Text = row [ "IBB980" ] . ToString ( );
            txtIBB005 . Text = row [ "IBB005" ] . ToString ( );
            txtRAA015 . Text = row [ "IBB003" ] . ToString ( );
        }
        private void sonOfNum_EditValueChanged ( object sender ,EventArgs e )
        {
            DevExpress . XtraEditors . BaseEdit edit = gridView1 . ActiveEditor;
            if ( edit == null )
                return;
            if ( gridView1 . FocusedColumn . FieldName . Equals ( "DEL001" ) )
            {
                if ( edit . EditValue == null )
                    return;
                if ( string . IsNullOrEmpty ( edit . EditValue . ToString ( ) ) )
                    return;
                DataRow row = tablePNum . Select ( "DEA001='" + edit . EditValue + "'" ) [ 0 ];
                if ( row == null )
                    return;
                if ( !row [ "DEA001" ] . ToString ( ) . Equals ( edit . EditValue ) )
                    return;
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL002" ] ,row [ "DEA002" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL003" ] ,row [ "DEA057" ] . ToString ( ) );
                gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL004" ] ,row [ "DEA003" ] . ToString ( ) );
            }
        }
        private void btnPNum_Click ( object sender ,EventArgs e )
        {
            DataRow row = gridView1 . GetFocusedDataRow ( );
            if ( row == null )
                return;
            FormMainQuery form = new FormMainQuery ( row [ "DEL001" ] . ToString ( ) ,"批号查询" );
            form . StartPosition = System . Windows . Forms . FormStartPosition . CenterParent;
            if ( form . ShowDialog ( ) == System . Windows . Forms . DialogResult . OK )
            {
                Dictionary<int ,string> dicSpe = form . getSpe;
                Dictionary<int ,string> dicWare = form . getWare;
                if ( dicSpe != null && dicSpe . Count > 0 )
                {
                    for ( int i = 0 ; i < dicSpe . Count ; i++ )
                    {
                        if ( i == 0 )
                        {
                            gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL005" ] ,dicSpe [ i ] );
                            gridView1 . SetFocusedRowCellValue ( gridView1 . Columns [ "DEL006" ] ,dicWare [ i ] );
                        }
                        else
                        {
                            DataRow rowAdd = tableView . NewRow ( );
                            rowAdd [ "DEL001" ] = row [ "DEL001" ] . ToString ( );
                            rowAdd [ "DEL002" ] = row [ "DEL002" ] . ToString ( );
                            rowAdd [ "DEL003" ] = row [ "DEL003" ] . ToString ( );
                            rowAdd [ "DEL004" ] = row [ "DEL004" ] . ToString ( );
                            rowAdd [ "DEL005" ] = dicSpe [ i ];
                            rowAdd [ "DEL006" ] = dicWare [ i ];
                            tableView . Rows . Add ( rowAdd );
                        }
                    }
                }
            }
        }
        //生成工单
        private void btnOne_Click ( object sender ,EventArgs e )
        {
            if (txtDEL015.Enabled==false || txtDEL015 . Text . Equals ( "审核" ) )
            {
                XtraMessageBox . Show ( "请审核" );
                return;
            }
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "工单单号不可为空" );
                return ;
            }

            int resu = _bll . SaveOrder ( txtRAA001 . Text );
            if ( resu == -1 )
                XtraMessageBox . Show ( "标准系统没有此工单" );
            else if ( resu == -2 )
                XtraMessageBox . Show ( "单身无数据" );
            else if ( resu == 1 )
                XtraMessageBox . Show ( "成功写入工单" );
            else
                XtraMessageBox . Show ( "写入工单失败" );

        }
        //生成领料单
        private void btnTwo_Click ( object sender ,EventArgs e )
        {
            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "工单单号不可为空" );
                return;
            }
            int resu = _bll . SavePick ( txtRAA001 . Text );
            if ( resu == -1 )
                XtraMessageBox . Show ( "单身无数据" );
            else if ( resu == -2 )
                XtraMessageBox . Show ( "标准系统已经存在领料单" );
            else if ( resu == 1 )
                XtraMessageBox . Show ( "成功写入领料单" );
            else
                XtraMessageBox . Show ( "写入领料单失败" );
        }
        private void gridControl1_KeyPress ( object sender ,System . Windows . Forms . KeyPressEventArgs e )
        {
            if ( e . KeyChar == (char)Keys.Enter && toolSave.Visibility== DevExpress.XtraBars.BarItemVisibility.Always )
            {
                if ( XtraMessageBox . Show ( "确认删除?" ,"删除" ,MessageBoxButtons . OKCancel ) == DialogResult . OK )
                {
                    DataRow row = gridView1 . GetFocusedDataRow ( );
                    if ( row == null )
                        return;
                    int idx = string . IsNullOrEmpty ( row [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( row [ "idx" ] . ToString ( ) );
                    if ( idx > 0 )
                    {
                        if ( !idxList . Contains ( idx ) )
                            idxList . Add ( idx );
                    }
                    tableView . Rows . Remove ( row );
                    gridControl1 . RefreshDataSource ( );
                    row = null;
                    sum = 0;
                    resultCal = string . Empty;
                    caclu ( sum ,resultCal ,row );
                }
            }
        }
        private void gridView1_InitNewRow ( object sender ,DevExpress . XtraGrid . Views . Grid . InitNewRowEventArgs e )
        {
            //DevExpress . XtraGrid . Views . Grid . GridView view = sender as DevExpress . XtraGrid . Views . Grid . GridView;
            //view . SetRowCellValue ( e . RowHandle ,view . Columns [ "U1" ] ,dtNow );
            
        }
        private void gridView1_CellValueChanged ( object sender ,DevExpress . XtraGrid . Views . Base . CellValueChangedEventArgs e )
        {
            if ( e . Column . FieldName == "DEL007" || e . Column . FieldName == "DEL008" || e . Column . FieldName == "DEL009" )
            {
                gridView1 . CloseEditor ( );
                gridView1 . UpdateCurrentRow ( );
                DataRow row = null;
                sum = 0;
                resultCal = string . Empty;
                sum = caclu ( sum ,resultCal ,row );
                row = gridView1 . GetDataRow ( e . RowHandle );
                if ( row != null )
                {
                    model . DEL007 = string . IsNullOrEmpty ( row [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL007" ] . ToString ( ) );
                    model . DEL008 = string . IsNullOrEmpty ( row [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL008" ] . ToString ( ) );
                    model . DEL009 = string . IsNullOrEmpty ( row [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL009" ] . ToString ( ) );
                    resultCal = sum == 0 ? 0 . ToString ( ) : Math . Round ( Convert . ToDecimal ( model . DEL007 * model . DEL008 + model . DEL009 ) / sum * 100 ,2 ) + "%";
                    gridView1 . SetRowCellValue ( e . RowHandle ,gridView1 . Columns [ "DEL010" ] ,resultCal );
                }
            }
        }
        //清空
        private void btnClear_Click ( object sender ,EventArgs e )
        {
            txtDEA002 . Text = txtDEA057 . Text = txtIBB961 . Text = txtIBB980 . Text = txtIBB981 . Text = txtRAA001 . Text = txtRAA003 . Text = txtRAA015 . Text = txtRAA018 . Text = txtDEL013 . Text = txtDEL015 . Text = string . Empty;
            txtDEL015 . Enabled = true;
        }
        #endregion

        #region Method
        void getQueryForColumn ( )
        {
            strW = "1=1";
            strW = " AND IBB015='N' AND IBB023='T' AND IBB965!=''";
            tableQuery = _bll . getTableQuery ( strW );
            txtRAA001 . Properties . DataSource = tableQuery;
            txtRAA001 . Properties . DisplayMember = "IBB001";
            txtRAA001 . Properties . ValueMember = "IBB001";

            tablePNum = _bll . getTablePNum ( );
            sonOfNum . DataSource = tablePNum;
            sonOfNum . DisplayMember = "DEA001";
            sonOfNum . ValueMember = "DEA001";

            DataTable tableOdd = _bll . getOddTable ( );
            txtDEL015 . Properties . DataSource = tableOdd;
            txtDEL015 . Properties . DisplayMember = "DEL015";
            txtDEL015 . Properties . ValueMember = "DEL015";
        }
        void controlEnable ( )
        {
            gridView1 . OptionsBehavior . Editable = true;
            txtDEL013 . Enabled = true;
        }
        void controlUnEnable ( )
        {
            gridView1 . OptionsBehavior . Editable = false;
            txtDEL013 . Enabled = false;
        }
        void controlClear ( )
        {
            txtDEA002 . Text = txtDEA057 . Text = txtIBB961 . Text = txtIBB980 . Text = txtIBB981 . Text = txtRAA001 . Text = txtRAA003 . Text =  txtRAA015 . Text = txtRAA018 . Text = txtDEL013 . Text =txtDEL015.Text=txtDFA002.Text=txtDEL018.Text= string . Empty;
            gridControl1 . DataSource = null;
        }
        bool checkTable ( )
        {
            result = true;

            if ( string . IsNullOrEmpty ( txtRAA001 . Text ) )
            {
                XtraMessageBox . Show ( "请选择订单号" );
                return false;
            }
            if ( string . IsNullOrEmpty ( txtRAA015 . Text ) )
            {
                XtraMessageBox . Show ( "请选择主件品号" );
                return false;
            }

            gridView1 . CloseEditor ( );
            gridView1 . UpdateCurrentRow ( );

            if ( tableView == null || tableView . Rows . Count < 1 )
            {
                XtraMessageBox . Show ( "请选择单身数据" );
                return false;
            }

            var query = from p in tableView . AsEnumerable ( )
                        group p by new
                        {
                            t1 = p . Field<string> ( "DEL001" ) ,
                            t2 = p . Field<string> ( "DEL005" ) ,
                            t3 = p . Field<string> ( "DEL006" )
                        } into g
                        select new
                        {
                            del001 = g . Key . t1 ,
                            del002 = g . Key . t2 ,
                            del003 = g . Key . t3 ,
                            count = g . Count ( )
                        };

            if ( query == null )
                return true;

            foreach ( var co in query )
            {
                if ( co != null && !string . IsNullOrEmpty ( co . ToString ( ) ) )
                {
                    if ( co . count > 1 )
                    {
                        XtraMessageBox . Show ( "子件品号:" + co . del001 + "\n\r批号:" + co . del002 + "\n\r仓库:" + co . del003 + "\n\r重复出现,请核实" ,"提示" );
                        result = false;
                        break;
                    }
                }
            }

            if ( result == false )
                return false;

            var select = tableView . AsEnumerable ( )
                . Where ( p => p . Field<string> ( "DEL001" ) == null || p . Field<string> ( "DEL001" ) == "" )
                . Select ( p => new
                {
                    del001 = p . Field<string> ( "DEL001" )
                }
                );
            if ( select != null )
            {
                foreach ( var p in select )
                {
                    if ( p == null || string . IsNullOrEmpty ( p . del001 ) )
                    {
                        XtraMessageBox . Show ( "子件品号不可为空" );
                        return false;
                    }
                }
            }

            select = null;
            select = tableView . AsEnumerable ( )
                 . Where ( p => p . Field<string> ( "DEL005" ) == null || p . Field<string> ( "DEL005" ) == "" )
                 . Select ( p => new
                 {
                     del001 = p . Field<string> ( "DEL005" )
                 }
                 );
            if ( select != null )
            {
                foreach ( var p in select )
                {
                    if ( p == null || string . IsNullOrEmpty ( p . del001 ) )
                    {
                        XtraMessageBox . Show ( "批号不可为空" );
                        return false;
                    }
                }
            }


            return result;
        }
        void getPrintTable ( )
        {
            tablePrintOne = _bll . getTablePrintOne ( txtDEL015 . Text );
            tablePrintOne . TableName = "TableOne";
            tablePrintTwo = _bll . getTablePrintTwo ( txtDEL015 . Text );
            tablePrintTwo . TableName = "TableTwo";
        }
        void examine ( string state )
        {
            toolQuery . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            toolAdd . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;

            if ( state . Equals ( "审核" ) )
            {
                btn . Text = "反审核";
                //Concell1 . Show ( );           
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
            }
            else
            {
                btn . Text = "审核";
                //Concell1 . Hide ( );
                toolEdit . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolDelete . Visibility = DevExpress . XtraBars . BarItemVisibility . Always;
                toolSave . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolCanecl . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolPrint . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
                toolExport . Visibility = DevExpress . XtraBars . BarItemVisibility . Never;
            }
        }
        decimal caclu ( decimal sum ,string resultCal ,DataRow row )
        {
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = gridView1 . GetDataRow ( i );
                model . DEL007 = string . IsNullOrEmpty ( row [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( row [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( row [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL009" ] . ToString ( ) );
                sum += Convert . ToDecimal ( model . DEL007 * model . DEL008 + model . DEL009 );
            }
            for ( int i = 0 ; i < tableView . Rows . Count ; i++ )
            {
                row = gridView1 . GetDataRow ( i );
                model . DEL007 = string . IsNullOrEmpty ( row [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( row [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( row [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( row [ "DEL009" ] . ToString ( ) );
                resultCal = sum == 0 ? 0 . ToString ( ) : Math . Round ( Convert . ToDecimal ( model . DEL007 * model . DEL008 + model . DEL009 ) / sum * 100 ,2 ) + "%";
                gridView1 . SetRowCellValue ( i ,gridView1 . Columns [ "DEL010" ] ,resultCal );
            }

            return sum;
        }
        #endregion

    }
}