<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuaranteeRePrincipalTotal.aspx.cs"
    Inherits="WebUI.ReportStatistics.GuaranteeRePrincipalTotal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/select2css.js"></script>
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.aggregates.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>

    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />


    <script type="text/javascript">

        $(function () {

            //$.ajax({
            //    url: "/HanderAshx/ReportStatistics/DataStatistics.ashx",
            //    data: { "Method": "GuaranteeRePrincipalTotal" },
            //    dataType: "json",
            //    success: function (data) {
            
            //    }
            //});


            //数据源
            var source = {
                url: '/HanderAshx/ReportStatistics/DataStatistics.ashx',
                data: { "Method": "GuaranteeRePrincipalTotal" },
                cache: false,
                datatype: "json",
                //root: 'Rows',
                datafields: [
                    { name: 'RealName', type: 'string' },
                    { name: 'RePrincipal', type: 'number' }
                ]
            };
            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            //主题
            var theme = "arctic";

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1200,
                sortable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                editable: false,
                selectionmode: 'singlecell',
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'RealName', text: '<b>担保公司名称</b>', width: 600, cellsalign: 'center', align: 'center' },
                    { dataField: 'RePrincipal', text: '<b>应还本金</b>', width: 600, cellsalign: 'center', align: 'center', aggregates: ['sum'] },
                ]
            });
        });


    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="jqxgrid">
        </div>
    </form>
</body>
</html>
