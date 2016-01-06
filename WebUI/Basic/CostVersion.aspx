<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostVersion.aspx.cs" Inherits="WebUI.Basic.CostVersion" %>

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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.columnsresize.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {
            //主题
            var theme = "arctic";

            //数据源
            var source = {
                url: '/HanderAshx/Basic/CostVersionHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'VersionName', type: 'string' },
                    { name: 'ID', type: 'int' },
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
                var parm = column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<a style='height:25px;line-height:25px;margin-left:10px;' href='CostSetting.aspx?" + parm + "'  target='_self'>相关费用设置</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };


            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 700,
                autoheight: true,
                virtualmode: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                        { text: '<b>版本名称</b>', dataField: 'VersionName', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>查看费用设置</b>', dataField: 'ID', width: 100, cellsrenderer: linkrenderer, cellsalign: 'center', align: 'center' },
                        { text: '<b>创建时间</b>', dataField: 'CreateTime', width: 200, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>更新时间</b>', dataField: 'UpdateTime', width: 200, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>启用状态</b>', dataField: 'EnableStatus', width: 100, cellsalign: 'center', align: 'center' }
                ]
            });

        });
    </script>
</head>
<body>
    <div id='jqxWidget' style="font-size: 14px; font-family: Verdana; float: left;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>
