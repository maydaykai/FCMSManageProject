<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperationDayTotal.aspx.cs" Inherits="WebUI.ReportStatistics.OperationDayTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>网站运营统计日报表</title>
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
            $("#btnSearchNow").click(function () {
                $("#txtDateStart").val($.FormatDateTime(new Date()));
                $("#txtDateEnd").val($.FormatDateTime(new Date()));
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
                url: '/HanderAshx/ReportStatistics/TotalOperationHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'TotalDate', type: 'date' },
                    { name: 'AddRegUserNum', type: 'int' },
                    { name: 'AddRealAuthUserNum', type: 'int' },
                    { name: 'AddVipUserNum', type: 'int' },
                    { name: 'AddRechargeUserNum', type: 'int' },
                    { name: 'AddRechargeAmount', type: 'number' },
                    { name: 'AddBidUserNum', type: 'int' },
                    { name: 'RegUserTotal', type: 'int' },
                    { name: 'RealUserTotal', type: 'int' },
                    { name: 'VipUserTotal', type: 'int' },
                    { name: 'BidUserTotal', type: 'int' },
                    { name: 'NoBidVipUserTotal', type: 'int' },
                    { name: 'RegBidRate', type: 'int' }
                    
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
                    { dataField: 'TotalDate', text: '<b>日期</b>', width: 140, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'AddRegUserNum', text: '<b>新增注册用户数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'AddRealAuthUserNum', text: '<b>新增实名认证用户数</b>', width: 140, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'AddVipUserNum', text: '<b>新增VIP用户数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'AddRechargeUserNum', text: '<b>新增充值用户数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'AddRechargeAmount', text: '<b>新增充值金额</b>', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'AddBidUserNum', text: '<b>新增投资用户数</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'RegUserTotal', text: '<b>注册用户总数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealUserTotal', text: '<b>实名认证用户总数</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'VipUserTotal', text: '<b>VIP用户总数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'BidUserTotal', text: '<b>投资用户总数</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'NoBidVipUserTotal', text: '<b>未投资VIP用户总数</b>', width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'RegBidRate', text: '<b>注册投资比%</b>', width: 120, cellsalign: 'center', align: 'center' }

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
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnSearch" value="查 询" class="inputButton" /></div>
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnSearchNow" value="查询当天" style="width: 80px;" class="inputButton" /></div>
           <div style="float: left; margin-left: 5px;"><input type="button" id="btnOutput" value="导 出" class="inputButton" /></div>
        </div>
       <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
