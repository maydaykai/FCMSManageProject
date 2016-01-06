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
    public class EnterpriseAuthentDal
    {
        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(EnterpriseAuthentModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update EnterpriseAuthent set ");
            strSql.Append(" AuthentNumber = @AuthentNumber ,  ");
            strSql.Append(" CityID = @CityID ,  ");
            strSql.Append(" Address = @Address ,  ");
            strSql.Append(" UpdateTime = @UpdateTime  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.ID} ,                                           
                        new SqlParameter("@AuthentNumber", SqlDbType.Int,4){Value = model.AuthentNumber},
			            new SqlParameter("@CityID", SqlDbType.Int,4){Value=model.CityID} ,            
                        new SqlParameter("@Address", SqlDbType.NVarChar,300){Value=model.Address} ,  
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime}       
            };

            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 数据分页
        /// </summary>
        public DataTable GetPageList(string fields, string filters, string sortStr, int currentPage, int pageSize, out int total)
        {
            int totalPage;
            const string tables = " EnterpriseAuthent P LEFT JOIN dbo.Member M ON M.ID=P.MemberID";
            return SqlHelper.ExecutePageForProc(SqlHelper.ConnectionStringLocal, fields, tables, filters, sortStr, currentPage, pageSize, out total, out totalPage);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EnterpriseAuthentModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, AuthentResult, AuthentNumber, RequestXml, ResponseXml, ExamStatus, ExpireTime, ApplyTime, UpdateTime, MemberID, EnterpriseName, RegistrationNo, OrganizationCode, LegalName, OperatorRealName, OperatorIdentityCard, OperatorPhone ,CityID , Address ");
            strSql.Append("  from EnterpriseAuthent ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};

            var model = new EnterpriseAuthentModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuthentResult"].ToString() != "")
                {
                    model.AuthentResult = int.Parse(ds.Tables[0].Rows[0]["AuthentResult"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AuthentNumber"].ToString() != "")
                {
                    model.AuthentNumber = int.Parse(ds.Tables[0].Rows[0]["AuthentNumber"].ToString());
                }
                model.RequestXml = ds.Tables[0].Rows[0]["RequestXml"].ToString();
                model.ResponseXml = ds.Tables[0].Rows[0]["ResponseXml"].ToString();
                if (ds.Tables[0].Rows[0]["ExamStatus"].ToString() != "")
                {
                    model.ExamStatus = int.Parse(ds.Tables[0].Rows[0]["ExamStatus"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ExpireTime"].ToString() != "")
                {
                    model.ExpireTime = DateTime.Parse(ds.Tables[0].Rows[0]["ExpireTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ApplyTime"].ToString() != "")
                {
                    model.ApplyTime = DateTime.Parse(ds.Tables[0].Rows[0]["ApplyTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MemberID"].ToString() != "")
                {
                    model.MemberID = int.Parse(ds.Tables[0].Rows[0]["MemberID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CityID"].ToString() != "")
                {
                    model.CityID = int.Parse(ds.Tables[0].Rows[0]["CityID"].ToString());
                }
                model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                model.EnterpriseName = ds.Tables[0].Rows[0]["EnterpriseName"].ToString();
                model.RegistrationNo = ds.Tables[0].Rows[0]["RegistrationNo"].ToString();
                model.OrganizationCode = ds.Tables[0].Rows[0]["OrganizationCode"].ToString();
                model.LegalName = ds.Tables[0].Rows[0]["LegalName"].ToString();
                model.OperatorRealName = ds.Tables[0].Rows[0]["OperatorRealName"].ToString();
                model.OperatorIdentityCard = ds.Tables[0].Rows[0]["OperatorIdentityCard"].ToString();
                model.OperatorPhone = ds.Tables[0].Rows[0]["OperatorPhone"].ToString();

                return model;
            }
            return null;
        }

    }
}
