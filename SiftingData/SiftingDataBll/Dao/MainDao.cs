using System . Data;
using System . Text;
using StudentMgr;
using System . Collections;
using System;
using System . Data . SqlClient;
using System . Collections . Generic;
using System . Linq;

namespace SiftingDataBll . Dao
{
    public class MainDao
    {
        /// <summary>
        /// 获取单头数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTableQuery ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT IBB001,IBA003,DFA002,IBB003,IBB004,IBB961 FROM DCSIBA A INNER JOIN DCSIBB B ON A.IBA001=B.IBB001 INNER JOIN TPADFA C ON DFA001=IBA004 " );
            if ( !string . IsNullOrEmpty ( strWhere ) )
                strSql . AppendFormat ( "{0}" ,strWhere );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取主件品号信息
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public DataTable getTableQueryPin ( string orderNum ,string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DISTINCT IBB003,IBB004,IBB041,IBB961,IBB006,IBB005,IBB980,IBB981,IBB002 FROM DCSIBB WHERE IBB001='{0}' {1} " ,orderNum ,strWhere );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取品号
        /// </summary>
        /// <returns></returns>
        public DataTable getTablePNum ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DEA001,DEA002,DEA057,DEA003 FROM TPADEA" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取子表
        /// </summary>
        /// <param name="gdNum"></param>
        /// <returns></returns>
        public DataTable getTableView ( string strWhere )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT idx,DEL001,DEL002,DEL003,DEL004,DEL005,DEL006,DEL007,DEL008,DEL009,DEL010,DEL011,DEL012,DEL013,DEL014,DEL015,DEL016,DEL017,DEL018 FROM QIXDEL WHERE {0}" ,strWhere );
            
            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 根据子件品号获取批号
        /// </summary>
        /// <param name="pNum"></param>
        /// <returns></returns>
        public DataTable getTablePiHao ( string pNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LOA011,LOA002,LOA003 FROM JSKLOA WHERE LOA001='{0}'" ,pNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }
        
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <returns></returns>
        public bool Save ( DataTable table ,string remark ,string oderNum,string num ,string piNum ,string numOfOrder )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            SiftingDataEntity . MainEntity model = new SiftingDataEntity . MainEntity ( );
            model . DEL013 = remark;
            model . DEL015 = getOdd ( );
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                model . DEL011 = piNum;
                model . DEL001 = table . Rows [ i ] [ "DEL001" ] . ToString ( );
                model . DEL002 = table . Rows [ i ] [ "DEL002" ] . ToString ( );
                model . DEL003 = table . Rows [ i ] [ "DEL003" ] . ToString ( );
                model . DEL004 = table . Rows [ i ] [ "DEL004" ] . ToString ( );
                model . DEL005 = table . Rows [ i ] [ "DEL005" ] . ToString ( );
                model . DEL006 = table . Rows [ i ] [ "DEL006" ] . ToString ( );
                model . DEL007 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) );
                model . DEL010 = table . Rows [ i ] [ "DEL010" ] . ToString ( ); /*string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) );*/
                model . DEL012 = table . Rows [ i ] [ "DEL012" ] . ToString ( );
                model . DEL016 = num;
                model . DEL017 = oderNum;
                model . DEL018 = numOfOrder;

