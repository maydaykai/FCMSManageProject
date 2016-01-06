<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WithdrawCashManage.aspx.cs" Inherits="WebUI.MemberManage.WithdrawCashManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会员提现管理</title>
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
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <script src="/js/lhgdialog/lhgcore.lhgdialog.min.js"></script>
    <script src="/js/lhgdialog/ShowDialog.js"></script>
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
            
            //获取参数值
            function param(name, value) {
                this.name = name;
                this.value = value;
            }
            var url = window.location.href;
            var p = url.split("?");
            var all = new Array();
            var params = p.length > 1 ? p[1].split("&") : new Array();
            for (var i = 0; i < params.length; i++) {
                var pname = params[i].split("=")[0];
                var pvalue = params[i].split("=")[1];
                all[i] = new param(pname, pvalue);
            }
            function getPValueByName(name) {
                for (var i = 0; i < all.length; i++) {
                    if (all[i].name == name)
                        return all[i].value;
                }
            }
            
            if (getPValueByName("memberName") != "") {
                $("#txtName").val(getPValueByName("memberName"));
            }
            if (getPValueByName("SN") != "") {
                $("#txtSN").val(getPValueByName("SN"));
            }
            if (getPValueByName("checkStatus") != "") {
                $("#sel_checkStatus").val(getPValueByName("checkStatus"));
            }
            if (getPValueByName("status") != "") {
                $("#sel_remittanceStatus").val(getPValueByName("status"));
            }
            if (getPValueByName("cashModeStatus") != "") {
                $("#sel_cashMode").val(getPValueByName("cashModeStatus"));
            }
            if (getPValueByName("withdrawAmountMin") != "") {
                $("#txtWithdrawAmountMin").val(getPValueByName("withdrawAmountMin"));
            }
            if (getPValueByName("withdrawAmountMax") != "") {
                $("#txtWithdrawAmountMax").val(getPValueByName("withdrawAmountMax"));
            }

            //数据源
            var source = {
                url: '/HanderAshx/MemberManage/WithdrawCashHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'RealName', type: 'string' },
                    { name: 'CashMode', type: 'int' },
                    { name: 'BankAccountType', type: 'int' },
                    { name: 'BankName', type: 'string' },
                    { name: 'BLBankName', type: 'string' },
                    { name: 'AuthentBankName', type: 'string' },
                    { name: 'REQ_SN', type: 'string' },
                    { name: 'CashAmount', type: 'number' },
                    { name: 'CashFee', type: 'number' },
                    { name: 'FundsTrusteeshipFee', type: 'number' },
                    { name: 'Status', type: 'int' },
                    { name: 'StatusStr', type: 'string' },
                    { name: 'CashStatusStr', type: 'string' },
                    { name: 'ApplicationTime', type: 'date' },
                    { name: 'UpdateTime', type: 'date' },
                    { name: 'IsWithdrawAlert', type: 'int' },
                    { name: 'MemberID', type: 'int' },
                    { name: 'WarningStatus', type: 'int' },
                    { name: 'WarningNote', type: 'string' }
                ],
                pagesize: 20,
                formatdata: function(data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'C.ApplicationTime';
                    data.sortorder = data.sortorder || 'ASC';
                    data.memberName = $("#txtName").val() || "";
                    data.SN = $("#txtSN").val() || "";
                    data.checkStatus = $("#sel_checkStatus").val() || "";
                    data.status = $("#sel_remittanceStatus").val() || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
                    data.withdrawAmountMin = $("#txtWithdrawAmountMin").val() || 0;
                    data.withdrawAmountMax = $("#txtWithdrawAmountMax").val() || 0;
                    data.cashModeStatus = $("#sel_cashMode").val() || "";
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function() { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function(data) {
                    source.totalrecords = data.TotalRows;
                    $("#aggregate").html("总记录数：<span style='color:#ff0000;'>" + data.TotalRows + "&nbsp;</span>条&nbsp;&nbsp;总额：<span style='color:#ff0000;'>" + formatNumber(data.AmountAggregate, 2, 1) + "</span>");
                }
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
                var content;
                if (data.Status >= 2) content = "查看";
                else content = "审核";
                var parm = "../MemberManage/WithdrawCashAudit.aspx?" + formatedData + '&' + "CashMode=" + data.CashMode + "&" + column + "=" + value + "&columnId=<%=ColumnId %>";
                var link = "<a style='text-align:center;margin-left:25px;line-height:25px;' href='" + parm + "' target='_self'>" + content + "</a>";
                var html = link;
                return html;
            };
            
            var cashMode = function (row, column, value) {
                var link = "—";
                if (value == "0") link = "<span style='text-align:center;margin-left:25px;line-height:25px;'>线上</span>";
                else if (value == "1") link = "<span style='text-align:center;margin-left:25px;line-height:25px;color:#FF6347;'>线下</span>";
                var html = link;
                return html;
            };

            var bankName = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var link = "—";
                if (data.CashMode == "0") {
                    if(data.BankAccountType == "1")
                        link = "<span style='text-align:center;margin-left:50px;line-height:25px;'>" + value + "</span>";
                    else
                        link = "<span style='text-align:center;margin-left:50px;line-height:25px;'>" + data.AuthentBankName + "</span>";
                } else if (data.CashMode == "1") link = "<span style='text-align:center;margin-left:50px;line-height:25px;color:#FF6347;'>" + data.BLBankName + "</span>";
                var html = link;
                return html;
            };
            
            var isWithdrawAlert = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var link = "—";
                //var limitDays = "3";
                //var percent = "0.8";
                //var parm = "/MemberManage/WithdrawAlertDetail.aspx?MemberId=" + data.MemberID + "&limitDays=" + limitDays + "&percent=" + percent + "&columnId=" + <%=ColumnId %>;
                var parm = "/ReportStatistics/MemberFondRecord.aspx?MemberName=" + data.MemberName + "&columnId=" + <%=ColumnId %>;
                if (data.IsWithdrawAlert == "1") link = "<a href='javascript:void(0)' onclick=\"MessageWindow(1050, 650,'" + parm + "')\"; target='_self'><span style='color:#f00;text-align:center;line-height:25px;display:block;'>" + data.RealName + "</span></a>";
                else if (data.IsWithdrawAlert == "0") link = "<a href='javascript:void(0)' onclick=\"MessageWindow(1050, 650,'" + parm + "')\"; target='_self'><span style='color:#000;text-align:center;line-height:25px;display:block;'>" + data.RealName + "</span></a>";
                var html = link;
                return html;
            };
            
            var warningStatus = function (row, column, value) {
                var data = $("#jqxgrid").jqxGrid('getrowdata', row);
                var html = "<span style='text-align:center;line-height:25px;display:block;'>—</span>";
                if(data.WarningStatus==0)
                    html="<span style='text-align:center;line-height:25px;display:block;'>正常</span>";
                if(data.WarningStatus==1)
                    html="<span style='text-align:center;line-height:25px;display:block;'>风险</span>";
                else if(data.WarningStatus==2)
                    html="<span style='text-align:center;line-height:25px;display:block;'>存疑</span>";
                else if(data.WarningStatus==3)
                    html="<span style='text-align:center;line-height:25px;display:block;'>通过</span>";
                else if(data.WarningStatus==4)
                    html="<span style='text-align:center;line-height:25px;display:block;'>拒绝</span>";
                return html;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1000,
                renderstatusbar: function (statusbar) {
                    var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                    var aggregate = $("<div id='aggregate' style='float: right; margin-right: 0px; cursor:pointer;'></div>");
                    aggregate.jqxButton({ width: 400, height: 20 });
                    container.append(aggregate);
                    statusbar.append(container);
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
                    { dataField: 'ID', text: '<b>操作</b>', width: 70, cellsalign: 'center', align: 'center', cellsrenderer: linkrenderer, sortable: false },
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'RealName', text: '<b>真实姓名</b>', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: isWithdrawAlert },
                    { dataField: 'CashMode', text: '<b>提现类型</b>', width: 80, cellsalign: 'center', align: 'center', cellsrenderer: cashMode },
                    { dataField: 'BankName', text: '<b>银行名称</b>', width: 150, cellsalign: 'center', align: 'center', cellsrenderer: bankName },
                    { dataField: 'CashAmount', text: '<b>提现金额(元)</b>', width: 120, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'CashFee', text: '<b>手续费(元)</b>', width: 80, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'FundsTrusteeshipFee', text: '<b>资金托管费(元)</b>', width: 100, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'StatusStr', text: '<b>审核状态</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'CashStatusStr', text: '<b>汇款状态</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'ApplicationTime', text: '<b>申请时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'UpdateTime', text: '<b>更新时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'REQ_SN', text: '<b>交易流水号</b>', width: 200, cellsalign: 'center', align: 'center' },
                    { dataField: 'WarningStatus', text: '<b>风险状态</b>', width: 100, cellsalign: 'center', align: 'center', cellsrenderer: warningStatus },
                    { dataField: 'WarningNote', text: '<b>预警备注</b>', width: 500, cellsalign: 'center', align: 'center' }
                ]
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv">
        <span class="fl">会员名：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl" maxlength="20" style="width: 150px;" id="txtName" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl">交易流水号：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl" maxlength="20" style="width: 150px;" id="txtSN" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="sel_checkStatus" id="sel_checkStatus" class="fl">
            <option value="">--审核状态--</option>
            <option value="0">初审中</option>
            <option value="1">复审中</option>
            <option value="2">初审不通过</option>
            <option value="3">复审不通过</option>
            <option value="4">复审通过</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_checkStatus" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="sel_remittanceStatus" id="sel_remittanceStatus" class="fl">
            <option value="">--汇款状态--</option>
            <option value="0">汇款中</option>
            <option value="1">汇款成功</option>
            <option value="2">汇款失败</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_remittanceStatus" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="sel_cashMode" id="sel_cashMode" class="fl">
            <option value="">--提现类型--</option>
            <option value="0">线上</option>
            <option value="1">线下</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_sel_cashMode" />
        </div>
        <div style="height: 1px; clear: both;">&nbsp;</div>
        <span class="fl">交易时间：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtStartDate" class="input_text" type="text" onclick="WdatePicker({ maxDate: '#F{$dp.$D(\'txtEndDate\')}', dateFmt: 'yyyy-MM-dd HH:mm:ss' })" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl" style="margin-left: 5px; margin-right: 5px;">～</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtEndDate" class="input_text" type="text" onclick="WdatePicker({ minDate: '#F{$dp.$D(\'txtStartDate\')}', dateFmt: 'yyyy-MM-dd HH:mm:ss' })" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl">提现金额：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtWithdrawAmountMin" class="input_text" type="text" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl" style="margin-left: 5px; margin-right: 5px;">～</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtWithdrawAmountMax" class="input_text" type="text" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
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
