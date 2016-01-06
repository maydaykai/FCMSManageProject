<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BidConfigManage.aspx.cs" Inherits="WebUI.Basic.BidConfigManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>投资额度上下限设置</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script src="../js/lhgdialog/lhgdialog.min.js"></script>
    <script src="../js/lhgdialog/ShowDialog.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //主题
            var theme = "arctic";

            //数据源
            var source = {
                url: '/HanderAshx/Basic/BidConfigHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'LoanAmount', type: 'number' },
                    { name: 'MinInvestment', type: 'number' },
                    { name: 'MaxInvestment', type: 'number' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'UpdateTime', type: 'date' },
                    { name: 'EnableStatus', type: 'bool' }
                ]
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var linkrenderer = function (row, column, value) {
                var parm = "/Basic/BidConfigEdit.aspx?" + column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<a style='height:25px;line-height:25px;margin-left:5px;' href='javascript:void(0)' onclick=\"MessageWindow(315,190,'" + parm + "')\"; target='_self'>修改</a>";
                return link;
            };
            var checkBoxBool = function (row, column, value) {
                var html = "<div style='margin-left:30px;height:28px; line-height:28px;'><span class='iconCheckBox iconCheckBox-" + value + "' style='*margin-top:5px;'>&nbsp;</span></div>";
                return html;
            };

            var loanAmount = function (row, column, value) {
                var html = "<div style='margin-right:5px;height:25px; line-height:25px;text-align:right;'>≥&nbsp;" + fmoney(value, 2) + "</div>";
                return html;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 840,
                autoheight: true,
                virtualmode: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 60, height: 20 });
                    addButton.click(function (event) {
                        MessageWindow(315, 190, '/Basic/BidConfigEdit.aspx?columnId=<%=ColumnId%>');
                    });

                },
                showstatusbar: true,
                columns: [
                        { text: '<b>操作</b>', dataField: 'ID', width: 40, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer },
                        { text: '<b>借款金额(¥)</b>', dataField: 'LoanAmount', width: 120, cellsformat: "F2", cellsalign: 'right', align: 'center', cellsrenderer: loanAmount },
                        { text: '<b>最小投资金额(¥)</b>', dataField: 'MinInvestment', width: 120, cellsformat: "F2", cellsalign: 'right', align: 'center' },
                        { text: '<b>最大投资金额(¥)</b>', dataField: 'MaxInvestment', width: 120, cellsformat: "F2", cellsalign: 'right', align: 'center' },
                        { text: '<b>创建时间</b>', dataField: 'CreateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>更新时间</b>', dataField: 'UpdateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>启用状态</b>', dataField: 'EnableStatus', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: checkBoxBool }
                ]
            });

        });

            function fmoney(s, n) {
                n = n > 0 && n <= 20 ? n : 2;
                s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";
                var l = s.split(".")[0].split("").reverse(),
                r = s.split(".")[1];
                t = "";
                for (i = 0; i < l.length; i++) {
                    t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
                }
                return t.split("").reverse().join("") + "." + r;
            }

    </script>
</head>
<body>
    <div id='jqxWidget' style="font-size: 14px; font-family: Verdana; float: left;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>
