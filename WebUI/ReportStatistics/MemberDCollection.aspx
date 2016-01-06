<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberDCollection.aspx.cs" Inherits="WebUI.ReportStatistics.MemberDCollection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>会员账户数据汇总</title>
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
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>

    <script src="/js/lhgdialog/ShowDialog.js"></script>
    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        .selectDiv2 .select_box { width: 105px; }
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
                url: '/HanderAshx/ReportStatistics/MemberDCollectionHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'I_I_Principal', type: 'number' },
                    { name: 'I_I_Interest', type: 'number' },
                    { name: 'I_I_OverdueInterest', type: 'number' },
                    { name: 'I_I_Penalty', type: 'number' },
                    { name: 'I_I_RecommendIncentives', type: 'number' },
                    { name: 'I_P_Bidding', type: 'number' },
                    { name: 'I_P_InvestorServingFee', type: 'number' },
                    { name: 'I_F_FrozenBid', type: 'number' },
                    { name: 'I_F_FrozenInterest', type: 'number' },
                    { name: 'L_I_LoanAmount', type: 'number' },
                    { name: 'L_I_ReGuaranteeFee', type: 'number' },
                    { name: 'L_I_ReLoanServingFee', type: 'number' },
                    { name: 'L_P_LoanAmount', type: 'number' },
                    { name: 'L_P_Interest', type: 'number' },
                    { name: 'L_P_OverdueInterest', type: 'number' },
                    { name: 'L_P_OverduePayment', type: 'number' },
                    { name: 'L_P_PenaltyByBid', type: 'number' },
                    { name: 'L_P_LoanServingFee', type: 'number' },
                    { name: 'L_P_PenaltyBySys', type: 'number' },
                    { name: 'O_I_Recharge', type: 'number' },
                    { name: 'O_I_Settlement', type: 'number' },
                    { name: 'O_I_RedEnvelope', type: 'number' },
                    { name: 'O_I_ActivitiesAwardFroz', type: 'number' },
                    { name: 'O_I_ActivitiesAward', type: 'number' },
                    { name: 'O_P_CashAmount', type: 'number' },
                    { name: 'O_P_RechargeFee', type: 'number' },
                    { name: 'O_P_CashFee', type: 'number' },
                    { name: 'O_P_VipFee', type: 'number' },
                    { name: 'O_F_Recharge', type: 'number' },
                    { name: 'O_F_Settlement', type: 'number' },
                    { name: 'MemberBalance', type: 'number' },
                    { name: 'DueInPrincipal', type: 'number' },
                    { name: 'DueInInterest', type: 'number' },
                    { name: 'DueInOverdueInterest', type: 'number' },
                    { name: 'SstillPrincipal', type: 'number' },
                    { name: 'SstillInterest', type: 'number' },
                    { name: 'SstillOverdueInterest', type: 'number' },
                    { name: 'SstillLoanServingFee', type: 'number' }

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
                    { dataField: 'MemberName', text: '<span style="color:#9F79EE;">会员名</span>', columngroup: 'MI', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<span style="color:#9F79EE;">真实姓名</span>', columngroup: 'MI', width: 110, cellsalign: 'center', align: 'center' },
                    { dataField: 'MemberBalance', text: '<span style="color:#EE7600;">账户可用余额</span>', columngroup: 'MB', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'DueInPrincipal', text: '<span style="color:#EE7600;">待收本金</span>', columngroup: 'MD', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'DueInInterest', text: '<span style="color:#EE7600;">待收利息</span>', columngroup: 'MD', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'DueInOverdueInterest', text: '<span style="color:#EE7600;">待收逾期利息</span>', columngroup: 'MD', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SstillPrincipal', text: '<span style="color:#EE7600;">待还本金</span>', columngroup: 'MS', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SstillInterest', text: '<span style="color:#EE7600;">待还利息</span>', columngroup: 'MS', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SstillOverdueInterest', text: '<span style="color:#EE7600;">待还逾期利息</span>', columngroup: 'MS', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SstillLoanServingFee', text: '<span style="color:#EE7600;">待还借款人居间服务费</span>', columngroup: 'MS', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_I_Principal', text: '<span style="color:#ff0000;">已收本金</span>', columngroup: 'II', width: 100, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_I_Interest', text: '<span style="color:#ff0000;">已收利息</span>', columngroup: 'II', width: 100, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_I_OverdueInterest', text: '<span style="color:#ff0000;">已收逾期利息</span>', columngroup: 'II', width: 100, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_I_Penalty', text: '<span style="color:#ff0000;">已收提前还款违约金</span>', columngroup: 'II', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_I_RecommendIncentives', text: '<span style="color:#ff0000;">已收推荐投资奖励</span>', columngroup: 'II', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_P_Bidding', text: '<span style="color:#ff0000;">借出本金</span>', columngroup: 'IP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_P_InvestorServingFee', text: '<span style="color:#ff0000;">投资人居间服务费</span>', columngroup: 'IP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_F_FrozenBid', text: '<span style="color:#ff0000;">竞标冻结</span>', columngroup: 'IF', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'I_F_FrozenInterest', text: '<span style="color:#ff0000;">冻结利息</span>', columngroup: 'IF', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_I_LoanAmount', text: '<span style="color:#436EEE">借款本金</span>', columngroup: 'LI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_I_ReGuaranteeFee', text: '<span style="color:#436EEE">退还担保费</span>', columngroup: 'LI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_I_ReLoanServingFee', text: '<span style="color:#436EEE">退还借款人居间服务费</span>', columngroup: 'LI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_P_LoanAmount', text: '<span style="color:#436EEE">已还本金</span>', columngroup: 'LP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_P_Interest', text: '<span style="color:#436EEE">已还利息</span>', columngroup: 'LP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_P_OverdueInterest', text: '<span style="color:#436EEE">已还逾期利息</span>', columngroup: 'LP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_P_OverduePayment', text: '<span style="color:#436EEE">已还逾期滞纳金</span>', columngroup: 'LP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_P_PenaltyByBid', text: '<span style="color:#436EEE">提前还款违约金给投资人</span>', columngroup: 'LP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_P_LoanServingFee', text: '<span style="color:#436EEE">借款人居间服务费</span>', columngroup: 'LP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'L_P_PenaltyBySys', text: '<span style="color:#436EEE">提前还款违约金给平台</span>', columngroup: 'LP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_I_Recharge', text: '<span style="color:#8B5A00;">充值总额</span>', columngroup: 'OI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_I_Settlement', text: '<span style="color:#8B5A00;">银行结算利息</span>', columngroup: 'OI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_I_RedEnvelope', text: '<span style="color:#8B5A00;">红包</span>', columngroup: 'OI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_I_ActivitiesAwardFroz', text: '<span style="color:#8B5A00;">冻结活动奖励</span>', columngroup: 'OI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_I_ActivitiesAward', text: '<span style="color:#8B5A00;">活动奖励</span>', columngroup: 'OI', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_P_CashAmount', text: '<span style="color:#8B5A00;">提现</span>', columngroup: 'OP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_P_RechargeFee', text: '<span style="color:#8B5A00;">会员充值手续费</span>', columngroup: 'OP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_P_CashFee', text: '<span style="color:#8B5A00;">会员提现手续费OP</span>', columngroup: 'OP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_P_VipFee', text: '<span style="color:#8B5A00;">Vip年费</span>', columngroup: 'OP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_F_Recharge', text: '<span style="color:#8B5A00;">提现冻结金额</span>', columngroup: 'OF', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'O_F_Settlement', text: '<span style="color:#8B5A00;">会员提现手续费</span>', columngroup: 'OF', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" }

                ],
                columngroups:
                [
                    { text: '<b style="color:#9F79EE;font-size:14px;">会员信息</b>', align: 'center', name: 'MI' },
                    { text: '<b style="color:#ff0000;font-size:14px;">投资行为</b>', align: 'center', name: 'I' },
                    { text: '<span style="color:#ff0000;font-size:14px;">收入</span>', align: 'center', name: 'II', parentgroup: 'I', },
                    { text: '<span style="color:#ff0000;font-size:14px;">支出</span>', align: 'center', name: 'IP', parentgroup: 'I', },
                    { text: '<span style="color:#ff0000;font-size:14px;">冻结</span>', align: 'center', name: 'IF', parentgroup: 'I', },
                    { text: '<b style="color:#436EEE;font-size:14px;">借款行为</b>', align: 'center', name: 'L' },
                    { text: '<span style="color:#436EEE;font-size:14px;">收入</span>', align: 'center', name: 'LI', parentgroup: 'L', },
                    { text: '<span style="color:#436EEE;font-size:14px;">支出</span>', align: 'center', name: 'LP', parentgroup: 'L', },
                    //{ text: '<b>冻结</b>', align: 'center', name: 'LF', parentgroup: 'L', },
                    { text: '<b style="color:#8B5A00;font-size:14px;">其他</b>', align: 'center', name: 'O' },
                    { text: '<span style="color:#8B5A00;font-size:14px;">收入</span>', align: 'center', name: 'OI', parentgroup: 'O', },
                    { text: '<span style="color:#8B5A00;font-size:14px;">支出</span>', align: 'center', name: 'OP', parentgroup: 'O', },
                    { text: '<span style="color:#8B5A00;font-size:14px;">冻结</span>', align: 'center', name: 'OF', parentgroup: 'O', },
                    { text: '<b style="color:#EE7600;font-size:14px;">会员账户信息</b>', align: 'center', name: 'M' },
                    { text: '<span style="color:#EE7600;font-size:14px;">可用余额</b>', align: 'center', name: 'MB', parentgroup: 'M', },
                    { text: '<span style="color:#EE7600;font-size:14px;">待收</span>', align: 'center', name: 'MD', parentgroup: 'M', },
                    { text: '<span style="color:#EE7600;font-size:14px;">待还</span>', align: 'center', name: 'MS', parentgroup: 'M', }
                ],

            });

            $("#btn_Download").click(function () {
                if ($("#txtDownloadDate").val() == "") {
                    $("#txtDownloadDate").focus();
                    MessageAlert('请选择统计日期！', 'warning', '');
                    return;
                }
                var downDate = $("#txtDownloadDate").val().replace(/\-/g, "");
                $("#DownloadUrl").html("<a href=\"http://file.rjb777.com/ExcelReport/" + downDate.substring(0, 6) + "/会员每日对账报表" + downDate + ".xls\">下载（会员每日对账报表" + downDate + "）.xls</a>");
            });

        });

        function PValidata() {
            if ($("#txtDownloadDate").val() == "") {
                MessageAlert('请选择统计日期！', 'warning', '');
                $("#txtDownloadDate").focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <div class="fl">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selSUserType" id="selSUserType" class="fl" runat="server">
                <option value="0">会员名</option>
                <option value="1">真实姓名</option>
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
            <%--            <div style="float: left; margin-left: 5px;">
                <input id="ExcelExport" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport_Click" />
            </div>--%>
            <div class="fl" style="margin-left: 20px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div style="float: left;">
                <input id="txtDownloadDate" type="text" name="txtDownloadDate" value="" onclick="WdatePicker({ maxDate: '%y-%M-%d' })" class="input_text fl" maxlength="20" style="width: 100px;" runat="server" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div style="float: left; margin-left: 5px;">
                <asp:Button ID="Button1" runat="server" Text="会员资金汇总" CssClass="inputButton" Width="100px" OnClick="MemberFundSummaryExcel_Click" OnClientClick="return PValidata();" />
                <asp:Button ID="Button2" runat="server" Text="期初余额" CssClass="inputButton" Width="100px" OnClick="PlatformCapitalStockExcel_Click" OnClientClick="return PValidata();" />
                <input type="button" id="btn_Download" value="会员对账明细下载" class="inputButton" style="width: 140px;" />
                <span id="DownloadUrl"></span>
            </div>
        </div>
        <div id="jqxWidget" style="font-size: 13px; font-family: Verdana; float: left; clear: both;">
            <div id="jqxgrid">
            </div>
        </div>
    </form>
</body>
</html>

