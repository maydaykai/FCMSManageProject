/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-11
Description: 渠道号管理
Update: 
**************************************************************/
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using ManageFcmsCommon;
using System;
using System.Data;

namespace WebUI.AdvertisingManage
{
    public partial class ChannelManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var data = AdvertisementBLL.Instance.GetChannelList();
                DataRow dr = data.NewRow();
                dr["Id"] = -1;
                dr["Channel"] = "--渠道类型--";
                data.Rows.InsertAt(dr, 0);
                this.sel_channel.DataSource = data;
                this.sel_channel.DataTextField = "Channel";
                this.sel_channel.DataValueField = "Id";
                this.sel_channel.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
                    this.sel_channel.SelectedValue = Request.QueryString["cid"];
            }
        }

        protected void ExcelExport1_Click(object sender, EventArgs e)
        {
            var filter = string.Empty;
            if (this.sel_channel.SelectedValue.Trim() != "-1")
                filter += " AND DC.Id=" + this.sel_channel.SelectedValue.Trim();
            if (!string.IsNullOrEmpty(this.txtSecondary.Value.Trim()))
                filter += " AND DCA.classifyName LIKE '%" + this.txtSecondary.Value.Trim() + "%'";

            var stream = AbstractReader.Copy(Server.MapPath("ExcelTeml/ChannelManageTemp.xlsx"));
            var doc = SpreadsheetDocument.Open(stream, true);
            var worksheetPart = SpreadsheetReader.GetWorksheetPartByName(doc, "Sheet1");
            var writer = new WorksheetWriter(doc, worksheetPart);
            SpreadsheetStyle style = SpreadsheetReader.GetDefaultStyle(doc);
            int total;
            var dt = AdvertisementBLL.Instance.GetChannelList(filter, 1, 50000, out total);
            int i = 2;
            foreach (DataRow dr in dt.Rows)
            {
                writer.PasteText("A" + i, dr["rowNum"].ToString(), style);
                writer.PasteText("B" + i, dr["Channel"].ToString(), style);
                writer.PasteText("C" + i, dr["classifyName"].ToString(), style);
                writer.PasteText("D" + i, dr["fClassifyName"].ToString(), style);
                writer.PasteText("E" + i, "channel=" + dr["id"].ToString() + "&channelRemark=" + dr["fId"].ToString(), style);
                i++;
            }
            SpreadsheetWriter.Save(doc);

            Response.Clear();
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", "bp_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx"));
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            stream.WriteTo(Response.OutputStream);
            Response.End();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (fileUp.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "keys1", "<script>MessageAlert('请您选择Excel文件', 'warning', '');</script> ");
                    return;//当无文件时,返回
                }

                string filename = "Sheet1";              //获取Execle文件名  DateTime日期函数
                string savePath = Server.MapPath(("upfiles\\") + fileUp.FileName);//Server.MapPath 获得虚拟服务器相对路径
                string fields = "一级分类,二级分类,三级分类";
                fileUp.SaveAs(savePath);                        //SaveAs 将上传的文件内容保存在服务器上
                DataSet ds = ConvertHelper.ExcelDataSource(savePath, filename, fields);           //调用自定义方法           
                var msg = AdvertisementBLL.Instance.ImportChannel(ds.Tables[0]).Split('|');
                string[] msgE = { "success", "error", "warning" };
                ClientScript.RegisterStartupScript(this.GetType(), "keys", "<script>MessageAlert('" + msg[1] + "', '" + msgE[int.Parse(msg[0]) - 1] + "', '');</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "keys1", "<script>MessageAlert('" + ex.Message + "', 'warning', '');</script> ");
            }
        }
    }
}