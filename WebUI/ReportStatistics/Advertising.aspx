<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Advertising.aspx.cs" Inherits="WebUI.ReportStatistics.Advertising" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>广告统计</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/datetime.js"></script>
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
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        .selectDiv .select_box {
        width: 85px;
        }
        .selectDiv2 .select_box {
            width:115px;
        }
    </style>
    <script type="text/javascript">
        function GetRequest() {
            var url = location.search; //获取url中"?"符后的字串
            var theRequest = new Object();
            if (url.indexOf("?") != -1) {
                var str = url.substr(1);
                strs = str.split("&");
                for (var i = 0; i < strs.length; i++) {
                    theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
                }
            }
            return theRequest;
        }

        var Request = new Object();
        Request = GetRequest();

        $(function () {
            $("#btnSearch").click(function () {
                pageIndex = -1;
                $("#jqxgrid").jqxGrid('updatebounddata');
            });

            (function ($) {
                var FormatDateTime = function FormatDateTime() { };
                $.FormatDateTime = function (obj) {
                    var myDate = obj;
                    var year = myDate.getFullYear();
                    var month = ("0" + (myDate.getMonth() + 1)).slice(-2);
                    var day = ("0" + myDate.getDate()).slice(-2);
                    var h = ("0" + myDate.getHours()).slice(-2);
                    var m = ("0" + myDate.getMinutes()).slice(-2);
                    var s = ("0" + myDate.getSeconds()).slice(-2);
                    var mi = ("00" + myDate.getMilliseconds()).slice(-3);

                    return year + "-" + month + "-" + day;
                }
            })(jQuery);
            
            if (Request["s"] == undefined)
                $("#txtDateStart").val($.FormatDateTime(new Date()));
            else
                $("#txtDateStart").val(Request["s"]);

            if (Request["e"] == undefined)
                $("#txtDateEnd").val($.FormatDateTime(new Date()));
            else
                $("#txtDateEnd").val(Request["e"]);
            
            if (Request["r"] != undefined)
                $("#selCurrStatus").val(Request["r"]);
            if (Request["cr"] != undefined)
                $("#txtChannelRemark").val(Request["cr"]);
            if (Request["c"] != undefined)
                $("#selChannel").val(Request["c"]);
            if (Request["mina"] != undefined)
                $("#txtMinBalance").val(Request["mina"]);
            if (Request["maxa"] != undefined)
                $("#txtMaxBalance").val(Request["maxa"]);
            //主题
            var theme = "arctic";
            var pageIndex = 0;

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

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/AdvertisingHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'rowNum', type: 'int' },
                    { name: 'Channel', type: 'string' },
                    { name: 'ChannelRemark', type: 'string' },
                    { name: 'RegSource', type: 'string' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RegisterDate', type: 'string' },
                    { name: 'RegisterTime', type: 'string' },
                    { name: 'Balance', type: 'number' },
                    { name: 'IsComple', type: 'string' },
                    { name: 'Payable', type: 'number' },
                    { name: 'Sex', type: 'string' },
                    { name: 'Birthday', type: 'string' },
                    { name: 'IsBank', type: 'string' },
                    { name: 'Pay', type: 'number' },
                    { name: 'FirstPay', type: 'string' },
                    { name: 'FirstPayAmount', type: 'number' },
                    { name: 'FirstWithdraw', type: 'string' },
                    { name: 'FirstWithdrawAmount', type: 'number' },
                    { name: 'BidAmount', type: 'number' },
                    { name: 'Amount', type: 'number' },
                    { name: 'VisitRecord', type: 'string' },
                    { name: 'VisitCreateTime', type: 'string' },
                    { name: 'VisitMemberName', type: 'string' },
                    { name: 'Mobile', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    if (Request["i"] == undefined || Request["i"] == "-1")
                        pageIndex = pageIndex == -1 ? 0 : data.pagenum || 0;
                    else
                        pageIndex = Request["i"];

                    data.currentpage = pageIndex;
                    data.pagesize = data.pagesize || 20;
                    data.dateStart = $("#txtDateStart").val() || "";
                    data.dateEnd = $("#txtDateEnd").val() || "";
                    data.revStatus = $("#selCurrStatus").val() || 0;
                    data.channelRemark = $("#txtChannelRemark").val() || "";
                    data.channel = $("#selChannel").val() || -1;
                    data.minBalance = $("#txtMinBalance").val() || 0;
                    data.maxBalance = $("#txtMaxBalance").val() || 0;
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
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var content = "回访";
                var parm = "ReturnVisitEdit.aspx?mid=" + value + "&columnId=<%=ColumnId %>&s=" + $("#txtDateStart").val() + "&e=" + $("#txtDateEnd").val() + "&r=" + $("#selCurrStatus").val() + "&i=" + pageIndex + "&cr=" + $("#txtChannelRemark").val() + "&c=" + $("#selChannel").val() + "&mina=" + $("#txtMinBalance").val() + "&maxa=" + $("#txtMaxBalance").val() + "";
                var link = "<a style='text-align:center;margin-left:25px;line-height:25px;' href='" + parm + "' target='_self'>" + content + "</a>";
                var html = link;
                return html;
            };

            var linkrenderer2 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                return value == "" ? "<span style='text-align:center;margin-left:20px;line-height:25px;'>未回访</span>" : "<span style='text-align:center;margin-left:15px;line-height:25px;'>" + value + "</span>";
            };

            var linkrenderer3 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var html = value;
                switch (value)
                {
                    case 0:
                        html = "PC";
                        break;
                    case 1:
                        html = "WAP";
                        break;
                    case 2:
                        html = "IOS";
                        break;
                    case 3:
                        html = "Android";
                        break;
                }
                return "<span style='text-align:center;margin-left:15px;line-height:25px;'>" + html + "</span>";
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1200,
                sortable: false,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'rowNum', text: '<b>序号</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'ID', text: '<b>操作</b>', width: 80, cellsalign: 'center', cellsrenderer: linkrenderer, align: 'center' },
                    { dataField: 'VisitRecord', text: '<b>回访状态</b>', width: 80, cellsalign: 'center', cellsrenderer: linkrenderer2, align: 'center' },
                    { dataField: 'RegSource', text: '<b>注册来源</b>', width: 80, cellsalign: 'center', cellsrenderer: linkrenderer3, align: 'center' },
                    { dataField: 'ChannelRemark', text: '<b>广告来源</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'Channel', text: '<b>广告渠道</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberName', text: '<b>用户名称</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'Mobile', text: '<b>手机号码</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'RegisterDate', text: '<b>注册日期</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'RegisterTime', text: '<b>注册时间(小时)</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'Sex', text: '<b>性别</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'Birthday', text: '<b>出生日期</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'Balance', text: '<b>可用余额(元)</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'Payable', text: '<b>待收金额(元)</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'Pay', text: '<b>累积充值金额(元)</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'FirstPay', text: '<b>首次充值时间</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'FirstPayAmount', text: '<b>首次充值金额(元)</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'FirstWithdraw', text: '<b>最后提现时间</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'FirstWithdrawAmount', text: '<b>最后提现金额(元)</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'IsComple', text: '<b>是否通过实名认证</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'IsBank', text: '<b>是否绑定银行卡</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'BidAmount', text: '<b>历史投资最高额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'VisitCreateTime', text: '<b>回访时间</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'VisitMemberName', text: '<b>回访人员</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'Amount', text: '<b>回访后累积投资额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" }
                ],
                ready: function () {
                    if (Request["i"] != undefined && Request["i"] != "-1") {
                        //$(".jqx-input").val(Request["i"]);
                        //$(".jqx-icon-arrow-right").click();
                        Request["i"] = "-1";
                    }
                }
            });            
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1250px;">
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">查询日期从：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtDateStart" class="input_text" runat="server" type="text" onclick="WdatePicker()" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top: 8px; margin-left: 5px; margin-right: 5px;">至</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtDateEnd" class="input_text" type="text" runat="server" onclick="WdatePicker()" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">广告来源：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtChannelRemark" class="input_text" runat="server" type="text" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">广告渠道：</span>
            <div class="fl" style="margin-left: 5px;">
                <img src="/images/gray_left.png" width="4" height="29" alt="" />
            </div>
            <asp:DropDownList ID="selChannel" runat="server" class="fl" ClientIDMode="Static"></asp:DropDownList>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selChannel" />
            </div>
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">回访状态：</span>
            <div class="fl" style="margin-left: 5px;">
                <img src="/images/gray_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selCurrStatus" id="selCurrStatus" class="fl" runat="server">
                <option value="0">全部</option>
                <option value="1">未回访</option>
                <option value="2">已回访</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selCurrStatus" />
            </div>
            </div>
        <div id="Div1" class="selectDiv" style="min-width:1000px;float:left;margin-top:20px; margin-left:6px;">
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">可用余额：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtMinBalance" class="input_text" runat="server" type="text" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top: 8px; margin-left: 5px; margin-right: 5px;">至</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtMaxBalance" class="input_text" type="text" runat="server" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnSearch" value="查 询" class="inputButton" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input id="ExcelExport" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport_Click" />
            </div>
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
