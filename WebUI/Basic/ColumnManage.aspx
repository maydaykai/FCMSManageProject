<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ColumnManage.aspx.cs" Inherits="WebUI.Basic.ColumnManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>栏目列表</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
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
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            //主题
            var theme = "arctic";

            //数据源
            var source = {
                url: '/HanderAshx/Basic/ColumnHander.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'Name', type: 'string' },
                    { name: 'ID', type: 'int' }
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
                var parm = column + "=" + value;
                var link = "<a style='text-align:center;float:right;height:25px; line-height:25px; margin-right:50px;' href='ColumnEdit.aspx?" + parm + "&columnId=<%=ColumnId.ToString()%>'  target='_self'>修改</a><a style='text-align:center;float:right;height:25px; line-height:25px; margin-right:10px;' href='ColumnEdit.aspx?pid=" + value + "&columnId=<%=ColumnId.ToString()%>'  target='_self'>添加子栏目</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };


            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 400,
                autoheight: true,
                virtualmode: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                        { text: '栏目名称', dataField: 'Name', width: 200, cellsalign: 'left', align: 'left' },
                        { text: '栏目管理', dataField: 'ID', width: 200, cellsrenderer: linkrenderer, cellsalign: 'center', align: 'center' }
                ]
            });

        });
    </script>
</head>
<body>
    <div>
        <input type="button" value="添加根栏目" id="btnAddColumn" onclick="javascript: location.href = 'ColumnEdit.aspx?pid=0&columnId=<%=ColumnId.ToString()%>';" class="inputButton" style="width:120px; margin-bottom:5px;" />
    </div>
    <div id='jqxWidget' style="font-size: 14px; font-family: Verdana; float: left;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>
