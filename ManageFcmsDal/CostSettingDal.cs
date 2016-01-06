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
    public class CostSettingDal
    {
        /// <summary>
        /// 增加
        /// </summary>
        public int Add(CostSettingModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("insert into CostSetting(");
            strSql.Append("EnableStatus,CreateTime,UpdateTime,CostVersionID,FeeType,CalculationMode,CalInitialValue,CalInitialProportion,IncreasingMode,IncreasUnit,IncreasProportion");
            strSql.Append(") values (");
            strSql.Append("@EnableStatus,@CreateTime,@UpdateTime,@CostVersionID,@FeeType,@CalculationMode,@CalInitialValue,@CalInitialProportion,@IncreasingMode,@IncreasUnit,@IncreasProportion");
            strSql.Append(") ");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
			            new SqlParameter("@EnableStatus", SqlDbType.Bit,1){Value=model.EnableStatus} ,            
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value=model.CreateTime} ,            
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value=model.UpdateTime} ,            
                        new SqlParameter("@CostVersionID", SqlDbType.Int,4){Value=model.CostVersionId} ,            
                        new SqlParameter("@FeeType", SqlDbType.Int,4){Value=model.FeeType} ,            
                        new SqlParameter("@CalculationMode", SqlDbType.Int,4){Value=model.CalculationMode} ,            
                        new SqlParameter("@CalInitialValue", SqlDbType.Decimal,9){Value=model.CalInitialValue} ,            
                        new SqlParameter("@CalInitialProportion", SqlDbType.Decimal,9){Value=model.CalInitialProportion} ,            
                        new SqlParameter("@IncreasingMode", SqlDbType.Int,4){Value=model.IncreasingMode} ,            
                        new SqlParameter("@IncreasUnit", SqlDbType.Decimal,9){Value=model.IncreasUnit} ,            
                        new SqlParameter("@IncreasProportion", SqlDbType.Decimal,9){Value=model.IncreasProportion}             
              
            };
            var obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : ConvertHelper.ToInt(obj.ToString());

        }


        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(CostSettingModel model)
        {
            var strSql = new StringBuilder();
            strSql.Append("update CostSetting set ");
            strSql.Append(" EnableStatus = @EnableStatus , ");
            strSql.Append(" UpdateTime = @UpdateTime , ");
            strSql.Append(" CalculationMode = @CalculationMode , ");
            strSql.Append(" CalInitialValue = @CalInitialValue , ");
            strSql.Append(" CalInitialProportion = @CalInitialProportion , ");
            strSql.Append(" IncreasingMode = @IncreasingMode , ");
            strSql.Append(" IncreasUnit = @IncreasUnit , ");
            strSql.Append(" IncreasProportion = @IncreasProportion  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
			            new SqlParameter("@ID", SqlDbType.Int,4){Value = model.Id} ,            
                        new SqlParameter("@EnableStatus", SqlDbType.Bit,1){Value = model.EnableStatus} ,                       
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value = model.UpdateTime} ,                                   
                        new SqlParameter("@CalculationMode", SqlDbType.Int,4){Value = model.CalculationMode} ,            
                        new SqlParameter("@CalInitialValue", SqlDbType.Decimal,9){Value = model.CalInitialValue} ,            
                        new SqlParameter("@CalInitialProportion", SqlDbType.Decimal,9){Value = model.CalInitialProportion} ,            
                        new SqlParameter("@IncreasingMode", SqlDbType.Int,4){Value = model.IncreasingMode} ,            
                        new SqlParameter("@IncreasUnit", SqlDbType.Decimal,9){Value = model.IncreasUnit} ,            
                        new SqlParameter("@IncreasProportion", SqlDbType.Decimal,9){Value = model.IncreasProportion}             
              
            };

            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from [CostSetting] ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value = id}
			};
            var rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }

        /// <summary>
        /// 获取设置费用分页列表
        /// </summary>
        /// <param name="whereStr"></param>
        /// <param name="orderBy"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="rowsCount"></param>
        /// <returns></returns>
        public List<CostSettingModel> GetCostSetList(string whereStr, string orderBy, int currentPage, int pageSize, ref int rowsCount)
        {
            var costSettingModel = new List<CostSettingModel>();
            string sqlCount = "select count(*) as totals from dbo.CostSetting";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlCount = sqlCount + " where " + whereStr;
            }
            SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlCount);
            if (reader.Read())
            {
                rowsCount = reader["totals"] != DBNull.Value ? Convert.ToInt32(reader["totals"]) : 0;
            }
            reader.Close();
            var sqlPage = @"select (ROW_NUMBER() OVER(ORDER BY " + orderBy + ")) AS rownum, " +
                          "[ID],[CostVersionID],[FeeType],[CalculationMode],[CalInitialValue],[CalInitialProportion],[IncreasingMode],[IncreasUnit],[IncreasProportion],[EnableStatus],[CreateTime],[UpdateTime] from dbo.CostSetting";
            if (!string.IsNullOrEmpty(whereStr))
            {
                sqlPage = sqlPage + " where " + whereStr;
            }
            var startIndex = (currentPage - 1) * pageSize + 1;
            sqlPage = @"Select [ID],[CostVersionID],[FeeType],[CalculationMode],[CalInitialValue],[CalInitialProportion],[IncreasingMode],[IncreasUnit],[IncreasProportion],[EnableStatus],[CreateTime],[UpdateTime] 
                        from (" + sqlPage + ") tmp where rownum between " + startIndex + " and " + currentPage * pageSize;
            reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocal, CommandType.Text, sqlPage);
            while (reader.Read())
            {
                var info = GetFcmsUserModel(reader);
                costSettingModel.Add(info);
            }
            reader.Close();
            return costSettingModel;
        }

        private static CostSettingModel GetFcmsUserModel(SqlDataReader dr)
        {
            var costSettingModel = new CostSettingModel
            {
                Id = ConvertHelper.ToInt(dr["ID"].ToString()),
                CostVersionId = ConvertHelper.ToInt(dr["CostVersionID"].ToString()),
                FeeType = ConvertHelper.ToInt(dr["FeeType"].ToString()),
                CalculationMode = ConvertHelper.ToInt(dr["CalculationMode"].ToString()),
                CalInitialValue = ConvertHelper.ToDecimal(dr["CalInitialValue"].ToString()),
                CalInitialProportion = ConvertHelper.ToDecimal(dr["CalInitialProportion"].ToString()),
                IncreasingMode = ConvertHelper.ToInt(dr["IncreasingMode"].ToString()),
                IncreasUnit = ConvertHelper.ToDecimal(dr["IncreasUnit"].ToString()),
                IncreasProportion = ConvertHelper.ToDecimal(dr["IncreasProportion"].ToString()),
                EnableStatus = ConvertHelper.ToBool(dr["EnableStatus"].ToString()),
                CreateTime = dr["CreateTime"].Equals(DBNull.Value) ? DateTime.Now : ConvertHelper.ToDateTime(dr["CreateTime"].ToString()),
                UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) ? DateTime.Now : ConvertHelper.ToDateTime(dr["UpdateTime"].ToString())
            };
            return costSettingModel;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public CostSettingModel GetModel(int id)
        {
            var strSql = new StringBuilder();
            strSql.Append("select ID, EnableStatus, CreateTime, UpdateTime, CostVersionID, FeeType, CalculationMode, CalInitialValue, CalInitialProportion, IncreasingMode, IncreasUnit, IncreasProportion  ");
            strSql.Append("  from CostSetting ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4){Value=id}
			};

            var model = new CostSettingModel();
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EnableStatus"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["EnableStatus"].ToString() == "1") || (ds.Tables[0].Rows[0]["EnableStatus"].ToString().ToLower() == "true"))
                    {
                        model.EnableStatus = true;
                    }
                    else
                    {
                        model.EnableStatus = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CostVersionID"].ToString() != "")
                {
                    model.CostVersionId = int.Parse(ds.Tables[0].Rows[0]["CostVersionID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FeeType"].ToString() != "")
                {
                    model.FeeType = int.Parse(ds.Tables[0].Rows[0]["FeeType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CalculationMode"].ToString() != "")
                {
                    model.CalculationMode = int.Parse(ds.Tables[0].Rows[0]["CalculationMode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CalInitialValue"].ToString() != "")
                {
                    model.CalInitialValue = decimal.Parse(ds.Tables[0].Rows[0]["CalInitialValue"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CalInitialProportion"].ToString() != "")
                {
                    model.CalInitialProportion = decimal.Parse(ds.Tables[0].Rows[0]["CalInitialProportion"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IncreasingMode"].ToString() != "")
                {
                    model.IncreasingMode = int.Parse(ds.Tables[0].Rows[0]["IncreasingMode"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IncreasUnit"].ToString() != "")
                {
                    model.IncreasUnit = decimal.Parse(ds.Tables[0].Rows[0]["IncreasUnit"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IncreasProportion"].ToString() != "")
                {
                    model.IncreasProportion = decimal.Parse(ds.Tables[0].Rows[0]["IncreasProportion"].ToString());
                }

                return model;
            }
            return null;
        }

        /// <summary>
        ///获取费用比例
        /// </summary>
        /// <param name="computeEnumType">计算类型</param>
        /// <param name="feeType">费用类型</param>
        /// <param name="comparValue">比较值</param>
        /// <returns></returns>
        public decimal GetChargeRate(CostSettingModel.ComputeEnumType computeEnumType, FeeType.FeeTypeEnum feeType, decimal comparValue)
        {
            return GetChargeOrRate(computeEnumType, feeType, comparValue, -1);
        }

        /// <summary>
        ///获取费用比例
        /// </summary>
        /// <param name="computeEnumType">计算类型</param>
        /// <param name="feeType">费用类型</param>
        /// <param name="comparValue">比较值</param>
        /// <param name="basicValue">基值（备用）</param>
        /// <returns></returns>
        private decimal GetChargeOrRate(CostSettingModel.ComputeEnumType computeEnumType, FeeType.FeeTypeEnum feeType, decimal comparValue, int basicValue)
        {
            //当天大于30时，转换为月
            if ((int)computeEnumType == 0 && comparValue > 366)
                throw new Exception("比较值不能大于366天");
            if ((int)computeEnumType == 0 && comparValue > 30)
            {
                computeEnumType = CostSettingModel.ComputeEnumType.Month;
                comparValue = ParseMonth(comparValue);
            }

            SqlParameter[] parameters = {
			            new SqlParameter("@CalType", SqlDbType.Int,4){Value=(int)computeEnumType} ,            
                        new SqlParameter("@FeeType", SqlDbType.Int){Value=(int)feeType} ,            
                        new SqlParameter("@ComparValue", SqlDbType.Decimal){Value=comparValue} ,            
                        new SqlParameter("@returnType", SqlDbType.Int,4){Value=basicValue}
            };
            var obj = SqlHelper.ExecuteNonQueryOutPut(SqlHelper.ConnectionStringLocal, CommandType.StoredProcedure, "Proc_GetChargeRate", "@ReturnOutPut", parameters);
            return obj;
        }

        /// <summary>
        /// 天数转换月数
        /// </summary>
        /// <param name="days">天数</param>
        /// <returns></returns>
        private static decimal ParseMonth(decimal days)
        {
            if (days <= 30) days = 1;
            if (days <= 45.5m) days = 1.5m;
            else if (days <= 61) days = 2;
            else if (days <= 92) days = 3;
            else if (days <= 122) days = 4;
            else if (days <= 153) days = 5;
            else if (days <= 183) days = 6;
            else if (days <= 214) days = 7;
            else if (days <= 244) days = 8;
            else if (days <= 275) days = 9;
            else if (days <= 305) days = 10;
            else if (days <= 336) days = 11;
            else if (days <= 366) days = 12;
            return days;
        }
    }
}
