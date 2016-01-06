using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;

namespace ManageFcmsConn
{
    /// <summary>
    /// 数据库的通用访问代码
    /// 此类为抽象类，不允许实例化，在应用时直接调用即可
    /// </summary>
    public abstract class SqlHelper
    {
        //获取数据库连接字符串，其属于静态变量且只读，项目中所有文档可以直接使用，但不能修改
        public static readonly string ConnectionStringLocal = StringHelper.DesDecrypt(ConfigurationManager.AppSettings["FcmsConn"], "87654321");
        // 哈希表用来存储缓存的参数信息，哈希表可以存储任意类型的参数。
        private static readonly Hashtable ParmCache = Hashtable.Synchronized(new Hashtable());

        ///  <summary>
        /// 执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        ///  使用参数数组形式提供参数列表
        ///  </summary>
        ///  <param name="connectionString">一个有效的数据库连接字符串</param>
        ///  <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        ///  <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值0:失败 1:成功</returns>
        public static int ExecuteNonQueryVal(string connectionString, CommandType cmdType, string cmdText,
                                             params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    cmd.CommandTimeout = 0;
                    //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    var returnParam = new SqlParameter("returnVal", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                    cmd.Parameters.Add(returnParam);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return Convert.ToInt32(returnParam.Value);
                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return 0;
            }
        }


