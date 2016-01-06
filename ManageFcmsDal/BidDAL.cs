using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageFcmsCommon;
using ManageFcmsConn;
using ManageFcmsModel;

namespace ManageFcmsDal
{
    public class BidDAL
    {
        /// <summary>
        /// 获取投资记录
        /// </summary>
        /// <param name="Where"></param>
        /// <returns></returns>
        public List<BidModel> GetBidRecordList(string Where)
        {
            List<BidModel> list = new List<BidModel>();
            string sql2 = "SELECT B.*,L.LoanRate,M.MemberName,MI.RealName,(SELECT RealName From MemberInfo WHERE MemberID=(SELECT RecMemberID FROM dbo.MemberRecommended WHERE RecedMemberID=B.MemberID)) RecRealName FROM dbo.Bidding B INNER JOIN dbo.Member M ON B.MemberID=M.ID INNER JOIN dbo.Loan L ON B.LoanID = L.ID LEFT JOIN dbo.MemberInfo MI ON B.MemberID=MI.MemberID ";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " WHERE " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            while (reader.Read())
            {
                BidModel info = getBidRecordListModelByDr(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }
        /// <summary>
        /// 获取投资记录
        /// </summary>
        /// <param name="Where"></param>
        /// <param name="OrderBy"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="TotalRows"></param>
        /// <returns></returns>
        public DataTable GetBidRecordDt(string Where, string OrderBy, int PageIndex, int PageSize, ref int TotalRows)
        {
            string sql1 = "SELECT count(*) as totals FROM dbo.Bidding B INNER JOIN dbo.Loan L ON B.LoanID = L.ID INNER JOIN dbo.Member M ON L.MemberID=M.ID LEFT JOIN dbo.MemberInfo MI ON L.MemberID=MI.MemberID LEFT JOIN dbo.DimLoanUse U ON L.DimLoanUseID=U.ID LEFT JOIN dbo.DimExamStatus E ON L.ExamStatus=E.ID";
            if (!string.IsNullOrEmpty(Where))
            {
                sql1 = sql1 + " WHERE " + Where;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sql1);
            if (reader.Read())
            {
                TotalRows = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            string sql2 = "select (ROW_NUMBER() OVER(ORDER BY " + OrderBy + ")) AS rownum, B.*,L.LoanRate,L.LoanNumber,L.LoanAmount,L.LoanTerm,L.BorrowMode,L.CreateTime LoanCreateTime,L.FullScaleTime,M.MemberName,MI.RealName,U.LoanUseName,E.ExamStatusName FROM dbo.Bidding B INNER JOIN dbo.Loan L ON B.LoanID = L.ID INNER JOIN dbo.Member M ON L.MemberID=M.ID LEFT JOIN dbo.MemberInfo MI ON L.MemberID=MI.MemberID LEFT JOIN dbo.DimLoanUse U ON L.DimLoanUseID=U.ID LEFT JOIN dbo.DimExamStatus E ON L.ExamStatus=E.ID";
            if (!string.IsNullOrEmpty(Where))
            {
                sql2 = sql2 + " WHERE " + Where;
            }
            sql2 = "Select * from (" + sql2 + ") tmp where rownum between " + ((PageIndex - 1) * PageSize + 1) + " and " + PageIndex * PageSize;
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql2);
            return (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) ? ds.Tables[0] : null;
        }
        private BidModel getBidRecordListModelByDr(SqlDataReader dr)
        {
            BidModel model = new BidModel();
            model.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
            model.BidType = dr["BidType"] != DBNull.Value ? Convert.ToInt32(dr["BidType"]) : 0;
            model.MemberID = dr["MemberID"] != DBNull.Value ? Convert.ToInt32(dr["MemberID"]) : 0;
            model.MemberName = dr["MemberName"] != DBNull.Value ? dr["MemberName"].ToString() : "";
            model.RealName = dr["RealName"] != DBNull.Value ? dr["RealName"].ToString() : "";
            model.RecRealName = dr["RecRealName"] != DBNull.Value ? dr["RecRealName"].ToString() : "";
            model.LoanID = dr["LoanID"] != DBNull.Value ? Convert.ToInt32(dr["LoanID"]) : 0;
            model.LoanRate = dr["LoanRate"] != DBNull.Value ? Convert.ToDecimal(dr["LoanRate"]) : 0;
            model.BidAmount = dr["BidAmount"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmount"]) : 0;
            model.BidStatus = dr["BidStatus"] != DBNull.Value ? Convert.ToInt32(dr["BidStatus"]) : 0;
            model.CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate;
            model.UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate;

            return model;
        }

        public List<BidModel> GetBidListByLoanId(int loanId)
        {
            var list = new List<BidModel>();
            var strSql = new StringBuilder();
            strSql.Append("SELECT dbo.Member.MemberName, dbo.Bidding.LoanID, dbo.Bidding.MemberID, dbo.Bidding.BidType, dbo.Bidding.ID, dbo.Bidding.BidAmount, dbo.Bidding.BidStatus, ");
            strSql.Append(" dbo.Bidding.CreateTime, dbo.Bidding.UpdateTime, dbo.Member.Mobile, dbo.MemberInfo.RealName, dbo.MemberInfo.IdentityCard");
            strSql.Append(" FROM dbo.Bidding INNER JOIN");
            strSql.Append(" dbo.Member ON dbo.Bidding.MemberID = dbo.Member.ID INNER JOIN");
            strSql.Append(" dbo.MemberInfo ON dbo.Bidding.MemberID = dbo.MemberInfo.MemberID");
            strSql.Append(" WHERE dbo.Bidding.LoanID = @LoanID AND dbo.Bidding.CALoanID=0");
            SqlParameter[] parameters = { new SqlParameter("@LoanID", SqlDbType.Int, 4) { Value = loanId } };
            var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            while (reader.Read())
            {
                var info = BidModelInit(reader);
                list.Add(info);
            }
            reader.Close();
            return list;
        }

        private BidModel BidModelInit(SqlDataReader dr)
        {
            var model = new BidModel
                {
                    ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0,
                    BidType = dr["BidType"] != DBNull.Value ? Convert.ToInt32(dr["BidType"]) : 0,
                    MemberID = dr["MemberID"] != DBNull.Value ? Convert.ToInt32(dr["MemberID"]) : 0,
                    LoanID = dr["LoanID"] != DBNull.Value ? Convert.ToInt32(dr["LoanID"]) : 0,
                    BidAmount = dr["BidAmount"] != DBNull.Value ? Convert.ToDecimal(dr["BidAmount"]) : 0,
                    BidStatus = dr["BidStatus"] != DBNull.Value ? Convert.ToInt32(dr["BidStatus"]) : 0,
                    CreateTime = dr["CreateTime"] != DBNull.Value ? Convert.ToDateTime(dr["CreateTime"]) : PublicConst.MinDate,
                    UpdateTime = dr["UpdateTime"] != DBNull.Value ? Convert.ToDateTime(dr["UpdateTime"]) : PublicConst.MinDate,
                    Mobile = dr["Mobile"] != DBNull.Value ? dr["Mobile"].ToString() : "",
                    MemberName = dr["MemberName"] != DBNull.Value ? dr["MemberName"].ToString() : "",
                    RealName = dr["RealName"] != DBNull.Value ? dr["RealName"].ToString() : "",
                    IdentityCard = dr["IdentityCard"] != DBNull.Value ? dr["IdentityCard"].ToString() : ""
                };
            return model;
        }

        /// <summary>
        /// 返回受让人列表（债权转让合同）
        /// </summary>
        /// <param name="caId"></param>
        /// <returns></returns>
        public DataSet GetCaBidListByCaID(int caId)
        {
            const string sqlStr = "SELECT MI.MemberID,MI.RealName,M.MemberName,MI.IdentityCard,B.BidAmount FROM dbo.Bidding B LEFT JOIN dbo.Member M ON B.MemberID=M.ID INNER JOIN dbo.MemberInfo MI ON MI.MemberID=M.ID WHERE B.CALoanID=@CaID ORDER BY B.ID ";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@CaID",SqlDbType.Int,4){Value = caId}, 
                };
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlStr, parameters);
            return ds;
        }
    }

}
