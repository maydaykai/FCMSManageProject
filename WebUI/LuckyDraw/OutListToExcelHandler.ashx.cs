using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LuckyDraw.Business;
using LuckyDraw.Model;
using LuckyDraw.BusinessModel;

namespace WebUI.LuckyDraw
{
    /// <summary>
    /// OutListToExcelHandler 的摘要说明
    /// </summary>
    public class OutListToExcelHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "application/x-xls";
            context.Response.AddHeader("Content-Disposition", "attachment;filename=MemberList.xls");
            int id = Convert.ToInt32(context.Request["id"]);
            HSSFWorkbook hssfWorkbook = OutputSearchResult(id);
            hssfWorkbook.Write(context.Response.OutputStream);
        }
        public HSSFWorkbook OutputSearchResult(int id)
        {
            BizSweepstakeRecord bs = new BizSweepstakeRecord();
            List<AwardWinnerModel> list = bs.GetAllBySId(id);

            HSSFWorkbook hssfWorkbook = new HSSFWorkbook();
            ISheet sheet = hssfWorkbook.CreateSheet("会员列表");
            IRow rowHeader = sheet.CreateRow(0);
            rowHeader.CreateCell(0, CellType.STRING).SetCellValue("会员名");
            rowHeader.CreateCell(1, CellType.STRING).SetCellValue("真实姓名");
            rowHeader.CreateCell(2, CellType.STRING).SetCellValue("联系方式");
            rowHeader.CreateCell(3, CellType.STRING).SetCellValue("奖品");
            rowHeader.CreateCell(4, CellType.STRING).SetCellValue("中奖时间");

            for (int i = 0; i < list.Count; i++)
            {
                IRow dataRow = sheet.CreateRow(i + 1);
                dataRow.CreateCell(0, CellType.STRING).SetCellValue(list[i].MemberName);
                dataRow.CreateCell(1, CellType.STRING).SetCellValue(list[i].RealName);
                dataRow.CreateCell(2, CellType.STRING).SetCellValue(list[i].Phone);
                dataRow.CreateCell(3, CellType.STRING).SetCellValue(list[i].PrizeName);
                dataRow.CreateCell(4, CellType.STRING).SetCellValue(list[i].CreateDate.ToString());
            }
            return hssfWorkbook;
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