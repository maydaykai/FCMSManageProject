using System;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ManageFcmsCommon
{
    /// <summary>
    ///     分页控件
    /// </summary>
    [ToolboxData("<{0}:Pagination runat=server></{0}:Pagination>")]
    //定义一个方法调用就行了
    public class Pagination : WebControl, IPostBackEventHandler, INamingContainer
    {
        #region  PostBack Stuff

        private static readonly object EventCommand = new object();


        public void RaisePostBackEvent(string eventArgument)
        {
            OnPageChanged(new CommandEventArgs(UniqueID, Convert.ToInt32(eventArgument)));
        }

        public event CommandEventHandler PageChanged
        {
            add { Events.AddHandler(EventCommand, value); }
            remove { Events.RemoveHandler(EventCommand, value); }
        }

        protected virtual void OnPageChanged(CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            CurrentPage = currnetPageIndx;
            var clickHandler = (CommandEventHandler) Events[EventCommand];
            if (clickHandler != null) clickHandler(this, e);
        }

        #endregion

        /// <summary>
        ///     显示页数
        /// </summary>
        private int _showPages = 3;


        /// <summary>
        ///     一页条数
        /// </summary>
        /// <summary>
        ///     一页条数
        /// </summary>
        [DefaultValue(10), Category("分页设置"), Description("每一笔的记录数")]
        public int PageSize
        {
            get
            {
                if (DesignMode)
                    return 10;
                object o = ViewState["PageSize"];
                if ((o != null) && (o.ToString().Length > 0))
                {
                    return Convert.ToInt32(o);
                }
                return 10;
                // return _currentIndex;
            }
            // set { _currentIndex = value; }
            set { ViewState["PageSize"] = value; }
        }

        /// <summary>
        ///     当前页码
        /// </summary>
        [DefaultValue(1), Category("分页设置"), Description("当前页码")]
        public int CurrentPage
        {
            get
            {
                if (DesignMode)
                    return 1;
                object o = ViewState["aspnetCurrentPage"];
                if ((o != null) && (o.ToString().Length > 0))
                {
                    return Convert.ToInt32(o);
                }
                return 1;

                // return _currentIndex;
            }
            // set { _currentIndex = value; }
            set { ViewState["aspnetCurrentPage"] = value; }
        }

        /// <summary>
        ///     总条数
        /// </summary>
        [DefaultValue(1), Category("分页设置"), Description("总记录数")]
        public int TotalRecords
        {
            get
            {
                if (DesignMode)
                    return 50;
                if (ViewState["aspnetTotalRecords"] == null)
                {
                    ViewState["aspnetTotalRecords"] = 1;
                    return 1;
                }
                return Convert.ToInt32(ViewState["aspnetTotalRecords"]);
                // return _currentIndex;
            }
            // set { _currentIndex = value; }
            set
            {
                if (value < 0)
                {
                    throw new Exception("记录总条数不能为负数");
                }
                ViewState["aspnetTotalRecords"] = value;
            }
        }

        /// <summary>
        ///     总页数
        /// </summary>
        [DefaultValue(0), Category("默认数据"), Description("总页数"), ReadOnly(true)]
        public int PageCount
        {
            get
            {
                if (DesignMode)
                {
                    return 10;
                }
                if (TotalRecords%PageSize > 0)
                {
                    return (TotalRecords/PageSize) + 1;
                }
                return (TotalRecords/PageSize);
            }
        }

        /// <summary>
        ///     分页导航显示页码按钮数
        /// </summary>
        [DefaultValue(10), Category("分页设置"), Description("分页导航按钮显示个数")]
        public int ShowPages
        {
            set {
                _showPages = value > 0 ? value : 10;
            }

            get
            {
                return _showPages <= 0 ? 10 : _showPages;
            }
        }

        [DefaultValue("pager")]
        public override string CssClass
        {
            get
            {
                //return string.IsNullOrEmpty(ViewState["CssClass"].ToString()) ? "pager" : ViewState["CssClass"].ToString();
                if (ViewState["CssClass"] == null)
                {
                    ViewState["CssClass"] = "pager";
                    return "pager";
                }
                return ViewState["CssClass"].ToString();
            }
            set { base.CssClass = value; }
        }


        [DefaultValue("right")]
        [Browsable(true)]
        public string Align
        {
            get
            {
                if (ViewState["Align"] == null)
                {
                    ViewState["Align"] = "right";
                    return "right";
                }
                return ViewState["Align"].ToString();
            }
            set { ViewState["Align"] = value; }
        }

        [DefaultValue(false)]
        [Browsable(true)]
        [Description("是否显示下列页面导航")]
        public bool ShowDropDown
        {
            get
            {
                if (ViewState["ShowDropDown"] == null)
                {
                    ViewState["ShowDropDown"] = false;
                    return false;
                }
                return bool.Parse(ViewState["ShowDropDown"].ToString());
            }
            set { ViewState["ShowDropDown"] = value; }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string leftInfo;
            var centerInfo = new StringBuilder();

            //分页条分三部分，leftInfo是最左边的部分，用来显示当前页/总页数，每页显示的记录条数
            leftInfo = RenderLeft();

            //中间的部分是分页部分
            int min; //要显示的页面数最小值
            int max; //要显示的页面数最大值

            if (CurrentPage > PageCount) //当前页必须小于最大页
            {
                CurrentPage = PageCount;
            }

            if (CurrentPage%ShowPages == 0) //如果恰好整除
            {
                min = CurrentPage + 1;
                max = CurrentPage + ShowPages;
            }
            else if (CurrentPage%ShowPages == 1 && CurrentPage > ShowPages)
            {
                min = ((CurrentPage/ShowPages) - 1)*ShowPages + 1;
                max = CurrentPage - 1;
            }
            else
            {
                min = (CurrentPage/ShowPages)*ShowPages + 1;
                max = ((CurrentPage/ShowPages) + 1)*ShowPages;
            }


            string numberStr = ""; //循环生成数字序列部分  

            for (int i = min; i <= max; i++)
            {
                if (i > PageCount) continue;
                if (CurrentPage == i) //如果是当前页，用斜体和红色显示
                {
                    numberStr = numberStr + "<span class=\"current\">" + i.ToString() + "</span>";
                }
                else
                {
                    numberStr = numberStr + "<span><a href=" +
                                Page.ClientScript.GetPostBackClientHyperlink(this, i.ToString()) + ">" +
                                i.ToString() + "</a></span>";
                }
            }


            //第一页，上一页，下一页,最后一页

            //if(this.TotalRecords>this.PageSize)               
            //centerInfo.AppendFormat("{0}{1}{2}{3}{4}", this.RenderFirst(), this.RenderBack(), numberStr, RenderNext(), RenderLast());

            //update by mwmao 2010-03-17
            centerInfo.AppendFormat("{0}{1}{2}{3}", RenderFirst(), RenderBack(), RenderNext(), RenderLast());


            var drop = new StringBuilder();
            if (ShowDropDown)
            {
                drop.AppendFormat(
                    "<select name='__joyosoft_ui_pagination_select' onchange=\"__doPostBack('" + ClientID +
                    "',this.options[this.selectedIndex].value)\">", PageCount);
                //drop.Append("<option >--请选择页码--</option>");
                //下拉框
                for (int i = 1; i <= PageCount; i++)
                {
                    drop.AppendFormat(
                        i == CurrentPage
                            ? "<option selected='selected' value='{0}'>{0}</option>"
                            : "<option value='{0}'>{0}</option>", i);
                }
                drop.Append("</select>");
            }
            //HTML字符串
            var sb = new StringBuilder();
            sb.AppendFormat("<div class='{3}' style='text-align:{4};'><SPAN class='contenspan'>" +
                            "{0}" +
                            "{1}" +
                            "{2}" +
                            "</SPAN><div style='clear:both;'></div></div>", leftInfo,
                            centerInfo,
                            drop,
                            CssClass, Align);

            writer.Write(sb.ToString());
            //base.Render(writer);
        }

        #region

        private string RenderLeft()
        {
            string _temp = "";
            if (PageCount == 0)
                CurrentPage = 0;
            _temp = "<span class='text'>共 " + TotalRecords.ToString() + " 条</span><span class='text'> " +
                    CurrentPage.ToString() + "/" + PageCount.ToString() + " 页 </span>";
            return _temp;
        }

        /// <summary>
        ///     首页
        /// </summary>
        /// <returns></returns>
        private string RenderFirst()
        {
            if ((CurrentPage - 1) <= 0)
                return "<span class='disabled'>首页 </span>";
            string templateli = "<span><a href=\"{0}\" title=\"返回首页\">首页</a></span>";
            return String.Format(templateli, Page.ClientScript.GetPostBackClientHyperlink(this, "1"));
        }

        /// <summary>
        ///     上一页
        /// </summary>
        /// <returns></returns>
        private string RenderBack()
        {
            if ((CurrentPage - 1) <= 0)
                return "<span class='disabled'>上一页</span>";
            string templateli = "<span><a href=\"{0}\" title=\"上一页 " + (CurrentPage - 1).ToString() +
                                "\">上一页</a></span>";
            return String.Format(templateli,
                                 Page.ClientScript.GetPostBackClientHyperlink(this, (CurrentPage - 1).ToString()));
        }

        /// <summary>
        ///     下一页
        /// </summary>
        /// <returns></returns>
        private string RenderNext()
        {
            if (CurrentPage == PageCount)
                return "<span class='disabled'>下一页</span>";
            string templateli = "<span><a href=\"{0}\" title=\"下一页 " + (CurrentPage + 1).ToString() +
                                "\">下一页</a></span>";
            return String.Format(templateli,
                                 Page.ClientScript.GetPostBackClientHyperlink(this, (CurrentPage + 1).ToString()));
        }

        /// <summary>
        ///     尾页
        /// </summary>
        /// <returns></returns>
        private string RenderLast()
        {
            if (CurrentPage == PageCount)
                return "<span class='disabled last'>尾页</span>";
            string templateli = "<span class='last'><a href=\"{0}\" title=\"尾页\">尾页</a></span>";
            return String.Format(templateli, Page.ClientScript.GetPostBackClientHyperlink(this, PageCount.ToString()));
        }

        #endregion
    }
}