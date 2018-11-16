using System;
using System . Data . SqlClient;
using System . Data;
using System . Collections;

namespace StudentMgr
{
    public static class SqlHelper
    {
        //public static readonly string connstr =
        //    ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        static bool result=true;
        static int resultCount=0;
        static object resultObj=null;
        private static string connstr = string.Empty;
        static string param = string . Empty;

        //Sql Server数据库连接字符串
        public static string Connstr
        {
            get
            {
                connstr = string . Empty;
                if ( string . IsNullOrEmpty ( connstr ) )
                {
                    string host = Utility . IniUtil . ReadIniValue ( AppDomain . CurrentDomain . SetupInformation . ApplicationBase + "config.ini" ,"DB" ,"host" );
                    string dbname = Utility . IniUtil . ReadIniValue ( AppDomain . CurrentDomain . SetupInformation . ApplicationBase + "config.ini" ,"DB" ,"dbname" );
                    string user = Utility . IniUtil . ReadIniValue ( AppDomain . CurrentDomain . SetupInformation . ApplicationBase + "config.ini" ,"DB" ,"user" );
                    string password = Utility . IniUtil . ReadIniValue ( AppDomain . CurrentDomain . SetupInformation . ApplicationBase + "config.ini" ,"DB" ,"password" );
                    connstr = string . Format ( "Data Source={0};Initial Catalog={1};User Id={2};Password={3};Connect Timeout=120" ,host ,dbname ,user ,password );
                }
                
                

                return connstr;
            }
        }

        /// <summary>
        /// 连接并打开数据库
        /// </summary>
        /// <returns></returns>
        public static SqlConnection OpenConnection( )
        {
            SqlConnection conn = new SqlConnection( Connstr );
            conn.Open( );
            return conn;
        }

