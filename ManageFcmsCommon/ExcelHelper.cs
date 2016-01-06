using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;

namespace ManageFcmsCommon
{
    public class ExcelHelper
    {
        #region  DataTable导出到Excel模板（单个类别）

        /// <summary>
        /// DataTable导出到Excel模板（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strFileName">生成的文件路径、名称</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识</param>
        /// <param name="titleName">表头名称</param>
        /// <param name="rowIndex">行索引（从第几行开始写数据）</param>
        public static void ExportExcelForDtByNpoi(DataTable dtSource, string strFileName, string strTemplateFileName, int flg, string titleName, int rowIndex)
        {
            // 利用模板，DataTable导出到Excel（单个类别）
            using (MemoryStream ms = ExportExcelForDtByNpoi(dtSource, strTemplateFileName, flg, titleName, rowIndex))
            {
                byte[] data = ms.ToArray();
                //var fs = new FileStream("D:\\" + strFileName + ".xls", FileMode.OpenOrCreate);
                //var w = new BinaryWriter(fs);
                //w.Write(data);
                //fs.Close();
                //ms.Close();
                #region 客户端保存

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                //Encoding pageEncode = Encoding.GetEncoding(PageEncode);
                response.Charset = "UTF-8";
                response.ContentType = "application/vnd-excel";//"application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + strFileName));
                HttpContext.Current.Response.BinaryWrite(data);

                #endregion
            }
        }

        /// <summary>
        /// DataTable导出到Excel模板（单个类别）
        /// </summary>
        /// <param name="dtSource">DataTable</param>
        /// <param name="strTemplateFileName">模板的文件路径、名称</param>
        /// <param name="flg">文件标识(Excel Sheet名称)</param>
        /// <param name="titleName">表头名称</param>
        /// <param name="rIndex">起始行(排除EXCEL表头)</param>
        /// <returns></returns>
        private static MemoryStream ExportExcelForDtByNpoi(DataTable dtSource, string strTemplateFileName, int flg, string titleName, int rIndex)
        {
            const int totalIndex = 65534;           // EXCEL总行数(office2007-2003目前最大行数65535)
            int rowIndex = rIndex;                       // 起始行(排除EXCEL表头)
            int dtRowIndex = dtSource.Rows.Count;   // DataTable的数据行数

            var file = new FileStream(strTemplateFileName, FileMode.Open, FileAccess.Read);//读入excel模板
            var workbook = new HSSFWorkbook(file);
            string sheetName = "";
            switch (flg)
            {
                case 1:
                    sheetName = "Sheet1";
                    break;
            }
            var sheet = workbook.GetSheet(sheetName);

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = " 深圳融金宝互联网金融服务有限公司";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = " 深圳融金宝互联网金融服务有限公司"; //填加xls文件作者信息
                si.ApplicationName = " 深圳融金宝互联网金融服务有限公司"; //填加xls文件创建程序信息
                si.LastAuthor = " 深圳融金宝互联网金融服务有限公司"; //填加xls文件最后保存者信息
                si.Comments = " 深圳融金宝互联网金融服务有限公司"; //填加xls文件作者信息
                si.Title = titleName; //填加xls文件标题信息
                si.Subject = titleName;//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            //#region 表头
            //HSSFRow headerRow = sheet.GetRow(0);
            //HSSFCell headerCell = headerRow.GetCell(0);
            //headerCell.SetCellValue(titleName);
            //#endregion

            // 隐藏多余行
            for (int i = rowIndex + dtRowIndex; i < totalIndex; i++)
            {
                HSSFRow dataRowD = (HSSFRow)sheet.GetRow(i);
                if (dataRowD == null) break;
                dataRowD.Height = 0;
                dataRowD.ZeroHeight = true;
                //sheet.RemoveRow(dataRowD);//这样操作很慢
            }

            foreach (DataRow row in dtSource.Rows)
            {
                #region 填充内容

                HSSFRow dataRow = (HSSFRow)sheet.GetRow(rowIndex) ?? (HSSFRow)sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列（0开始）
                foreach (DataColumn column in dtSource.Columns)
                {
                    if (columnIndex >= dtSource.Columns.Count)
                        break;

                    HSSFCell newCell = (HSSFCell)dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = (HSSFCell)dataRow.CreateCell(columnIndex);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    columnIndex++;
                }
                #endregion

                rowIndex++;
            }

            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            #region 清理cell值是0的（若非必要不必清理）
            //System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //int cellCount = headerRow.LastCellNum;

            //for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //{
            //    HSSFRow row = sheet.GetRow(i);
            //    if (row != null)
            //    {
            //        for (int j = row.FirstCellNum; j < cellCount; j++)
            //        {
            //            HSSFCell c = row.GetCell(j);
            //            if (c != null)
            //            {
            //                switch (c.CellType)
            //                {
            //                    case HSSFCellType.NUMERIC:
            //                        if (c.NumericCellValue == 0)
            //                        {
            //                            c.SetCellType(HSSFCellType.STRING);
            //                            c.SetCellValue(string.Empty);
            //                        }
            //                        break;
            //                    case HSSFCellType.BLANK:

            //                    case HSSFCellType.STRING:
            //                        if (c.StringCellValue == "0")
            //                        { c.SetCellValue(string.Empty); }
            //                        break;

            //                }
            //            }
            //        }

            //    }

            //}
            #endregion

            using (var ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;
                //sheet.Dispose();
                //workbook.Dispose();//释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        #endregion
    }
}
