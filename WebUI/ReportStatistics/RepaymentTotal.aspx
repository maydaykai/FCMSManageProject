 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepaymentTotal.aspx.cs" Inherits="WebUI.ReportStatistics.RepaymentTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span {
            height:29px;text-indent: 5px;line-height: 29px;
        }
        .selectDiv .select_box {
        width: 85px;
        }
        .selectDiv2 .select_box {
        width: 105px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#applyfilter").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });

            $("#applyfilter2").click(function () {
                $("#jqxgrid2").jqxGrid('updatebounddata');
            });

            $("#applyfilter3").click(function () {
                $("#jqxgrid3").jqxGrid('updatebounddata');
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
            
            if (getPValueByName("checkStatus") != "") {
                $("#sel_checkStatus").val(getPValueByName("checkStatus"));
            }

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/RepaymentTotalHandler.ashx?t=q',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'LoanID', type: 'string' },
                    { name: 'Agency', type: 'string' },
                    { name: 'LoanAmount', type: 'string' },
                    { name: 'LoanTerm', type: 'string' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'RePayTime', type: 'date' },
                    { name: 'ReAmount', type: 'number' },
                    { name: 'RePrincipal', type: 'number' },
                    { name: 'ReInterest', type: 'number' },
                    { name: 'ReOverInterest', type: 'number' },
                    { name: 'PenaltyAmount', type: 'number' },
                    { name: 'FactReInterest', type: 'number' },
                    { name: 'UpdateTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function(data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.checkStatus = $("#sel_checkStatus").val() || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function() { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function(data) {
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
                width: 1000,
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
                    { dataField: 'RePayTime', text: '<b>应还款日期</b>', cellsformat: "yyyy/MM/dd", width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'UpdateTime', text: '<b>实际还款日期</b>', cellsformat: "yyyy/MM/dd", width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'Agency', text: '<b>区域</b>', width: 70, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<b>融资方</b>', width: 300, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanAmount', text: '<b>借款金额(万)</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanTerm', text: '<b>借款期限</b>', width: 70, cellsalign: 'center', align: 'center' },
                    { dataField: 'ReAmount', text: '<b>实际还款</b>', width: 120, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'RePrincipal', text: '<b>应还本金</b>', width: 120, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'ReInterest', text: '<b>应还利息</b>', width: 100, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'FactReInterest', text: '<b>实还利息</b>', width: 100, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'ReOverInterest', text: '<b>应还逾期利息</b>', width: 100, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'PenaltyAmount', text: '<b>提前还款违约金</b>', width: 100, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'LoanNumber', text: '<b>项目编号</b>', width: 120, cellsalign: 'center', align: 'center' }
                ]
            });

            //数据源
            var source2 = {
                url: '/HanderAshx/ReportStatistics/RepaymentTotalHandler.ashx?t=t',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'repDate', type: 'date' },
                    { name: 'principal', type: 'number' },
                    { name: 'interest', type: 'number' },
                    { name: 'amount', type: 'number' },
                    { name: 'reppCount', type: 'int' },
                    { name: 'repCount', type: 'int' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.startDate = $("#txtStartDate2").val() || "";
                    data.endDate = $("#txtEndDate2").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid2").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source2.totalrecords = data.TotalRows;
                }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source2, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });


            //数据绑定
            $("#jqxgrid2").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 800,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 30,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'repDate', text: '<b>还款日期</b>', cellsformat: "yyyy/MM/dd", width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'principal', text: '<b>已还本金</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'interest', text: '<b>已还利息</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'amount', text: '<b>本息合计</b>', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2", aggregates: ['sum'] },
                    { dataField: 'reppCount', text: '<b>还本(笔)</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                    { dataField: 'repCount', text: '<b>还款(笔)</b>', width: 120, cellsalign: 'center', align: 'center', aggregates: ['sum'] }
                ]
            });

            //数据源
            var source3 = {
                url: '/HanderAshx/ReportStatistics/RepaymentTotalHandler.ashx?t=z',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'Agency', type: 'string' },
                    { name: 'Amount', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.startDate = $("#txtStartDate3").val() || "";
                    data.endDate = $("#txtEndDate3").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid3").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source3.totalrecords = data.TotalRows;
                }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source3, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });


            //数据绑定
            $("#jqxgrid3").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 460,
                sortable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'Agency', text: '<b>区域</b>', cellsformat: "yyyy/MM/dd", width: 140, cellsalign: 'center', align: 'center' },
                    { dataField: 'Amount', text: '<b>本周回款</b>', width: 320, cellsalign: 'center', align: 'center', cellsformat: "F2" }
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="searchbar" class="selectDiv" style="min-width:1000px;">
        <span class="fl">还款时间：</span>
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
        <select name="sel_checkStatus" id="sel_checkStatus" runat="server" class="fl">
            <option value="">--分支机构--</option>
            <option value="普宁">普宁</option>
            <option value="海口">海口</option>
            <option value="河北">河北</option>
            <option value="广西">广西</option>
            <option value="广州">广州</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_checkStatus" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter" value="查 询" class="inputButton" />

            <input id="ExcelExport1" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport1_Click" />
        </div>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>

    <div id="Div1" class="selectDiv" style="min-width:1000px;float:left;margin-top:20px; margin-left:6px;">
    <span class="fl">还款时间：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtStartDate2" class="input_text" type="text" runat="server" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'txtEndDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl" style="margin-left: 5px; margin-right: 5px;">～</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtEndDate2" class="input_text" type="text" runat="server" onclick="WdatePicker({ minDate: '#F{$dp.$D(\'txtStartDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter2" value="查 询" class="inputButton" />
            <input id="ExcelExport2" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport2_Click" />
        </div>
    </div>    
    <div id='jqxWidget2' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid2">
        </div>
    </div>

    <div id="Div2" class="selectDiv" style="min-width:1000px;float:left;margin-top:20px; margin-left:6px;">
    <span class="fl">还款时间：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtStartDate3" class="input_text" type="text" runat="server" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'txtEndDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl" style="margin-left: 5px; margin-right: 5px;">～</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtEndDate3" class="input_text" type="text" runat="server" onclick="WdatePicker({ minDate: '#F{$dp.$D(\'txtStartDate\')}', dateFmt: 'yyyy-MM-dd' })" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter3" value="查 询" class="inputButton" />
            <input id="ExcelExport3" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport3_Click" />
        </div>
    </div>
    <div id='jqxWidget3' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid3">
        </div>
    </div>
    </form>
</body>
</html>