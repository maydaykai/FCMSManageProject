<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberFondRecord.aspx.cs" Inherits="WebUI.ReportStatistics.MemberFondRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员资金流水明细</title>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script> 
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 105px; }
    </style>
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

            if (getPValueByName("MemberName") != "") {
                $("#txtName").val(getPValueByName("MemberName"));
            }

            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/MemberFondRecordHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'RowID', type: 'string' },
                    { name: 'FeeTypeString', type: 'string' },
                    { name: 'Amount1', type: 'number' },
                    { name: 'MemberBalance', type: 'number' },
                    { name: 'Status', type: 'string' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'Description', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.currentpage = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.uName = encodeURI($.trim($("#txtName").val())) || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
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

            var statusRender = function(row, column, value) {
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                switch (value) {
                case 1:
                    html += "正常";
                    break;
                case 2:
                    html += "冻结";
                    break;
                case 3:
                    html += "作废";
                    break;
                }
                html += "</div>";
                return html;
            };


            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1000,
                sortable: false,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['20','30', '50'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'RowID', text: '序号', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'FeeTypeString', text: '交易类型', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'Amount1', text:'变动金额', width: 120, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'MemberBalance', text:'可用余额', width: 120, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'Status', text: '状态', width: 120, cellsalign: 'center', align: 'center', cellsrenderer: statusRender },
                    { dataField: 'CreateTime', text:'交易时间', width: 220, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd HH:mm:ss" },
                    { dataField: 'Description', text: '描述', width: 220, cellsalign: 'center', align: 'center' }
                    

                ]
            });
            //$("#btnOutput").jqxButton({ theme: theme });
            //$("#btnOutput").click(function () {
            //    $("#jqxgrid").jqxGrid('exportdata', 'xls', 'jqxGrid');
            //});
        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <span class="fl">会员名：</span>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input type="text" class="input_text fl" maxlength="20" style="width: 150px;" id="txtName" runat="server"/>
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-top:5px; margin-left:5px;">记录时间：</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtStartDate" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" runat="server"/>
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
           <span class="fl" style="margin-top:8px; margin-left:5px; margin-right:5px;">~</span>
           <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
           </div>
           <input id="txtEndDate" class="input_text" type="text" onclick="WdatePicker()" style="width:100px" runat="server"/>
           <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
           </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
            </div>
            <%--<div style="float: left; margin-left: 5px;">
                <input type="button" id="btnOutput" value="导出当页" style="width: 100px" class="inputButton"/>
            </div>--%>
            <div style="float: left; margin-left: 5px;">
                <input id="ExcelExport" type="button" value="导出全部" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport_Click" />
            </div>
        </div>
        <div id="jqxWidget" style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
