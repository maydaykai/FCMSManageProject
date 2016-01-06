<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelEffectAnalyze.aspx.cs" Inherits="WebUI.AdvertisingManage.ChannelEffectAnalyze" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>渠道效果分析</title>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.aggregates.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
         
    <script type="text/javascript">
        $(function () {
            $("#applyfilter").click(function () {
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

            $("#txtStartDate").val($.FormatDateTime(new Date()));
            $("#txtEndDate").val($.FormatDateTime(new Date()));

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

            //获取参数值
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

            function getPValueByName(name) {
                for (var i = 0; i < all.length; i++) {
                    if (all[i].name == name)
                        return all[i].value;
                }
            }

            var formatDate = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = data.channelFee / data.RegCount;
                if (parm.toString() == "NaN" || parm.toString() == "Infinity")
                    parm = 0;
                return "<div style='line-height:25px;text-align: center;'>" + parm.toFixed(2) + "</div>";
            };

            var formatDate2 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = data.channelFee / data.bidCount;
                if (parm.toString() == "NaN" || parm.toString() == "Infinity")
                    parm = 0;
                return "<div style='line-height:25px;text-align: center;'>" + parm.toFixed(2) + "</div>";
            };
            
            var formatDate3 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = data.channelFee / data.bidAmount;
                if (parm.toString() == "NaN" || parm.toString() == "Infinity")
                    parm = 0;
                return "<div style='line-height:25px;text-align: center;'>" + parm.toFixed(2) + "</div>";
            };

            var formatDate4 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                return "<div style='line-height:25px;text-align: center;'>" + data.channelFee.toFixed(2) + "</div>";
            };

            //数据源
            var source = {
                url: '/HanderAshx/AdvertisingManage/ChannelEffectAnalyzeHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'RegTime', type: 'string' },
                    { name: 'Channel', type: 'string' },
                    { name: 'channelFee', type: 'number' },
                    { name: 'RegCount', type: 'int' },
                    { name: 'dayBidCount', type: 'int' },
                    { name: 'bidCount', type: 'int' },
                    { name: 'payCount', type: 'int' },
                    { name: 'payAmount', type: 'number' },
                    { name: 'bidAmount', type: 'number' },
                    { name: 'daishou', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.channelId = $("#sel_channel").val() || -1;
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
                    data.statType = $("#sel_statType").val() || 0;
                    data.statMode = $("#sel_statMode").val() || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source.totalrecords = data.TotalRows;
                }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1100,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'RegTime', text: '<b>日期</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'Channel', text: '<b>渠道</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'c', text: '<b>费用</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: formatDate4 },
                    { dataField: 'RegCount', text: '<b>注册人数</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'dayBidCount', text: '<b>当日投资人数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'bidCount', text: '<b>投资人数</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'payCount', text: '<b>充值人数</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'payAmount', text: '<b>充值金额</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'bidAmount', text: '<b>投资金额</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'daishou', text: '<b>待收金额</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: '', text: '<b>注册成本</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: formatDate },
                    { dataField: 'a', text: '<b>投资成本</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: formatDate2 },
                    { dataField: 'b', text: '<b>ROI</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: formatDate3 }
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <span class="fl">注册时间：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtStartDate" class="input_text" type="text" runat="server" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'txtEndDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-left: 5px; margin-right: 5px;">～</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtEndDate" class="input_text" type="text" runat="server" onclick="WdatePicker({ minDate: '#F{$dp.$D(\'txtStartDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>

            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <asp:DropDownList name="sel_channel" ID="sel_channel" runat="server" class="fl" ClientIDMode="Static">
            </asp:DropDownList>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_channel" />
            </div>

            <span class="fl">统计类型：</span>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="sel_statType" id="sel_statType" runat="server" class="fl">
                <option value="0">注册日期</option>
                <option value="1">投资日期</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_statType" />
            </div>

            <span class="fl">统计模式：</span>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="sel_statMode" id="sel_statMode" runat="server" class="fl">
                <option value="0">按天显示</option>
                <option value="1">按周显示</option>
                <option value="2">按月显示</option>
                <option value="3">按年显示</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_statMode" />
            </div>

            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
                <%--<input id="ExcelExport1" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport1_Click" />--%>
            </div>
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
