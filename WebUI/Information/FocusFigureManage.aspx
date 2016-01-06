<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FocusFigureManage.aspx.cs" Inherits="WebUI.Information.FocusFigureManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <%--<script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>--%>
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
            display: inline-block;
            margin-right: 10px;
        }

            .selectDiv .select_box {
                width: 85px;
            }

        .wideHeight {
            height: 200px;
        }

        .imgL {
            max-width: 400px;
            width: expression(document.body.clientWidth > 400?"400px":"auto" );
        }

        .imgS {
            max-width: 200px;
            width: expression(document.body.clientWidth > 200?"200px":"auto" );
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
                url: '/HanderAshx/Information/FocusFigureHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'Url', type: 'string' },
                    { name: 'LargePicture', type: 'string' },
                    { name: 'SmallPicture', type: 'string' },
                    { name: 'Status', type: 'int' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'UpdateTime', type: 'string' },
                    { name: "Title", type: "string" },
                    { name: "ShowDesc", type: "int" }
                ],
                pagesize: 5,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.ID';
                    data.sortorder = data.sortorder || 'DESC';
                    data.status = $("#selStatus").val() || "";
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
                var parm = "/Information/FocusFigureEdit.aspx?" + column + "=" + value + "&columnId=<%=ColumnId%>";
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
                width: 900,
                pageable: true,
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
                    { dataField: 'ID', text: '<b>操作</b>', width: 40, align: 'center', sortable: false, cellsrenderer: linkrenderer },
                    { dataField: 'SmallPicture', text: '<b>缩略图</b>', width: 210, cellsalign: 'center', align: 'center', cellsrenderer: imgSRenderer },
                    { dataField: 'Title', text: '<b>标题</b>', width: 210, cellsalign: 'center', align: 'center' },
                    { dataField: 'Status', text: '<b>状态</b>', width: 40, cellsalign: 'center', align: 'center', cellsrenderer: statusRender },
                    { dataField: 'UpdateTime', text: '<b>更新时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center', cellsrenderer: dateatimeRenderer },
                    { dataField: 'ShowDesc', text: '<b>排序值</b>', width: 210, cellsalign: 'center', align: 'center' },

                ],
                showtoolbar: true,
                rendertoolbar: function (toolbar) {
                    var container = $("<div style='margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加</span></div>");
                    addButton.jqxButton({ width: 60, height: 20 });
                    addButton.click(function (event) {
                        location.href = '/Information/FocusFigureEdit.aspx?columnId=<%=ColumnId%>';
                    });
                    container.append(addButton);
                    toolbar.append(container);
                }
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
        <div class="selectDiv fl">
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select id="selStatus" name="status" class="fl">
                <option value="" selected="selected">--状态--</option>
                <option value="1">启用</option>
                <option value="0">不启用</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_status" />
            </div>
        </div>
        <div class="fl" style="margin-left: 5px;">
            <input type="button" id="btnSearch" class="inputButton" value="查询" />
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
    <div style='position: absolute; padding: 5px; z-index: 99; display: none;' id="showDiv"></div>
</body>
</html>
