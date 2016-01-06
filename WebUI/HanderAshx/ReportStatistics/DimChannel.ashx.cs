using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManageFcmsBll;
using System.Data;
using ManageFcmsCommon;
using Newtonsoft.Json;
using System.IO;
using ManageFcmsConn;

namespace WebUI.HanderAshx.ReportStatistics
{
    /// <summary>
    /// DimChannel 的摘要说明
    /// </summary>
    public class DimChannel : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "Application/json";
            var method = this.GetType().GetMethod(context.Request["method"]);
            method.Invoke(this, new object[] { context });
        }


        public void GetChannel(HttpContext hc)
        {
            var ds = new DimChannelBll().GetDimChannel();
            List<object> list = new List<object>() { new { ChannelKey = -1, ChannelValue = "全部来源" } };
            Dictionary<int, string> dic = new Dictionary<int, string>();
            var dr = ds.Tables[0].Rows;
            for (int i = 0; i < dr.Count; i++)
            {
                dic.Add(Convert.ToInt32(dr[i]["Id"]), dr[i]["Channel"].ToString());
            }
            list.AddRange(dic.Select(p => new
            {
                ChannelKey = p.Key,
                ChannelValue = p.Value
            }));

            hc.Response.Write("{\"TotalRows\":" + dic.Count + ",\"Rows\":" + JsonConvert.SerializeObject(list) + "}");

        }

        public void ExportExcel(HttpContext hc)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            string filesName = hc.Server.MapPath("~/ExcelDownLoads");
            if (!Directory.Exists(filesName))
            {
                Directory.CreateDirectory(filesName);
            }

            string filePath = hc.Server.MapPath("~/ExcelDownLoads/" + fileName);

            string sql = "with TempTable as ( select row_number() over (order by  RePayTime ASC  ) as RowID, (select 'RJB' + LoanNumber from Loan where ID = LoanID) as '标号',(SELECT Balance FROM dbo.Member WHERE id = (select MemberID from Loan where ID = LoanID)) AS '可用余额',PeNumber as '期数', LoanID, RePayTime as '应还款时间',datediff(dd,getdate(),repaytime) as '到期天数', SUM(RePrincipal) '应还本金(元)', SUM(ReInterest) '应还利息(元)', SUM(ReOverInterest) '应还逾期利息(元)', (select MemberName from Member where ID=(select MemberID from Loan where ID=LoanID)) '会员名', (select RealName from  MemberInfo where MemberID=(select MemberID  from Loan where ID = LoanID)) '真实姓名',  Case IsExtend when 0 then '未展期' when 1 then '已展期' end as IsExtendStr, Case Status when 0 then '未还' when 1 then '部分已还' when 2 then '全额已还' when 3 then '作废' end as '还款状态', (select Agency from dbo.Loan where ID = LoanID) as '分支机构'  from  RepaymentPlan where   OverStatus in (0,5) and status<2 and datediff(dd,getdate(),repaytime)>=0  group by PeNumber,LoanID,IsExtend,Status,RePayTime,datediff(dd,getdate(),repaytime)  ) select * from TempTable where RowID between 1 and " + int.MaxValue;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocal, CommandType.Text, sql);

            CreateExcel((ds == null ? new DataTable() : ds.Tables[0]), filePath);
            hc.Response.Write("{\"url\":\"/ExcelDownLoads/" + fileName + "\"}");
        }

        public void CreateExcel(DataTable dt, string filePath)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return;
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string title = "";

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                title += dt.Columns[i].ColumnName + "\t";
            }
            title = title.Substring(0, title.Length - 1) + "\n";
            sw.Write(title);

            foreach (DataRow row in dt.Rows)
            {

                string line = "";

                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格

                }

                line = line.Substring(0, line.Length - 1) + "\n";

                sw.Write(line);

            }
            sw.Close();
            fs.Close();
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}