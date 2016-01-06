<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Marketing_RoleManager.aspx.cs" Inherits="WebUI.UserMarketing.Marketing_Role.Marketing_RoleManager" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxcheckbox.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxpanel.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/global.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            //主题
            var theme = "arctic";

            //数据源
            var source = {
                url: '/HanderAshx/UserMarketing/RoleHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'RoleName', type: 'string' },
                    { name: 'Id', type: 'int' },
                    { name: 'Leave', type: 'int' },
                    { name: 'PartentID', type: 'int' }
                    
                    
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
               
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = column + "=" + value;
                // alert(parm);
               
                var link = "";
                if (data.Leave != 3) {
                    link = link + "<a style='text-align:center;float:right;height:25px; line-height:25px; margin-right:10px;' href='Role_Edit.aspx?pid=" + value + "&PartentID=" + data.PartentID + "&Leave=" + data.Leave + "&columnId=<%=ColumnId.ToString()%>'  target='_self'>添加下级角色</a> ";
                }
                else {
                    link = link + "<a style='text-align:center;float:right;height:25px; line-height:25px; margin-right:10px;' href='#'  target='_self'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a> ";
                }
             
                link=link+"<a style='text-align:center;float:right;height:25px; line-height:25px; margin-right:10px;' href='SystemRole.aspx?pid=" + value + "&PartentID="+data.PartentID+"&Leave="+data.Leave+"&columnId=<%=ColumnId.ToString()%>'  target='_self'>指定系统用户</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };


            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 750,
                autoheight: true,
                virtualmode: true,
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                        { text: '角色名称', dataField: 'RoleName', width: 200, cellsalign: 'left', align: 'left' },
                          { text: '当前等级', dataField: 'Leave', width: 200,cellsalign: 'center', align: 'center' },
                        { text: '操作', dataField: 'Id', width: 300, cellsrenderer: linkrenderer, cellsalign: 'center', align: 'center' },
                        //{ text: '状态', dataField: 'Status', width: 200, cellsrenderer: linkrenderer, cellsalign: 'center', align: 'center' },
                      
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

