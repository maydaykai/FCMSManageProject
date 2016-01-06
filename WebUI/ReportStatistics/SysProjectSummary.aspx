<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysProjectSummary.aspx.cs" Inherits="WebUI.ReportStatistics.SysProjectSummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>平台项目汇总</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/global.css" rel="stylesheet" />
    <link rel="stylesheet" href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.base.css" type="text/css" />
    <script type="text/javascript" src="/js/jqwidgets-ver3.1.0/scripts/jquery-1.10.2.min.js"></script>
    <script src="/js/datetime.js"></script>
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
                url: '/HanderAshx/ReportStatistics/SysProjectSummaryHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'LoanID', type: 'int' },
		            { name: 'LoanNumber', type: 'string' },
                    { name: 'LoanRealName', type: 'string' },
		            { name: 'LoanMemberID', type: 'int' },
                    { name: 'ExamStatus', type: 'string' },
                    { name: 'PrepaymentStatus', type: 'string' },
                    { name: 'LoanAmount', type: 'number' },
                    { name: 'LoanRate', type: 'number' },
                    { name: 'LoanTerm', type: 'string' },
                    { name: 'BidStratTime', type: 'date' },
                    { name: 'BidEndTime', type: 'date' },
                    { name: 'ReviewTime', type: 'string' },
                    { name: 'GuaranteeName', type: 'string' },
                    { name: 'LastRePayTime', type: 'string' },
                    { name: 'LastRePayDate', type: 'string' },
                    { name: 'BiddingFreezing', type: 'number' },
                    { name: 'SA_Principal', type: 'number' },
                    { name: 'HA_Principal', type: 'number' },
                    { name: 'SS_Principal', type: 'number' },
                    { name: 'SA_Interest', type: 'number' },
                    { name: 'HA_Interest', type: 'number' },
                    { name: 'SS_Interest', type: 'number' },
                    { name: 'BO_Interest', type: 'number' },
                    { name: 'PrepaymentPenalty', type: 'number' },
                    { name: 'LoanServiceCharges', type: 'number' },
                    { name: 'BidServiceCharges', type: 'number' },
                    { name: 'RecoIncentive', type: 'number' },
                    { name: 'ActivitiesAward', type: 'number' },
                    { name: 'ProjectIncome', type: 'number' },
                    { name: 'OverdueDays', type: 'int' },
                    { name: 'Agency', type: 'string' },
                    { name: 'PrepaymentPrincipal', type: 'number' },
                    { name: 'PrepaymentInterest', type: 'number' }
                ],
                pagesize: 20,
                formatdata: function (data) {
                    data.currentpage = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.loanNumber = encodeURI(encodeURI($.trim($("#txtLoanNumber").val()))) || "";
                    data.prepaymentStatus = $("#selPrepaymentStatus").val();
                    data.loanStatus = $("#selLoanStatus").val();
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

            var formatDate = function (row, column, value) {
                var formatVal;
                if (value == "—") {
                    formatVal = "<span style='color:#ff0000;height:25px;line-height:25px;margin-left:60px;'>—</span>";
                } else {
                    formatVal = "<span style='height:25px;line-height:25px;margin-left:40px;'>" + value + "</span>";
                }
                return formatVal;
            };

            /*add by 2015-08-26 lxy 新增查看提前还款信息*/
            var formatDate2 = function (row, column, value) {
                return "<input type='button' value='提前还款计算' onclick='javascript:getRepaymentAmount(" + value + ");' style='margin-top:3px;margin-left:15px;cursor:pointer;' />";
            };

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
                    { dataField: 'LoanID', text: '<span style="color:#9F79EE;">操作</span>', columngroup: 'L', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: formatDate2 },
		            { dataField: 'LoanNumber', text: '<span style="color:#9F79EE;">项目编号</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanRealName', text: '<span style="color:#9F79EE;">借款人</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'GuaranteeName', text: '<span style="color:#9F79EE;">担保公司</span>', columngroup: 'L', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'ExamStatus', text: '<span style="color:#9F79EE;">借款标状态</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'PrepaymentStatus', text: '<span style="color:#9F79EE;">是否提前还款</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'LoanAmount', text: '<span style="color:#9F79EE;">借款金额</span>', columngroup: 'L', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'LoanRate', text: '<span style="color:#9F79EE;">年利率（%）</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center', cellsformat: "F2" },
                    { dataField: 'LoanTerm', text: '<span style="color:#9F79EE;">借款期限</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center' },
                    { dataField: 'BidStratTime', text: '<span style="color:#9F79EE;">竞标开始日</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'BidEndTime', text: '<span style="color:#9F79EE;">竞标截至日</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd" },
                    { dataField: 'ReviewTime', text: '<span style="color:#9F79EE;">放款日</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd", cellsrenderer: formatDate },
                    { dataField: 'LastRePayTime', text: '<span style="color:#9F79EE;">最后应还款日</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd", cellsrenderer: formatDate },
                    { dataField: 'LastRePayDate', text: '<span style="color:#9F79EE;">最后一次还款时间</span>', columngroup: 'L', width: 150, cellsalign: 'center', align: 'center', cellsformat: "yyyy-MM-dd", cellsrenderer: formatDate },
                    { dataField: 'Agency', text: '<span style="color:#9F79EE;">分支机构</span>', columngroup: 'L', width: 350, cellsalign: 'center', align: 'center' },
                    { dataField: 'BiddingFreezing', text: '<span style="color:#FF6347;">竞标冻结</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SA_Principal', text: '<span style="color:#FF6347;">应还本金</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'HA_Principal', text: '<span style="color:#FF6347;">已还本金</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SS_Principal', text: '<span style="color:#FF6347;">待还本金</span>', columngroup: 'P', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SA_Interest', text: '<span style="color:#EE7600;">应还利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'HA_Interest', text: '<span style="color:#EE7600;">已还利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'SS_Interest', text: '<span style="color:#EE7600;">待还利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'BO_Interest', text: '<span style="color:#EE7600;">已还逾期利息</span>', columngroup: 'I', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'PrepaymentPrincipal', text: '<span style="color:#EE6A50;">提前还款本金</span>', columngroup: 'PP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'PrepaymentInterest', text: '<span style="color:#EE6A50;">提前还款利息</span>', columngroup: 'PP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'PrepaymentPenalty', text: '<span style="color:#EE6A50;">提前还款违约金</span>', columngroup: 'PP', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'LoanServiceCharges', text: '<span style="color:#9F79EE;">收借款人居间服务费</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'BidServiceCharges', text: '<span style="color:#9F79EE;">收投资人居间服务费</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'RecoIncentive', text: '<span style="color:#9F79EE;">推荐投资奖励</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ActivitiesAward', text: '<span style="color:#9F79EE;">活动奖励</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'ProjectIncome', text: '<span style="color:#9F79EE;">净项目损益</span>', columngroup: 'S', width: 150, cellsalign: 'right', align: 'center', cellsformat: "F2" },
                    { dataField: 'OverdueDays', text: '<span style="color:#8B0000;">逾期天数</span>', columngroup: 'V', width: 150, cellsalign: 'center', align: 'center' }
                ],
                columngroups:
                [
                    { text: '<span style="color:#9F79EE;font-size:14px;">项目信息</span>', align: 'center', name: 'L' },
                    { text: '<span style="color:#FF6347;font-size:14px;">本金</span>', align: 'center', name: 'P' },
                    { text: '<span style="color:#EE7600;font-size:14px;">利息</span>', align: 'center', name: 'I' },
                    { text: '<span style="color:#EE6A50;font-size:14px;">提前还款</span>', align: 'center', name: 'PP' },
                    { text: '<span style="color:#9F79EE;font-size:14px;">平台项目损益</span>', align: 'center', name: 'S' },
                    { text: '<span style="color:#8B0000;font-size:14px;">项目逾期情况</span>', align: 'center', name: 'V' }
                ],

            });

            $("#btn_Download").click(function () {
                if ($("#txtDownloadDate").val() == "") {
                    $("#txtDownloadDate").focus();
                    alert("请选择要生成的日期！");
                    return;
                }
                var downDate = $("#txtDownloadDate").val().replace(/\-/g, "");
                $("#DownloadUrl").html("<a href=\"http://file.rjb777.com/ExcelReport/" + downDate.substring(0, 6) + "/平台项目汇总报表" + downDate + ".xls\">下载（平台项目汇总报表" + downDate + "）.xls</a>");
            });
        });

        /*add by 2015-08-26 lxy 查询提前还款信息*/
        function getRepaymentAmount(v)
        {
            var obj = new Object();
            obj.loanId = v;
            obj.type = 2;
            var jsonobj = JSON.stringify(obj);
            $.ajax({
                type: "POST",
                url: "SysProjectSummary.aspx/getRepaymentAmount",
                contentType: "application/json; charset=utf-8",
                data: jsonobj,
                dataType: "json",
                success: function (result) {
                    var jsondatas = JSON.parse(result.d);
                    if (jsondatas.TotalRows > 0) {
                        alert("提前还款所需金额:" + jsondatas.Rows[0].RepaymentAmount + "元");
                    } else {
                        alert("查询出错！");
                    }
                },
                error: function () {
                    alert("error");
                }
            });
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
            <div class="fl" style="height: 29px; line-height: 29px;">
                项目编号
            </div>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <div style="float: left;">
                <input id="txtLoanNumber" type="text" name="txtLoanNumber" value="" class="input_text fl" maxlength="20" style="width: 150px;" runat="server" />
                <div class="fl">
                    <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
                </div>
            </div>
            <div class="fl" style="height: 29px; line-height: 29px;">
                还款状态
            </div>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selPrepaymentStatus" id="selPrepaymentStatus" runat="server" class="fl">
                <option value="0">全部</option>
                <option value="1">提前还款</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selPrepaymentStatus" />
            </div>
            <div class="fl" style="height: 29px; line-height: 29px;">
                借款状态
            </div>
            <div class="fl" style="margin-left: 10px;">
                <img src="/images/input_left.png" width="4" height="29" alt="" />
            </div>
            <select name="selLoanStatus" id="selLoanStatus" runat="server" class="fl">
                <option value="0">全部</option>
                <option value="1">平台初审中</option>
                <option value="2">平台初审不通过</option>
                <option value="3">平台二审中</option>
                <option value="4">平台二审不通过</option>
                <option value="5">竞标中</option>
                <option value="6">流标</option>
                <option value="7">平台复审中</option>
                <option value="8">平台复审不通过</option>
                <option value="9">还款中</option>
                <option value="10">还款完成</option>
            </select>
            <div class="fl" style="margin-left: -5px; cursor: pointer;">
                <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selLoanStatus" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input type="button" id="applyfilter" value="查 询" class="inputButton" />
            </div>
            <div style="float: left; margin-left: 5px;">
                <input id="ExcelExport" type="button" value="导出Excel" class="inputButton" style="width: 100px;" runat="server" onserverclick="ExcelExport_Click" />
            </div>

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
                <input type="button" id="btn_Download" value="生成报表下载连接" class="inputButton" style="width: 140px;" placeholder="选择要生成的日期" />
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
