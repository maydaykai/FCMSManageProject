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

namespace WebUI.HanderAshx.Basic
{
    /// <summary>
    /// ImportExcelHandler 的摘要说明
    /// </summary>
    public class ImportExcelHandler : IHttpHandler
    {
        private int _sign = 0;
        private string _smsContent = "";
        private string _filePath;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AddHeader("Pragma", "No-Cache");
            context.Response.ContentType = "text/json";
            _sign = ConvertHelper.QueryString(context.Request, "sign", 0);
            _smsContent = ConvertHelper.QueryString(context.Request, "smsContent", "");
            _filePath = ConvertHelper.QueryString(context.Request, "filePath", "");
            _filePath = HttpContext.Current.Server.UrlDecode(_filePath);
            _smsContent = HttpContext.Current.Server.UrlDecode(_smsContent);
            context.Response.Write(GetJson(_sign));
        }


        private Object GetJson(int type)
        {
            DataTable dt = ImportingExcelData(_filePath);
            
            if (type == 0)
            {
                var s = JsonHelper.DataTableToJson(dt);
                return s;
            }
            else if (type == 1)
            {
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return "{\"Result\":\"warning\", \"Message\":\"请先导入数据\"}";
                }
                if (string.IsNullOrEmpty(_smsContent))
                {
                    return "{\"Result\":\"warning\", \"Message\":\"请输入短信内容\"}";
                }
                if (_smsContent.Length > 100)
                {
                    return "{\"Result\":\"warning\", \"Message\":\"短信内容最大不得超过100个字符\"}";
                }
                var smsConyent = HtmlHelper.DeleteHtml(_smsContent);
                try
                {
                    var connection = new SqlConnection(SqlHelper.ConnectionStringLocal);
                    var sqlDataAdapter = new SqlDataAdapter();
                    var insertcommand = new SqlCommand(@"INSERT INTO [SMS]([Mobile],[SmsContent],[SmsType]) VALUES (@Mobile,'【融金宝】" + smsConyent + "',2)", connection);
                    insertcommand.Parameters.Clear();
                    insertcommand.Parameters.Add("@Mobile", SqlDbType.VarChar, 20, "Mobile");

                    sqlDataAdapter.InsertCommand = insertcommand;
                    var countSuccess = sqlDataAdapter.Update(dt);

                    var result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocal, CommandType.Text, "INSERT INTO [dbo].[BatSmsHistory]([SmsContent],[OpUID],[SendCount]) values('" + smsConyent + "',1," + countSuccess + ")");
                    insertcommand.Dispose();
                    connection.Close();
                    return "{\"Result\":\"success\", \"Message\":\"发送成功" + countSuccess + "条短信\"}"; ;

                }
                catch (Exception)
                {
                    return "{\"Result\":\"error\", \"Message\":\"发送失败，请联系技术人员\"}";
                }
            }
            return "{\"Result\":\"error\", \"Message\":\"操作异常，请联系技术人员\"}";
        }




        private const string A = "SerialNumber", B = "Mobile";


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
            for (var i = 0; i < rows.GetRowsCount(); i++)
            {
                var row = dt.NewRow();
                int rowIndex = 1 + i;
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
        private DataTable ImportingExcelData(string filePath)
        {
            try
            {
                using (var document = SpreadsheetDocument.Open(filePath, false))
                {
                    var worksheet = document.GetWorksheet();
                    var rows = worksheet.Descendants<Row>().ToList();
                    var sharedStringTable = document.GetSharedStringTable();
                    // 读取Excel中的数据
                    return ReadExcelData(rows, sharedStringTable);

                }
            }
            catch (Exception exx)
            {
                Log4NetHelper.WriteError(exx);
                return null;
            }
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}