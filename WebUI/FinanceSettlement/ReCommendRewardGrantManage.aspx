<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReCommendRewardGrantManage.aspx.cs" Inherits="WebUI.FinanceSettlement.ReCommendRewardGrantManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>推荐奖励发放管理</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
    </style>
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

            //数据源
            var source = {
                url: '/HanderAshx/FinanceSettlement/RecommendRewardHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'Year', type: 'int' },
                    { name: 'Month', type: 'int' },
                    { name: 'AuditStatus', type: 'int' },
                    { name: 'Amount', type: 'number' },
                    { name: 'AuditStatusString', type: 'string' },
                    { name: 'ApplyTime', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'ApplyTime';
                    data.sortorder = data.sortorder || 'Desc';
                    data.year = $("#txtYear").val() || "";
                    data.status = $("#selStatus").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) { source.totalrecords = data.TotalRows; }
            }

            //数据处理
            var dataadapter = new $.jqx.dataAdapter(source, {
                contentType: "application/json; charset=utf-8",
                loadError: function (xhr, status, error) {
                    alert(error);
                }
            });

            var linkrenderer = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var parm = column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "";
                link = "<a href='RecommendRewardGrantAdd.aspx?" + parm + "'  target='_self' style='margin-left:10px;height:25px;line-height:25px;'>" + ((data.AuditStatus == 2 || data.AuditStatus == 3 || data.AuditStatus == 4) ? "查看" : "审核") + "</a>";
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };
            var daterenderer = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                return "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">" + data.Year + "年" + data.Month + "月</div>";
            };
            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 700,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>新增发放推荐奖励申请</span></div>");
                    container.append(addButton);
                    statusbar.append(container);
                    addButton.jqxButton({ width: 160, height: 20 });
                    addButton.click(function (event) {
                        window.location.href = "/FinanceSettlement/RecommendRewardGrantAdd.aspx?columnId=<%=ColumnId%>";
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
                    { dataField: 'ID', text: '<b>操作</b>', width: 80, cellsrenderer: linkrenderer, align: 'center' },
                    { dataField: 'Year', text: '<b>申请年月</b>', width: 150, cellsrenderer: daterenderer, cellsalign: 'center', align: 'center' },
                    { dataField: 'Amount', text: '<b>金额（元）</b>', width: 150, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'AuditStatusString', text: '<b>审核状态</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'ApplyTime', text: '<b>申请时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' }
                ]
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
        <span class="fl">申请年：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl" maxlength="20" style="width: 100px;" id="txtYear" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selStatus" id="selStatus" class="fl">
            <option value="">--审核状态--</option>
            <option value="0">初审中</option>
            <option value="1">复审中</option>
            <option value="2">初审不通过</option>
            <option value="3">复审不通过</option>
            <option value="4">复审通过</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selStatus" />
        </div>
        <div style="float: left; margin-left: 5px;">
            <input type="button" id="applyfilter" value="查 询" class="inputButton" />
        </div>
    </div>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
        <div id="jqxgrid">
        </div>
    </div>
</body>
</html>
