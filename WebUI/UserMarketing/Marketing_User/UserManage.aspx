<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_User.UserManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户管理</title>
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

            //数据源
            var source = {
                url: '/HanderAshx/UserManage/UserHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'UserName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'AnotherName', type: 'string' },
                    { name: 'Mobile', type: 'string' },
                    { name: 'RegTime', type: 'date' },
                    { name: 'LastLoginTime', type: 'date' },
                    { name: 'RelationID', type: 'int' },
                    { name: 'IsLock', type: 'bool' },
                    { name: 'UpdateTime', type: 'date' },
                    { name: 'RoleId', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'ID';
                    data.sortorder = data.sortorder || 'desc';
                    //data.filterscount = data.filterscount || 0;
                    data.uname = encodeURI($("#userName").val()) || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                //filter: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'filter'); },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
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
                var link = "<a href='Role_Distribution.aspx?" + parm + "'  target='_self' style='margin-left:10px;height:25px;line-height:25px;'>分配</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };

            var isLockRenderer = function (row, column, value) {
                var strHtml = '<div style="text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;">';
                if (value) {
                    strHtml += "是";
                } else {
                    strHtml += "否";
                }
                strHtml += "</div>";
                return strHtml;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 980,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                //filterable: true,
                //showfilterrow: true,//过滤
                rendergridrows: function (args) {
                    return args.data;
                },
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                   // var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 60, height: 20 });
                    addButton.click(function (event) {
                        window.location.href = "/UserManage/UserEdit.aspx?columnId=<%=ColumnId%>";
                    });
                },
                showstatusbar: true,
                showtoolbar: true,
                rendertoolbar: function (toolbar) {
                    var me = this;
                    var container = $("<div style='margin: 5px;'></div>");
                    var span = $("<span style='float: left; margin-top: 5px; margin-right: 4px;'>用户名: </span>");
                    var input = $("<input class='jqx-input jqx-widget-content jqx-rc-all' id='userName' type='text' style='height: 23px; float: left; width: 223px;' />");
                    toolbar.append(container);
                    container.append(span);
                    container.append(input);
                    if (theme != "") {
                        input.addClass('jqx-widget-content-' + theme);
                        input.addClass('jqx-rc-all-' + theme);
                    }
                    var oldVal = "";
                    input.on('keydown', function (event) {
                        if (input.val().length > 0) {

                            if (me.timer) {
                                clearTimeout(me.timer);
                            }
                            if (oldVal != input.val()) {
                                me.timer = setTimeout(function () {
                                    $("#jqxgrid").jqxGrid('updatebounddata');
                                }, 1000);
                                oldVal = input.val();
                            }
                        }
                    });
                },
                columns: [
                        { text: '<b>操作</b>', dataField: 'ID', width: 50, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer },
                        { text: '<b>用户名</b>', dataField: 'UserName', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>真实姓名</b>', dataField: 'RealName', width: 150, cellsalign: 'center', align: 'center' },
                        { text: '<b>别名</b>', dataField: 'AnotherName', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>手机号码</b>', dataField: 'Mobile', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>注册时间</b>', dataField: 'RegTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>最后登录时间</b>', dataField: 'LastLoginTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>是否锁定</b>', dataField: 'IsLock', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: isLockRenderer },
                        { text: '<b>更新时间</b>', dataField: 'UpdateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>所属角色</b>', dataField: 'RoleId', width: 180 }
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
