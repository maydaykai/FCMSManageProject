using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ManageFcmsCommon;
using ManageFcmsConn;
using System.IO;

namespace WebUI.SmsManage
{
    public partial class SmsMass : BasePage
    {
        protected DataTable DataTemp = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}