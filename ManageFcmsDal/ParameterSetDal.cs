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
    public class ParameterSetDal
    {
        /// <summary>
        /// 更新所有参数设置数据
        /// </summary>
        public bool Update(List<ParameterSetModel> list)
        {
            var sql = new StringBuilder();
            var parmList = new List<SqlParameter>();
            for (var i = 0; i < list.Count; i++)
            {
                sql.AppendFormat("update dbo.ParameterSet set ParameterValue=@ParameterValue{0} where ID=@ID{0}; ", i);
                parmList.Add(new SqlParameter("@ParameterValue" + i, list[i].ParameterValue));
                parmList.Add(new SqlParameter("@ID" + i, list[i].ID));
            }
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, sql.ToString(), parmList.ToArray());
            return count > 0;
        }

        /// <summary>
        /// 配置参数类型 
        /// <para>1 = 逾期天数:超过指定天数则默认为逾期</para>
        /// <para>2 = 6个月（含）以内的上限年利率</para>
        /// <para>3 = 6个月到1年（含）的年利率上限</para>
        /// <para>4 = 借款最小金额</para>
        /// <para>5 = 借款最大金额</para>
        /// <para>6 = 展期借款利率上浮度</para>
        /// <para>7 = 提前还款违约金投资人所得比例</para>
        /// <para>8 = 最高提现额</para>
        /// </summary>	
        public ParameterSetModel GetParameterSetByParameType(int parameterType)
        {
            const string sqlStr = "select top 1 ID,ParameterType,ParameterName,ParameterValue,ParameterUnit,Remarks from [ParameterSet] where ParameterType=@ParameterType";
            var param = new SqlParameter("@ParameterType", parameterType);
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlStr, param);

            if (ds != null && ds.Tables.Count > 0)
            {
                var dr = ds.Tables[0].Rows[0];
                var model = new ParameterSetModel()
                {
                    ID = ConvertHelper.ToInt(dr["ID"].ToString()),
                    ParameterType = ConvertHelper.ToInt(dr["ParameterType"].ToString()),
                    ParameterName = dr["ParameterName"].ToString(),
                    ParameterValue = ConvertHelper.ToDecimal(dr["ParameterValue"].ToString()),
                    ParameterUnit = dr["ParameterUnit"].ToString(),
                    Remarks = dr["Remarks"].ToString()
                };
                return model;
            }
            return null;
        }

        /// <summary>
        /// 获取所有参数设置数据
        /// </summary>
        public List<ParameterSetModel> GetParameterSetList()
        {
            const string sqlStr = "select ID,ParameterType,ParameterName,ParameterValue,ParameterUnit,Remarks from [ParameterSet]";
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlStr);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return (from DataRow dr in ds.Tables[0].Rows
                        select new ParameterSetModel()
                            {
                                ID = ConvertHelper.ToInt(dr["ID"].ToString()),
                                ParameterType = ConvertHelper.ToInt(dr["ParameterType"].ToString()),
                                ParameterName = dr["ParameterName"].ToString(),
                                ParameterValue = ConvertHelper.ToDecimal(dr["ParameterValue"].ToString()),
                                ParameterUnit = dr["ParameterUnit"].ToString(),
                                Remarks = dr["Remarks"].ToString()
                            }).ToList();
            }
            return null;
        }
    }
}
