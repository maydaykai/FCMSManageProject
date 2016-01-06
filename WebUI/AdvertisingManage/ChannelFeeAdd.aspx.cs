﻿/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 渠道费用编辑
Update: 
**************************************************************/
using DocumentFormat.OpenXml.Extensions;
using DocumentFormat.OpenXml.Packaging;
using ManageFcmsBll;
using System;
using System.Collections.Generic;
using System.Data;

namespace WebUI.AdvertisingManage
{
    public partial class ChannelFeeAdd : BasePage
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
    }
}