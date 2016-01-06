/*************************************************************
Author:		 卢侠勇
Create date: 2015-8-4
Description: 编辑客服回访信息
Update: 
**************************************************************/
using System;
using ManageFcmsBll;

namespace WebUI.ReportStatistics
{
    public partial class ReturnVisitEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var member = new MemberBll().GetModel(int.Parse(Request.QueryString["mid"]));
                if (member != null)
                {
                    this.txtMemberName.Value = member.MemberName;
                    this.txtMobile.Value = member.Mobile;
                    var memberInfo = new MemberInfoBll().GetModel(member.ID);
                    if (memberInfo != null)
                        this.txtRealName.Value = memberInfo.RealName;
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var record = this.selCurrStatus.Value;
            var notes = this.txtNotes.InnerText;
            if(ReportBll.Instance.AddReturnVisit(int.Parse(Request.QueryString["mid"]), this.MemberId, record, notes))
                ClientScript.RegisterClientScriptBlock(GetType(), "ok", "LHG_Tips('TZ', '回访成功', 2, 'success.png', 'Advertising.aspx?s=" + Request.QueryString["s"] + "&e=" + Request.QueryString["e"] + "&r=" + Request.QueryString["r"] + "&i=" + Request.QueryString["i"] + "&columnId=" + this.ColumnId + "&cr=" + Request.QueryString["cr"] + "&c=" + Request.QueryString["c"] + "&mina=" + Request.QueryString["mina"] + "&maxa=" + Request.QueryString["maxa"] + "');", true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "no", "LHG_Tips('TZ', '回访失败', 2, 'warning.png', 'ReturnVisitEdit.aspx?columnId=" + this.ColumnId + "');", true);
        }
    }
}