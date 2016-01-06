using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ManageFcmsCommon;
using ManageFcmsConn;

namespace WebUI.HanderAshx.UserManage
{
    /// <summary>
    /// SmsSendByExcelHandler 的摘要说明
    /// </summary>
    public class SmsSendByExcelHandler : IHttpHandler
    {
        protected DataTable DataTemp = new DataTable();
        private string _filePath = "";
        private string _smsContent = "";
        private string _sendTime = "";
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _filePath = ConvertHelper.QueryString(context.Request, "filePath", "");
            _smsContent = ConvertHelper.QueryString(context.Request, "smsContent", "");
            _sendTime = ConvertHelper.QueryString(context.Request, "sendTime", "");
            context.Response.Write(SmsSendByExcel(_filePath));

        }

        private string SmsSendByExcel(string excelPath)
        {
            _smsContent = HttpContext.Current.Server.UrlDecode(_smsContent);
            if (string.IsNullOrEmpty(excelPath))
            {
                return "{\"msg\":\"非常抱歉，上传的文件已过期或丢失，请重新上传。\",\"icon\":\"warning\"}";
            }
            if (_smsContent == null || string.IsNullOrEmpty(_smsContent.Trim()))
            {
                return "{\"msg\":\"请填写短信内容。\",\"icon\":\"warning\"}";
            }

            if (_smsContent.Trim().Length > 200)
            {
                return "{\"msg\":\"短信内容过长，最长不超过200个字符。\",\"icon\":\"warning\"}";
            }

            ImportingExcelData(excelPath);
            if (DataTemp == null || DataTemp.Rows.Count <= 0)
            {
                return "{\"msg\":\"非常抱歉，读取不到上传文件的数据，请重新上传。\",\"icon\":\"warning\"}";
            }

            try
            {
                var smsConyent = HtmlHelper.DeleteHtml(_smsContent.Trim());
                var sTime = string.IsNullOrEmpty(_sendTime.Trim()) ? DateTime.Now : ConvertHelper.ToDateTime(_sendTime.Trim());
                var connection = new SqlConnection(SqlHelper.ConnectionStringLocal);
                var sqlDataAdapter = new SqlDataAdapter();
                var insertcommand = new SqlCommand(@"INSERT INTO [SMS]([Mobile],[SmsContent],[SmsType],SendTime,[SendStatus]) VALUES (@Mobile,'" + smsConyent + "【融金宝】',2,'" + sTime + "',0)", connection);
                insertcommand.Parameters.Clear();
                insertcommand.Parameters.Add("@Mobile", SqlDbType.VarChar, 20, "手机号码");
                insertcommand.CommandTimeout = 300;
                sqlDataAdapter.InsertCommand = insertcommand;
                var countSuccess = sqlDataAdapter.Update(DataTemp);
                if (countSuccess > 0)
                {
                    DataTemp.Clear();
                    var result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, "INSERT INTO [dbo].[BatSmsHistory]([SmsContent],[OpUID],[SendCount]) values('" + smsConyent + "',1," + countSuccess + ")");
                    insertcommand.Dispose();
                    connection.Close();
                    return "{\"msg\":\"恭喜您，已成功加入<span style='color:#ff0000'> " + countSuccess + " </span>条短信至短信队列。\",\"icon\":\"success\"}";
                }
                return "{\"msg\":\"操作失败，请联系技术人员。\",\"icon\":\"warning\"}";
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return "{\"msg\":\"操作异常，请联系技术人员。\",\"icon\":\"warning\"}";
            }
        }




        private const string A = "序号", B = "手机号码";


        /// <summary>
        /// 读取Excel数据
        /// </summary>
        private static DataTable ReadExcelData(List<Row> rows, SharedStringTablePart sharedStringTable)
        {
            var dt = CreateDataTable();
            ReadExcelRows(rows, sharedStringTable, dt);
            return dt;
        }

        private static void ReadExcelRows(List<Row> rows, SharedStringTablePart sharedStringTable, DataTable dt)
        {
            for (var i = 0; i < rows.GetRowsCount() - 1; i++)
            {
                var row = dt.NewRow();
                int rowIndex = 2 + i;
                var cells = rows.GetCells(rowIndex);
                row[A] = 1 + i;
                row[B] = cells.GetCellValue("A" + rowIndex, sharedStringTable);
                dt.Rows.Add(row);
            }
        }

        private static DataTable CreateDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add(A, typeof(string));
            dt.Columns.Add(B, typeof(string));
            return dt;
        }

        /// <summary>
        /// 导入Excel数据
        /// </summary>
        private void ImportingExcelData(string filePath)
        {
            try
            {
                using (var document = SpreadsheetDocument.Open(filePath, false))
                {
                    var worksheet = document.GetWorksheet();
                    var rows = worksheet.Descendants<Row>().ToList();
                    var sharedStringTable = document.GetSharedStringTable();
                    // 读取Excel中的数据
                    DataTemp = ReadExcelData(rows, sharedStringTable);

                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
            }
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