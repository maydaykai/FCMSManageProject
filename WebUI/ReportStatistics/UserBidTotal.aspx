<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserBidTotal.aspx.cs" Inherits="WebUI.ReportStatistics.UserBidTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
                url: '/HanderAshx/ReportStatistics/UserBidTotalHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'TotalDate', type: 'date' },
                    { name: 'RegUserNum', type: 'int' },
                    { name: 'BankCardAuthentNum', type: 'int' },
                    { name: 'RechargeNum', type: 'int' },
                    { name: 'RechargeAmount', type: 'number' },
                    { name: 'WithdrawNum', type: 'int' },
                    { name: 'WithdrawAmount', type: 'number' },
                    { name: 'BidNum', type: 'int' },
                    { name: 'BidAmount', type: 'number' },
                    { name: 'TodayRepayment', type: 'number' },
                    { name: 'TodayBidAmount', type: 'number' },
                    { name: 'TodayBidNum', type: 'int' },
                    { name: 'TodayCRBidAmount', type: 'number' },
                    { name: 'TodayCRBidNum', type: 'int' },
                    { name: 'TodayNoPayableNum', type: 'int' },
                    { name: 'TodayPayableNum', type: 'int' },
                    { name: 'PhoneBidAmount', type: 'number' },
                    { name: 'PhoneBidNum', type: 'int' },
                    { name: 'TodayNewBidAmount', type: 'int' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.dateStart = $("#txtDateStart").val() || "";
                    data.dateEnd = $("#txtDateEnd").val() || "";
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
                width: 1240,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                editable: true,
                selectionmode: 'singlecell',
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'TotalDate', text: '<b>统计日期</b>', width: 120, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'RegUserNum', text: '<b>注册用户数</b>', width: 80, cellsalign: 'center', align: 'center',aggregates: ['sum']},
                    { dataField: 'BankCardAuthentNum', text: '<b>银行卡验证数</b>', width: 100, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'RechargeNum', text: '<b>充值次数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'RechargeAmount', text: '<b>充值金额(元)</b>', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'WithdrawNum', text: '<b>提现次数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'WithdrawAmount', text: '<b>提现金额(元)</b>', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'BidNum', text: '<b>投标次数</b>', width: 80, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'BidAmount', text: '<b>投标金额</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'TodayRepayment', text: '<b>当日还款金额</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'TodayBidAmount', text: '<b>当日投借款标金额</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'TodayBidNum', text: '<b>当日投借款标人数</b>', width: 140, cellsalign: 'center', align: 'center',  aggregates: ['sum'] },
                    { dataField: 'TodayCRBidAmount', text: '<b>当日投债权转让金额</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'TodayCRBidNum', text: '<b>当日投债权转让人数</b>', width: 140, cellsalign: 'center', align: 'center',  aggregates: ['sum'] },
                    { dataField: 'TodayNoPayableNum', text: '<b>当日投资没有待收人数</b>', width: 150, cellsalign: 'center', align: 'center',  aggregates: ['sum'] },
                    { dataField: 'TodayPayableNum', text: '<b>当日投资有待收人数</b>', width: 140, cellsalign: 'center', align: 'center',  aggregates: ['sum'] },
                    { dataField: 'PhoneBidAmount', text: '<b>手机投资金额</b>', width: 150, cellsalign: 'center', align: 'center',  aggregates: ['sum'] },
                    { dataField: 'PhoneBidNum', text: '<b>手机投资人数</b>', width: 140, cellsalign: 'center', align: 'center',  aggregates: ['sum'] },
                    { dataField: 'TodayNewBidAmount', text: '<b>当日新增首投人数</b>', width: 140, cellsalign: 'center', align: 'center', aggregates: ['sum'] }
                
                    
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

           <span class="fl" style="margin-top:5px; margin-left:5px;">查询日期从：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtDateStart" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <span class="fl" style="margin-top:8px; margin-left:5px; margin-right:5px;">至</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtDateEnd" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnSearch" value="查 询" class="inputButton" /></div><div style="float: left; margin-left: 5px;"><input type="button" id="btnOutput" value="导 出" class="inputButton" /></div>
        </div>
       <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
