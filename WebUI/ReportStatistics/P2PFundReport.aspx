<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="P2PFundReport.aspx.cs" Inherits="WebUI.ReportStatistics.P2PFundReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                url: '/HanderAshx/ReportStatistics/P2PFundReportHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'FeeType', type: 'string' },
                    { name: 'Amount1', type: 'number' },
                    { name: 'MemberBalance', type: 'number' },
                    { name: 'Status', type: 'int' },
                    { name: 'Description', type: 'string' },
                    { name: 'UpdateTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.UpdateTime';
                    data.sortorder = data.sortorder || 'DESC';
                    data.status = $("#selStatus").val() || "";
                    data.feeType = $("#selFeeType").val() || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
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

            var statusRender = function (row, column, value) {
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                switch (value) {
                    case 1: html += "正常"; break;
                    case 2: html += "冻结"; break;
                    case 3: html += "作废"; break;
                }
                html += "</div>";
                return html;
            }

            var feeTypeRenderer = function (row, column, value) {
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                html += $("#<%=selFeeType.ClientID%>").find("option[value=" + value + "]").text();
                html += "</div>";
                return html;
            }

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1050,
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
                    { dataField: 'FeeType', text: '<b>费用类型</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: feeTypeRenderer },
                    { dataField: 'Amount1', text: '<b>金额(元)</b>', width: 140, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'MemberBalance', text: '<b>账户可用余额(元)</b>', width: 170, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'Status', text: '<b>状态</b>', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: statusRender },
                    { dataField: 'Description', text: '<b>资金变动描述说明</b>', width: 280, cellsalign: 'center', align: 'center' },
                    { dataField: 'UpdateTime', text: '<b>记录时间</b>', width: 180, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd HH:mm:ss" }
                ]
            });
            //导出数据
            $("#btnOutput").click(function () {
                var data = new Object();
                data.pagenum = data.pagenum || 0;
                data.pagesize = data.pagesize || 20;
                data.sortdatafield = data.sortdatafield || 'P.UpdateTime';
                data.sortorder = data.sortorder || 'DESC';
                data.status = $("#selStatus").val() || "";
                data.feeType = $("#selFeeType").val() || "";
                data.startDate = $("#txtStartDate").val() || "";
                data.endDate = $("#txtEndDate").val() || "";
                formatedData = buildQueryString(data);
                window.open("/HanderAshx/ReportStatistics/P2PFundReportHandler.ashx?output=1&" + formatedData, "_blank");
            });
        });       
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width:1250px;">
           <div class="selectDiv2">
               <div class="fl">
                    <img src="/images/input_left.png" width="4" height="29" alt="" />
               </div>
               <asp:DropDownList ID="selFeeType" runat="server" DataTextField="Value" DataValueField="Key"></asp:DropDownList>
               <div class="fl" style="margin-left: -5px; cursor: pointer;">
                    <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selFeeType" />
               </div>
           </div>
            <div class="fl" style="margin-left:5px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selStatus" id="selStatus" class="fl" runat="server">
                <option value="">--状态--</option>
                <option value="1">正常</option>
                <option value="2">冻结</option>
                <option value="3">作废</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer; margin-right:5px;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selStatus" />
            </div>
           <span class="fl" style="margin-top:5px; margin-left:5px;">记录时间：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtStartDate" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <span class="fl" style="margin-top:8px; margin-left:5px; margin-right:5px;">~</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtEndDate" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" />
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
