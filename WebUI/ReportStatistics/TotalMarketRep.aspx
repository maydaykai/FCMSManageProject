﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TotalMarketRep.aspx.cs" Inherits="WebUI.ReportStatistics.TotalMarketRep" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>营销报表统计</title>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>

    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

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
                    var day = ("0" + (myDate.getDate())).slice(-2);
                    var h = ("0" + myDate.getHours()).slice(-2);
                    var m = ("0" + myDate.getMinutes()).slice(-2);
                    var s = ("0" + myDate.getSeconds()).slice(-2);
                    var mi = ("00" + myDate.getMilliseconds()).slice(-3);

                    return year + "-" + month + "-" + day;
                }
            })(jQuery);

            $("#txtDate1").val($.FormatDateTime(new Date()));
            $("#txtDate2").val($.FormatDateTime(new Date()));
            var btnoutput = <%=_btnOutPut %>;

            if (btnoutput == 0) {
                $("#btnOutput").hide();
            }

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

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/TotalMarketRepHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'RealName', type: 'string' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RegNum', type: 'int' },
                    { name: 'BidNum', type: 'int' },
                    { name: 'BidAmount', type: 'number' },
                    { name: 'BidNumContinued', type: 'int' },
                    { name: 'BidAmountContinued', type: 'number' },
                    { name: 'SumBidNum', type: 'int' },
                    { name: 'SumBidAmount', type: 'number' },
                    { name: 'HistoryBidNum', type: 'int' },
                    { name: 'HistoryBidAmont', type: 'number' },
                    { name: 'RewardRate', type: 'number' },
                    { name: 'RewardAmount', type: 'number' },
                    { name: 'RewardBidAmount', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.date1 = $("#txtDate1").val() || "";
                    data.date2 = $("#txtDate2").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
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
                sortable: true,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                editable: true,
                selectionmode: 'singlecell',
                //pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'RealName', text: '真实姓名', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberName', text: '会员名', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'RegNum', text: '当周注册人数', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'BidNum', text: '当周注册用户中投资人数', width: 180, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'BidAmount', text: '当周注册用户中投资金额', width: 180, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'BidNumContinued', text: '当周续投人数', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'BidAmountContinued', text: '当周续投金额', width: 120, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'SumBidNum', text: '当月投资人数', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'SumBidAmount', text: '当月投资金额', width: 120, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'HistoryBidNum', text: '历史投资人数', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'HistoryBidAmont', text: '历史投资金额', width: 120, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'RewardRate', text: '奖励比率', width: 80, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'RewardAmount', text: '奖励金额', width: 120, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'RewardBidAmount', text: '折算投资金额', width: 120, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] }
                ]
            });
            $("#btnOutput").jqxButton({ theme: theme });
            $("#btnOutput").click(function () {
                $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxGrid');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width:1250px;">

           <span class="fl" style="margin-top:5px; margin-left:5px;">起始日期：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtDate1" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
            <span class="fl" style="margin-top:5px; margin-left:5px;">结束日期：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtDate2" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnSearch" value="查 询" class="inputButton" /></div><div style="float: left; margin-left: 5px;"><input type="button" id="btnOutput" value="导 出" class="inputButton"/></div>
        </div>
       <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>


