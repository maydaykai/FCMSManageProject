using ManageFcmsConn;
using ManageFcmsModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageFcmsDal
{
    public class InformationDal
    {
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(InformationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Information(");
            strSql.Append("SectionID,Title,Content,Status,Recommend,NewsImage,PubTime,UpdateTime,ShowDesc,Image_value,MediaTypeID,SummaryCount,url,ExtendedContent");
            strSql.Append(") values (");
            strSql.Append("@SectionID,@Title,@Content,@Status,@Recommend,@NewsImage,@PubTime,@UpdateTime,@ShowDesc,@Image_value,@MediaTypeID,@SummaryCount,@url,@ExtendedContent");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@SectionID", SqlDbType.Int,4){Value=model.SectionID} ,            
                        new SqlParameter("@Title", SqlDbType.NVarChar,400){Value=model.Title} ,            
                        new SqlParameter("@Content", SqlDbType.NVarChar,-1){Value=model.Content} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value=model.Status} ,            
                        new SqlParameter("@Recommend", SqlDbType.Bit,1){Value=model.Recommend} ,            
                        new SqlParameter("@NewsImage", SqlDbType.NVarChar,100){Value=model.NewsImage} ,            
                        new SqlParameter("@PubTime", SqlDbType.DateTime){Value=model.PubTime} ,  
                        new SqlParameter("@ShowDesc", SqlDbType.Int,4){Value=model.ShowDesc} ,      
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime} ,
                        new SqlParameter("@Image_value", SqlDbType.Int,4){Value=model.MediaTypeId},
                        new SqlParameter("@MediaTypeId", SqlDbType.Int){Value=model.Image_value},
                        new SqlParameter("@SummaryCount", SqlDbType.NVarChar,-1){Value=model.SummaryCount} ,
                        new SqlParameter("@url", SqlDbType.NVarChar,200){Value=model.url},
                        new SqlParameter("@ExtendedContent",SqlDbType.Xml){Value=model.ExtendedContent}
              
            };

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(InformationModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Information set ");

            strSql.Append(" SectionID = @SectionID , ");
            strSql.Append(" Title = @Title , ");
            strSql.Append(" Content = @Content , ");
            strSql.Append(" Status = @Status , ");
            strSql.Append(" Recommend = @Recommend , ");
            strSql.Append(" NewsImage = @NewsImage , ");
            strSql.Append(" PubTime = @PubTime , ");
            strSql.Append(" ShowDesc = @ShowDesc , ");
            strSql.Append(" UpdateTime = @UpdateTime, ");
            strSql.Append(" Image_value = @Image_value, ");
            strSql.Append(" MediaTypeId=@MediaTypeId,");
            strSql.Append(" SummaryCount = @SummaryCount, ");
            strSql.Append(" url = @url , ");
            strSql.Append(" ExtendedContent=@ExtendedContent");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@SectionID", SqlDbType.Int,4){Value = model.SectionID} ,            
                        new SqlParameter("@Title", SqlDbType.NVarChar,400){Value = model.Title} ,            
                        new SqlParameter("@Content", SqlDbType.NVarChar,-1){Value = model.Content} ,            
                        new SqlParameter("@Status", SqlDbType.Int,4){Value = model.Status} ,            
                        new SqlParameter("@Recommend", SqlDbType.Bit,1){Value = model.Recommend} ,            
                        new SqlParameter("@NewsImage", SqlDbType.NVarChar,100){Value = model.NewsImage} ,            
                        new SqlParameter("@PubTime", SqlDbType.DateTime){Value = model.PubTime} ,     
                        new SqlParameter("@ShowDesc", SqlDbType.Int,4){Value=model.ShowDesc} ,  
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,
                        new SqlParameter("@MediaTypeId", SqlDbType.Int){Value = model.MediaTypeId},
                        new SqlParameter("@Image_value", SqlDbType.Int,4){Value=model.MediaTypeId},
                        new SqlParameter("@SummaryCount", SqlDbType.NVarChar,-1){Value = model.SummaryCount} ,
                        new SqlParameter("@url", SqlDbType.NVarChar,200){Value=model.url},
                        new SqlParameter("@ExtendedContent", SqlDbType.Xml){Value=model.ExtendedContent}
            };


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="SectionID"></param>
        /// <returns></returns>
        public bool UpdateAllStatus(int SectionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE Information set Recommend=0 where SectionID=@SectionID");
            SqlParameter[] parameters = {
					new SqlParameter("@SectionID", SqlDbType.Int,4)
			};
            parameters[0].Value = SectionID;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Information ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Information ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return rows > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public InformationModel GetModel(int ID)
        {
            
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, SectionID, Title, Content, Status, Recommend, NewsImage, PubTime, UpdateTime,ShowDesc, Image_value,MediaTypeId,SummaryCount,url,ExtendedContent");
            strSql.Append("  from Information ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
            parameters[0].Value = ID;


            InformationModel model = new InformationModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SectionID"].ToString() != "")
                {
                    model.SectionID = int.Parse(ds.Tables[0].Rows[0]["SectionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Image_value"].ToString() != "")
                {
                    model.MediaTypeId = int.Parse(ds.Tables[0].Rows[0]["Image_value"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MediaTypeId"].ToString() != "")
                {
                    model.Image_value = int.Parse(ds.Tables[0].Rows[0]["MediaTypeId"].ToString());
                }
                model.Title = ds.Tables[0].Rows[0]["Title"].ToString();
                model.SummaryCount = ds.Tables[0].Rows[0]["SummaryCount"].ToString();
                model.Content = ds.Tables[0].Rows[0]["Content"].ToString();
                model.ExtendedContent = ds.Tables[0].Rows[0]["ExtendedContent"].ToString();
                int relust = 0;
                if (int.TryParse(ds.Tables[0].Rows[0]["ShowDesc"].ToString(), out relust))
                {
                    model.ShowDesc = Convert.ToInt32(ds.Tables[0].Rows[0]["ShowDesc"].ToString());
                }
                else
                {
                    model.ShowDesc = relust;
                }
               
                if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                {
                    model.Status = int.Parse(ds.Tables[0].Rows[0]["Status"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Recommend"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["Recommend"].ToString() == "1") || (ds.Tables[0].Rows[0]["Recommend"].ToString().ToLower() == "true"))
                    {
                        model.Recommend = true;
                    }
                    else
                    {
                        model.Recommend = false;
                    }
                }
                model.NewsImage = ds.Tables[0].Rows[0]["NewsImage"].ToString();
                if (ds.Tables[0].Rows[0]["PubTime"].ToString() != "")
                {
                    model.PubTime = DateTime.Parse(ds.Tables[0].Rows[0]["PubTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["url"].ToString() != "")
                {
                    model.url = ds.Tables[0].Rows[0]["url"].ToString();
                }

                return model;
            }
            return null;
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " Information P left join MediaType b on P.MediaTypeID=b.ID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }
    }
}
