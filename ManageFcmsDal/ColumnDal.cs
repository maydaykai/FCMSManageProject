using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public partial class ColumnDal
    {
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(ColumnModel model, string rightGroup)
        {
            SqlParameter[] parameters = {           
                        new SqlParameter("@Description", SqlDbType.NVarChar,200){Value=model.Description} ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,50){Value=model.Name} ,            
                        new SqlParameter("@LinkUrl", SqlDbType.NVarChar,200){Value=model.LinkUrl} ,                     
                        new SqlParameter("@ICon", SqlDbType.NVarChar,100){Value=model.ICon} ,            
                        new SqlParameter("@ParentID", SqlDbType.Int,4){Value=model.ParentID} ,            
                        new SqlParameter("@Sort", SqlDbType.Int,4){Value=model.Sort} ,            
                        new SqlParameter("@Visible", SqlDbType.Bit,1){Value=model.Visible} ,            
                        new SqlParameter("@RightStr", SqlDbType.NVarChar,200){Value=rightGroup}                          
            };

            var obj = SqlHelper.ExecuteNonQueryVal(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_ColumnAdd", parameters);
            return obj;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public int Update(ColumnModel model, string rightGroup)
        {
            SqlParameter[] parameters = {    
                        new SqlParameter("@ID", SqlDbType.Int){Value=model.ID} ,  
                        new SqlParameter("@Description", SqlDbType.NVarChar,200){Value=model.Description} ,            
                        new SqlParameter("@Name", SqlDbType.NVarChar,50){Value=model.Name} ,            
                        new SqlParameter("@LinkUrl", SqlDbType.NVarChar,200){Value=model.LinkUrl} ,                     
                        new SqlParameter("@ICon", SqlDbType.NVarChar,100){Value=model.ICon} ,                       
                        new SqlParameter("@Sort", SqlDbType.Int,4){Value=model.Sort} ,            
                        new SqlParameter("@Visible", SqlDbType.Bit,1){Value=model.Visible} ,            
                        new SqlParameter("@RightStr", SqlDbType.NVarChar,200){Value=rightGroup}                          
            };

            var obj = SqlHelper.ExecuteNonQueryVal(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_ColumnUpdate", parameters);
            return obj;
        }


        ///// <summary>
        ///// 更新
        ///// </summary>
        //public bool Update(ColumnModel model)
        //{
        //    var strSql = new StringBuilder();
        //    strSql.Append("update [Column] set ");
        //    strSql.Append(" UpdateTime = @UpdateTime , ");
        //    strSql.Append(" Description = @Description , ");
        //    strSql.Append(" Name = @Name , ");
        //    strSql.Append(" LinkUrl = @LinkUrl , ");
        //    strSql.Append(" ICon = @ICon , ");
        //    strSql.Append(" ParentID = @ParentID , ");
        //    strSql.Append(" Sort = @Sort , ");
        //    strSql.Append(" Visible = @Visible , ");
        //    strSql.Append(" where ID=@ID ");

        //    SqlParameter[] parameters = {
        //                new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
        //                new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
        //                new SqlParameter("@Description", SqlDbType.NVarChar,200){Value = model.Description} ,            
        //                new SqlParameter("@Name", SqlDbType.NVarChar,50){Value = model.Name} ,            
        //                new SqlParameter("@LinkUrl", SqlDbType.NVarChar,200){Value = model.LinkUrl} ,                
        //                new SqlParameter("@ICon", SqlDbType.NVarChar,100){Value = model.ICon} ,            
        //                new SqlParameter("@ParentID", SqlDbType.Int,4){Value = model.ParentID} ,            
        //                new SqlParameter("@Sort", SqlDbType.Int,4){Value = model.Sort} ,            
        //                new SqlParameter("@Visible", SqlDbType.Bit,1){Value = model.Visible}                                     
        //    };

        //    var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
        //    return rows > 0;
        //}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public ColumnModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, UpdateTime, Description, Name, LinkUrl, ICon, ParentID, Sort, Visible, CreateTime  ");
            strSql.Append("  from [Column] ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};

            var model = new ColumnModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.LinkUrl = ds.Tables[0].Rows[0]["LinkUrl"].ToString();
                model.ICon = ds.Tables[0].Rows[0]["ICon"].ToString();
                if (ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Visible"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Visible"].ToString() == "1") || (ds.Tables[0].Rows[0]["Visible"].ToString().ToLower() == "true"))
                    {
                        model.Visible = true;
                    }
                    else
                    {
                        model.Visible = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                return model;
            }
            return null;
        }


        /// <summary>
        /// 获取栏目列表 相关列数据
        /// </summary>
        /// <param name="fields"> ID, UpdateTime, Description, Name, LinkUrl, ICon, ParentID, Sort, Visible, CreateTime</param>
        /// <param name="visible">0:获取所有栏目 1：获取启用的栏目</param>
        /// <returns></returns>
        public DataSet GetColumnlList(string fields, int visible, string where="")
        {
            var strSql = new StringBuilder();
            strSql.Append("select " + fields + " ");
            strSql.Append("  from [Column] ");
            if (visible == 1)
            {
                strSql.Append("  where Visible=" + visible);
            }
            if (!string.IsNullOrEmpty(where))
            {
                strSql.Append(" and ").Append(where);
            }
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 获取指定节点下的所有子栏目
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="visible"></param>
        /// <param name="rootID"></param>
        /// <returns></returns>
        public DataSet GetChildColumnList(string fields, int visible, int rootID)
        {
            var strSql = new StringBuilder();
            strSql.Append("select " + fields + " ");
            strSql.Append("  from dbo.GetChildColumn(").Append(rootID).Append(") ");
            if (visible == 1)
            {
                strSql.Append("  where Visible=" + visible);
            }
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }

        /// <summary>
        /// 根据栏目ID获取该栏目所拥有的操作权限
        /// </summary>
        /// <param name="columnId">栏目ID</param>
        /// <returns></returns>
        public DataSet GetColRightList(int columnId)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT CR.ColumnID,R.ID AS RightID,R.RightName FROM dbo.ColumnRight CR RIGHT JOIN dbo.[Right] R ON CR.RightID=R.ID WHERE CR.ColumnID=@ColumnID");
            SqlParameter[] parameters = {
					new SqlParameter("@ColumnID", SqlDbType.Int){Value = columnId}
			};
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return ds;
        }
    }
}
