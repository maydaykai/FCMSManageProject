<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LowerLevelReward.aspx.cs" Inherits="WebUI.ReportStatistics.LowerLevelReward" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>推荐下级奖励报表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/select2css.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcheckbox.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdropdownlist.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        #searchbar span {
            height: 29px;
            text-indent: 5px;
            line-height: 29px;
        }

        .selectDiv .select_box {
            width: 85px;
        }

        .selectDiv2 .select_box {
            width: 105px;
        }
    </style>
    <script type="text/javascript">
        function param(name, value) {
            this.name = name;
            this.value = value;
        }
        var url = window.location.href;
        var p = url.split("?");
        var all = new Array();
        var params = p.length > 1 ? p[1].split("&") : new Array();
        for (var i = 0; i < params.length; i++) {
            var pname = params[i].split("=")[0];
            var pvalue = params[i].split("=")[1];
            all[i] = new param(pname, pvalue);
        }
        //获取参数值
        function getPValueByName(name) {
            for (var i = 0; i < all.length; i++) {
                if (all[i].name == name)
                    return all[i].value;
            }
        }
        
        var api = frameElement.api, W = api.opener;

        function opdg(w, h, url)
        {
            W.$.dialog({
                /*标题*/
                title: '<span class="icon icon-36" style="margin-top:5px;">&nbsp;</span>',
                /*宽度*/
                width: w,
                /*高度*/
                height: h,
                /*内容*/
                content: "url:" + url,
                /*最大化按钮*/
                max: false,
                /*最小化按钮*/
                min: false,
                /*取消*/        //cancel: true,
                /*静止定位*/
                fixed: true,
                /*锁屏*/
                lock: true,
                /*禁止拖动*/
                drag: true,
                /*改变大小*/
                resize: false,
                parent:api
                //ok:function(){if(mURL!=""){window.location.href=""+mURL+"";}}
            });
        }
	
        $(function () {
            var sYear = getPValueByName("sYear");
            var sMonth = getPValueByName("sMonth");
            var eYear = getPValueByName("eYear");
            var eMonth = getPValueByName("eMonth");
            var memberId = getPValueByName("memberId");
            $("#startDate").val(sYear + "年" + sMonth + "月");
            $("#endDate").val(eYear + "年" + eMonth + "月");
            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
            //主题
            var theme = "arctic";

            //参数组装
            function buildQueryString(data) {
                var str = ''; for (var prop in data) {
                                  if (data.hasOwnProperty(prop)) {
                                      str += prop + '=' + data[prop] + '&';
                                  }
                              }
                return str.substr(0, str.length - 1);
            }

            var formatedData = '';
            var lower = getPValueByName("LowerLevel");
            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/ReCommendRewardHandler.ashx?_sign=-1&lowerLevel="' + lower + '"',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'MemberId', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'SumInterest', type: 'number' },
                    { name: 'RewardRate', type: 'number' },
                    { name: 'Reward', type: 'number' },
                    { name: 'TotalDate', type: 'date' },
                    { name: 'LowerLevel', type: 'string' },
                    { name: 'BidAmount', type: 'number' },
                    { name: 'LowerBidAmount', type: 'number' },
                    { name: 'SelfInterest', type: 'number' },
                    { name: 'SelfInterestRate', type: 'number' },
                    { name: 'DirectReward', type: 'number' },
                    { name: 'IndirectReward', type: 'number' }
                ],
                pagesize: 15,
                formatdata: function (data) {
                    data.currentpage = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.uName = encodeURI(encodeURI($.trim($("#txtName").val()))) || "";
                    data.uType = $.trim($("#selSUserType").val()) || "0";
                    data.memberId = memberId;
                    var sDate = $("#startDate").val();
                    var eDate = $("#endDate").val();
                    if (sDate == "") {
                        MessageAlert("请输入开始时间。", "warning", null);
                        return false;
                    } else {
                        data.sYear = sDate.substr(0, 4);
                        data.sMonth = sDate.substr(5, 2);
                    }
                    if (eDate == "") {
                        MessageAlert("请输入结束时间。", "warning", null);
                        return false;
                    } else {
                        data.eYear = eDate.substr(0, 4);
                        data.eMonth = eDate.substr(5, 2);
                    }
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function (column, direction) { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var linkrenderer = function (row, column, value) {
                if (value === "") return "<span style=\"margin-left:25px;\">暂无</span>";
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var sDate = $("#startDate").val();
                var eDate = $("#endDate").val();
                var parm = "/ReportStatistics/LowerLevelReward.aspx?memberId=" + data.MemberId + "&sYear=" + sDate.substr(0, 4) + "&sMonth=" + sDate.substr(5, 2) + "&eYear=" + eDate.substr(0, 4) + "&eMonth=" + eDate.substr(5, 2) + "&columnId=" + <%=ColumnId %>;
                var link = "<a style='color:rgb(22, 22, 221);margin-left:25px;' href='javascript:void(0)' onclick=\"opdg(1080, 550,'" + parm + "')\"; target='_self'>查看</a>";
                var html = link;
                return html;
            };
            var numrenderer = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var rewardRate = (value * 100).toFixed(2);
                if (data.SumInterest === 0) rewardRate = 0.00;
                return "<div style='overflow: hidden;  text-overflow: ellipsis; padding-bottom: 2px; text-align: right; margin-right: 2px; margin-left: 4px; margin-top: 4px;'>" + rewardRate + "</div>";
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1060,
                sortable: false,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '15', '20'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'MemberName', text: '<span style="color:#9F79EE;">会员名</span>', columngroup: 'MI', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<span style="color:#9F79EE;">真实姓名</span>', columngroup: 'MI', width: 90, cellsalign: 'center', align: 'center' },
                    { dataField: 'LowerBidAmount', text: '<span style="color:#EE7600;">该分支总投资金额(￥)</span>', columngroup: 'I', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'BidAmount', text: '<span style="color:#EE7600;">本人有效投资金额(￥)</span>', columngroup: 'I', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SelfInterest', text: '<span style="color:#EE7600;">本人利息收益(￥)</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SelfInterestRate', text: '<span style="color:#EE7600;">本人利息收益奖励比例(%)</span>', columngroup: 'I', width: 180, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SumInterest', text: '<span style="color:#EE7600;">当月应收总利息(￥)</span>', columngroup: 'I', width: 120, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'RewardRate', text: '<span style="color:#EE7600;">奖励比例(%)</span>', columngroup: 'I', width: 90, cellsalign: 'right', align: 'center', cellsformat: "F2", cellsrenderer: numrenderer },
                    { dataField: 'Reward', text: '<span style="color:#EE7600;">奖励金额(￥)</span>', columngroup: 'I', width: 90, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'DirectReward', text: '<span style="color:#EE7600;">直接推荐奖励(￥)</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'IndirectReward', text: '<span style="color:#EE7600;">间接接推荐奖励(￥)</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SumSubReward', text: '<span style="color:#EE7600;">下级总奖励(￥)</span>', columngroup: 'I', width: 110, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'TotalDate', text: '<span style="color:#EE7600;">统计时间</span>', columngroup: 'I', width: 100, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'LowerLevel', text: '<span style="color:#EE7600;">已推荐人</span>', columngroup: 'I', cellsrenderer: linkrenderer, width: 80, cellsalign: 'center', align: 'center' }
                ],
                columngroups:
                [
                    { text: '<b style="color:#9F79EE;font-size:14px;">会员信息</b>', align: 'center', name: 'MI' },
                    { text: '<b style="color:#ff0000;font-size:14px;">推荐奖励信息</b>', align: 'center', name: 'I' }
                ],

            });
        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selSUserType" id="selSUserType" class="fl" runat="server">
                <option value="0">会员名</option>
                <option value="1">真实姓名</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSUserType" />
            </div>
            <div style="float: left;">
                <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div style="float: left; margin-left: 15px;">
                <div class="fl">
                    查询日期：
                </div>
                <div class="fl">
                    <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                </div>
                <input type="text" id="startDate" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月',minDate:'2014-07-01',errDealMode:'1'})" class="input_text" readonly="readonly" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
                <div class="fl">
                    &nbsp;至&nbsp;
                </div>
                <div class="fl">
                    <img src="../images/input_left.png" style="width: 4px; height: 29px;" alt="" />
                </div>
                <input type="text" id="endDate" onfocus="WdatePicker({skin:'whyGreen',dateFmt:'yyyy年MM月'})" class="input_text" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
            </div>
        </div>
        <div id="jqxWidget" style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
        <div id='jqxwindow'>
        </div>
    </form>
</body>
</html>
