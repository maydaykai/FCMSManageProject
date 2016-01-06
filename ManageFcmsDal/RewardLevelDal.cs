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
    public class RewardLevelDal
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int addDimRewardLevel(DimRewardLevelModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DimRewardLevel(");
            strSql.Append("Interest,RewardScale,RankLevel,LevelDesc,IsDisable,CreateTime,UpdateTime");
            strSql.Append(") values (");
            strSql.Append("@Interest,@RewardScale,@RankLevel,@LevelDesc,@IsDisable,@CreateTime,@UpdateTime");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
			            new SqlParameter("@Interest", SqlDbType.Decimal,9){Value= model.Interest},
                        new SqlParameter("@RewardScale", SqlDbType.Decimal,9){Value= model.RewardScale},
                        new SqlParameter("@RankLevel", SqlDbType.Int,4){Value= model.RankLevel},
                        new SqlParameter("@LevelDesc", SqlDbType.NVarChar,128){Value= model.LevelDesc},
                        new SqlParameter("@IsDisable", SqlDbType.Bit,1){Value= model.IsDisable},
                        new SqlParameter("@CreateTime", SqlDbType.DateTime){Value= DateTime.Now},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= DateTime.Now},
                        };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt32(obj);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool updateDimRewardLevel(DimRewardLevelModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DimRewardLevel set ");

            strSql.Append(" Interest = @Interest , ");
            strSql.Append(" RewardScale = @RewardScale , ");
            strSql.Append(" RankLevel = @RankLevel , ");
            strSql.Append(" LevelDesc = @LevelDesc , ");
            strSql.Append(" IsDisable = @IsDisable , ");
            strSql.Append(" UpdateTime = @UpdateTime  ");
            strSql.Append(" where ID=@ID ");

            SqlParameter[] parameters = {
                        new SqlParameter("@ID", SqlDbType.Int,4){Value= model.ID},
			            new SqlParameter("@Interest", SqlDbType.Decimal,9){Value= model.Interest},
                        new SqlParameter("@RewardScale", SqlDbType.Decimal,9){Value= model.RewardScale},
                        new SqlParameter("@RankLevel", SqlDbType.Int,4){Value= model.RankLevel},
                        new SqlParameter("@LevelDesc", SqlDbType.NVarChar,128){Value= model.LevelDesc},
                        new SqlParameter("@IsDisable", SqlDbType.Bit,1){Value= model.IsDisable},
                        new SqlParameter("@UpdateTime", SqlDbType.DateTime){Value= model.UpdateTime},
                        };

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString(), parameters);
            return rows > 0;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DimRewardLevelModel getDimRewardLevelModel(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 ID, Interest, RewardScale, RankLevel, LevelDesc, IsDisable, CreateTime, UpdateTime  ");
            strSql.Append(" from DimRewardLevel ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }

            DimRewardLevelModel model = new DimRewardLevelModel();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 给对象赋值
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Interest"].ToString() != "")
                {
                    model.Interest = decimal.Parse(ds.Tables[0].Rows[0]["Interest"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RewardScale"].ToString() != "")
                {
                    model.RewardScale = decimal.Parse(ds.Tables[0].Rows[0]["RewardScale"].ToString());
                }
                if (ds.Tables[0].Rows[0]["RankLevel"].ToString() != "")
                {
                    model.RankLevel = int.Parse(ds.Tables[0].Rows[0]["RankLevel"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LevelDesc"].ToString() != "")
                {
                    model.LevelDesc = ds.Tables[0].Rows[0]["LevelDesc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsDisable"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsDisable"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsDisable"].ToString().ToLower() == "true"))
                    {
                        model.IsDisable = true;
                    }
                    else
                    {
                        model.IsDisable = false;
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
                #endregion
                return model;
            }
            return null;
        }
        /// <summary>
        /// 返回所有推荐奖励比例
        /// </summary>
        /// <returns></returns>
        public DataSet GetRewardLevelList()
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT * FROM dbo.[DimRewardLevel] ORDER BY RankLevel asc");
            var ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, strSql.ToString());
            return ds;
        }
    }
}
