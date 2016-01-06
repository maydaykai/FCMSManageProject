﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="P2PFundRecord.aspx.cs" Inherits="WebUI.ReportStatistics.P2PFundRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>平台资金情况</title>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.aggregates.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #select_selAndOr { width: 40px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 115px; }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#btnSearch").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
            //主题
            var theme = "arctic";

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/P2PFundRecordHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'CreateTime', type: 'string' },
                    { name: 'InvestorPayAmount', type: 'number' },
                    { name: 'BorrowerPayAmount', type: 'number' },
                    { name: 'InvestorWithdrawAmount', type: 'number' },
                    { name: 'BorrowerWithdrawAmount', type: 'number' },
                    { name: 'InvestorRetainAmount', type: 'number' },
                    { name: 'BorrowerRetainAmount', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.currentpage = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.dateStart = $("#txtDateStart").val() || "";
                    data.dateEnd = $("#txtDateEnd").val() || "";
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
                width: 1235,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                //editable: true,
                selectionmode: 'singlecell',
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'CreateTime', text: '<b>日期</b>', width: 120, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'InvestorPayAmount', text: '投资者充值金额(元)', width: 180, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'], columngroup: 'Influx' },
                    { dataField: 'BorrowerPayAmount', text: '借款人充值金额(元)', width: 180, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'], columngroup: 'Influx' },
                    { dataField: 'InvestorWithdrawAmount', text: '投资者提现金额(元)', width: 180, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'], columngroup: 'Outflow' },
                    { dataField: 'BorrowerWithdrawAmount', text: '借款人提现金额(元)', width: 180, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'], columngroup: 'Outflow' },
                    { dataField: 'InvestorRetainAmount', text: '投资者留存金额(元)', width: 200, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'], columngroup: 'Retain' },
                    { dataField: 'BorrowerRetainAmount', text: '借款人留存金额(元)', width: 200, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'], columngroup: 'Retain' }
                ],
                columngroups:
                [
                    { text: '<b>资金流入</b>', align: 'center', name: 'Influx' },
                    { text: '<b>资金流出</b>', align: 'center', name: 'Outflow' },
                    { text: '<b>资金留存</b>', align: 'center', name: 'Retain' }
                ]
            });

        });
        //参数组装
        function buildQueryString(data) {
            var str = ''; for (var prop in data) {
                if (data.hasOwnProperty(prop)) {
                    str += prop + '=' + data[prop] + '&';
                }
            }
            return str.substr(0, str.length - 1);
        }
    </script>
</head>
<body>
    <form runat="server" id="form1">
        <div id="searchbar" class="selectDiv" style="min-width:1250px;">

           <span class="fl" style="margin-top:5px; margin-left:5px;">查询日期从：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtDateStart" class="input_text" runat="server"  type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <span class="fl" style="margin-top:8px; margin-left:5px; margin-right:5px;">至</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtDateEnd" class="input_text" type="text" runat="server" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'txtDateStart\',{M:1});}', minDate: '#F{$dp.$D(\'txtDateStart\',{d:0});}' })" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <div style="float: left; margin-left: 5px;">
               <input type="button" id="btnSearch" value="查 询" class="inputButton" />
               <input id="ExcelExport" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport_Click" />
           </div>
        </div>
        <div id="jqxWidget" style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>