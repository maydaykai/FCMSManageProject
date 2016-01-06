<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationMonthTotal.aspx.cs" Inherits="WebUI.ReportStatistics.OperationMonthTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>网站运营统计月报表</title>
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

            $("#txtYear").val((new Date()).getFullYear());
            
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
                url: '/HanderAshx/ReportStatistics/TotalOperationMHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'TotalMonth', type: 'string' },
                    { name: 'AddRegUserNum', type: 'int' },
                    { name: 'AddVipUserNum', type: 'int' },
                    { name: 'AddBidUserNum', type: 'int' },
                    { name: 'RegBidRate', type: 'number' },
                    { name: 'BackflowUserNum', type: 'int' },
                    { name: 'KeepUserNum', type: 'int' },
                    { name: 'WithdrawUserNum', type: 'int' },
                    { name: 'UserTotal', type: 'int' },
                    { name: 'DealTotal', type: 'number' },
                    { name: 'AdvanceTotal', type: 'number' },
                    { name: 'LoanAmountTotal', type: 'number' },
                    { name: 'LoanNumTotal', type: 'int' }
                    
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.yearnum = $("#txtYear").val() || "";
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
                width: 1280,
                sortable: true,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                editable: true,
                selectionmode: 'singlecell',
                //pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'TotalMonth', text: '<b>月份</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'AddRegUserNum', text: '<b>本月新增注册数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'AddVipUserNum', text: '<b>本月新增VIP数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'AddBidUserNum', text: '<b>本月新增投资用户数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'RegBidRate', text: '<b>本月注册投资比%</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'BackflowUserNum', text: '<b>本月回流用户数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'KeepUserNum', text: '<b>本月留存用户数</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'WithdrawUserNum', text: '<b>本月撤资用户数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'UserTotal', text: '<b>本月总用户数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'DealTotal', text: '<b>本月成交量</b>', width: 140, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'AdvanceTotal', text: '<b>本月垫付金额</b>', width: 140, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'LoanAmountTotal', text: '<b>本月借款金额</b>', width: 140, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'LoanNumTotal', text: '<b>本月借款数量</b>', width: 140, cellsalign: 'center', align: 'center', aggregates: ['sum'] }

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

           <span class="fl" style="margin-top:5px; margin-left:5px;">查询年份：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtYear" class="input_text" type="text" onclick="WdatePicker({dateFmt:'yyyy'})" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnSearch" value="查 询" class="inputButton" /></div>
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnOutput" value="导 出" class="inputButton" /></div>
        </div>
       <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>