        /// <summary>
        /// 返回受影响的行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery( string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteNonQuery( conn, cmdText, parameters );
            }
        }

        /// <summary>
        /// 执行没有参数的SQL语句  返回受影响的行数
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery ( string cmdText )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteNonQuery( conn ,cmdText );
            }
        }

        /// <summary>
        ///  执行没有参数的SQL语句  返回是否成功
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static bool ExecuteNonQueryBool ( string cmdText )
        {
            try
            {
                using ( SqlConnection conn = new SqlConnection ( Connstr ) )
                {
                    conn . Open ( );
                    int row = ExecuteNonQuery ( conn ,cmdText );
                    if ( row > 0 )
                    {
                        result = true;
                    }
                    else
                        result= false;
                }
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                Utility . LogHelper . WriteLog ( cmdText );
                result = false;
                throw new Exception ( ex . Message );
            }

            return result;
        }

        public static object ExecuteScalar( string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteScalar( conn, cmdText, parameters );
            }
        }

        /// <summary>
        /// 返回一个DataTable实例
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable( string cmdText,
            params SqlParameter[] parameters )
        {
            using (SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteDataTable( conn, cmdText, parameters );
            }
        }
        public static DataTable ExecuteDataTable (string conS, string cmdText ,
            params SqlParameter [ ] parameters )
        {
            using ( SqlConnection conn = new SqlConnection ( conS ) )
            {
                conn . Open ( );
                return ExecuteDataTable ( conn ,cmdText ,parameters );
            }
        }
        /// <summary>
        /// 返回一个DataSet
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static DataSet Query ( string SQLString ,params object[] parameter )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                conn.Open( );
                return ExecuteDataSet( conn ,SQLString ,parameter );
            }
        }

        public static int ExecuteNonQuery( SqlConnection conn, string cmdText,
           params SqlParameter[] parameters )
        {
            try
            {
                using ( SqlCommand cmd = conn . CreateCommand ( ) )
                {
                    cmd . CommandText = cmdText;
                    cmd . Parameters . AddRange ( parameters );
                    resultCount= cmd . ExecuteNonQuery ( );
                }
            }
            catch ( Exception ex )
            {
                resultCount = 0;
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                Utility . LogHelper . WriteLog ( cmdText );
                throw new Exception ( ex . Message );
            }

            return resultCount;
        }

        public static int ExecuteNonQuery ( SqlConnection conn ,string cmdText )
        {
            try
            {
                using ( SqlCommand cmd = conn . CreateCommand ( ) )
                {
                    cmd . CommandText = cmdText;
                    resultCount= cmd . ExecuteNonQuery ( );
                }
            }
            catch ( Exception ex )
            {
                resultCount = 0;
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace ); 
                Utility . LogHelper . WriteLog ( cmdText );
                throw new Exception ( ex . Message );
            }

            return resultCount;
        }

        public static object ExecuteScalar ( SqlConnection conn ,string cmdText ,
            params SqlParameter [ ] parameters )
        {
            try
            {
                using ( SqlCommand cmd = conn . CreateCommand ( ) )
                {
                    cmd . CommandText = cmdText;
                    cmd . Parameters . AddRange ( parameters );
                    resultObj = cmd . ExecuteScalar ( );
                }
            }
            catch ( Exception ex )
            {
                resultObj = 0;
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                Utility . LogHelper . WriteLog ( cmdText );
                throw new Exception ( ex . Message );
            }

            return resultObj;
        }

        public static DataTable ExecuteDataTable ( SqlConnection conn ,string cmdText ,
            params SqlParameter [ ] parameters )
        {
            try
            {
                using ( SqlCommand cmd = conn . CreateCommand ( ) )
                {
                    cmd . CommandText = cmdText;
                    cmd . Parameters . AddRange ( parameters );
                    using ( SqlDataAdapter adapter = new SqlDataAdapter ( cmd ) )
                    {
                        DataTable dt = new DataTable ( );
                        adapter . Fill ( dt );
                        //组装日计划排计划记录日志
                        //LogHelperToSql . SaveLog ( cmdText ,parameters );
                        return dt;
                    }
                }
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                Utility . LogHelper . WriteLog ( cmdText );
                throw new Exception ( ex . Message );
            }
        }



        public static DataSet ExecuteDataSet ( SqlConnection conn ,string SQLString ,params object[] parameter )
        {
            using ( SqlCommand cmd = conn.CreateCommand( ) )
            {
                PrepareCommand( cmd ,conn ,null ,SQLString ,parameter );
                using ( SqlDataAdapter da = new SqlDataAdapter( cmd ) )
                {
                    DataSet ds = new DataSet( );
                    try
                    {
                        da.Fill( ds ,"ds" );
                        cmd.Parameters.Clear( );
                    }
                    catch (SqlException ex) {
                        Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                        throw new Exception ( ex . Message );
                    }
                    finally {
                        cmd.Dispose( );
                        conn.Close( );
                    }

                    return ds;
                }
            }
        }

        /// <summary>
        /// 不用Sql语句更新数据到数据库
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="table"></param>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool UpdateTable (DataTable table,string sql,params object[] parameter)
        {
            bool result = false;
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                DataSet ds = new DataSet( );

                try
                {
                    table.TableName = "R_PQZB";
                    ds.Tables.Add( table.Copy( ) );
                    SqlDataAdapter sda = new SqlDataAdapter( );
                    sda.SelectCommand = new SqlCommand( sql ,conn );
                    SqlCommandBuilder cd = new SqlCommandBuilder( sda );
                    conn.Open( );
                    sda.FillSchema( ds ,SchemaType.Mapped );
                    sda.Fill( ds );
                    table = ds.Tables[0];
                    int row = sda.Update( table );
                    table.AcceptChanges( );
                    result = true;
                }
                catch ( SqlException ex )
                {
                    Utility . LogHelper . WriteLog ( "批量保存" ,ex );
                    throw new Exception ( ex . Message + "\n\r" + ex . StackTrace );
                }
                finally
                {
                    conn.Close( );
                }
            }
            return result;
        }


        #region 执行存储过程

        private static string _proceName; //存储过程名

        public static void StoreProcedure ( string sprocName )
        {
            _proceName = sprocName;
        }

        /// <summary>
        /// 执行存储过程  不返回值
        /// </summary>
        /// <param name="sprocName">存储过程名</param>
        /// <param name="parameters">参数</param>
        public static void ExecuteNoStore ( params SqlParameter[] parameters )
        {
            using (SqlConnection conn=new SqlConnection( Connstr ) )
            {
                SqlCommand cmd = new SqlCommand( _proceName ,conn );
                cmd.CommandType = CommandType.StoredProcedure;
                AddInParaValues( cmd ,parameters );
                conn.Open( );
                cmd.ExecuteNonQuery( );
                conn.Close( );
            }
        }

        /// <summary>
        /// 执行存储过程  返回Table
        /// </summary>
        /// <param name="conS"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteNoStore ( string conS ,params SqlParameter [ ] parameters )
        {
            try
            {
               DataTable dt = new DataTable ( );
                try
                {
                    using ( SqlConnection conn = new SqlConnection ( conS ) )
                    {
                        SqlCommand cmd = new SqlCommand ( _proceName ,conn );
                        cmd . CommandType = CommandType . StoredProcedure;

                        for ( int i = 0 ; i < parameters . Length ; i++ )
                        {
                            cmd . Parameters . Add ( parameters [ i ] );
                        }

                        conn . Open ( );
                        using ( SqlDataAdapter sda = new SqlDataAdapter ( ) )
                        {
                            sda . SelectCommand = cmd;
                            sda . Fill ( dt );                        
                        }
                        conn . Close ( );
                        return dt;
                    }
                }
                catch ( Exception ex)
                {
                    dt = null;
                    Utility . LogHelper . WriteLog ( ex . Message + "\r\n" + ex . StackTrace );
                    throw new Exception ( ex . Message );
                }
            }
            catch (Exception ex)
            {
                Utility . LogHelper . WriteLog ( ex . Message + "\r\n" + ex . StackTrace );
                throw new Exception ( ex . Message );
            }
        }
        public static DataTable ExecuteNoStoreTable ( params SqlParameter [ ] parameters )
        {
            try
            {
                DataTable dt = new DataTable ( );
                try
                {
                    using ( SqlConnection conn = new SqlConnection ( Connstr ) )
                    {
                        SqlCommand cmd = new SqlCommand ( _proceName ,conn );
                        cmd . CommandType = CommandType . StoredProcedure;

                        for ( int i = 0 ; i < parameters . Length ; i++ )
                        {
                            cmd . Parameters . Add ( parameters [ i ] );
                        }

                        conn . Open ( );
                        using ( SqlDataAdapter sda = new SqlDataAdapter ( ) )
                        {
                            sda . SelectCommand = cmd;
                            sda . Fill ( dt );
                        }
                        conn . Close ( );
                        return dt;
                    }
                }
                catch ( Exception ex )
                {
                    dt = null;
                    Utility . LogHelper . WriteLog ( ex . Message + "\r\n" + ex . StackTrace );
                    throw new Exception ( ex . Message );
                }
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteLog ( ex . Message + "\r\n" + ex . StackTrace );
                throw new Exception ( ex . Message );
            }
        }

        /// <summary>
        /// 执行存储过程  返回一个表
        /// </summary>
        /// <param name="sprocName">存储过程名称</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTableToStore ( params SqlParameter[] parameters )
        {
            SqlCommand cmd = new SqlCommand( _proceName ,new SqlConnection( Connstr ) );
            cmd.CommandType = CommandType.StoredProcedure;
            AddInParaValues( cmd ,parameters );
            SqlDataAdapter ada = new SqlDataAdapter( cmd );
            DataTable dt = new DataTable( );
            ada.Fill( dt );
            return dt;
        }

        /// <summary>
        /// 执行存储过程  返回一个表
        /// </summary>
        /// <param name="sprocName">存储过程名称</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTableToStore (string conS, params SqlParameter [ ] parameters )
        {
            try
            {
                SqlCommand cmd = new SqlCommand ( _proceName ,new SqlConnection ( conS ) );
                cmd . CommandType = CommandType . StoredProcedure;
                AddInParaValues ( cmd ,conS ,parameters );
                SqlDataAdapter ada = new SqlDataAdapter ( cmd );
                DataTable dt = new DataTable ( );
                ada . Fill ( dt );
                return dt;
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteLog ( ex . Message + ex . StackTrace );
                return null;
            }
        }

        /// <summary>
        /// 执行存储过程，返回SqlDataReader对象   在SqlDataReader对象关闭的同时，数据库连接自动关闭
        /// </summary>
        /// <param name="sprocName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReaderStore ( params SqlParameter[] parameters )
        {
            SqlConnection conn = new SqlConnection( Connstr );
            SqlCommand cmd = new SqlCommand( _proceName ,conn );
            cmd.CommandType = CommandType.StoredProcedure;
            AddInParaValues( cmd ,parameters );
            conn.Open( );
            return cmd.ExecuteReader( CommandBehavior.CloseConnection );
        }

        /// <summary>
        /// 获取存储过程的参数列表
        /// </summary>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        private static ArrayList GetParameters ( )
        {
            SqlCommand cmd = new SqlCommand( "dbo.sp_sproc_columns_90" ,new SqlConnection( Connstr ) );
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue( "@procedure_name" ,_proceName );
            SqlDataAdapter sda = new SqlDataAdapter( cmd );
            DataTable dt = new DataTable( );
            sda.Fill( dt );
            ArrayList al = new ArrayList( );
            for ( int i = 0 ; i < dt.Rows.Count ; i++ )
            {
                al.Add( dt.Rows[i][3].ToString( ) );
            }
            return al;
        }

        /// <summary>
        /// 获取存储过程的参数列表
        /// </summary>
        /// <param name="sprocName"></param>
        /// <returns></returns>
        private static ArrayList GetParameters (  string conS )
        {
            SqlCommand cmd = new SqlCommand ( "dbo.sp_sproc_columns_90" ,new SqlConnection ( conS ) );
            cmd . CommandType = CommandType . StoredProcedure;
            cmd . Parameters . AddWithValue ( "@procedure_name" ,_proceName );
            SqlDataAdapter sda = new SqlDataAdapter ( cmd );
            DataTable dt = new DataTable ( );
            sda . Fill ( dt );
            ArrayList al = new ArrayList ( );
            for ( int i = 0 ; i < dt . Rows . Count ; i++ )
            {
                al . Add ( dt . Rows [ i ] [ 3 ] . ToString ( ) );
            }
            return al;
        }

        /// <summary>
        /// 为 SqlCommand 添加参数及赋值
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        private static void AddInParaValues ( SqlCommand cmd ,params SqlParameter[] parameters )
        {
            cmd.Parameters.Add( new SqlParameter( "@RETURN_VALUE" ,SqlDbType.Int ) );
            cmd.Parameters["@RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
            if ( parameters != null )
            {
                ArrayList al = GetParameters( );
                for ( int i = 0 ; i < parameters.Length ; i++ )
                {
                    string str = al [ i + 1 ] . ToString ( );
                    SqlParameter sr = parameters [ i ];
                    cmd .Parameters.AddWithValue( al[i + 1].ToString( ) ,parameters[i] );
                }
            }
        }

        /// <summary>
        /// 为 SqlCommand 添加参数及赋值
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameters"></param>
        private static void AddInParaValues ( SqlCommand cmd,string conS ,params SqlParameter [ ] parameters )
        {
            //cmd . Parameters . Add ( new SqlParameter ( "@RETURN_VALUE" ,SqlDbType . Int ) );
            //cmd . Parameters [ "@RETURN_VALUE" ] . Direction = ParameterDirection . ReturnValue;
            if ( parameters != null )
            {
                ArrayList al = GetParameters ( conS );
                for ( int i = 0 ; i < parameters . Length ; i++ )
                {
                    string str = al [ i + 1 ] . ToString ( );
                    SqlParameter sr = parameters [ i ];
                    cmd . Parameters . AddWithValue ( al [ i + 1 ] . ToString ( ) ,parameters [ i ] );
                }
            }
        }

        #endregion


        public static void PrepareCommand ( SqlCommand cmd ,SqlConnection conn ,SqlTransaction trans ,string cmdText ,object [ ] parameter )
        {
            param = string . Empty;
            if ( conn . State != ConnectionState . Open )
                conn . Open ( );
            cmd . Connection = conn;
            cmd . CommandText = cmdText;
            if ( trans != null )
                cmd . Transaction = trans;
            cmd . CommandType = CommandType . Text;
            if ( parameter != null )
            {
                foreach ( SqlParameter para in parameter )
                {
                    if ( ( para . Direction == ParameterDirection . InputOutput || para . Direction == ParameterDirection . Input ) && ( para . Value == null ) )
                    {
                        para . Value = DBNull . Value;
                    }
                    cmd . Parameters . Add ( para );

                    if ( param == string . Empty )
                        param = " [ " + para . ToString ( ) + ":" + para . Value + " ] ";
                    else
                        param = param + " [ " + para . ToString ( ) + ":" + para . Value + " ] ";

                    Utility . LogHelper . WriteLog ( para . ToString ( ) + " " + para . Value );
                }
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）
        /// </summary>
        /// <param name="SQLSting">计算查询结果语句</param>
        /// <param name="parameter"></param>
        /// <returns>查询结果（object）</returns>
        public static object GetSingle ( string SQLSting ,params object[] parameter )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                using ( SqlCommand cmd = new SqlCommand( ) )
                {
                    try
                    {
                        
                        PrepareCommand( cmd ,conn ,null ,SQLSting ,parameter );
                        object obj = cmd.ExecuteScalar( );
                        if ( ( object.Equals( obj ,null ) ) || ( object.Equals( obj ,System.DBNull.Value ) ) )
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch( SqlException e )
                    {
                        Utility . LogHelper . WriteLog ( e . Message + "\n\r" + e . StackTrace );
                        Utility . LogHelper . WriteLog ( SQLSting );
                        throw new Exception ( e . Message );
                    }
                    finally {
                        cmd.Dispose( );
                        conn.Close( );
                    }
                }    
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static bool Exists ( string sql ,params object[] parameter )
        {
            try
            {
                object result = GetSingle ( sql ,parameter );
                if ( result == null )
                    return false;
                long count = 0;
                long . TryParse ( result . ToString ( ) ,out count );
                return count > 0 ? true : false;
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                Utility . LogHelper . WriteLog ( sql );
                throw new Exception ( ex . Message );
            }
        }

        /// <summary>
        /// 返回结果  是否存在
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int returnCount ( string sql ,params object [ ] parameter )
        {
            try
            {
                object result = GetSingle ( sql ,parameter );
                if ( result == null )
                    return 0;
                //int count = 0;
                //int . TryParse ( result . ToString ( ) ,out count );
                //return count;
                return 1;
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                Utility . LogHelper . WriteLog ( sql );
                throw new Exception ( ex . Message );
            }
        }

        /// <summary>
        /// 返回总记录数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int returnSumCount ( string sql ,params object [ ] parameter )
        {
            try
            {
                object result = GetSingle ( sql ,parameter );
                if ( result == null )
                    return 0;
                int count = 0;
                int . TryParse ( result . ToString ( ) ,out count );
                return count;
            }
            catch ( Exception ex )
            {
                Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                Utility . LogHelper . WriteLog ( sql );
                throw new Exception ( ex . Message );
            }
        }

        /// <summary>
        /// 执行SQL语句，返回自增列值
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static int ExecuteSqlReturnId ( string SQLString ,params object[] parameter )
        {
            using ( SqlConnection conn = new SqlConnection( Connstr ) )
            {
                using ( SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand( cmd ,conn ,null ,SQLString ,parameter );
                        object obj = cmd.ExecuteScalar( );

                        cmd.Parameters.Clear( );
                        if ( obj == null )
                            return 0;
                        else
                        {
                            return Convert . ToInt32 ( obj );
                        }
                    }
                    catch (Exception E)
                    {
                        Utility . LogHelper . WriteLog ( E . Message + "\n\r" + E . StackTrace );
                        Utility . LogHelper . WriteLog ( SQLString );
                        throw new Exception ( E . Message );
                    }
                    finally {
                        cmd.Dispose( );
                        conn.Close( );
                    }
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList"></param>
        public static bool ExecuteSqlTran ( ArrayList SQLStringList )
        {
            using (SqlConnection conn=new SqlConnection( Connstr ) )
            {
                conn.Open( );
                SqlCommand cmd = new SqlCommand( );
                cmd.Connection = conn;
                SqlTransaction tran = conn.BeginTransaction( );
                cmd.Transaction = tran;
                string strsql = string . Empty;
                try
                {
                   
                    for ( int i = 0 ; i < SQLStringList.Count ; i++ )
                    {
                        strsql = SQLStringList[i].ToString( );
                        if ( strsql.Trim( ).Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.ExecuteNonQuery( );
                        }
                    }
                    tran.Commit( );
                    return true;
                }
                catch (Exception ex)
                {
                    Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex .StackTrace );
                    Utility . LogHelper . WriteLog ( strsql );
                    tran . Rollback ( );
                    throw new Exception ( ex . Message );
                }
                finally
                {
                    cmd.Dispose( );
                    conn.Close( );
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句  实现数据库事务
        /// </summary>
        /// <param name="SQLStringList"></param>
        /// <returns>此事务的实现  可以有parameter[]参数列表</returns>
        public static bool ExecuteSqlTran ( Hashtable SQLStringList )
        {
            using ( SqlConnection conn = new SqlConnection ( Connstr ) )
            {
                bool result = false;
                conn . Open ( );
                using ( SqlTransaction trans = conn . BeginTransaction ( ) )
                {
                    SqlCommand cmd = new SqlCommand ( );
                    string cmdText = string . Empty;
                    try
                    {
                        //循环
                        foreach ( DictionaryEntry myDE in SQLStringList )
                        {
                            cmdText = myDE . Key . ToString ( );
                            Object [ ] cmdParms = ( Object [ ] ) myDE . Value;
                            PrepareCommand ( cmd ,conn ,trans ,cmdText ,cmdParms );
                            int val = cmd . ExecuteNonQuery ( );
                            cmd . Parameters . Clear ( );
                        }

                        trans . Commit ( );
                        result = true;
                    }
                    catch ( Exception ex )
                    {
                        trans . Rollback ( );
                        Utility . LogHelper . WriteLog ( ex . Message + "\n\r" + ex . StackTrace );
                        Utility . LogHelper . WriteLog ( cmdText );
                        throw new Exception ( ex . Message );
                    }
                    finally
                    {
                        cmd . Dispose ( );
                        conn . Close ( );
                    }
                }
                return result;
            }
        }

        public static object ToDBValue(this object value)
        {
            return value == null ? DBNull.Value : value;
        }

        public static object FromDBValue(this object dbValue)
        {
            return dbValue == DBNull.Value ? null : dbValue;
        }
    }
}
