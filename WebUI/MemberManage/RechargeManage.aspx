<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeManage.aspx.cs" Inherits="WebUI.MemberManage.RechargeManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>充值管理</title>
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
    <script src="../js/jqwidgets-ver3.1.0/jqwidgets/jqxgrid.aggregates.js"></script>
    <link href="/js/jqwidgets-ver3.1.0/jqwidgets/styles/jqx.arctic.css" rel="stylesheet" />
    <script src="../js/FormatDecimal.js"></script>
    <script src="../js/My97DatePicker/WdatePicker.js"></script>
    <link href="/css/icon.css" rel="stylesheet" />
    <link href="/css/select.css" rel="stylesheet" />
    <style type="text/css">
        #searchbar span { height: 29px; text-indent: 5px; line-height: 29px; }
        .selectDiv .select_box { width: 85px; }
        #select_selRechargeChannel{ width: 136px;}
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
                url: '/HanderAshx/MemberManage/RechargeHandler.ashx',
                cache: false,
                datatype: "json",
                root: 'Rows',
                datafields: [
                    { name: 'ID', type: 'int' },
                    { name: 'Type', type: 'int' },
                    { name: 'AuditStatus', type: 'int' },
                    { name: 'MemberName', type: 'string' },
                    { name: 'TypeString', type: 'string' },
                    { name: 'RechargeChannelString', type: 'string' },
                    { name: 'MerchantOrderNo', type: 'string' },
                    { name: 'OrderNumber', type: 'string' },
                    { name: 'OrderNumberString', type: 'string' },
                    { name: 'Amount', type: 'number' },
                    { name: 'RechargeFee', type: 'number' },
                    { name: 'StatusString', type: 'string' },
                    { name: 'ApplicationTime', type: 'date' },
                    //{ name: 'UpdateTime', type: 'date' },
                    { name: 'AuditStatusStr', type: 'string' }
                ],
                pagenum: 0,
                pagesize: 20,
                formatdata: function (data) {
                    data.pagenum = data.pagenum || 0;
                    data.pagesize = data.pagesize || 20;
                    data.sortdatafield = data.sortdatafield || 'P.ApplicationTime';
                    data.sortorder = data.sortorder || 'Desc';
                    data.SN = $("#txtSN").val() || "";
                    data.memberName = $("#txtName").val() || "";
                    data.type = $("#selType").val() || "";
                    data.rechargeChannel = $("#selRechargeChannel").val() || "";
                    data.status = $("#selStatus").val() || "";
                    data.startDate = $("#txtStartDate").val() || "";
                    data.endDate = $("#txtEndDate").val() || "";
                    data.rechargeAmountMin = $("#txtRechargeAmountMin").val() || 0;
                    data.rechargeAmountMax = $("#txtRechargeAmountMax").val() || 0;
                    formatedData = buildQueryString(data);
                    return formatedData;
                },
                sort: function () { $("#jqxgrid").jqxGrid('updatebounddata', 'sort'); },
                beforeprocessing: function (data) {
                    source.totalrecords = data.TotalRows;
                    $("#aggregate").html("总记录数：<span style='color:#ff0000;'>" + data.TotalRows + "&nbsp;</span>条&nbsp;&nbsp;充值总计：<span style='color:#ff0000;'>" + formatNumber(data.AmountAggregate, 2, 1) + "</span>");
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
                var parm = column + "=" + value + "&columnId=<%=ColumnId%>";
                var link = "";
                var paras = "orderNo=" + data.MerchantOrderNo + "&orderDatetime=" + $.jqx.dataFormat.formatDate(data.ApplicationTime, "yyyyMMddHHmmss");
                if (data.Type == 0) {
                    link = "<a href='RechargeView.aspx?" + paras + "' target='_self' style='margin-left:10px;height:25px;line-height:25px;'>查看</a>";
                } else if (data.Type == 1) {
                    link = "<a href='RechargeUnderLine.aspx?" + parm + "'  target='_self' style='margin-left:10px;height:25px;line-height:25px;'>" + ((data.AuditStatus == 2 || data.AuditStatus == 3 || data.AuditStatus == 4) ? "查看" : "审核") + "</a>";
                }
                var html = $.jqx.dataFormat.formatlink(link);
                return html;
            };

            //数据绑定
            $("#jqxgrid").jqxGrid({
                theme: theme,
                source: dataadapter,
                width: 1000,
                renderstatusbar: function (statusbar) {
                    var btnOffline = <%=_btnOffline %>;
                    if (btnOffline == 1) {
                        var container = $("<div style='overflow: hidden; position: relative; margin: 5px;'></div>");
                        var addButton = $("<div style='float: left; margin-left: 5px; cursor:pointer;'><img style='position: relative; margin-top: 2px;' src='/js/jqwidgets-ver3.1.0/images/add.png'/><span style='margin-left: 4px; position: relative; top: -3px;'>线下充值</span></div>");
                        addButton.jqxButton({ width: 80, height: 20 });
                        addButton.click(function (event) {
                            window.location.href = "RechargeUnderLine.aspx?columnId=<%=ColumnId%>";
                             });
                        container.append(addButton);
                        var aggregate = $("<div id='aggregate' style='float: right; margin-right: 0px; cursor:pointer;'></div>");
                        aggregate.jqxButton({ width: 400, height: 20 });
                        container.append(aggregate);
                        statusbar.append(container);
                    }
                   
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
                    { dataField: 'ID', text: '<b>操作</b>', width: 50, cellsrenderer: linkrenderer, align: 'center' },
                    { dataField: 'MemberName', text: '<b>会员名</b>', width: 120, cellsalign: 'center', align: 'center' },
                    { dataField: 'TypeString', text: '<b>充值类型</b>', width: 80, cellsalign: 'center', align: 'center' },
                    { dataField: 'RechargeChannelString', text: '<b>充值渠道</b>', width: 160, cellsalign: 'center', align: 'center' },
                    { dataField: 'Amount', text: '<b>充值金额（元）</b>', width: 150, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'RechargeFee', text: '<b>手续费（元）</b>', width: 110, cellsalign: 'right', cellsformat: "F2", align: 'center' },
                    { dataField: 'StatusString', text: '<b>充值状态</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'AuditStatusStr', text: '<b>审核状态</b>', width: 100, cellsalign: 'center', align: 'center' },
                    { dataField: 'ApplicationTime', text: '<b>申请/交易时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' },
                    { dataField: 'MerchantOrderNo', text: '<b>商户订单号</b>', width: 250, cellsalign: 'center', align: 'center' },
                    { dataField: 'OrderNumberString', text: '<b>订单号</b>', width: 200, cellsalign: 'center', align: 'center' }
                    //{ dataField: 'UpdateTime', text: '<b>更新时间</b>', cellsformat: "yyyy-MM-dd HH:mm:ss", width: 180, cellsalign: 'center', align: 'center' }
                ]
            });
        });
    </script>
