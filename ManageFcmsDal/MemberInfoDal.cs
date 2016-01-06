using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ManageFcmsCommon;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class MemberInfoDal
    {
        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string tables, string filters, string sortStr, int currentPage, int pageSize, out int total, out int totalPage)
        {
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(MemberInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MemberInfo set ");

            strSql.Append(" Telephone = @Telephone , ");
            strSql.Append(" Fax = @Fax , ");
            strSql.Append(" CreateTime = @CreateTime , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" MemberID = @MemberID , ");
            strSql.Append(" RealName = @RealName , ");
            strSql.Append(" IdentityCard = @IdentityCard , ");
            strSql.Append(" Sex = @Sex , ");
            strSql.Append(" Province = @Province , ");
            strSql.Append(" City = @City , ");
            strSql.Append(" Address = @Address , ");
            strSql.Append(" Birthday = @Birthday  ");
          //  strSql.Append(" IsMarket = @IsMarket,  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,            
                        new SqlParameter("@Telephone", SqlDbType.VarChar,20){Value = model.Telephone} ,            
                        new SqlParameter("@Fax", SqlDbType.VarChar,50){Value = model.Fax} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value = model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,            
                        new SqlParameter("@MemberID", SqlDbType.Int,4){Value = model.MemberID} ,            
                        new SqlParameter("@RealName", SqlDbType.NVarChar,50){Value = model.RealName} ,            
                        new SqlParameter("@IdentityCard", SqlDbType.VarChar,20){Value = model.IdentityCard} ,            
                        new SqlParameter("@Sex", SqlDbType.Char,2){Value = model.Sex} ,            
                        new SqlParameter("@Province", SqlDbType.Int,4){Value = model.Province} ,            
                        new SqlParameter("@City", SqlDbType.Int,4){Value = model.City} ,            
                        new SqlParameter("@Address", SqlDbType.NVarChar,200){Value = model.Address} ,            
                        new SqlParameter("@Birthday", SqlDbType.DateTime){Value = model.Birthday} 
                      //  new SqlParameter("@IsMarket", SqlDbType.DateTime){Value = model.IsMarket}
              
            };


            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MemberInfoModel GetModel(int memberID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID, Telephone, Fax, CreateTime, UpdateTime, MemberID, RealName, IdentityCard, Sex, Province, City, Address, Birthday  ");
            strSql.Append("  from MemberInfo ");
            strSql.Append(" where MemberID=@MemberID");
            SqlParameter[] parameters = {
					new SqlParameter("@MemberID", SqlDbType.Int,4)
			};
            parameters[0].Value = memberID;


            MemberInfoModel model = new MemberInfoModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                model.Telephone = ds.Tables[0].Rows[0]["Telephone"].ToString();
                model.Fax = ds.Tables[0].Rows[0]["Fax"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.IdentityCard = ds.Tables[0].Rows[0]["IdentityCard"].ToString();
                model.Sex = ds.Tables[0].Rows[0]["Sex"].ToString();
                if (ds.Tables[0].Rows[0]["Province"].ToString() != "")
                {
                    model.Province = int.Parse(ds.Tables[0].Rows[0]["Province"].ToString());
                }
                if (ds.Tables[0].Rows[0]["City"].ToString() != "")
                {
                    model.City = int.Parse(ds.Tables[0].Rows[0]["City"].ToString());
                }
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }
    }
}
