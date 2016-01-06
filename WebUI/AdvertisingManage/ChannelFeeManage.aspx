﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelFeeManage.aspx.cs" Inherits="WebUI.AdvertisingManage.ChannelFeeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>渠道费用管理</title>
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
    <script src="/js/Common.js"></script>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
         
    <script type="text/javascript">
        $(function () {
            $("#applyfilter").click(function () {
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
                var parm = "/AdvertisingManage/ChannelFeeDetails.aspx?cid=" + data.Id + "&columnId=" + <%=ColumnId %>;
                return "<div style='line-height:30px;text-align: center;'><a href='"+parm+"' style='cursor:pointer;' >" + value + "</a></div>";
                // onclick=\"MessageWindow(1100, 550,'" + parm + "')\";
            };

            //数据源
            var source = {
                url: '/HanderAshx/AdvertisingManage/ChannelFeeHandler.ashx?t=list',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'Id', type: 'int' },
                    { name: 'Channel', type: 'string' },
                    { name: 'Rebate', type: 'number' },
                    { name: 'dayFee', type: 'number' },
                    { name: 'zhouFee', type: 'number' },
                    { name: 'monthFee', type: 'number' },
                    { name: 'feeSum', type: 'number' },
                    { name: 'createTime', type: 'date' },
                    { name: 'periodFee', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";;
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
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 2px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>新增渠道费用</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 120, height: 20 });
                    addButton.click(function (event) {
                        window.location.href = "/AdvertisingManage/ChannelFeeAdd.aspx?columnId=<%=ColumnId%>";
                    });
                },
                showstatusbar: true,
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
                    { dataField: 'Channel', text: '<b>渠道</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: formatDate },
                    { dataField: 'dayFee', text: '<b>当天费用</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'zhouFee', text: '<b>本周费用</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'monthFee', text: '<b>本月费用</b>', width: 160, cellsalign: 'center', align: 'center' },
                    { dataField: 'feeSum', text: '<b>累计费用</b>', width: 170, cellsalign: 'center', align: 'center' },
                    { dataField: 'Rebate', text: '<b>费用备注(返点)%</b>', width: 170, cellsalign: 'center', align: 'center' },
                    { dataField: 'periodFee', text: '<b>查询时段费用</b>', width: 170, cellsalign: 'center', align: 'center' },
                    { dataField: 'createTime', text: '<b>录入时间</b>', cellsformat: "yyyy/MM/dd", width: 150, cellsalign: 'center', align: 'center' }
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <span class="fl">查询时间：</span>
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
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
                <input id="ExcelExport1" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport1_Click" />
            </div>
        </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
    </form>
</body>
</html>
