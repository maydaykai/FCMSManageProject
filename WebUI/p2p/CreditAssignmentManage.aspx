<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditAssignmentManage.aspx.cs" Inherits="WebUI.p2p.CreditAssignmentManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=9" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>债权管理</title>
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="/css/icon.css" rel="stylesheet" />
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
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.aggregates.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="../css/select.css" rel="stylesheet" />
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="../css/global.css" rel="stylesheet" />
    <style type="text/css">
        .selectDiv {
            min-width: 1350px;
        }

            .selectDiv .select_box {
                width: 85px;
            }

            .selectDiv span {
                margin-top: 5px;
            }

        .selectDiv2 .select_box {
            width: 110px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnSearch').bind("click", function () {
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
                url: '/HanderAshx/p2p/CreditAssignment.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'LoanNumber', type: 'string' },
                    { name: 'OldLoanNumber', type: 'string' },
                    { name: 'LoanAmount', type: 'number' },
                    { name: 'DiscountRate', type: 'string' },
                    { name: 'RealLoanAmount', type: 'string' },
                    { name: 'LoanTermInfo', type: 'string' },
                    { name: 'RemainedDays', type: 'string' },
                    { name: 'BiddingProcess', type: 'string' },
                    { name: 'CreateTime', type: 'date' },
                    { name: 'FullScaleTime', type: 'date' },
                    { name: 'ExamStatusName', type: 'string' },
                    { name: 'FreezeAmount', type: 'number' }
                ],

                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'C.LoanNumber';
                    data.sortorder = data.sortorder || 'desc';
                    data.loanNumber = $("#txtNumber").val() || "";
                    data.oldLoanNumber = $("#txtOldNumber").val() || "";
                    data.examStatus = $("#sel_examstatus").val() || 0;
                    data.startDate = $("#startDate").val() || "";
                    data.endDate = $("#endDate").val() || "";

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
                width: 1280,
                sortable: true,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                showaggregates: true,
                showstatusbar: true,
                statusbarheight: 50,
                pagesizeoptions: ['10', '20', '30', '100'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
                        { text: '<b>转让人</b>', dataField: 'MemberName', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>真实姓名</b>', dataField: 'RealName', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>转让编号</b>', dataField: 'LoanNumber', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>原始借款编号</b>', dataField: 'OldLoanNumber', width: 120, cellsalign: 'center', align: 'center' },
                        { text: '<b>转让金额(元)</b>', dataField: 'LoanAmount', cellsformat: "F2", width: 150, cellsalign: 'right', align: 'center', aggregates: ['sum'] },
                        { text: '<b>折价率</b>', dataField: 'DiscountRate', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: function (row, column, value) { return "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">" + value + "%</div>"; } },
                        { text: '<b>实际转让金额(元)</b>', dataField: 'RealLoanAmount', cellsformat: "F2", width: 150, cellsalign: 'right', align: 'center', aggregates: ['sum'] },
                        { text: '<b>借款期限</b>', dataField: 'LoanTermInfo', width: 70, cellsalign: 'center', align: 'center' },
                        { text: '<b>剩余天数</b>', dataField: 'RemainedDays', width: 100, cellsalign: 'center', align: 'center' },
                        { text: '<b>投标进度</b>', dataField: 'BiddingProcess', width: 70, cellsalign: 'center', align: 'center', cellsrenderer: function (row, column, value) { return "<div style=\"text-overflow: ellipsis; overflow: hidden; padding-bottom: 2px; text-align: center; margin-top: 5px;\">" + value + "%</div>"; } },
                        { text: '<b>冻结金额(元)</b>', dataField: 'FreezeAmount', cellsformat: "F2", width: 150, cellsalign: 'right', align: 'center', aggregates: ['sum'] },
                        { text: '<b>状态</b>', dataField: 'ExamStatusName', width: 80, cellsalign: 'center', align: 'center' },
                        { text: '<b>申请时间</b>', dataField: 'CreateTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' },
                        { text: '<b>满标时间</b>', dataField: 'FullScaleTime', width: 180, cellsformat: "yyyy-MM-dd HH:mm:ss", cellsalign: 'center', align: 'center' }
                ]
            });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="selectDiv">
            <span class="fl">转让编号：</span>
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input type="text" id="txtNumber" name="input" class="fl input_text" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl">原始借款编号：</span>
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input type="text" id="txtOldNumber" name="input" class="fl input_text" />
            <div class="fl">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-left: 5px;">状态：</span>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select id="sel_examstatus" runat="server" name="examstatus">
                <option value="0" selected="selected">不限</option>
                <option value="1">转让中</option>
                <option value="2">成功转让</option>
                <option value="3">撤销转让</option>
                <option value="4">自动撤掉</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_examstatus" />
            </div>
            <span class="fl" style="margin-left: 5px;">申请时间：从</span>
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="startDate" class="fl input_text" type="text" onclick="WdatePicker()" style="width: 100px" runat="server" />
            <div class="fl" style="margin-right: 5px;">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <span class="fl" style="margin-left: 5px;margin-right: 5px;">至</span>
            <div style="width: 4px; float: left;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <input id="endDate" class="fl input_text" type="text" onclick="WdatePicker()" style="width: 100px" runat="server" />
            <div class="fl" style="margin-right: 5px;">
                <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
            </div>
            <input type="button" id="btnSearch" value="查询" class="inputButton" />
        </div>
        <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
            <div id="jqxgrid">
            </div>
        </div>

    </form>
    <script src="../js/select2css.js"></script>
</body>
</html>
