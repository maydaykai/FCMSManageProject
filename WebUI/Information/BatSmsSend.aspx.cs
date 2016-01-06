using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;

namespace WebUI.Information
{
    public partial class BatSmsSend : BasePage
    {
        private int _userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            _userId = ConvertHelper.ToInt(SessionHelper.Get("FcmsUserId").ToString());
            if (!IsPostBack)
            {
                textReviewComments.InnerText = "";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (textReviewComments.InnerText.Length > 190)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "<script type='text/javascript'>alert('短信内容超出长度！');</script>");
                return;
            }
            if (textReviewComments.InnerText.Trim().Length == 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "<script type='text/javascript'>alert('短信内容超出长度！');</script>");
                return;
            }
            int chooseAll, noBid, canUseBalance, noBidMonth, canBidAmount;
            decimal useBalance = 0, useBalance1 = 0, bidAmount = 0;
            DateTime sendTime;
            chooseAll = radio_no.Checked == true ? 1 : 0;
            noBid = ck_nobid.Checked == true ? 1 : 0;
            canUseBalance = ck_canusebalance.Checked == true ? 1 : 0;
            noBidMonth = ck_nobidmonth.Checked == true ? 1 : 0;
            canBidAmount = ck_bidamount.Checked == true ? 1 : 0;

            if (!string.IsNullOrEmpty(txt_canusebalance.Value))
            {
                useBalance = Convert.ToDecimal(txt_canusebalance.Value);
                if (!string.IsNullOrEmpty(txt_canusebalance1.Value))
                {
                    useBalance1 = Convert.ToDecimal(txt_canusebalance1.Value);
                    if (useBalance1 < useBalance)
                    {
                        useBalance1 = useBalance;
                    }
                }
                else
                {
                    useBalance1 = useBalance;
                }
            }

            if (!string.IsNullOrEmpty(txt_bidamount.Value))
            {
                bidAmount = Convert.ToDecimal(txt_bidamount.Value);
            }
            if (!string.IsNullOrEmpty(txtSendTime.Value))
            {
                sendTime = Convert.ToDateTime(txtSendTime.Value);
            }
            else
            {
                sendTime = DateTime.Now;
            }


            ProcBatSmsSend bss = new ProcBatSmsSend();
            ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                                  bss.BatSmsSend(textReviewComments.InnerText, chooseAll, noBid, canUseBalance, useBalance, useBalance1, noBidMonth, canBidAmount, bidAmount, _userId, sendTime)
                                                                       ? "<script type='text/javascript'>alert('短信插入发送队列成功,稍后将自动发送！');</script>"
                                                                       : "<script type='text/javascript'>alert('操作失败！');</script>");
        }
    }
}