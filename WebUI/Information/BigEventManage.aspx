<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BigEventManage.aspx.cs" Inherits="WebUI.Information.BigEventManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/global.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
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
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <style type="text/css">
        .selectDiv {
            display: inline-block;
            margin-right: 10px;
        }

            .selectDiv .select_box {
                width: 85px;
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
                url: '/HanderAshx/Information/InformationHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'SectionID', type: 'int' },
                    { name: 'Title', type: 'string' },
                    { name: 'Status', type: 'int' },
                    { name: 'Recommend', type: 'bool' },
                    { name: 'PubTime', type: 'date' },
                    { name: 'ShowDesc', type: 'int' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.ID';
                    data.sortorder = data.sortorder || 'DESC';
                    data.columnId = "<%=ColumnId%>";
                    data.status = $("#selStatus").val() || "";
                    data.recommend = $("#selRecommend").val() || "";
                    //媒体类别
                    data.Image_value = $("#Select1").val() || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
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

            var linkrenderer = function (row, column, value) {
                var parm = "/Information/BigEvent.aspx?" + column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "<a style='height:25px;line-height:25px;margin-left:5px;' href='" + parm + "' target='_self'>修改</a>";
                return link;
            };

            var statusRender = function (row, colum, value) {
                var temp = parseInt(value);
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                switch (temp) {
                    case 0: html += "未审核"; break;
                    case 1: html += "审核通过"; break;
                    case 2: html += "审核不通过"; break;
                }
                html += "</div>";
                return html;
            }

            var recommendRender = function (row, colum, value) {
                var html = "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">";
                if (value == true || value == "true") {
                    html += "是";
                }
                else if (value == false || value == "false") {
                    html += "否";
                }
                html += "</div>";
                return html;
            }

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1020,
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
                    { dataField: 'ID', text: '<b>操作</b>', width: 40, cellsrenderer: linkrenderer, align: 'center', sortable: false },
                    { dataField: 'Title', text: '<b>标题</b>', width: 600, cellsalign: 'center', align: 'center' },
                    { dataField: 'Status', text: '<b>审核状态</b>', width: 90, cellsalign: 'center', align: 'center', cellsrenderer: statusRender },
                    { dataField: 'Recommend', text: '<b>是否推荐</b>', width: 90, cellsalign: 'center', align: 'center', cellsrenderer: recommendRender },
                    { dataField: 'PubTime', text: '<b>发布时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'ShowDesc', text: '<b>排序值</b>', width: 50, cellsalign: 'center', align: 'center' }
                ],
                showstatusbar: true,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>增加</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 60, height: 20 });
                    addButton.click(function (event) {
                        location.href = '/Information/BigEvent.aspx?columnId=<%=ColumnId%>';
                    });
                },
            });

            $("#btnSearch").click(function () {
                $("#jqxgrid").jqxGrid('updatebounddata');
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="display: none;">
            <div class="selectDiv fl">
                <div class="fl">
                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                </div>
                <select id="selStatus" name="selStatus" class="fl">
                    <option value="" selected="selected" class="fl">--审核状态--</option>
                    <option value="0">未审核</option>
                    <option value="1">审核通过</option>
                    <option value="2">审核不通过</option>
                </select>
                <div class="fl" style="margin-left: -5px; cursor: pointer;">
                    <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selStatus" />
                </div>
            </div>
            <div class="selectDiv fl">
                <div class="fl">
                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                </div>
                <select id="selRecommend" name="selRecommend" class="fl">
                    <option value="" selected="selected" class="fl">--是否推荐--</option>
                    <option value="1">是</option>
                    <option value="0">否</option>
                </select>
                <div class="fl" style="margin-left: -5px; cursor: pointer;">
                    <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selRecommend" />
                </div>
            </div>

            <div class="selectDiv fl">
                <div class="fl">
                    <img src="/images/input_left.png" width="4" height="29" alt="" />
                </div>
                <select id="Select1" name="Select1" class="fl">
                    <option value="0" selected="selected" class="fl">--选择媒体类别--</option>
                    <option value="1">第一财经</option>
                    <option value="2">深圳区特报</option>
                    <option value="3">经济观察报</option>
                    <option value="4">证券时报网</option>
                    <option value="5">深圳商报</option>
                    <option value="6">晶报</option>
                    <option value="7">深圳晚报</option>
                    <option value="8">腾讯财经</option>
                    <option value="9">光明网</option>
                    <option value="10">和讯</option>
                    <option value="11">新浪财经</option>
                    <option value="12">金融家</option>
                    <option value="13">搜狐新闻</option>
                    <option value="14">奥一网</option>
                    <option value="15">潮人在线</option>
                    <option value="16">广东新闻网</option>
                </select>
                <div class="fl" style="margin-left: -5px; cursor: pointer;">
                    <img src="../images/select_right.png" width="31" height="29" alt="" id="img_Select1" />
                </div>
            </div>

            <div class="fl" style="margin-top: 5px;">发布时间：</div>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtStartDate" class="input_text" type="text" onclick="WdatePicker()" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <div class="fl" style="margin-top: 5px; margin-left: 5px; margin-right: 5px;">~</div>
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="txtEndDate" class="input_text" type="text" onclick="WdatePicker()" style="width: 100px" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <div class="fl" style="margin-left: 5px;">
                <input type="button" id="btnSearch" class="inputButton" value="查询" />
            </div>
        </div>
        <div style="clear: both">
            <div id='jqxWidget' style="font-size: 14px; font-family: Verdana; float: left;">
                <div id="jqxgrid">
                </div>
            </div>
        </div>
    </form>
</body>
</html>
