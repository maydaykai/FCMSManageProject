<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberProjectSummary.aspx.cs" Inherits="WebUI.ReportStatistics.MemberProjectSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>投资人项目汇总</title>
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
        .selectDiv .select_box { width: 100px; }
        .selectDiv2 .select_box { width: 120px; }
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
                url: '/HanderAshx/ReportStatistics/MemberProjectHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
		            { name: 'BidMemberName', type: 'string' },
                    { name: 'BidRealName', type: 'string' },
		            { name: 'LoanNumber', type: 'string' },
                    { name: 'LoanRealName', type: 'string' },
                    { name: 'ExamStatus', type: 'string' },
                    { name: 'LoanRate', type: 'number' },
                    { name: 'LoanTerm', type: 'string' },
                    { name: 'BidStratTime', type: 'date' },
                    { name: 'ReviewTime', type: 'date' },
                    { name: 'BiddingFreezing', type: 'number' },
                    { name: 'AR_Principal', type: 'number' },
                    { name: 'RE_Principal', type: 'number' },
                    { name: 'DI_Principal', type: 'number' },
                    { name: 'AR_Interest', type: 'number' },
                    { name: 'RE_Interest', type: 'number' },
                    { name: 'DI_Interest', type: 'number' },
                    { name: 'BO_Interest', type: 'number' },
                    { name: 'BidServiceCharges', type: 'number' },
                    { name: 'RecoIncentive', type: 'number' },
                    { name: 'ActivitiesAward', type: 'number' },
                    { name: 'ProjectIncome', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.currentpage = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.uName = encodeURI(encodeURI($.trim($("#txtName").val()))) || "";
                    data.uType = $.trim($("#selSUserType").val()) || "0";
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


            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1200,
                sortable: false,
                pageable: true,
                autoheight: true,
                virtualmode: true,
                sorttogglestates: 1,
                pagesizeoptions: ['10', '20', '30'],
                rendergridrows: function (args) {
                    return args.data;
                },
                columns: [
		            { dataField: 'BidMemberName', text: '<span style="color:#9F79EE;">会员名</span>', columngroup: 'B', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'BidRealName', text: '<span style="color:#9F79EE;">真实姓名</span>', columngroup: 'B', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanNumber', text: '<span style="color:#FF6347;">借款编号</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanRealName', text: '<span style="color:#FF6347;">借款人真实姓名</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'ExamStatus', text: '<span style="color:#FF6347;">项目状态</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanRate', text: '<span style="color:#FF6347;">年利率（%）</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'LoanTerm', text: '<span style="color:#FF6347;">借款期限</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center'},
                    { dataField: 'BidStratTime', text: '<span style="color:#FF6347;">竞标开始日</span>', columngroup: 'L', width: 150, cellsalign: 'right', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'ReviewTime', text: '<span style="color:#FF6347;">放款日</span>', columngroup: 'L', width: 150, cellsalign: 'right', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'BiddingFreezing', text: '<span style="color:#EE7600;">竞标冻结</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'AR_Principal', text: '<span style="color:#FF6347;">应收本金</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'RE_Principal', text: '<span style="color:#EE7600;">已收本金</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'DI_Principal', text: '<span style="color:#EE7600;">待收本金</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'AR_Interest', text: '<span style="color:#9F79EE;">应收利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'RE_Interest', text: '<span style="color:#9F79EE;">已收利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'DI_Interest', text: '<span style="color:#9F79EE;">待收利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'BO_Interest', text: '<span style="color:#9F79EE;">已收逾期利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'BidServiceCharges', text: '<span style="color:#8B0000;">已付投资人居间服务费</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'RecoIncentive', text: '<span style="color:#8B0000;">推荐投资奖励</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ActivitiesAward', text: '<span style="color:#8B0000;">活动奖励</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ProjectIncome', text: '<span style="color:#8B0000;">平台损益</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" }
                ],
                columngroups:
                [
                    { text: '<span style="color:#9F79EE;font-size:14px;">投资人信息</span>', align: 'center', name: 'B' },
                    { text: '<span style="color:#FF6347;font-size:14px;">项目信息</span>', align: 'center', name: 'L' },
                    { text: '<span style="color:#EE7600;font-size:14px;">本金</span>', align: 'center', name: 'P' },
                    { text: '<span style="color:#9F79EE;font-size:14px;">利息</span>', align: 'center', name: 'I' },
                    { text: '<span style="color:#8B0000;font-size:14px;">平台损益</span>', align: 'center', name: 'S' }
                ],

            });
        });
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selSUserType" id="selSUserType" class="fl" runat="server">
                <option value="0">投资人会员名</option>
                <option value="1">投资人真实姓名</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selSUserType" />
            </div>
            <div style="float: left;">
                <input id="txtName" type="text" name="txtName" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input id="ExcelExport" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport_Click" />
            </div>
        </div>
        <div id="jqxWidget" style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>
