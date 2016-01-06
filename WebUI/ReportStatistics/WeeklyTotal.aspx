<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeeklyTotal.aspx.cs" Inherits="WebUI.ReportStatistics.WeeklyTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>周报报表</title>
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
        $(function () {
            $("#btnSearch").click(function () {
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

            $("#txtDateStart").val($.FormatDateTime(new Date()));
            $("#txtDateEnd").val($.FormatDateTime(new Date()));

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
                url: '/HanderAshx/ReportStatistics/WeeklyTotalHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'A', type: 'int' },
                    { name: 'B', type: 'int' },
                    { name: 'C', type: 'number' },
                    { name: 'D', type: 'int' },
                    { name: 'E', type: 'int' },
                    { name: 'F', type: 'number' },
                    { name: 'G', type: 'int' },
                    { name: 'H', type: 'number' },
                    { name: 'I', type: 'int' },
                    { name: 'J', type: 'number' },
                    { name: 'K', type: 'int' },
                    { name: 'L', type: 'int' },
                    { name: 'M', type: 'number' },
                    { name: 'N', type: 'int' },
                    { name: 'O', type: 'number' },
                    { name: 'P', type: 'int' },
                    { name: 'Q', type: 'number' },
                    { name: 'R', type: 'int' },
                    { name: 'S', type: 'number' },
                    { name: 'T', type: 'number' },
                    { name: 'U', type: 'int' },
                    { name: 'V', type: 'int' },
                    { name: 'W', type: 'int' },
                    { name: 'X', type: 'number' },
                    { name: 'Y', type: 'number' },
                    { name: 'Z', type: 'number' },
                    { name: 'AA', type: 'number' },

                    { name: 'AB', type: 'int' },
                    { name: 'AC', type: 'int' },
                    { name: 'AD', type: 'int' },
                    { name: 'AE', type: 'int' },
                    { name: 'AF', type: 'int' },
                    { name: 'AG', type: 'number' },
                    { name: 'AH', type: 'int' },
                    { name: 'AI', type: 'int' },
                    { name: 'AJ', type: 'int' },
                    { name: 'AK', type: 'int' },
                    { name: 'AL', type: 'number' },
                    { name: 'AM', type: 'int' },
                    { name: 'AN', type: 'int' },
                    { name: 'AO', type: 'int' },
                    { name: 'AP', type: 'int' },
                    { name: 'AQ', type: 'number' },
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.dateStart = $("#txtDateStart").val() || "";
                    data.dateEnd = $("#txtDateEnd").val() || "";
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
                    { dataField: 'A', text: '<b>发标个数</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'B', text: '<b>发标借款标个数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'C', text: '<b>发标借款标金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'D', text: '<b>满标个数</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'E', text: '<b>满标借款标个数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'F', text: '<b>满标借款标金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'G', text: '<b>债权转让个数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'H', text: '<b>债权转让金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'I', text: '<b>债权转让满标个数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'J', text: '<b>债权转让满标金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'K', text: '<b>债权转让投标人数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'L', text: '<b>投资人数</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'M', text: '<b>投资金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'N', text: '<b>充值人数</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'O', text: '<b>充值金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" }
                ]
            });

            //数据绑定
            $("#jqxgrid1").jqxGrid({
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
                    { dataField: 'O', text: '<b>充值金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'P', text: '<b>提现人数</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'Q', text: '<b>提现金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'R', text: '<b>借款标投资人数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'S', text: '<b>借款标投资金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'T', text: '<b>回款金额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'U', text: '<b>注册用户数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'V', text: '<b>注册用户中充值用户数</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'W', text: '<b>注册用户中投资用户数</b>', width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'X', text: '<b>注册用户中投资金额(元)</b>', width: 180, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'Y', text: '<b>注册投资转化率%</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'Z', text: '<b>贷款余额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'AA', text: '<b>贷款总额(元)</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" }
                ]
            });

            //数据绑定
            $("#jqxgrid2").jqxGrid({
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
                    { dataField: 'AB', text: '<b>小金宝累计绑定用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AC', text: '<b>APP注册用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AD', text: '<b>通过APP注册并投资的用户数</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'AE', text: '<b>APP充值用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AF', text: '<b>APP投资用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AG', text: '<b>APP投资金额</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'AH', text: '<b>WAP注册用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AI', text: '<b>通过WAP注册并投资的用户数</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'AJ', text: '<b>WAP充值用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AK', text: '<b>WAP投资用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AL', text: '<b>WAP投资金额</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'AM', text: '<b>PC注册用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AN', text: '<b>通过PC注册并投资的用户数</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'AO', text: '<b>PC充值用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AP', text: '<b>PC投资用户数</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'AQ', text: '<b>PC投资金额</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" }

                ]
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
        <div id="Div1" class="selectDiv" style="min-width:1000px;float:left;margin-top:20px; margin-left:6px;">
        </div>
        <div id='jqxWidget1' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid1">
            </div>
        </div>
        <div id="Div2" class="selectDiv" style="min-width:1000px;float:left;margin-top:20px; margin-left:6px;">
        </div>
        <div id='jqxWidget2' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid2">
            </div>
        </div>
    </form>
</body>
</html>
