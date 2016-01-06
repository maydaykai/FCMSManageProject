<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelUserManage.aspx.cs" Inherits="WebUI.AdvertisingManage.ChannelUserManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>渠道商权限管理</title>
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
                var parm = "/AdvertisingManage/ChannelUserAdd.aspx?columnId=<%=ColumnId%>";
                return "<div style='line-height:25px;text-align: center;'><a href='" + parm + "&id=" + data.Id + "' style='cursor:pointer;' >[修改]</a></div>";
            };

            var formatDate2 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var txt = data.Permissions == "" ? "" : (data.Permissions + ",").replace(/1,/g, "[汇总数据]").replace(/2,/g, "[数据明细]").replace(/3,/g, "[结算费用]");
                return "<div style='line-height:25px;text-align: center;'>" + txt + "</div>";
            };

            var formatDate3 = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = data.IsDelete == 1 ? "启用" : "禁用";
                return "<div style='line-height:25px;text-align: center;'>" + parm + "</div>";
            };

            //数据源
            var source = {
                url: '/HanderAshx/AdvertisingManage/ChannelUserHandler.ashx?t=list',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'rowNum', type: 'int' },
                    { name: 'Id', type: 'int' },
                    { name: 'ChannelName', type: 'string' },
                    { name: 'DimChannelId', type: 'int' },
                    { name: 'ContactPerson', type: 'string' },
                    { name: 'Permissions', type: 'string' },
                    { name: 'IsDelete', type: 'int' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'Channel', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
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
                width: 1080,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 2px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>新增渠道商</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 120, height: 20 });
                    addButton.click(function (event) {
                        window.location.href = "/AdvertisingManage/ChannelUserAdd.aspx?t=1&columnId=<%=ColumnId%>";
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
                    { dataField: 'rowNum', text: '<b>序号</b>', width: 50, cellsalign: 'center', align: 'center' },
                    { dataField: 'CreateTime', text: '<b>创立日期</b>', cellsformat: "yyyy/MM/dd", width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'ChannelName', text: '<b>用户名</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'Channel', text: '<b>对应渠道</b>', width: 160, cellsalign: 'center', align: 'center' },
                    { dataField: 'Permissions', text: '<b>查看权限</b>', width: 250, cellsalign: 'center', align: 'center', cellsrenderer: formatDate2 },
                    { dataField: 'ContactPerson', text: '<b>联系人</b>', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: '', text: '<b>帐户状态</b>', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: formatDate3 },
                    { dataField: 'a', text: '<b>操作</b>', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: formatDate }
                ]
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px; display:none;">            
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