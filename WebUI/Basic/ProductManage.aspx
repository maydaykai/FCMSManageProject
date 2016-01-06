<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductManage.aspx.cs" Inherits="WebUI.Basic.ProductManage" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="Description">产品管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdatatable.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxtreegrid.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.columnsresize.js"></script>
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.edit.js"></script>
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxnumberinput.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.selection.js"></script>


    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="../css/icon.css" rel="stylesheet" />
    <style type="text/css">
        .selectDiv .select_box {
            float: left;
            width: 100px;
        }

        .selectDiv ul {
            width: 120px;
        }

        #treeGrid a {
            text-decoration: none;
            color: #444444;
        }

            #treeGrid a:hover {
                text-decoration: none;
                color: #ff0000;
            }
    </style>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //主题
            var theme = "arctic";

            var source =
            {
                url: '/HanderAshx/Basic/ProductHandler.ashx',
                dataType: "json",
                dataFields: [
                    { name: 'Name', type: 'string' },
                    { name: 'ID', type: 'int' },
                    { name: 'ParentId', type: 'int' },
                    { name: 'Designate', type: 'string' }
                ],
                hierarchy:
                {
                    keyDataField: { name: 'ID' },
                    parentDataField: { name: 'ParentId' }
                },
                id: 'EmployeeID'
            };

            var dataAdapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            $("#treeGrid").jqxTreeGrid(
            {
                theme: theme,
                width: 200,
                height: 400,
                source: dataAdapter,
                editable: true,
                editSettings: { saveOnPageChange: true, saveOnBlur: true, saveOnSelectionChange: true, cancelOnEsc: true, saveOnEnter: true, editSingleCell: true, editOnDoubleClick: true, editOnF2: true },
                altRows: true,
                ready: function () {
                    for (var i = 0; i < 20; i++) {
                        $("#treeGrid").jqxTreeGrid('expandRow', i);
                    }
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>发布新产品</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 90, height: 20 });
                    addButton.click(function (event) {
                        // window.location.href = "CostSettingEdit.aspx?columnId=";
                    });
                },
                showstatusbar: true,
                columns: [
                  { text: '<b>产品名称</b>', dataField: 'Name', cellsalign: 'left', align: 'center' },
                  { text: '<b>产品编号</b>', dataField: 'ID', width: 80, cellsalign: 'center', align: 'center', hidden: true }
                ]
            });
            
            $("#treeGrid").on('cellEndEdit', function (event) {
                var args = event.args;
                var data = $("#treeGrid").jqxGrid('getrow', args.rowindex);
                $.ajax({
                    type: "GET",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    url: "/HanderAshx/Basic/ProductHandler.ashx?sign=2&field=" + encodeURI(args.datafield + "='" + args.value + "'") + "&productscoreId=" + data.ID,
                    dataType: "json",
                    cache: false,
                    success: function (resultData) {
                        if (resultData.result == 0) {
                            LHG_Tips("smsTipA", "更新失败，请联系管理人员！", 3, 'success.png', '');
                        }
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    },
                    complete: function (x) {
                    }
                });
            });

            $("#treeGrid").on('rowSelect', function (event) {
                var productId = event.args.row.ID;
                //数据源
                var dataSource = {
                    url: '/HanderAshx/Basic/ProductHandler.ashx?sign=1&productId=' + productId + '',
                    cache: false,
                    datatype: "json",
                    root: 'Rows',
                    datafields: [
                        { name: 'ID', type: 'int' },
                        { name: 'ScoreItems', type: 'string' },
                        { name: 'FullMarks', type: 'number' }
                    ]
                };
                var adapter = new $.jqx.dataAdapter(dataSource);
                //// update data source.
                $("#jqxgrid").jqxGrid({ source: adapter });

            });
            

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                width: 250,
                autoheight: true,
                virtualmode: true,
                editable: true,
                rendergridrows: function (args1) {
                    return args1.data;
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加项目</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 80, height: 20 });
                    addButton.click(function (event) {
                        // window.location.href = "CostSettingEdit.aspx?columnId=";
                    });
                },
                showstatusbar: true,
                columns: [
                        { text: '<b>ID</b>', dataField: 'ID', width: 50, cellsalign: 'center', align: 'center', hidden: true },
                        {
                            text: '<b>项目名称</b>', dataField: 'ScoreItems', width: 150, cellsalign: 'center', align: 'center', cellvaluechanging: function (row, column, columntype, oldvalue, newvalue) {
                                if (newvalue == "" || newvalue == oldvalue || oldVal) return oldvalue;
                            }
                        },
                        {
                            text: '<b>满分值</b>', dataField: 'FullMarks', width: 100, cellsalign: 'center', align: 'center', columntype: 'numberinput',
                            cellvaluechanging: function (row, column, columntype, oldvalue, newvalue) {
                                if (newvalue == "") return oldvalue;
                            }
                        }
                ]
            });

            var oldVal;
            $("#jqxgrid").on('cellbeginedit', function (event) {
                var args = event.args;
                oldVal = args.value;
            });

            $("#jqxgrid").on('cellendedit', function (event) {
                var args = event.args;
                var data = $("#jqxgrid").jqxGrid('getrowdata', args.rowindex);
                $.ajax({
                    type: "GET",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    url: "/HanderAshx/Basic/ProductHandler.ashx?sign=2&field=" + encodeURI(args.datafield + "='" + args.value + "'") + "&productscoreId=" + data.ID,
                    dataType: "json",
                    cache: false,
                    success: function (resultData) {
                        if (resultData.result == 0) {
                            LHG_Tips("smsTipA", "更新失败，请联系管理人员！", 3, 'success.png', '');
                        }
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    },
                    complete: function (x) {
                    }
                });
            });

        });

        //#region 项目列表
        function GetProjectList(productId) {

            //主题
            var theme1 = "arctic";

            //数据源
            var source1 = {
                url: '/HanderAshx/Basic/ProductHandler.ashx?sign=1&productId=' + productId + '',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'ScoreItems', type: 'string' },
                    { name: 'FullMarks', type: 'number' }
                ]
            };

            //数据处理
            var dataadapter1 = new $.jqx.dataAdapter(source1, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            //var tRenderer = function (row, column, value) {   
            //    var link;
            //    link = data.ID;
            //    return link;
            //};

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme1,
                source: dataadapter1,
                width: 250,
                autoheight: true,
                virtualmode: true,
                editable: true,
                //enabletooltips: true,
                rendergridrows: function (args1) {
                    return args1.data;
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加项目</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 80, height: 20 });
                    addButton.click(function (event) {
                        // window.location.href = "CostSettingEdit.aspx?columnId=";
                    });
                },
                showstatusbar: true,
                columns: [
                        { text: '<b>ID</b>', dataField: 'ID', width: 50, cellsalign: 'center', align: 'center', hidden: true },
                        { text: '<b>项目名称</b>', dataField: 'ScoreItems', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>满分值</b>', dataField: 'FullMarks', width: 100, cellsalign: 'center', align: 'center', columntype: 'numberinput' }
                ]
            });

            //$("#jqxgrid").on('cellbeginedit', function (event) {
            //    debugger;
            //    var args = event.args;
            //    var data = $("#jqxgrid").jqxGrid('getrowdata', args.rowindex);
            //    $("#cellbegineditevent").html("Event Type: cellbeginedit, Column: " + args.datafield + ", Row: " + (1 + args.rowindex) + ", Value: " + args.value + ",ID：" + data.ID);
            //});
            $("#jqxgrid").on('cellendedit', function (event) {
                var args = event.args;
                var data = $("#jqxgrid").jqxGrid('getrowdata', args.rowindex);
                $.ajax({
                    type: "GET",
                    async: true,
                    contentType: "application/json; charset=utf-8",
                    url: "/HanderAshx/Basic/ProductHandler.ashx?sign=2&field=" + encodeURI(args.datafield + "='" + args.value + "'") + "&productscoreId=" + data.ID,
                    dataType: "json",
                    cache: false,
                    success: function (resultData) {
                        alert(resultData.message);
                    },
                    error: function (xmlHttpRequest) {
                        alert(xmlHttpRequest.innerText);
                    },
                    complete: function (x) {
                    }
                });
            });
        }
        //#endregion 项目列表

    </script>

</head>
<body>
    <form id="Form1" runat="server">
        <div style="width: auto; min-width: 800px;">
            <div id="treeGrid" style="float: left;">
            </div>
            <div id="jqxgrid" style="float: left; margin-left: 10px;">
            </div>
            <div id="cellendeditevent"></div>
            <div id="cellbegineditevent"></div>
        </div>
    </form>
</body>
</html>