</head>
<body>
    <div id="searchbar" class="selectDiv" style="min-width: 1000px;">
        <span class="fl">会员名：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input type="text" class="input_text fl" maxlength="20" style="width: 120px;" id="txtName" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl">订单号/商户订单号：</span>
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
        <select name="selType" id="selType" class="fl">
            <option value="">--充值类型--</option>
            <option value="0">线上</option>
            <option value="1">线下</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selType" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selRechargeChannel" id="selRechargeChannel" class="fl">
            <option value="">--充值渠道--</option>
            <option value="0">线下充值</option>
            <option value="1">通联支付</option>
            <option value="2">通联移动支付（IOS）</option>
            <option value="3">通联移动支付（Android）</option>
            <option value="4">通联WAP支付</option>
            <option value="5">连连支付</option>
            <option value="6">连连移动支付（IOS）</option>
            <option value="7">连连移动支付（Android）</option>
            <option value="8">连连WAP支付</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selRechargeChannel" />
        </div>
        <div class="fl" style="margin-left: 10px;">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <select name="selStatus" id="selStatus" class="fl">
            <option value="">--充值状态--</option>
            <option value="0">付款中</option>
            <option value="1">付款成功</option>
            <option value="2">付款失败</option>
        </select>
        <div class="fl" style="margin-left: -5px; cursor: pointer;">
            <img src="../images/select_right.png" width="31" height="29" alt="" id="img_selStatus" />
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
        <span class="fl">充值金额：</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtRechargeAmountMin" class="input_text" type="text" style="width: 150px" />
        <div class="fl">
            <img src="../images/input_right.png" style="width: 4px; height: 29px;" alt="" />
        </div>
        <span class="fl" style="margin-left: 5px; margin-right: 5px;">～</span>
        <div class="fl">
            <img src="/images/input_left.png" width="4" height="29" alt="" />
        </div>
        <input id="txtRechargeAmountMax" class="input_text" type="text" style="width: 150px" />
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
