using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using ManageFcmsCommon;
using NPOI.HSSF.UserModel;

namespace WebUI.ReportStatistics
{
    public partial class DailyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                string nowDate = txtDownloadDate.Value.Trim();
                var memberBll = new MemberBll();
                var dt = memberBll.DailyReport(ConvertHelper.ToDateTime(nowDate));
                var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/DailyReportTempNOPI.xls"));

                if (dt != null && dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];
                    //var hssfworkbook = new HSSFWorkbook(stream);


                    var file = new FileStream(Server.MapPath("ExcelTeml/DailyReportTempNOPI.xls"), FileMode.Open, FileAccess.Read);//读入excel模板
                    var workbook = new HSSFWorkbook(file);
                    var sheet = (HSSFSheet)workbook.GetSheet("RJB_DailyReport");
                    sheet.GetRow(3).GetCell(2).SetCellValue(FormatDouble(dr["MemberBalance"].ToString()));
                    sheet.GetRow(3).GetCell(3).SetCellValue(FormatDouble(dr["BidFreezingTotal"].ToString()));
                    sheet.GetRow(3).GetCell(4).SetCellValue(FormatDouble(dr["PurchaseFrozenAmount"].ToString()));
                    sheet.GetRow(3).GetCell(5).SetCellValue(FormatDouble(dr["WithdrawCashFrozenAmount"].ToString()));
                    sheet.GetRow(3).GetCell(6).SetCellValue(FormatDouble(dr["SpecialAccount"].ToString()));
                    sheet.GetRow(3).GetCell(7).SetCellValue(FormatDouble(dr["PlatformAccount"].ToString()));
                    sheet.GetRow(5).GetCell(2).SetCellValue(FormatDouble(dr["RechargeAmount"].ToString()));
                    sheet.GetRow(5).GetCell(6).SetCellValue(FormatDouble(dr["RechargeAmount000"].ToString()));
                    sheet.GetRow(5).GetCell(7).SetCellValue(FormatDouble(dr["RechargeAmount777"].ToString()));
                    sheet.GetRow(6).GetCell(2).SetCellValue(FormatDouble(dr["NewSecPrincipal"].ToString()));
                    sheet.GetRow(6).GetCell(6).SetCellValue(FormatDouble(dr["NewSecPrincipal000"].ToString()));
                    sheet.GetRow(7).GetCell(2).SetCellValue(FormatDouble(dr["NewSecInterest"].ToString()));
                    sheet.GetRow(7).GetCell(6).SetCellValue(FormatDouble(dr["NewSecInterest000"].ToString()));
                    sheet.GetRow(8).GetCell(2).SetCellValue(FormatDouble(dr["RecommendReward"].ToString()));
                    sheet.GetRow(8).GetCell(7).SetCellValue(FormatDouble(dr["RecommendReward777"].ToString()));
                    sheet.GetRow(9).GetCell(2).SetCellValue(FormatDouble(dr["VipRedEnvelopes"].ToString()));
                    sheet.GetRow(9).GetCell(7).SetCellValue(FormatDouble(dr["VipRedEnvelopes777"].ToString()));
                    sheet.GetRow(10).GetCell(2).SetCellValue(FormatDouble(dr["RedEnvelopes"].ToString()));
                    sheet.GetRow(10).GetCell(7).SetCellValue(FormatDouble(dr["RedEnvelopes777"].ToString()));
                    sheet.GetRow(11).GetCell(2).SetCellValue(FormatDouble(dr["ActivityAward"].ToString()));
                    sheet.GetRow(11).GetCell(7).SetCellValue(FormatDouble(dr["ActivityAward777"].ToString()));
                    sheet.GetRow(12).GetCell(2).SetCellValue(FormatDouble(dr["BorrowerServices"].ToString()));
                    sheet.GetRow(12).GetCell(7).SetCellValue(FormatDouble(dr["BorrowerServices777"].ToString()));
                    sheet.GetRow(13).GetCell(2).SetCellValue(FormatDouble(dr["InvestorServices"].ToString()));
                    sheet.GetRow(13).GetCell(7).SetCellValue(FormatDouble(dr["InvestorServices777"].ToString()));
                    sheet.GetRow(14).GetCell(2).SetCellValue(FormatDouble(dr["CounterFee"].ToString()));
                    sheet.GetRow(14).GetCell(7).SetCellValue(FormatDouble(dr["CounterFee777"].ToString()));
                    sheet.GetRow(15).GetCell(2).SetCellValue(FormatDouble(dr["VIPAnnualFee"].ToString()));
                    sheet.GetRow(15).GetCell(7).SetCellValue(FormatDouble(dr["VIPAnnualFee777"].ToString()));
                    sheet.GetRow(16).GetCell(2).SetCellValue(FormatDouble(dr["ExperienceCoin"].ToString()));
                    sheet.GetRow(16).GetCell(7).SetCellValue(FormatDouble(dr["ExperienceCoin777"].ToString()));
                    sheet.GetRow(17).GetCell(2).SetCellValue(FormatDouble(dr["ExperienceCoinBack"].ToString()));
                    sheet.GetRow(17).GetCell(7).SetCellValue(FormatDouble(dr["ExperienceCoinBack777"].ToString()));
                    sheet.GetRow(18).GetCell(7).SetCellValue(FormatDouble(dr["BankVerificationFee"].ToString()));
                    sheet.GetRow(21).GetCell(2).SetCellValue(FormatDouble(dr["NewSecRollOutAmount"].ToString()));
                    sheet.GetRow(21).GetCell(6).SetCellValue(FormatDouble(dr["NewSecRollOutAmount000"].ToString()));
                    sheet.GetRow(22).GetCell(2).SetCellValue(FormatDouble(dr["BidFAmountFrozen"].ToString()));
                    sheet.GetRow(22).GetCell(3).SetCellValue(-FormatDouble(dr["BidFAmountFrozen"].ToString()));
                    sheet.GetRow(23).GetCell(2).SetCellValue(FormatDouble(dr["AssignmentFrozen"].ToString()));
                    sheet.GetRow(23).GetCell(4).SetCellValue(-FormatDouble(dr["AssignmentFrozen"].ToString()));
                    sheet.GetRow(24).GetCell(2).SetCellValue(FormatDouble(dr["WithdrawCashFrozen"].ToString()));
                    sheet.GetRow(24).GetCell(5).SetCellValue(-FormatDouble(dr["WithdrawCashFrozen"].ToString()));
                    sheet.GetRow(25).GetCell(2).SetCellValue(FormatDouble(dr["ABidFAmountFrozen"].ToString()));
                    sheet.GetRow(25).GetCell(3).SetCellValue(-FormatDouble(dr["ABidFAmountFrozen"].ToString()));
                    sheet.GetRow(26).GetCell(2).SetCellValue(FormatDouble(dr["AAssignmentFrozen"].ToString()));
                    sheet.GetRow(26).GetCell(4).SetCellValue(-FormatDouble(dr["AAssignmentFrozen"].ToString()));
                    sheet.GetRow(27).GetCell(2).SetCellValue(FormatDouble(dr["AWithdrawCashFrozen"].ToString()));
                    sheet.GetRow(27).GetCell(5).SetCellValue(-FormatDouble(dr["AWithdrawCashFrozen"].ToString()));
                    sheet.GetRow(28).GetCell(2).SetCellValue(FormatDouble(dr["RefundPurchaseGold"].ToString()));
                    sheet.GetRow(28).GetCell(4).SetCellValue(-FormatDouble(dr["RefundPurchaseGold"].ToString()));
                    sheet.GetRow(29).GetCell(2).SetCellValue(FormatDouble(dr["CashAmount"].ToString()));
                    sheet.GetRow(29).GetCell(7).SetCellValue(FormatDouble(dr["CashAmount777"].ToString()));
                    sheet.ForceFormulaRecalculation = true;
                    //Response.ContentType = "application/x-xls";
                    //Response.AddHeader("Content-Disposition", "attachment;filename=RJBDailyReport(" + ConvertHelper.ToDateTime(nowDate).ToString("yyyyMMdd") + ").xls");
                    //hssfworkbook.Write(Response.OutputStream);
                    //Response.End();


                    using (var ms = new MemoryStream())
                    {
                        workbook.Write(ms);
                        ms.Flush();
                        ms.Position = 0;
                        var data = ms.ToArray();
                        string filePath = "D:\\files\\ExcelReport\\" + ConvertHelper.ToDateTime(nowDate).ToString("yyyyMM");

                        if (!Directory.Exists(filePath))
                        {
                            var folder = Directory.CreateDirectory(filePath);
                        }
                        var fs = new FileStream(filePath + "\\RJB运营日报表(" + ConvertHelper.ToDateTime(nowDate).ToString("yyyyMMdd") + ").xls", FileMode.OpenOrCreate);
                        var w = new BinaryWriter(fs);
                        w.Write(data);
                        fs.Close();
                        ms.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                Log4NetHelper.WriteError(ex);
            }
        }

        private double FormatDouble(string drValue)
        {
            double doubV;
            double.TryParse(drValue, out doubV);
            return doubV;
        }
    }
}