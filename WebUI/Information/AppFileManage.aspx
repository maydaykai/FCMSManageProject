<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AppFileManage.aspx.cs" Inherits="WebUI.Information.AppFileManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script src="../js/jQuery/jquery-1.7.2.min.js"></script>
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <script src="../js/select2css.js"></script>
    <script src="../js/datetime.js"></script>
    <style type="text/css">
        .selectDiv {
            display:inline-block;margin-right:10px;
        }
        .selectDiv .select_box {
         width: 85px;
        }
        .wideHeight {
            height:200px;
        }
        .imgL {
            max-width:400px;
            width:expression(document.body.clientWidth > 400?"400px":"auto" );
        }
        .imgS {
            max-width:200px;
            width:expression(document.body.clientWidth > 200?"200px":"auto" );
        }
    </style>
    <script type="text/javascript">
        $(function () {
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
                url: '/HanderAshx/Information/AppFileHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'OS', type: 'string' },
                    { name: 'AppName', type: 'string' },
                    { name: 'Version', type: 'string' },
                    { name: 'VersionNum', type: 'int' },
                    { name: 'DownURL', type: 'string' },
                    { name: "UpdateTime", type: "date" }
                ],
                pagesize: 5,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.ID';
                    data.sortorder = data.sortorder || 'DESC';
                    data.os = data.os || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function (column, direction) { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
            };

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });
            var imgSRenderer = function (row, column, value) {
                var html = "<div style=\"overflow: visible; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                html += "<img class='imgS' src='" + value + "'/>";
                html += "</div>";
                return html;
            }

            var statusRender = function (row, colum, value) {
                var temp = parseInt(value);
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center;\">";
                switch (temp) {
                    case 0: html += "禁用"; break;
                    case 1: html += "启用"; break;
                }
                html += "</div>";
                return html;
            }

            var linkrenderer = function (row, column, value) {
                var parm = "/Information/AppFileEdit.aspx?" + column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; \">";
                link += "<a style='height:25px;line-height:25px;margin-left:5px;' href='" + parm + "' target='_self'>修改</a>";
                link += "</div>";
                return link;
            };

            var dateatimeRenderer = function (row, column, value) {
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center;\">";
                html += $.FormatDateTime(value.toString(), 2);
                html += "</div>";
                return html;
            }

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1280,
                pageable: false,
                rowsheight: 105,
                autoheight: true,
                altrows: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['5', '10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { dataField: 'ID', text: '<b>操作</b>', width: 80, align: 'center', sortable: false, cellsrenderer: linkrenderer },
                    { dataField: 'OS', text: '<b>手机平台</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'AppName', text: '<b>应用名称</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'Version', text: '<b>版本号</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'VersionNum', text: '<b>版本编号</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'DownURL', text: '<b>下载地址</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'UpdateTime', text: '<b>更新时间</b>', width: 200, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' }
                ]
            });
            $("#btnSearch").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
        });
          $(".imgS").live("mouseover", function (e) {
              $("#showDiv").html("<img src='" + $(this).attr("src") + "'/>").css("left", e.pageX).css("top", e.pageY).show();
          }).live("mouseout", function () {
              $("#showDiv").empty();
          });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
    <div style='position:absolute;padding:5px;z-index:99;display:none;' id="showDiv"></div>
</body>
</html>