                Add ( SQLString ,strSql ,model );
            }
            EditIbb ( SQLString ,model );

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void Add ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . MainEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "insert into QIXDEL(" );
            strSql . Append ( "DEL001,DEL002,DEL003,DEL004,DEL005,DEL006,DEL007,DEL008,DEL009,DEL010,DEL011,DEL012,DEL013,DEL015,DEL016,DEL017,DEL018)" );
            strSql . Append ( " values (" );
            strSql . Append ( "@DEL001,@DEL002,@DEL003,@DEL004,@DEL005,@DEL006,@DEL007,@DEL008,@DEL009,@DEL010,@DEL011,@DEL012,@DEL013,@DEL015,@DEL016,@DEL017,@DEL018)" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DEL001", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL002", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL003", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL004", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL005", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL006", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL007", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL008", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL009", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL010", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL011", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL012", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL013", SqlDbType.NVarChar,250),
                    new SqlParameter("@DEL015", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL016", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL017", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL018", SqlDbType.NVarChar,20)
            };
            parameters [ 0 ] . Value = model . DEL001;
            parameters [ 1 ] . Value = model . DEL002;
            parameters [ 2 ] . Value = model . DEL003;
            parameters [ 3 ] . Value = model . DEL004;
            parameters [ 4 ] . Value = model . DEL005;
            parameters [ 5 ] . Value = model . DEL006;
            parameters [ 6 ] . Value = model . DEL007;
            parameters [ 7 ] . Value = model . DEL008;
            parameters [ 8 ] . Value = model . DEL009;
            parameters [ 9 ] . Value = model . DEL010;
            parameters [ 10 ] . Value = model . DEL011;
            parameters [ 11 ] . Value = model . DEL012;
            parameters [ 12 ] . Value = model . DEL013;
            parameters [ 13 ] . Value = model . DEL015;
            parameters [ 14 ] . Value = model . DEL016;
            parameters [ 15 ] . Value = model . DEL017;
            parameters [ 16 ] . Value = model . DEL018;

            SQLString . Add ( strSql ,parameters );
        }
        void EditIbb ( Hashtable SQLString ,SiftingDataEntity . MainEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE DCSIBB SET IBB965='{0}' WHERE IBB001='{1}' AND IBB002='{2}'" ,model . DEL015 ,model . DEL017 ,model . DEL018 );
            SQLString . Add ( strSql ,null );
        }

        /// <summary>
        /// 更改
        /// </summary>
        /// <param name="table"></param>
        /// <param name="oderNum"></param>
        /// <param name="idxList"></param>
        /// <returns></returns>
        public bool Update ( DataTable table ,string oderNum ,List<int> idxList ,string remark ,string oddNum ,string num,string piNum ,string numOfOrder )
        {
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            SiftingDataEntity . MainEntity model = new SiftingDataEntity . MainEntity ( );
            model . DEL013 = remark;
            model . DEL015 = oderNum;
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                model . DEL011 = piNum;
                model . DEL016 = num;
                model . DEL017 = oddNum;
                model . DEL001 = table . Rows [ i ] [ "DEL001" ] . ToString ( );
                model . DEL002 = table . Rows [ i ] [ "DEL002" ] . ToString ( );
                model . DEL003 = table . Rows [ i ] [ "DEL003" ] . ToString ( );
                model . DEL004 = table . Rows [ i ] [ "DEL004" ] . ToString ( );
                model . DEL005 = table . Rows [ i ] [ "DEL005" ] . ToString ( );
                model . DEL006 = table . Rows [ i ] [ "DEL006" ] . ToString ( );
                model . DEL007 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL007" ] . ToString ( ) );
                model . DEL008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL008" ] . ToString ( ) );
                model . DEL009 = string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL009" ] . ToString ( ) );
                model . DEL010 = table . Rows [ i ] [ "DEL010" ] . ToString ( );/* string . IsNullOrEmpty ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "DEL010" ] . ToString ( ) );*/
                model . id = string . IsNullOrEmpty ( table . Rows [ i ] [ "idx" ] . ToString ( ) ) == true ? 0 : Convert . ToInt32 ( table . Rows [ i ] [ "idx" ] . ToString ( ) );
                model . DEL012 = table . Rows [ i ] [ "DEL012" ] . ToString ( );
                model . DEL018 = numOfOrder;
                if ( model . id < 1 )
                    Add ( SQLString ,strSql ,model );
                else
                    Edit ( SQLString ,strSql ,model );
            }
            
            if ( idxList != null && idxList . Count > 0 )
            {
                foreach ( int i in idxList )
                {
                    model . id = i;
                    if ( model . id > 0 )
                        Remove ( SQLString ,strSql ,model );
                }
            }

            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        void Edit ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . MainEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "update QIXDEL set " );
            strSql . Append ( "DEL001=@DEL001," );
            strSql . Append ( "DEL002=@DEL002," );
            strSql . Append ( "DEL003=@DEL003," );
            strSql . Append ( "DEL004=@DEL004," );
            strSql . Append ( "DEL005=@DEL005," );
            strSql . Append ( "DEL006=@DEL006," );
            strSql . Append ( "DEL007=@DEL007," );
            strSql . Append ( "DEL008=@DEL008," );
            strSql . Append ( "DEL009=@DEL009," );
            strSql . Append ( "DEL010=@DEL010," );
            strSql . Append ( "DEL011=@DEL011," );
            strSql . Append ( "DEL012=@DEL012," );
            strSql . Append ( "DEL013=@DEL013," );
            strSql . Append ( "DEL016=@DEL016," );
            strSql . Append ( "DEL017=@DEL017," );
            strSql . Append ( "DEL018=@DEL018 " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@DEL001", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL002", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL003", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL004", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL005", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL006", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL007", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL008", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL009", SqlDbType.Decimal,9),
                    new SqlParameter("@DEL010", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL011", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL012", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL013", SqlDbType.NVarChar,250),
                    new SqlParameter("@DEL016", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL017", SqlDbType.NVarChar,50),
                    new SqlParameter("@DEL018", SqlDbType.NVarChar,20),
                    new SqlParameter("@idx", SqlDbType.Int,4)};
            parameters [ 0 ] . Value = model . DEL001;
            parameters [ 1 ] . Value = model . DEL002;
            parameters [ 2 ] . Value = model . DEL003;
            parameters [ 3 ] . Value = model . DEL004;
            parameters [ 4 ] . Value = model . DEL005;
            parameters [ 5 ] . Value = model . DEL006;
            parameters [ 6 ] . Value = model . DEL007;
            parameters [ 7 ] . Value = model . DEL008;
            parameters [ 8 ] . Value = model . DEL009;
            parameters [ 9 ] . Value = model . DEL010;
            parameters [ 10 ] . Value = model . DEL011;
            parameters [ 11 ] . Value = model . DEL012;
            parameters [ 12 ] . Value = model . DEL013;
            parameters [ 13 ] . Value = model . DEL016;
            parameters [ 14 ] . Value = model . DEL017;
            parameters [ 15 ] . Value = model . DEL018;
            parameters [ 16 ] . Value = model . id;

            SQLString . Add ( strSql ,parameters );
        }

        void Remove ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . MainEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "delete from QIXDEL " );
            strSql . Append ( " where idx=@idx" );
            SqlParameter [ ] parameters = {
                    new SqlParameter("@idx", SqlDbType.Int,4)
            };
            parameters [ 0 ] . Value = model . id;

            SQLString . Add ( strSql ,parameters );
        }

        /// <summary>
        /// 工单是否审核
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool ExistsSH ( string orderNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM SGMRAA WHERE RAA024='T' AND RAA001='{0}'" ,orderNum );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 是否已经生成JSKLIA  JSKLIB
        /// </summary>
        /// <returns></returns>
        public bool ExistsJSK ( string orderNum ,string proName )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM JSKLIA WHERE LIA960='{0}' AND LIA964='{1}' AND LIA012='T'" ,orderNum ,proName );

            return SqlHelper . Exists ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public bool Delete ( string orderNum ,string orderNums ,string num )
        {
            ArrayList SQLString = new ArrayList ( );
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "delete from QIXDEL WHERE DEL015='{0}'" ,orderNum );
            SQLString . Add ( strSql );
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "UPDATE DCSIBB SET IBB965='' WHERE IBB001='{0}' AND IBB002='{1}'" ,orderNums ,num );
            SQLString . Add ( strSql );
            return SqlHelper . ExecuteSqlTran ( SQLString );
        }

        /// <summary>
        /// 插入工单单身数据
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SaveOrder ( string oddNum )
        {
            bool isOk = true;
            Hashtable SQLString = new Hashtable ( );
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT COUNT(1) FROM SGMRAA WHERE RAA001='{0}'" ,oddNum );
            if ( SqlHelper . Exists ( strSql . ToString ( ) ) == false )
                //表示易助订单没有单头数据
                return -1;
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT RAB002,RAB003 FROM SGMRAB WHERE RAB001='{0}'" ,oddNum );
            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt == null || dt . Rows . Count < 1 )
                isOk = false;
            
            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL011 RAB001,DEL001 RAB003,DEL002 RAB004,DEL003 RAB020,DEL004 RAB005,SUM(DEL007*DEL008+DEL009) RAB007,SUM(DEL007) RAB980,SUM(DEL008) RAB981,DEL010 RAB961 FROM QIXDEL WHERE DEL011='{0}' GROUP BY DEL011,DEL001,DEL002,DEL004,DEL003,DEL010" ,oddNum );
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                //表示本单号在本程序没有单身数据
                return -2;
            
            List<string> strList = new List<string> ( );

            SiftingDataEntity . OrderWorkEntity model = new SiftingDataEntity . OrderWorkEntity ( );
            model . RAB001 = oddNum;
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                model . RAB003 = table . Rows [ i ] [ "RAB003" ] . ToString ( );
                if ( isOk )
                {
                    model . RAB002 = dt . Compute ( "MAX(RAB002)" ,null ) . ToString ( );
                    if ( strList . Contains ( model . RAB002 ) )
                    {
                        model . RAB002 = strList . Max ( );
                        model . RAB002 = ( Convert . ToInt32 ( model . RAB002 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                        strList . Add ( model . RAB002 );
                    }
                    else
                    {
                        strList . Add ( model . RAB002 );
                        model . RAB002 = ( Convert . ToInt32 ( model . RAB002 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                        strList . Add ( model . RAB002 );
                    }
                }
                else
                {
                    model . RAB002 = i . ToString ( ) . PadLeft ( 3 ,'0' );
                    if ( strList . Contains ( model . RAB002 ) )
                    {
                        model . RAB002 = strList . Max ( );
                        model . RAB002 = ( Convert . ToInt32 ( model . RAB002 ) + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                        strList . Add ( model . RAB002 );
                    }
                    else
                        strList . Add ( model . RAB002 );
                }
                model . RAB004 = table . Rows [ i ] [ "RAB004" ] . ToString ( );
                model . RAB005 = table . Rows [ i ] [ "RAB005" ] . ToString ( );
                model . RAB007 = string . IsNullOrEmpty ( table . Rows [ i ] [ "RAB007" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "RAB007" ] . ToString ( ) );
                model . RAB980 = string . IsNullOrEmpty ( table . Rows [ i ] [ "RAB980" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "RAB980" ] . ToString ( ) );
                model . RAB981 = string . IsNullOrEmpty ( table . Rows [ i ] [ "RAB981" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "RAB981" ] . ToString ( ) );
                model . RAB961 = table . Rows [ i ] [ "RAB961" ] . ToString ( );
                model . RAB020 = table . Rows [ i ] [ "RAB020" ] . ToString ( );
                
                if ( dt . Select ( "RAB003='" + model . RAB003 + "'" ) . Length < 1 )
                    AddOrder ( SQLString ,strSql ,model );
            }

            if ( SqlHelper . ExecuteSqlTran ( SQLString ) )
                return 1;
            else
                return 2;
        }

        void AddOrder ( Hashtable SQLString ,StringBuilder strSql ,SiftingDataEntity . OrderWorkEntity model )
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO SGMRAB (" );
            strSql . Append ( "RAB001,RAB002,RAB003,RAB004,RAB005,RAB007,RAB020,RAB980,RAB981,RAB961) " );
            strSql . Append ( "VALUES (" );
            strSql . Append ( "@RAB001,@RAB002,@RAB003,@RAB004,@RAB005,@RAB007,@RAB020,@RAB980,@RAB981,@RAB961) " );
            SqlParameter [ ] parameter = {
                new SqlParameter("@RAB001",SqlDbType.NVarChar),
                new SqlParameter("@RAB002",SqlDbType.NVarChar),
                new SqlParameter("@RAB003",SqlDbType.NVarChar),
                new SqlParameter("@RAB004",SqlDbType.NVarChar),
                new SqlParameter("@RAB005",SqlDbType.NVarChar),
                new SqlParameter("@RAB007",SqlDbType.Decimal),
                new SqlParameter("@RAB020",SqlDbType.NVarChar),
                new SqlParameter("@RAB980",SqlDbType.Decimal),
                new SqlParameter("@RAB981",SqlDbType.Decimal),
                new SqlParameter("@RAB961",SqlDbType.NVarChar)
            };
            parameter [ 0 ] . Value = model . RAB001;
            parameter [ 1 ] . Value = model . RAB002;
            parameter [ 2 ] . Value = model . RAB003;
            parameter [ 3 ] . Value = model . RAB004;
            parameter [ 4 ] . Value = model . RAB005;
            parameter [ 5 ] . Value = model . RAB007;
            parameter [ 6 ] . Value = model . RAB020;
            parameter [ 7 ] . Value = model . RAB980;
            parameter [ 8 ] . Value = model . RAB981;
            parameter [ 9 ] . Value = model . RAB961;
            SQLString . Add ( strSql ,parameter );
        }

        /// <summary>
        /// 生成领料单
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public int SavePick ( string oddNum )
        {
            ArrayList SQLString = new ArrayList ( );
            StringBuilder strSql = new StringBuilder ( );

            strSql . AppendFormat ( "SELECT COUNT(1) FROM JSKLIA WHERE LIA960='{0}'" ,oddNum );
            if ( SqlHelper . Exists ( strSql . ToString ( ) ) )
                //已经存在领料单,不允许写入
                return -2;
            
            SiftingDataEntity . OrderPickEntity _header = new SiftingDataEntity . OrderPickEntity ( );
            _header . LIA001 = getOddNumPick ( );
            _header . LIA004 = "C01";
            _header . LIA005 = "004";
            _header . LIA960 = oddNum;

            strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO JSKLIA (" );
            strSql . Append ( "LIA001,LIA003,LIA004,LIA005,LIA012,LIA960) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{3}','{1}','{4}','F','{2}')" ,_header . LIA001 ,_header . LIA004 ,_header . LIA960 ,getTime ( ) . ToString ( "yyyyMMdd" ) ,_header . LIA005 );
            SQLString . Add ( strSql . ToString ( ) );

            strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL001 LIB003,DEL002 LIB004,DEL004 LIB005,DEL006 LIB006,DEL005 LIB019,DEL007*DEL008+DEL009 LIB008,DEL007 LIB982,DEL008 LIB983,DEL009 LIB984 FROM QIXDEL WHERE DEL011='{0}'" ,oddNum );
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table == null || table . Rows . Count < 1 )
                //表示本地没有数据
                return -1;

            SiftingDataEntity . OrderPickBodyEntity _body = new SiftingDataEntity . OrderPickBodyEntity ( );
            for ( int i = 0 ; i < table . Rows . Count ; i++ )
            {
                _body . LIB001 = _header . LIA001;
                _body . LIB002 = ( i + 1 ) . ToString ( ) . PadLeft ( 3 ,'0' );
                _body . LIB003 = table . Rows [ i ] [ "LIB003" ] . ToString ( );
                _body . LIB004 = table . Rows [ i ] [ "LIB004" ] . ToString ( );
                _body . LIB005 = table . Rows [ i ] [ "LIB005" ] . ToString ( );
                _body . LIB006 = table . Rows [ i ] [ "LIB006" ] . ToString ( );
                _body . LIB019 = table . Rows [ i ] [ "LIB019" ] . ToString ( );
                _body . LIB984 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB984" ] . ToString ( ) );
                _body . LIB008 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB008" ] . ToString ( ) );
                _body . LIB982 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB982" ] . ToString ( ) );
                _body . LIB983 = string . IsNullOrEmpty ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) ) == true ? 0 : Convert . ToDecimal ( table . Rows [ i ] [ "LIB983" ] . ToString ( ) );

                AddPick ( SQLString ,strSql ,_body );
            }

            if ( SqlHelper . ExecuteSqlTran ( SQLString ) )
                return 1;
            else
                return 2;
        }

        string getOddNumPick ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT MAX(LIA001) LIA001 FROM JSKLIA WHERE LIA001 LIKE 'CK{0}%'" ,getTime ( ) . ToString ( "yyyyMMdd" ) );

            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt != null && dt . Rows . Count > 0 )
            {
                string oddNum = dt . Rows [ 0 ] [ "LIA001" ] . ToString ( );
                if ( string . IsNullOrEmpty ( oddNum ) )
                    return "CK" + getTime ( ) . ToString ( "yyyyMMdd" ) + "0001";
                else
                    return "CK" + ( Convert . ToInt64 ( oddNum . Substring ( 2 ,12 ) ) + 1 ) . ToString ( );
            }
            else
                return "CK" + getTime ( ) . ToString ( "yyyyMMdd" ) + "0001";
        }

        DateTime getTime ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT GETDATE() t;" );

            DataTable dt = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( dt != null && dt . Rows . Count < 1 )
                return string . IsNullOrEmpty ( dt . Rows [ 0 ] [ "t" ] . ToString ( ) ) == true ? DateTime . Now : Convert . ToDateTime ( dt . Rows [ 0 ] [ "t" ] . ToString ( ) );
            else
                return DateTime . Now;
        }

        void AddPick ( ArrayList SQLString,StringBuilder strSql,SiftingDataEntity.OrderPickBodyEntity model)
        {
            strSql = new StringBuilder ( );
            strSql . Append ( "INSERT INTO JSKLIB (" );
            strSql . Append ( "LIB001,LIB002,LIB003,LIB004,LIB005,LIB006,LIB008,LIB019,LIB982,LIB983,LIB984) " );
            strSql . Append ( "VALUES (" );
            strSql . AppendFormat ( "'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}') " ,model . LIB001 ,model . LIB002 ,model . LIB003 ,model . LIB004 ,model . LIB005 ,model . LIB006 ,model . LIB008 ,model . LIB019 ,model . LIB982 ,model . LIB983 ,model . LIB984 );
            //SqlParameter [ ] parameter = {
            //    new SqlParameter("@LIB001",SqlDbType.NVarChar),
            //    new SqlParameter("@LIB002",SqlDbType.NVarChar),
            //    new SqlParameter("@LIB003",SqlDbType.NVarChar),
            //    new SqlParameter("@LIB004",SqlDbType.NVarChar),
            //    new SqlParameter("@LIB005",SqlDbType.NVarChar),
            //    new SqlParameter("@LIB006",SqlDbType.NVarChar),
            //    new SqlParameter("@LIB008",SqlDbType.Decimal),
            //    new SqlParameter("@LIB019",SqlDbType.NVarChar),
            //    new SqlParameter("@LIB982",SqlDbType.Decimal),
            //    new SqlParameter("@LIB983",SqlDbType.Decimal)
            //};
            //parameter [ 0 ] . Value = model . LIB001;
            //parameter [ 1 ] . Value = model . LIB002;
            //parameter [ 2 ] . Value = model . LIB003;
            //parameter [ 3 ] . Value = model . LIB004;
            //parameter [ 4 ] . Value = model . LIB005;
            //parameter [ 5 ] . Value = model . LIB006;
            //parameter [ 6 ] . Value = model . LIB008;
            //parameter [ 7 ] . Value = model . LIB019;
            //parameter [ 8 ] . Value = model . LIB982;
            //parameter [ 9 ] . Value = model . LIB983;
            SQLString . Add ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintOne ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DISTINCT IBB001,IBB003,IBB004,IBB041,IBB961,CONVERT(FLOAT,IBB006) IBB006,CONVERT(FLOAT,IBB980) IBB980,CONVERT(FLOAT,IBB981) IBB981 FROM DCSIBB A INNER JOIN QIXDEL B ON A.IBB001=B.DEL017 WHERE DEL015='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 获取打印列表
        /// </summary>
        /// <param name="oddNum"></param>
        /// <returns></returns>
        public DataTable getTablePrintTwo ( string oddNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT DEL001,DEL002,DEL003,DEL004,DEL005,DEL006,CONVERT(FLOAT,DEL007) DEL007,CONVERT(FLOAT,DEL008) DEL008,CONVERT(FLOAT,DEL009) DEL009,DEL010,CONVERT(FLOAT,DEL007*DEL008+DEL009) U0 FROM QIXDEL WHERE DEL015 ='{0}'" ,oddNum );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 是否LIA961='T'  如果是false  则不可以反审核
        /// </summary>
        /// <param name="orderNum"></param>
        /// <param name="piNum"></param>
        /// <returns></returns>
        public bool Exists ( string orderNum,string piNum )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . AppendFormat ( "SELECT LIA961 FROM JSKLIA WHERE LIA960='{0}' AND LIA964='{1}'" ,orderNum ,piNum );
            
            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count > 0 )
            {
                string state = string . Empty;
                state = table . Rows [ 0 ] [ "LIA961" ] . ToString ( );
                if ( state == string . Empty )
                    return true;
                else if ( state . Equals ( "T" ) )
                    return false;
                else
                    return true;
            }
            else
                return true;
        }

        /// <summary>
        /// 审核  反审核
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Examine ( SiftingDataEntity . MainEntity model )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "UPDATE QIXDEL SET DEL014=@DEL014 WHERE DEL015=@DEL015" );
            SqlParameter [ ] parameter = {
                new SqlParameter("@DEL014",SqlDbType.Bit),
                new SqlParameter("@DEL015",SqlDbType.NVarChar)
            };
            parameter [ 0 ] . Value = model . DEL014;
            parameter [ 1 ] . Value = model . DEL015;

            int rows = SqlHelper . ExecuteNonQuery ( strSql . ToString ( ) ,parameter );
            return rows > 0 ? true : false;
        }

        /// <summary>
        /// 获取单号
        /// </summary>
        /// <returns></returns>
        public DataTable getOddTable ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT DISTINCT DEL015,DEL017,DEL016 FROM QIXDEL ORDER BY DEL015 DESC" );

            return SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
        }

        /// <summary>
        /// 得到单号
        /// </summary>
        /// <returns></returns>
        public string getOdd ( )
        {
            StringBuilder strSql = new StringBuilder ( );
            strSql . Append ( "SELECT MAX(DEL015) DEL015 FROM QIXDEL" );

            DataTable table = SqlHelper . ExecuteDataTable ( strSql . ToString ( ) );
            if ( table != null && table . Rows . Count < 1 )
                return DateTime . Now . ToString ( "yyyyMMdd" ) + "001";
            else
            {
                string code = table . Rows [ 0 ] [ "DEL015" ] . ToString ( );
                if ( string . IsNullOrEmpty ( code ) )
                    return DateTime . Now . ToString ( "yyyyMMdd" ) + "001";
                else
                {
                    if ( code . Substring ( 0 ,8 ) . Equals ( DateTime . Now . ToString ( "yyyyMMdd" ) ) )
                        return ( Convert . ToInt64 ( code ) + 1 ) . ToString ( );
                    else
                        return DateTime . Now . ToString ( "yyyyMMdd" ) + "001";
                }
            }
        }

    }
}