        ///  <summary>
        /// 执行一个不需要返回值的SqlCommand命令，通过指定专用的连接字符串。
        ///  使用参数数组形式提供参数列表
        ///  </summary>
        ///  <param name="connectionString">一个有效的数据库连接字符串</param>
        ///  <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        ///  <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText,
                                          params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    //通过PrePareCommand方法将参数逐个加入到SqlCommand的参数集合中
                    cmd.CommandTimeout = 0;
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    int val = cmd.ExecuteNonQuery();
                    return val;
                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return 0;
            }
        }

        ///  <summary>
        /// 执行一条不返回结果的SqlCommand，通过一个已经存在的数据库连接
        ///  使用参数数组提供参数
        ///  </summary>
        ///  <param name="connection">一个现有的数据库连接</param>
        ///  <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        ///  <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText,
                                          params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand { CommandTimeout = 0 };
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一条不返回结果的SqlCommand，通过一个已经存在的数据库事物处理
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个存在的 sql 事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个数值表示此SqlCommand命令执行后影响的行数</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText,
                                          params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand { CommandTimeout = 0 };
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// 执行一条返回结果集的SqlCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例： 
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个包含结果的SqlDataReader</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText,
                                             params SqlParameter[] commandParameters)
        {
            try
            {
                var cmd = new SqlCommand();
                using (var conn = new SqlConnection(connectionString))
                {
                    cmd.CommandTimeout = 0;
                    // 在这里使用try/catch处理是因为如果方法出现异常，则SqlDataReader就不存在，
                    //CommandBehavior.CloseConnection的语句就不会执行，触发的异常由catch捕获。
                    //关闭数据库连接，并通过throw再次引发捕捉到的异常。
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    var sda = new SqlDataAdapter(cmd);
                    var ds = new DataSet();
                    sda.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds;
                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return null;
            }

        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的SqlCommand命令，通过专用的连接字符串。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例： 
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    cmd.CommandTimeout = 0;
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    if (val == DBNull.Value)
                    {
                        val = null;
                    }
                    cmd.Parameters.Clear();
                    return val;
                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(ex);
                return null;
            }
        }

        /// <summary>
        /// 执行存储过程返回输出参数的值(SqlDbType.Decimal类型)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="outPutParameter">输出参数</param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static Decimal ExecuteNonQueryOutPut(string connectionString, CommandType cmdType, string cmdText, string outPutParameter, params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    cmd.CommandTimeout = 0;
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    var returnParam = new SqlParameter(outPutParameter, SqlDbType.Decimal)
                        {
                            Direction = ParameterDirection.Output,
                            Precision = 18,
                            Scale = 2//SqlDbType.Decimal此处不设置位数和精度 小于1的小数会返回0
                        };
                    cmd.Parameters.Add(returnParam);
                    cmd.ExecuteNonQuery();
                    var obj = cmd.Parameters[outPutParameter].Value;
                    cmd.Parameters.Clear();
                    return ConvertHelper.ToDecimal(obj.ToString());
                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return 0;
            }
        }

        /// <summary>
        /// 执行一条返回第一条记录第一列的SqlCommand命令，通过已经存在的数据库连接。
        /// 使用参数数组提供参数
        /// </summary>
        /// <remarks>
        /// 使用示例：
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">一个已经存在的数据库连接</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>返回一个object类型的数据，可以通过 Convert.To{Type}方法转换类型</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText,
                                           params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand { CommandTimeout = 0 };
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// 为执行命令准备参数
        /// </summary>
        /// <param name="cmd">SqlCommand 命令</param>
        /// <param name="conn">已经存在的数据库连接</param>
        /// <param name="trans">数据库事物处理</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">Command text，T-SQL语句 例如 Select * from Products</param>
        /// <param name="cmdParms">返回带参数的命令</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType,
                                           string cmdText, IEnumerable<SqlParameter> cmdParms)
        {
            //判断数据库连接状态
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandTimeout = 0;
            //判断是否需要事物处理
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (var parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlCommand cmd, CommandType type, string sql, SqlParameter[] paras)
        {
            cmd.CommandTimeout = 0;
            cmd.CommandType = type;
            cmd.CommandText = sql;
            cmd.Parameters.Clear();
            if (paras != null)
            {
                foreach (var para in paras)
                {
                    cmd.Parameters.Add(para);
                }
            }
            return cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="type"></param>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlCommand cmd, string sql)
        {
            return ExecuteNonQuery(cmd, CommandType.Text, sql, null);
        }
        /// <summary>
        /// 执行sql返回SqlDataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText,
                                                  params SqlParameter[] commandParameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand command = new SqlCommand(cmdText, connection);

                command.CommandTimeout = 0;
                command.CommandType = cmdType;
                if (commandParameters != null)
                {
                    foreach (SqlParameter parameter in commandParameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                }
                connection.Open();
                SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Dispose();
                return dr;
            }
            catch (Exception e)
            {
                Log4NetHelper.WriteError(e);
                return null;
            }
        }

        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText)
        {
            return ExecuteReader(connectionString, cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, string cmdText)
        {
            return ExecuteReader(connectionString, CommandType.Text, cmdText, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">一个有效的数据库连接字符串</param>
        /// <param name="cmdType">SqlCommand命令类型 (存储过程， T-SQL语句， 等等。)</param>
        /// <param name="cmdText">存储过程的名字或者 T-SQL 语句</param>
        /// <param name="commandParameters">以数组形式提供SqlCommand命令中用到的参数列表</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReaderPre(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            var conn = new SqlConnection(connectionString);
            try
            {
                cmd.CommandTimeout = 0;
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                var rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception exception)
            {
                Log4NetHelper.WriteError(exception);
                cmd.Dispose();
                conn.Close();
                return null;
            }
        }

        /// <summary>
        /// 处理分页检索存储过程(SQL2005)
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="fields">需要查询的字段</param>
        /// <param name="tables">表名</param>
        /// <param name="filters">sql条件</param>
        /// <param name="sortStr">排序字符串</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecutePageForProc(string connectionString, string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        {
            var dataTable = new DataTable();
            var conn = new SqlConnection(connectionString);
            var pFields = new SqlParameter("@strFields", SqlDbType.NVarChar, 2000);
            var pTables = new SqlParameter("@strTableName", SqlDbType.NVarChar, 2000);
            var pFilters = new SqlParameter("@strWhere", SqlDbType.NVarChar, 4000);
            var pSortStr = new SqlParameter("@strOrderBy", SqlDbType.NVarChar, 200);
            var pCurrentPage = new SqlParameter("@CurrentPage", SqlDbType.Int);
            var pPageSize = new SqlParameter("@PageSize", SqlDbType.Int);
            var pTotal = new SqlParameter("@RecordCount", SqlDbType.Int);
            var pTotaPagel = new SqlParameter("@PageCount", SqlDbType.Int);

            using (var dAdapter = new SqlDataAdapter())
            {
                var sqlCommand = new SqlCommand
                    {
                        Connection = conn,
                        CommandTimeout = 0,
                        CommandText = "Proc_PagerProc",
                        CommandType = CommandType.StoredProcedure
                    };
                //参数赋值
                pFields.Value = fields;
                pTables.Value = tables;
                pFilters.Value = filters;
                pSortStr.Value = sortStr;
                pCurrentPage.Value = currentPage;
                pPageSize.Value = pageSize;
                pTotal.Direction = ParameterDirection.Output;
                pTotaPagel.Direction = ParameterDirection.Output;
                //添加参数
                sqlCommand.Parameters.Clear();
                sqlCommand.Parameters.Add(pFields);
                sqlCommand.Parameters.Add(pTables);
                sqlCommand.Parameters.Add(pFilters);
                sqlCommand.Parameters.Add(pSortStr);
                sqlCommand.Parameters.Add(pCurrentPage);
                sqlCommand.Parameters.Add(pPageSize);
                sqlCommand.Parameters.Add(pTotal);
                sqlCommand.Parameters.Add(pTotaPagel);
                try
                {
                    conn.Open();
                    dAdapter.SelectCommand = sqlCommand;
                    dAdapter.Fill(dataTable);
                    total = Convert.ToInt32(pTotal.Value);
                    totalPage = Convert.ToInt32(pTotaPagel.Value);
                }
                catch (Exception e)
                {
                    total = 0;
                    totalPage = 0;
                    Log4NetHelper.WriteError(e);
                }
                finally
                {
                    conn.Close();
                }
            }
            return dataTable;
        }
    }
}
