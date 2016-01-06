<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayLimitationManage.aspx.cs" Inherits="WebUI.Basic.PayLimitationManage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />

    <script type="text/javascript">


        $(function () {

            var jqx = $("#jqxgrid");

            jqx.jqxGrid({ editmode: 'dblclick' });
            //主题
            var theme = "arctic";

            //参数组装
            function buildQueryString(data) {
                var str = ''; for (var prop in data) {
                    if (data.hasOwnProperty(prop) && prop != "uid") {
                        str += prop + '=' + data[prop] + '&';
                    }
                }
                return str.substr(0, str.length - 1);
            }

            var formatedData = '';

            //数据源
            var source = {
                url: '/HanderAshx/Basic/PayLimitationHandler.ashx',
                data: { "Method": "GetData" },
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'Id', type: 'int' },
                    { name: 'BankName', type: 'string' },
                    { name: 'SinglePay', type: 'string' },
                    { name: 'SingleDay', type: 'string' },
                    { name: 'SingleMonth', type: 'string' },
                    { name: 'Remark', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.dateStart = $("#txtDateStart").val() || "";
                    data.dateEnd = $("#txtDateEnd").val() || "";
                    data.typeId = $("#sel_TotalType").val() || 1;
                    data.ChannelType = $("#jqxSelectVal").val() || "";
                    data.dateType = $("#sel_DateType").val() || 1;
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
                width: 1200,
                sortable: false,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                editable: true,
                selectionmode: 'singlerow',
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'Id', hidden: true },
                    { dataField: 'BankName', text: '<b>银行名称</b>', width: 210, cellsalign: 'center', align: 'center' },
                    { dataField: 'SinglePay', text: '<b>单笔限额</b>', width: 210, cellsalign: 'center', align: 'center', },
                    { dataField: 'SingleDay', text: '<b>单日限额</b>', width: 210, cellsalign: 'center', align: 'center', },
                    { dataField: 'SingleMonth', text: '<b>单月限额</b>', width: 210, cellsalign: 'center', align: 'center' },
                    { dataField: 'Remark', text: '<b>备注</b>', width: 360, cellsalign: 'center', align: 'center' }
                ]
            });

            $("#btnUpdate").click(function () {
                var selectindex = jqx.jqxGrid('getselectedrowindex');
                if (selectindex == -1) {
                    return;
                }
                var data = jqx.jqxGrid('getrowdata', selectindex);

                var str = buildJson(data);

                $.ajax({
                    url: "/HanderAshx/Basic/PayLimitationHandler.ashx",
                    data: { "Method": "Update", "Para": JSON.stringify(str) },
                    dataType: "json",
                    success: function (data) {
                        if (data.success == "success") {
                            MessageAlert('修改成功', 'warning', '');
                            jqx.jqxGrid("refresh");
                        }
                    }
                });
            });

            var isnewrow = false;

            $("#btnAdd").click(function () {
                var rowid = jqx.jqxGrid('getdatainformation').rowscount;
                if (isnewrow) {
                    var selectindex = jqx.jqxGrid('getselectedrowindex');
                    if (selectindex == -1) {
                        return;
                    }
                    var data = jqx.jqxGrid("getrowdata", selectindex);
                    var json = buildJson(data);
                    json.Id = 0;
                    $.ajax({
                        url: "/HanderAshx/Basic/PayLimitationHandler.ashx",
                        data: { "Method": "Add", "Para": JSON.stringify(json) },
                        dataType: "json",
                        success: function (data) {
                            if (data.success == "success") {
                                MessageAlert('添加成功', 'warning', '');
                                setTimeout('window.location.reload()', 2000);
                            }
                        }
                    });
                    isnewrow = false;
                } else {
                    jqx.jqxGrid('addrow', rowid, {});
                    isnewrow = true;
                }
            });

            $("#btnDelete").click(function () {
                var selectindex = jqx.jqxGrid('getselectedrowindex');
                if (selectindex == -1) {
                    return;
                }
                var data = jqx.jqxGrid('getrowdata', selectindex);
                var str = buildJson(data);
                $.ajax({
                    url: "/HanderAshx/Basic/PayLimitationHandler.ashx",
                    data: { "Method": "Delete", "Id": str.Id },
                    dataType: "json",
                    success: function (data) {
                        if (data.success == "success") {
                            MessageAlert('删除成功', 'warning', '');
                            var rowcount = jqx.jqxGrid("getdatainformation").rowscount;
                            if (rowcount == null || rowcount <= 1)
                                window.location.reload();
                            else
                                jqx.jqxGrid("deleterow", selectindex);
                        }
                    }
                });
            });

        });

        function buildJson(data) {
            var json = new Object();
            for (var prop in data) {
                if (data.hasOwnProperty(prop) && prop != "uid") {
                    json[prop] = data[prop];
                }
            }
            return json;
        }

    </script>

    <style type="text/css">
        .datagrid { font-size: 13px; font-family: Verdana; float: left; clear: both; margin-top: 15px; }
        .button { float: left; margin-left: 5px; }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1250px;">
            <div class="button">
                <input type="button" id="btnAdd" value="添 加" class="inputButton" />
            </div>
            <div class="button">
                <input type="button" id="btnUpdate" value="修 改" class="inputButton" />
            </div>
            <div class="button">
                <input type="button" id="btnDelete" value="删 除" class="inputButton" />
            </div>
        </div>
        <div id='jqxWidget' class="datagrid">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
