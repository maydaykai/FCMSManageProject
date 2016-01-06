/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-18
Description: 渠道效果分析
Update: 
**************************************************************/
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System;
using System.Data;

namespace WebUI.AdvertisingManage
{
    public partial class ChannelEffectAnalyze : BasePage
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
            }
        }
    }
}