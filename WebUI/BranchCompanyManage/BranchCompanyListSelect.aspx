<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchCompanyListSelect.aspx.cs" Inherits="WebUI.BranchCompanyManage.BranchCompanyListSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分公司管理</title>
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
        $(function () {
            //主题
            var theme = "arctic";

            var api = frameElement.api, W = api.opener;
            $("#jqxgrid").on('rowclick', function (event) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', event.args.rowindex);
                if (W.document.getElementById('txtBranchCompanyName') != null)
                    W.document.getElementById('txtBranchCompanyName').value = data.Name;
                if (W.document.getElementById('txtBranchCompanyID') != null)
                    W.document.getElementById('txtBranchCompanyID').value = data.ID;
                if (typeof (eval(W.SelectFun)) == "function") {
                    W.SelectFun();
                }
                api.close();
            });
            //数据源
            var source = {
                url: '/HanderAshx/BranchCompanyManage/BranchCompanyHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'Name', type: 'string' },
                    { name: 'SetUpDate', type: 'date' },
                    { name: 'Remark', type: 'string' }
                ]
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
                width: 840,
                autoheight: true,
                virtualmode: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { text: '<b>公司名称</b>', dataField: 'Name', width: 120, cellsalign: 'right', align: 'center' },
                    { text: '<b>成立日期</b>', dataField: 'SetUpDate', width: 120, cellsformat: "yyyy-MM-dd", cellsalign: 'right', align: 'center' },
                    { text: '<b>备注</b>', dataField: 'Remark', width: 560, cellsalign: 'left', align: 'left' }
                ]
            });
        });
    </script>
</head>
<body>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>