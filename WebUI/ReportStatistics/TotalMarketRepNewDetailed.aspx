<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TotalMarketRepNewDetailed.aspx.cs" Inherits="WebUI.ReportStatistics.TotalMarketRepNewDetailed" %>
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
            var date=new Date();
            var year = date.getFullYear();
            var month = ("0" + (date.getMonth() + 1)).slice(-2);

            var  day = new Date(year,month,0);   
            var lastdate = year + '-' + month + '-' + day.getDate();//获取当月最后一天日

            $("#txtDate1").val(year + "-" + month + "-" + "01");
            $("#txtDate2").val(lastdate);
 

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
                url: '/HanderAshx/ReportStatistics/TotalMarketRepNewDetailedHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ReviewTime', type: 'date' },
                    { name: 'EndTime', type: 'date' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'BidAmount', type: 'number' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'CALoanID', type: 'int' },
                    { name: 'ID', type: 'int' },
                    { name: 'curr_days', type: 'int' },
                    { name: 'next_days', type: 'int' },
                    { name: 'curr_days_money', type: 'number' },
                    { name: 'next_days_money', type: 'number' },
                    { name: 'day_money', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.date1 = $("#start_time").val() || "";
                    data.date2 = $("#end_time").val() || "";
                    data.memberName = $("#hidden_memberName").val() || "";
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


            var linkrenderer = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = column + "=" + value;
                alert(data.MemberName);
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
           
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1460,
                sortable: true,
                //pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                //editable: false,
                selectionmode: 'singlecell',
                //pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'ReviewTime', text: '开始时间', width: 150, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                    { dataField: 'EndTime', text: '结束时间', width: 150, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberName', text: '用户账号', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '用户名', width: 160, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanNumber', text: '借款标号', width: 160, cellsalign: 'right', align: 'center' },
                    { dataField: 'BidAmount', text: '投资金额', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'curr_days', text: '当月天数', width: 80, cellsalign: 'right', align: 'center' },
                    { dataField: 'next_days', text: '上月天数', width: 80, cellsalign: 'right', align: 'center'},
                    { dataField: 'curr_days_money', text: '当月投资金额(年化)', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2" , aggregates: ['sum']},
                    {dataField: 'next_days_money', text: '上月投资金额(年化)', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2" , aggregates: ['sum']}

                    

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

        <input type="hidden" runat="server" id="start_time" />
        <input type="hidden" runat="server" id="end_time" />
        <input type="hidden" runat="server" id="hidden_memberName" />

          <div id="searchbar" class="selectDiv" style="min-width: 1250px;">
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnOutput" value="导 出" class="inputButton" /></div>
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
