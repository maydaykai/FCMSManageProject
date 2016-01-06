using ManageFcmsCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebUI.ReportStatistics
{
    public partial class TotalMarketRepNewDetailed : System.Web.UI.Page
    {
        private string _memberName;
        private string _date;


        protected void Page_Load(object sender, EventArgs e)
        {

           
            if(!IsPostBack)
            {//column + "=" + value+"&time='"+data.Createtime+"'&memberName='"+data.MemberName+"'";
                _memberName = ConvertHelper.QueryString(Request, "memberName", "");
                _date = ConvertHelper.QueryString(Request, "time", "");
                //2015-09-30
                DateTime _end = Convert.ToDateTime(_date);
                DateTime curr_days = Convert.ToDateTime(_end.ToString("yyyy-MM") + "-01");
                DateTime endtime = curr_days.AddMonths(1).AddDays(-1);
                string nexttime = curr_days.AddMonths(-1).ToString("yyyy-MM-dd");



                start_time.Value = curr_days.ToString("yyyy-MM-dd HH:mm:ss");
                end_time.Value = _date;
                hidden_memberName.Value = _memberName;
            }
        }


      
    }
}