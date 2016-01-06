<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAwardWinners.aspx.cs" Inherits="WebUI.LuckyDraw.ViewAwardWinners" %>

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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxdata.export.js"></script>
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.export.js"></script>

    <script src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxwindow.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>

    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />

    <script type="text/javascript">
        $(function () {
            $("#btnSearch").click(function () {
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
                url: 'ViewAwardWinnersData.aspx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'SweepstakeName', type: 'string' },
                    { name: 'PrizeName', type: 'string' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'Phone', type: 'string' },
                    { name: 'CreateDate', type: 'date' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.Title = $('#title').val() || '';
                    data.SweepstakeId = $('#selType').val();
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    //data.sortdatafield = data.sortdatafield || 'P.RegTime';
                    //data.sortorder = data.sortorder || 'DESC';
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
                width: 1050,
                sortable: false,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                editable: false,
                selectionmode: 'singlecell',
                //pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                    { text: '抽奖活动名称', datafield: 'SweepstakeName', width: 300 },
                    { text: '会员名称', datafield: 'MemberName' },
                    { text: '真实姓名', datafield: 'RealName' },
                    { text: '联系方式', datafield: 'Phone' },
                    { text: '奖品名称', datafield: 'PrizeName' },
                    { text: '中奖时间', datafield: 'CreateDate', cellsformat: "yyyy-MM-dd HH:mm:ss" }
                ]
            });
            $("#btnOutput").jqxButton({ theme: theme });
            $("#btnOutput").click(function () {
                var data = Object();
                data.id = $('#selType').val();

                formatedData = buildQueryString(data);
                window.open("OutListToExcelHandler.ashx?" + formatedData, "_blank");
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1250px;">
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" />
            </div>
            <select id="selType" name="selType" runat="server">
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="/images/select_right.png" width="31" height="29" alt="" id="img_selType" />
            </div>
            <span class="fl" style="margin-top: 5px; margin-left: 5px;">用户名：</span>            
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="title" class="input_text" type="text" style="width: 200px" />
            <div class="fl">
                <img src="/images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnSearch" value="查 询" class="inputButton" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="btnOutput" value="导 出" class="inputButton" />
            </div>
        </div>
        <div id="jqxWidget" style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
